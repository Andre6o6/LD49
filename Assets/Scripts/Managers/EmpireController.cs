using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EmpireController : Singleton<EmpireController>
{
    public UnityEvent<int> OnStabilityChangedEvent;
    public UnityEvent<int> OnWinPointsChangedEvent;

    public Minister ArmyMinister;
    public Minister MoneyMinister;
    public Minister MoodMinister;
    
    public int MaxStability = 20;
    private int _stability = 20;
    private int _winPoints;
    
    private List<Minister> _ministers = new List<Minister>();
    
    public static void ChangeStability(int delta)
    {
        Instance.ChangeStabilityValue(delta);
    }

    protected override void Awake()
    {
        base.Awake();

        _stability = MaxStability;
        _ministers = new List<Minister>();
        _ministers.Add(ArmyMinister);
        _ministers.Add(MoneyMinister);
        _ministers.Add(MoodMinister);
    }

    private void Start()
    {
        GetMinLevelMinister();    //TEST
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

    public void AddWinPoint()
    {
        _winPoints += 1;
        OnWinPointsChangedEvent.Invoke(_winPoints);
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
        _stability = Mathf.Clamp(_stability, 0, MaxStability);
        OnStabilityChangedEvent.Invoke(_stability);
    }
}
