using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : Singleton<GameController>
{
    public UnityEvent OnTurnAdvanced;
    public UnityEvent OnAfterTurnAdvanced;
    
    public int Turn { get; private set; }

    public void AdvanceTurn()
    {
        Turn += 1;
        OnTurnAdvanced.Invoke();
        OnAfterTurnAdvanced.Invoke();
    }
}
