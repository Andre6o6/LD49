using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCardData
{
    public string Name;
    public int LevelRequirement;
    public MinisterSuite SuiteRequirement;
    public int TurnsToSolve = 1;
    public System.Action<Minister> CallbackWin;     //arg never null
    public System.Action<Minister> CallbackLose;    //arg may be null
    public bool DestroyOnFinish;
    public bool Common = true;    //Doesn't get taken from deck when drawn
    public bool Important;    //Ignore idle, but dont spawn on top of deck
    public bool SuperTask;
    public bool WinPoint;

    public void Dispose()
    {
        if (Common || DestroyOnFinish || Important)  //Don't return importants  
            return;
        
        DeckManager.AddCardToGlobalDeck(this);
    }
}
