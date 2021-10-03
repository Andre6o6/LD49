using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Minister : MonoBehaviour
{
    public int Level { get; private set; }
    public int Boredom { get; private set; }
    public bool CanAct { get; private set; }
    public bool Dead { get; private set; }
    
    public MinisterSuite Suite => _suite;
    
    [SerializeField] private MinisterSuite _suite;
    [SerializeField] private DragablePiece _piece;
    [SerializeField] private Image _bloodImage;
    private int _currentExp;
    private Color _defaultColor;

    private void Awake()
    {
        CanAct = true;
        Level = 1;
        
        _piece ??= GetComponent<DragablePiece>();
        _piece.OnMovedToSlotEvent.AddListener(OnMoveToSlot);

        GameController.Instance.OnTurnAdvanced.AddListener(OnAdvanceTurn);
    }

    private void OnDestroy()
    {
        if (GameController.E_Exists())
            GameController.Instance.OnTurnAdvanced.RemoveListener(OnAdvanceTurn);
    }

    public void GainExperience(int taskLevel)
    {
        Level += 1;
        /*if (taskLevel <= Level)
            _currentExp += 1;
        else
            _currentExp += 1 + (taskLevel - Level);    //TODO balance

        if (_currentExp >= Level / 2)
        {
            Level += 1;
            _currentExp = 0;
        }*/
    }

    public void GainLevel()
    {
        Level += 1;
    }

    public void LoseLevel()
    {
        Level -= 1;
    }

    public void ChangeBoredom(int delta)
    {
        Boredom += delta; 
        Boredom = Mathf.Clamp(Boredom, -11, 11);
    }

    public void TryKill()
    {
        if (Level < 5) return;

        if (Level - EmpireController.Instance.GetMinLevelMinister().Level >= 5)
            EmpireController.Instance.GetMinLevelMinister().Die(this);
    }

    public void Die(Minister killer)
    {
        Debug.Log(killer, killer);

        _piece.ReturnHome();
        
        if (_bloodImage != null)
            _bloodImage.gameObject.SetActive(true);

        CanAct = false;
        GameController.Instance.OnTurnAdvanced.RemoveListener(OnAdvanceTurn);
        EmpireController.Instance.RemoveMinister(Suite);


        KillTreacker.Instance.OnMinisterDied(this, killer);
        Dead = true;
    }

    private void OnMoveToSlot()
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

        if (CanAct) TryKill();
    }

    public string GetPosition()
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
