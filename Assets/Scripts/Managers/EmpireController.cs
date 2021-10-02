using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EmpireController : Singleton<EmpireController>
{
    public UnityEvent<int> OnStabilityChangedEvent;

    public int Gold = 100;
    
    public int Army
    {
        get => _army;
        set
        {
            _army =  value;
            if (_army < 0)
                _army = 0;
        }
    }
    
    public int Mood
    {
        get => _mood;
        set => _mood = Mathf.Clamp(value, 0, 100);
    }

    public Minister ArmyMinister;
    public Minister MoneyMinister;
    public Minister MoodMinister;
    
    [SerializeField] private int _stability = 10;
    [SerializeField] private int _army = 100;
    [SerializeField] private int _mood = 50;
    
    private List<Minister> _ministers = new List<Minister>();
    
    public static void ChangeStability(int delta)
    {
        Instance.ChangeStabilityValue(delta);
    }

    protected override void Awake()
    {
        base.Awake();
        _ministers.Add(ArmyMinister);
        _ministers.Add(MoneyMinister);
        _ministers.Add(MoodMinister);
    }

    public void RemoveMinister(MinisterSuite suite)
    {
        if (suite == MinisterSuite.Army)
        {
            _ministers.Remove(ArmyMinister);
            ArmyMinister = null;
        }
        else if (suite == MinisterSuite.Money)
        {
            _ministers.Remove(MoneyMinister);
            MoneyMinister = null;
        }
        else if (suite == MinisterSuite.Mood)
        {
            _ministers.Remove(MoodMinister);
            MoodMinister = null;
        }
    }

    public Minister GetMaxLevelMinister()
    {
        return _ministers.OrderByDescending(x => x.Level).First();
    }

    public Minister GetMinLevelMinister()
    {
        return _ministers.OrderBy(x => x.Level).First();
    }

    private void ChangeStabilityValue(int delta)
    {
        _stability += delta;
        OnStabilityChangedEvent.Invoke(_stability);
    }
}
