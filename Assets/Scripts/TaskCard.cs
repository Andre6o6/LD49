using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class TaskCard : MonoBehaviour
{
    public event System.Action<float> OnChanceFailEvent;
    public event System.Action<float> OnChanceSuccessEvent;
    
    public UnityEvent OnCardDestroyed;
    public UnityEvent OnTaskFailed;
    public UnityEvent OnTaskSuccess;

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
            minister.ChangeBoredom(GetExhaustionCost(minister, _data, false));
        
        _destroyNextTurn = true;
        _slot.Close();

        if (!_slot.Empty())
        {
            var parent = _levelReqText.transform.parent;
            var angle = new Vector3(0, 0, 15);
            
            var seq = DOTween.Sequence();
            seq.Append(parent.DORotate(angle, 0.1f));
            seq.Append(parent.DORotate(-angle, 0.1f));
            seq.Append(parent.DORotate(angle, 0.1f));
            seq.Append(parent.DORotate(Vector3.zero, 0.05f));
        }
        else
        {
            var seq = DOTween.Sequence();
            seq.Append(_turnsCountText.transform.DOScale(1.3f * Vector3.one, 0.2f))
                .Append(_turnsCountText.transform.DOScale(Vector3.one, 0.3f));
        }
        _loseImage.gameObject.SetActive(true);
        
        OnTaskFailed.Invoke();
    }
    
    private void Succeed()
    {
        Minister minister = _slot.Empty() ? null : _slot.Piece.Minister;    //TODO never null ?
        _data.CallbackWin.Invoke(minister);

        if (minister != null)
        {
            if (minister.Level < 5 + _data.LevelRequirement) 
                minister.GainExperience(_data.LevelRequirement);
            else
                minister.GainExperience(1);
            minister.ChangeBoredom(GetExhaustionCost(minister, _data, true));
        }

        _destroyNextTurn = true;
        _slot.Close();

        _turnsCountText.text = "";
        
        var parent = _levelReqText.transform.parent;
        var seq = DOTween.Sequence();
        seq.Append(parent.DOScale(1.3f * Vector3.one, 0.2f))
            .Append(parent.DOScale(Vector3.one, 0.3f));

        _winImage.gameObject.SetActive(true);
        
        OnTaskSuccess.Invoke();
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
            bool result = Random.Range(0, 1f) < chance;

            if (result == true)
                OnChanceSuccessEvent?.Invoke(chance);
            else
                OnChanceFailEvent?.Invoke(1 - chance);
            
            return result;
        }
    }
    
    private bool UnfavorableLevelCheck(Minister minister)
    {
        if (minister.Level < _data.LevelRequirement)
            return false;

        float chance = 1f / (GameSettings.BaseUnfavorableChance + Mathf.Abs(_data.LevelRequirement - minister.Level));
        bool result = Random.Range(0, 1f) > chance;
        
        if (result == false) 
            OnChanceFailEvent?.Invoke(chance);
        else
            OnChanceSuccessEvent?.Invoke(1 - chance);
        
        return result;
    }

    public int GetExhaustionCost(Minister minister, TaskCardData task, bool succeed = true)
    {
        //TODO Long tasks should eat stamina every turn (?)
        //TODO Take away stamina if in long chain without being home

        int exh = task.LevelRequirement / 2 - minister.Level / 4;

        if (_data.SuiteRequirement != MinisterSuite.None &&
            _data.SuiteRequirement != minister.Suite)
            exh += 1;

        if (exh < 0) exh = 0;
        if (succeed && 
            !TaskIsTrivial(minister.Level, task.LevelRequirement)) 
            exh += 1;    //at least 1 exh on success
        
        if (exh > 5) exh = 5;    //limit to 5
        
        return -exh;
    }

    private bool TaskIsTrivial(int level, int levelReq)
    {
        if (level > 5 && levelReq == 1) return true;
        if (level >= 8 && levelReq == 2) return true;
        if (level >= 12 && levelReq == 3) return true;
        return false;
    }
}
