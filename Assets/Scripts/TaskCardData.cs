using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCardData
{
    public string Name;
    public string Description;
    public int LevelRequirement;
    public MinisterSuite SuiteRequirement;

    public System.Func<bool> ResourceSpawnConditions = () => true;
    public System.Func<bool> ResourceWinConditions = () => true;
    
    public int TurnsToSolve = 1;
    public System.Action<Minister> CallbackWin;     //arg never null
    public System.Action<Minister> CallbackLose;    //arg may be null
    public bool DestroyOnFinish;
    public bool Common = true;    //Doesn't get taken from deck when drawn
    public bool Important;    //Ignore idle, but dont spawn on top of deck
    public bool SuperTask;

    public void Dispose()
    {
        if (Common || DestroyOnFinish || Important)  //Don't return importants  
            return;
        
        DeckManager.AddCardToGlobalDeck(this);
    }
}
