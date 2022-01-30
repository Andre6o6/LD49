using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Minister : MonoBehaviour
{
    public event System.Action OnMinisterDiedEvent;
    public event System.Action<int> OnMinisterLevelUpEvent;
    public event System.Action<int> OnMinisterBoredomChangeEvent;
    public event System.Action<int, string> OnMinisterUnavailableChangeEvent;

    public Bio Bio { get; private set; }
    public TraitControlledData Personality { get; private set; }
    
    public int Level { get; private set; } = 1;
    public int Boredom { get; private set; }
    public bool CanAct { get; private set; }
    public bool Dead { get; private set; }

    public MinisterSuite Suite => _suite;
    
    [SerializeField] private MinisterSuite _suite;
    [SerializeField] private DragablePiece _piece;
    [SerializeField] private Image _experienceBarImage;
    [SerializeField] private int _boredomMinThreshold = -11;
    [SerializeField] private int _boredomMaxThreshold = 11;
    [SerializeField] private int _sleepTurns = 2;
    [SerializeField] private int _turnsBeforeSleep = 3;
    [SerializeField, Range(0, 1)] private float _sleepChance = 0.5f;
    private Image _borderImage;
    private int _currentExp;
    private Color _defaultColor;
    private bool _exhausted;

    private int _noRecoveryTurnCount;    //For not recovering when just returned

    private string _unavailableReason = "Sleeping";
    private int _unavailableTurnsCount = -1;

    private void Awake()
    {
        _borderImage = GetComponentInChildren<Image>();
        
        CanAct = true;

        _piece ??= GetComponent<DragablePiece>();
        _piece.OnMovedToSlotEvent.AddListener(TickTurnTimer);
        
        GameController.Instance.OnTurnAdvanced.AddListener(OnAdvanceTurn);
    }

    private void Update()
    {
        //TODO: TESTING STUFF
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.L)) GainLevel();
            if (Input.GetKeyDown(KeyCode.K)) LoseLevel();
            
            if (Input.GetKeyDown(KeyCode.A)) ChangeBoredom(+1);
            if (Input.GetKeyDown(KeyCode.Q)) ChangeBoredom(-1);
            if (Input.GetKeyDown(KeyCode.S)) EmpireController.ChangeStability(+1);
            
            if (Input.GetKeyDown(KeyCode.W) && _piece.IsHome) EmpireController.Instance.AddWinPoint();
            
            if (Input.GetKeyDown(KeyCode.U)) SetUnavailable(2);
        }
    }

    private void OnDestroy()
    {
        if (GameController.E_Exists())
            GameController.Instance.OnTurnAdvanced.RemoveListener(OnAdvanceTurn);
    }

    public void GainExperience(int taskLevel)
    {
        int levelsGained = 0;
        
        _currentExp += 1 + taskLevel / 2;
        while (_currentExp >= Level)
        {
            _currentExp -= Level;
            GainLevel();
            levelsGained += 1;
        }

        float newFillAmount = _currentExp / (float)Level;
        var seq = DOTween.Sequence();
        for (int i = 0; i < levelsGained; i++)
        {
            seq.Append(
                DOTween.To(
                    () => _experienceBarImage.fillAmount,
                    x => _experienceBarImage.fillAmount = x,
                    1, 
                    0.5f * (1 - _experienceBarImage.fillAmount))
                .OnComplete(() => _experienceBarImage.fillAmount = 0)
            );
        }
        seq.Append(
            DOTween.To(
                () => _experienceBarImage.fillAmount,
                x => _experienceBarImage.fillAmount = x,
                newFillAmount - _experienceBarImage.fillAmount, 0.2f)
        );
    }

    public void GainLevel() 
    {
        Level += 1;
        OnMinisterLevelUpEvent?.Invoke(Level);
    }

    public void LoseLevel()
    {
        Level -= 1;
        OnMinisterLevelUpEvent?.Invoke(Level);
    }

    public void ChangeBoredom(int delta)
    {
        //TODO skips task exh 
        if (_unavailableTurnsCount > 0) return;    //Not change boredom while sleeping to prevent ui change
        
        Boredom += delta; 
        Boredom = Mathf.Clamp(Boredom, _boredomMinThreshold, _boredomMaxThreshold);
        
        if (CanAct && Boredom <= _boredomMinThreshold) GetExhausted();
        
        OnMinisterBoredomChangeEvent?.Invoke(Boredom);
    }

    public void TryKill()
    {
        if (Level < 5) return;

        if (Level - EmpireController.Instance.GetMinLevelMinister().Level >= 5)
            EmpireController.Instance.GetMinLevelMinister().Die(this);
    }

    public void Die(Minister killer)
    {
        _piece.ReturnHome();

        CanAct = false;
        GameController.Instance.OnTurnAdvanced.RemoveListener(OnAdvanceTurn);
        EmpireController.Instance.RemoveMinister(Suite);

        KillTreacker.Instance.OnMinisterDied(this, killer);
        Dead = true;
            
        OnMinisterDiedEvent?.Invoke();
    }

    public void SetUnavailable(int turns, string reason = "SLEEPING", bool forceHome = true)
    {
        _unavailableTurnsCount = turns;
        if (forceHome) _piece.ReturnHome();
        _unavailableReason = reason;

        CanAct = false;
        _defaultColor = _borderImage.color;
        _borderImage.color = Color.black;
        OnMinisterUnavailableChangeEvent?.Invoke(turns, reason);
    }

    public void SetNonRecoveryTurn(int turnCount = 1)
    {
        if (_exhausted) return;
        
        _noRecoveryTurnCount = turnCount;
    }

    private void TickTurnTimer(bool _)
    {
        GameController.Instance.AdvanceTurn();
    }

    private void OnAdvanceTurn()
    {
        if (Dead) return;

        if (_piece.IsHome)
        {
            //Sleep
            if (_unavailableTurnsCount > 1)
            {
                _unavailableTurnsCount -= 1;
                OnMinisterUnavailableChangeEvent?.Invoke(_unavailableTurnsCount, _unavailableReason);
                return;
            }

            if (_unavailableTurnsCount == 1)
            {
                CanAct = true;
                Boredom = _boredomMaxThreshold - 1;
                OnMinisterBoredomChangeEvent?.Invoke(Boredom);

                _borderImage.color = _defaultColor;

                _unavailableTurnsCount = 0; //Set to 0 to only call this once
                return;
            }
        }

        if (_piece.IsHome || _exhausted)    //exhausted spends a turn on task
        {
            //Passive boredom regen
            if (_noRecoveryTurnCount <= 0)
                ChangeBoredom(1);
            else
                _noRecoveryTurnCount -= 1;
        }

        if (!CanAct && Boredom >= 0) //stop recover
        {
            CanAct = true;
            _borderImage.color = _defaultColor;
            _exhausted = false;
        }

        if (CanAct && Boredom <= _boredomMinThreshold) //start recover, TODO not needed here
        {
            GetExhausted();
        }

        if (CanAct) TryKill();
    }

    private void GetExhausted()
    {
        CanAct = false;
        _noRecoveryTurnCount = 0;    //if exhausted - start recover instantly
        _defaultColor = _borderImage.color;
        _borderImage.color = Color.black;
        _exhausted = true;
    }

    public string GetPositionName()
    {
        if (Suite == MinisterSuite.Army)
            return "Marshal";
        if (Suite == MinisterSuite.Money)
            return "Steward";
        if (Suite == MinisterSuite.Mood)
            return "Chancellor";

        return "";
    }
    
    public string GetColoredPositionName()
    {
        if (Suite == MinisterSuite.Army)
            return "<#FF8300>Marshal</color>";
        if (Suite == MinisterSuite.Money)
            return "<#00E538>Steward</color>";
        if (Suite == MinisterSuite.Mood)
            return "<#60A7E5>Chancellor</color>";

        return "";
    }

    public string GetBoredomText()
    {
        if (_exhausted && Boredom < 0) return "<b>EXHAUSTED</b>";    //Recovering
        
        if (Boredom == _boredomMaxThreshold) return "Bored";
        if (Boredom == _boredomMinThreshold) return "<b>EXHAUSTED</b>";
        return  Boredom >= 0 ? "Rested" : "Tired";
    }
}

public enum MinisterSuite
{
    None,
    Money,
    Army,
    Mood,
}
