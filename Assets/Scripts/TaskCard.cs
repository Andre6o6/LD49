using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TaskCard : MonoBehaviour
{
    public UnityEvent OnCardDestroyed;
    public UnityEvent OnTaskFailed;

    public MinisterSuite Suite => _data.SuiteRequirement;
    
    [SerializeField] private Slot _slot;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private string _levelReqFormat = "LVL {0}";
    [SerializeField] private TMP_Text _levelReqText;
    [SerializeField] private string _timeFormat = "TIME\n{0}";
    [SerializeField] private TMP_Text _turnsCountText;
    //TODO suite color
    [SerializeField] private Image _backCardImage;
    [SerializeField] private MinisterColors _colors;
    [SerializeField] private Image _crownImage;
    
    [SerializeField] private Image _winImage;
    [SerializeField] private Image _loseImage;
    
    //Supercard
    [SerializeField] private bool _supercard;
    [SerializeField, Range(1, 3)] private int _supercardNumber;
    [SerializeField] private bool _tutorial;
    [SerializeField, Range(1, 3)] private int _tutorialNumber;
    
    private TaskCardData _data;
    private int _turnsLeft;
    private int _turnsSolving = 1;
    private bool _destroyNextTurn;

    private void Awake()
    {
        _slot ??= GetComponent<Slot>();
        if (_supercard)
        {
            _turnsSolving = 3;
            if (_supercardNumber == 1) Initialize(AllGameCards.SuperTask1);
            if (_supercardNumber == 2) Initialize(AllGameCards.SuperTask2);
            if (_supercardNumber == 3) Initialize(AllGameCards.SuperTask3);
        }
        else if (_tutorial)
        {
            if (_tutorialNumber == 1) Initialize(AllGameCards.Tutorial1);
            if (_tutorialNumber == 2) Initialize(AllGameCards.Tutorial2);
            if (_tutorialNumber == 3) Initialize(AllGameCards.Tutorial3);
        }
    }

    public void Initialize(TaskCardData data)
    {
        _data = data;
            
        _nameText.text = _data.Name;
        _levelReqText.text = string.Format(_levelReqFormat, _data.LevelRequirement);

        if (!_data.SuperTask)
        {
            _turnsLeft = _data.TurnsToSolve;
            _turnsCountText.text = string.Format(_timeFormat, _turnsLeft);
        }

        if (_data.WinPoint)
            _crownImage.gameObject.SetActive(true);

        _backCardImage.color = _colors.GetColor(_data.SuiteRequirement);
        GameController.Instance.OnTurnAdvanced.AddListener(TickTimer);
    }

    public TaskCardData GetData()
    {
        return _data;
    }

    private void TickTimer()
    {
        if (_supercard)
        {
            if (_loseImage.gameObject.activeInHierarchy)
                _loseImage.gameObject.SetActive(false);
        }

        if (_destroyNextTurn)
        {
            DestroySlot();
            return;
        }

        if (_slot.Empty())
        {
            if (_data.SuperTask) return;
            
            _turnsLeft -= 1;
            if (_turnsLeft < 0) //TODO <= 0 feels too rash
            {
                Fail();
                return;
            }
            _turnsCountText.text = string.Format(_timeFormat, _turnsLeft);
        }
        else
        {
            _turnsSolving -= 1;
            if (_turnsSolving < 0)
            {
                if (MinisterCanSucceed(_slot.Piece.Minister))
                    Succeed();
                else
                    Fail();
            }
        }
    }

    private void Fail()
    {
        if (_data.SuperTask)
        {
            if (!_slot.Empty()) _slot.Piece.ReturnHome();
            _loseImage.gameObject.SetActive(true);
            _turnsSolving = 2;
            return;
        }
        
        Minister minister = _slot.Empty() ? null : _slot.Piece.Minister;
        _data.CallbackLose.Invoke(minister);
        EmpireController.ChangeStability(-1);
        
        if (minister != null)
            minister.ChangeBoredom(-1 - _data.LevelRequirement / GameSettings.ExhaustionLvlDivider);
        
        _destroyNextTurn = true;
        _slot.Close();

        if (!_slot.Empty())
        {
            var parent = _levelReqText.transform.parent.gameObject;
            var angle = new Vector3(0, 0, 15);
            var seq = LeanTween.sequence();
            seq.append(LeanTween.rotate(parent, angle, 0.1f));
            seq.append(LeanTween.rotate(parent, -angle, 0.1f));
            seq.append(LeanTween.rotate(parent, angle, 0.1f));
            seq.append(LeanTween.rotate(parent, Vector3.zero, 0.05f));
        }
        else
        {
            var seq = LeanTween.sequence();
            seq.append(LeanTween.scale(_turnsCountText.gameObject, 1.3f * Vector3.one, 0.2f));
            seq.append(LeanTween.scale(_turnsCountText.gameObject, Vector3.one, 0.3f));
        }

        _loseImage.gameObject.SetActive(true);
        //TODO graphics
        
        OnTaskFailed.Invoke();
    }
    
    private void Succeed()
    {
        Minister minister = _slot.Empty() ? null : _slot.Piece.Minister;    //TODO never null ?
        _data.CallbackWin.Invoke(minister);

        if (minister != null)
        {
            minister.GainExperience(_data.LevelRequirement);
            minister.ChangeBoredom(-1 - _data.LevelRequirement / GameSettings.ExhaustionLvlDivider);
        }

        _destroyNextTurn = true;
        _slot.Close();

        _turnsCountText.text = "";
        
        var parent = _levelReqText.transform.parent.gameObject;
        var seq = LeanTween.sequence();
        seq.append(LeanTween.scale(parent, 1.3f * Vector3.one, 0.2f));
        seq.append(LeanTween.scale(parent, Vector3.one, 0.3f));
        
        _winImage.gameObject.SetActive(true);
    }

    private void DestroySlot()
    {
        OnCardDestroyed.Invoke();
        
        _data.Dispose();    //Return to deck
        
        GameController.Instance.OnTurnAdvanced.RemoveListener(TickTimer);
        if (!_slot.Empty())
        {
            _slot.Piece.ReturnHome();
        }
        Destroy(this.gameObject);
    }

    private bool MinisterCanSucceed(Minister minister)
    {
        if (_data.SuiteRequirement == MinisterSuite.None)
            return StrictLevelCheck(minister);

        if (_data.SuiteRequirement == minister.Suite)
            return FavorableLevelCheck(minister);
        else
            return UnfavorableLevelCheck(minister);

    }

    private bool StrictLevelCheck(Minister minister)
    {
        return minister.Level >= _data.LevelRequirement;
    }
    
    private bool FavorableLevelCheck(Minister minister)
    {
        if (minister.Level >= _data.LevelRequirement)
        {
            return true;
        }
        else
        {
            float chance = 1f / (GameSettings.BaseFavorableChance + Mathf.Abs(_data.LevelRequirement - minister.Level));
            return Random.Range(0, 1f) < chance;
        }
    }
    
    private bool UnfavorableLevelCheck(Minister minister)
    {
        if (minister.Level < _data.LevelRequirement)
            return false;

        float chance = 1f / (GameSettings.BaseUnfavorableChance + Mathf.Abs(_data.LevelRequirement - minister.Level));
        return Random.Range(0, 1f) > chance;
    }
}
