using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TaskCard : MonoBehaviour
{
    public UnityEvent OnCardDestroyed;
    public UnityEvent OnTaskFailed;
    
    [SerializeField] private Slot _slot;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _turnsCountText;
    //TODO suite color
    
    private TaskCardData _data;
    private int _turnsLeft;
    private int _turnsSolving = 1;
    //private bool _bufferTurn = true;    //TODO we may lose 1 turn based on how events are ordered
    private bool _destroyNextTurn;

    private void Awake()
    {
        _slot ??= GetComponent<Slot>();
    }

    public void Initialize(TaskCardData data)
    {
        _data = data;
            
        _nameText.text = _data.Name;
        _descriptionText.text = _data.Description;
        _turnsLeft = _data.TurnsToSolve;
        _turnsCountText.text = _turnsLeft.ToString();
        
        GameController.Instance.OnTurnAdvanced.AddListener(TickTimer);
    }

    public TaskCardData GetData()
    {
        return _data;
    }

    private void TickTimer()
    {
        if (_destroyNextTurn)
        {
            DestroySlot();
            return;
        }

        if (_slot.Empty())
        {
            _turnsLeft -= 1;
            if (_turnsLeft < 0) //TODO <= 0
            {
                Fail();
                return;
            }
            _turnsCountText.text = _turnsLeft.ToString();
        }
        else
        {
            _turnsSolving -= 1;
            if (_turnsSolving < 0)
            {
                if (MinisterCanSucceed(_slot.Piece.Minister) &&
                    _data.ResourceWinConditions.Invoke())
                    Succeed();
            }
        }
    }

    private void Fail()
    {
        Minister minister = _slot.Empty() ? null : _slot.Piece.Minister;
        _data.CallbackLose.Invoke(minister);
        
        _destroyNextTurn = true;
        _slot.Close();

        _nameText.text += "[X]";
        //TODO graphics
        
        OnTaskFailed.Invoke();
    }
    
    private void Succeed()
    {
        Minister minister = _slot.Empty() ? null : _slot.Piece.Minister;    //TODO never null ?
        _data.CallbackWin.Invoke(minister);
        
        if (minister != null)
            minister.GainExperience(_data.LevelRequirement);
        
        _destroyNextTurn = true;
        _slot.Close();
        
        _nameText.text += "[V]";
        //TODO graphics
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
        {
            return false;
        }
        else
        {
            float chance = 1f / (GameSettings.BaseUnfavorableChance + Mathf.Abs(_data.LevelRequirement - minister.Level));
            return Random.Range(0, 1f) > chance;
        }
    }
}
