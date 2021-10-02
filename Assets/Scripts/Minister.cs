using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minister : MonoBehaviour
{
    public int Level { get; private set; }
    public int Boredom { get; private set; }
    public bool CanAct { get; private set; }
    
    public MinisterSuite Suite => _suite;
    
    [SerializeField] private MinisterSuite _suite;
    [SerializeField] private DragablePiece _piece;
    private int _currentExp;

    private bool _bufferTurn;    //TODO fixme: To not get -1 exh as soon as we move
    

    private void Awake()
    {
        CanAct = true;
        
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
        if (taskLevel <= Level)
            _currentExp += 1;
        else
            _currentExp += 1 + (taskLevel - Level);    //TODO balance

        if (_currentExp >= Level / 2)
        {
            Level += 1;
            _currentExp = 0;
        }
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
        Boredom = Mathf.Clamp(Boredom + delta, -11, 11);
    }

    public void TryKill()
    {
        if (Level < 10) return;

        if (Level - EmpireController.Instance.GetMinLevelMinister().Level > 5)
            EmpireController.Instance.GetMinLevelMinister().Die(this);
    }

    public void Die(Minister killer = null)
    {
        //TODO Particle and sound
        //TODO game window that tells that 1 killed another
        
        CanAct = false;
        GameController.Instance.OnTurnAdvanced.RemoveListener(OnAdvanceTurn);
        EmpireController.Instance.RemoveMinister(Suite);
    }

    private void OnMoveToSlot()
    {
        _bufferTurn = true;
        GameController.Instance.AdvanceTurn();
    }

    private void OnAdvanceTurn()
    {
        if (_piece.IsHome)
        {
            Boredom += 1;
        }
        else if (!_piece.OnFinishedSlot)
        {
            if (_bufferTurn)
            {
                _bufferTurn = false;
                return;
            }

            Boredom -= 1;
        }

        if (!CanAct && Boredom >= 0)    //stop recover
            CanAct = true;
        //TODO highlight with color
        
        if (CanAct && Boredom <= -10)    //start recover
            CanAct = false;

        if (CanAct) TryKill();
    }
}

public enum MinisterSuite
{
    None,
    Money,
    Army,
    Mood,
}
