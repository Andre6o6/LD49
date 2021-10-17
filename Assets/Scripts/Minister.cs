using UnityEngine;
using UnityEngine.UI;

public class Minister : MonoBehaviour
{
    public event System.Action OnMinisterDiedEvent;
    public event System.Action<int> OnMinisterLevelUpEvent;
    public event System.Action<int> OnMinisterBoredomChangeEvent;

    public int Level { get; private set; } = 1;
    public int Boredom { get; private set; }
    public bool CanAct { get; private set; }
    public bool Dead { get; private set; }
    
    public MinisterSuite Suite => _suite;
    
    [SerializeField] private MinisterSuite _suite;
    [SerializeField] private DragablePiece _piece;
    [SerializeField] private Image _experienceBarImage;
    private int _currentExp;
    private Color _defaultColor;

    private void Awake()
    {
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
            if (Input.GetKeyDown(KeyCode.S)) EmpireController.ChangeStability(+1);
        }
    }

    private void OnDestroy()
    {
        if (GameController.E_Exists())
            GameController.Instance.OnTurnAdvanced.RemoveListener(OnAdvanceTurn);
    }

    public void GainExperience(int taskLevel)
    {
        _currentExp += taskLevel;
        if (_currentExp >= Level)
        {
            _currentExp -= Level;
            GainLevel();

            //TODO animate bar to fill=1, then to 0
        }

        float newFillAmount = (float) _currentExp / Level;
        LeanTween.value(this.gameObject, fill => _experienceBarImage.fillAmount = fill,
            _experienceBarImage.fillAmount, newFillAmount, 0.2f);
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
        Boredom += delta; 
        Boredom = Mathf.Clamp(Boredom, -11, 11);
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

    private void TickTurnTimer()
    {
        GameController.Instance.AdvanceTurn();
    }

    private void OnAdvanceTurn()
    {
        if (Dead) return;
        
        if (_piece.IsHome)
        {
            ChangeBoredom(1);
        }

        if (!CanAct && Boredom >= 0) //stop recover
        {
            CanAct = true;
            GetComponentInChildren<Image>().color = _defaultColor;
        }

        if (CanAct && Boredom <= -10) //start recover
        {
            CanAct = false;
            _defaultColor = GetComponentInChildren<Image>().color;
            GetComponentInChildren<Image>().color  = Color.black;
        }

        if (CanAct) TryKill();    //TODO trait maybe?
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
}

public enum MinisterSuite
{
    None,
    Money,
    Army,
    Mood,
}
