using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllGameCards
{
    public static TaskCardData TestCard = new TaskCardData()
    {
        Name = "Test",
        TurnsToSolve = 2,
        CallbackLose = (_) => Debug.Log("Lose"),
        CallbackWin = (_) => Debug.Log("Win")
    };
    
    //Army
    public static TaskCardData TrainArmy = new TaskCardData()
    {
        Name = "Train army",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 2,
        CallbackLose = (_) =>
        { },
        CallbackWin = (_) =>
        { },
        LevelRequirement = 2,
    };
    public static TaskCardData ForceRecruit = new TaskCardData()
    {
        Name = "Recruit levies",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 3,
        CallbackLose = (minister) => { },
        CallbackWin = (_) => { },
        LevelRequirement = 1,
    };
    public static TaskCardData MinisterCombat = new TaskCardData()
    {
        Name = "Duel",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (minister) =>
        {
            if (minister != null)
            {
                minister.ChangeBoredom(-5);
            }
        },
        CallbackWin = (minister) =>
        {
            minister.ChangeBoredom(-5);
            minister.GainLevel();
        },
        LevelRequirement = 5,
    };
    public static TaskCardData Revolt = new TaskCardData()
    {
        Name = "Revolt",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(Revolt2);
        },
        CallbackWin = (_) =>
        {
        },
        LevelRequirement = 1,
    };
    public static TaskCardData RevoltImportant = new TaskCardData()
    {
        Name = "Revolt",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(Revolt2);
        },
        CallbackWin = (_) =>
        {},
        LevelRequirement = 1,
        Common = false,
        Important = true,
        DestroyOnFinish = true,
    };
    public static TaskCardData Revolt2 = new TaskCardData()
    {
        Name = "Revolt",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 2,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(Revolt3);
        },
        CallbackWin = (_) =>
        {},
        LevelRequirement = 2,
        Common = false,
    };
    public static TaskCardData Revolt3 = new TaskCardData()
    {
        Name = "Revolt",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 3,
        CallbackLose = (_) =>
        {},
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(Revolt2);
        },
        LevelRequirement = 4,
        Common = false,
        Important = true,
    };
    public static TaskCardData Raid = new TaskCardData()
    {
        Name = "Raid by nomads",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 3,
        CallbackLose = (minister) =>
        { },
        CallbackWin = (_) =>
        { },
        LevelRequirement = 3,
    };
    public static TaskCardData RoadBanditsImportant = new TaskCardData()
    {
        Name = "Road bandits",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        { },
        CallbackWin = (_) =>
        { },
        LevelRequirement = 2,
        Common = false,
        Important = true,
        DestroyOnFinish = true,
    };
    public static TaskCardData RoadBandits = new TaskCardData()
    {
        Name = "Road bandits",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        { },
        CallbackWin = (_) =>
        {
        },
        LevelRequirement = 2,
        Common = false,
    };
    public static TaskCardData RogueMercenaries = new TaskCardData()
    {
        Name = "Rogue mercenaries",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(RoadBandits);
        },
        CallbackWin = (_) =>
        { },
        LevelRequirement = 4,
        Common = false,
        Important = true,
        DestroyOnFinish = true,
    };
    public static TaskCardData SatanCult = new TaskCardData()
    {
        Name = "Satan cult",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 2,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(SatanCult);
        },
        CallbackWin = (_) =>
        {
        },
        Common = false,
        DestroyOnFinish = true,
        LevelRequirement = 6,
    };
    //TODO monster chain
    
    //MONEY
    public static TaskCardData CollectTaxes = new TaskCardData()
    {
        Name = "Collect taxes",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 2,
        CallbackLose = (minister) =>
        {
            DeckManager.AddCardToGlobalDeck(TaxEvaders);
        },
        CallbackWin = (_) =>
        { },
        LevelRequirement = 1,
    };
    public static TaskCardData TaxEvaders = new TaskCardData()
    {
        Name = "Tax evaders",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 2,
        CallbackLose = (minister) =>
        {
            DeckManager.AddCardToGlobalDeck(TaxEvaders);
            DeckManager.AddCardToGlobalDeck(Revolt2);
        },
        CallbackWin = (_) =>
        { },
        Common = false,
        DestroyOnFinish = true,
        LevelRequirement = 3,
    };
    public static TaskCardData TradeRoute = new TaskCardData()
    {
        Name = "Establish trade route",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 2,
        CallbackLose = (minister) =>
        {
            DeckManager.AddCardToGlobalDeck(RoadBanditsImportant);
        },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(TradeDeal);
        },
        LevelRequirement = 3,
    };
    public static TaskCardData TradeDeal = new TaskCardData()
    {
        Name = "Shady trade deal",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 3,
        CallbackLose = (minister) =>
        { },
        CallbackWin = (_) =>
        { },
        Common = false,
        DestroyOnFinish = true,
        LevelRequirement = 4,
    };
    public static TaskCardData MintCoins = new TaskCardData()
    {
        Name = "Mint more coins",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 3,
        CallbackLose = (minister) =>
        { },
        CallbackWin = (_) =>
        { },
        LevelRequirement = 2,
    };
    public static TaskCardData Mercenaries = new TaskCardData()
    {
        Name = "Bribe mercenary band",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 3,
        CallbackLose = (minister) =>
        {
            DeckManager.AddCardToGlobalDeck(RogueMercenaries);
        },
        CallbackWin = (_) =>
        { },
        LevelRequirement = 4,
    };
    public static TaskCardData BuyCropsFamine = new TaskCardData()
    {
        Name = "Import more crops",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 3,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(Hunger);
            DeckManager.AddCardToGlobalDeck(Famine);
        },
        CallbackWin = (_) =>
        { },
        LevelRequirement = 3,
        Important = true,
        DestroyOnFinish = true,
    };
    //TODO Smugglers
    
    //MOOD
    public static TaskCardData Hunger = new TaskCardData()
    {
        Name = "Hunger in villages",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 3,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(Famine);
            DeckManager.AddCardToGlobalDeck(Revolt);
        },
        CallbackWin = (_) =>
        { },
        LevelRequirement = 1,
    };
    public static TaskCardData Famine = new TaskCardData()
    {
        Name = "Famine",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 2,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(Revolt2);
            DeckManager.AddCardToGlobalDeck(BigFamine);
        },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(BuyCropsFamine);
        },
        Common = false,
        DestroyOnFinish = true,
        LevelRequirement = 3,
    };
    public static TaskCardData BigFamine = new TaskCardData()
    {
        Name = "Famine",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(Revolt3);
            DeckManager.AddCardToGlobalDeck(Famine);
        },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(Famine);
        },
        Common = false,
        Important = true,
        DestroyOnFinish = true,
        LevelRequirement = 5,
    };
    public static TaskCardData Feud = new TaskCardData()
    {
        Name = "Reconcile a feud",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {},
        CallbackWin = (_) => { },
        LevelRequirement = 2,
    };
    public static TaskCardData BrewingMasses = new TaskCardData()
    {
        Name = "Quell civil unrest",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 3,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(Revolt);
        },
        CallbackWin = (_) =>
        { },
        Common = false,
        LevelRequirement = 3,
    };
    public static TaskCardData ReligionPush = new TaskCardData()
    {
        Name = "Prosecute nonbelievers",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 3,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(PaganCults);
        },
        CallbackWin = (_) =>
        { },
        LevelRequirement = 2,
    };
    public static TaskCardData PaganCults = new TaskCardData()
    {
        Name = "Pagan cults",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 2,
        CallbackLose = (minister) =>
        {
            DeckManager.AddCardToGlobalDeck(PaganCults);
            DeckManager.AddCardToGlobalDeck(SatanCult);
        },
        CallbackWin = (_) =>
        { },
        Common = false,
        LevelRequirement = 4,
    };
    
    public static TaskCardData SuperTask1 = new TaskCardData()
    {
        Name = "Deal with Nomans Steppe",
        SuiteRequirement = MinisterSuite.None,
        TurnsToSolve = 0,
        CallbackLose = (_) => { },
        CallbackWin = FinishSuperTask,
        SuperTask = true,
        DestroyOnFinish = true,
        LevelRequirement = 6,
    };
    public static TaskCardData SuperTask2 = new TaskCardData()
    {
        Name = "Deal with  Bohemian Province",
        SuiteRequirement = MinisterSuite.None,
        TurnsToSolve = 0,
        CallbackLose = (_) => { },
        CallbackWin = FinishSuperTask,
        SuperTask = true,
        DestroyOnFinish = true,
        LevelRequirement = 7,
    };
    public static TaskCardData SuperTask3 = new TaskCardData()
    {
        Name = "Deal with Blue Isles",
        SuiteRequirement = MinisterSuite.None,
        TurnsToSolve = 0,
        CallbackLose = (_) => { },
        CallbackWin = FinishSuperTask,
        SuperTask = true,
        DestroyOnFinish = true,
        LevelRequirement = 8,
    };

    private static void FinishSuperTask(Minister minister)
    {
        if (minister.Suite == MinisterSuite.Army)
            DeckManager.AddCardToGlobalDeck(War);
        if (minister.Suite == MinisterSuite.Money)
            DeckManager.AddCardToGlobalDeck(BuyRevolts1);
        if (minister.Suite == MinisterSuite.Mood)
            DeckManager.AddCardToGlobalDeck(SpyStart);
    }
    
    public static TaskCardData War = new TaskCardData()
    {
        Name = "War declared!",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(Battle1);
            DeckManager.AddCardToGlobalDeck(BattleImportant);
        },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(Battle1);
        },
        LevelRequirement = 6,
        Common = false,
        Important = true,
        DestroyOnFinish = true,
    };
    public static TaskCardData Battle1 = new TaskCardData()
    {
        Name = "Battle",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(Battle1);
            DeckManager.AddCardToGlobalDeck(BattleImportant);
        },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(Battle2);
        },
        LevelRequirement = 7,
        Common = false,
        DestroyOnFinish = true,
    };
    public static TaskCardData BattleImportant = new TaskCardData()
    {
        Name = "Town is under siege",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 2,
        CallbackLose = (_) =>
        {
            EmpireController.ChangeStability(-1);
        },
        CallbackWin = (_) =>
        {
        },
        LevelRequirement = 5,
        Common = false,
        Important = true,
        DestroyOnFinish = true,
    };
    public static TaskCardData Battle2 = new TaskCardData()
    {
        Name = "Surround enemy army",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(Battle1);
            DeckManager.AddCardToGlobalDeck(RogueMercenaries);
            DeckManager.AddCardToGlobalDeck(RoadBandits);
        },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(Battle3);
        },
        LevelRequirement = 8,
        Common = false,
        DestroyOnFinish = true,
    };
    public static TaskCardData Battle3 = new TaskCardData()
    {
        Name = "Storm the capital",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(Battle1);
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.AddWinPoint();
            EmpireController.ChangeStability(3);
        },
        LevelRequirement = 9,
        Common = false,
        WinPoint = true,
        DestroyOnFinish = true,
    };
    
    public static TaskCardData BuyRevolts1 = new TaskCardData()
    {
        Name = "Supply foreign revolts",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 3,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(BuyRevoltsRepeat);
        },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(BuyRevolts2);
            DeckManager.AddCardToGlobalDeck(BuyRevolts2);
            DeckManager.AddCardToGlobalDeck(BuyRevolts3);
        },
        LevelRequirement = 7,
        Common = false,
        Important = true,
        DestroyOnFinish = true,
    };
    public static TaskCardData BuyRevoltsRepeat = new TaskCardData()
    {
        Name = "Supply foreign revolts",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 3,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(BuyRevoltsRepeat);
            DeckManager.AddCardToGlobalDeck(Revolt2);
        },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(BuyRevolts3);
        },
        LevelRequirement = 7,
        Common = false,
        DestroyOnFinish = true,
    };
    
    public static TaskCardData BuyRevolts2 = new TaskCardData()
    {
        Name = "Smuggle troops",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 2,
        CallbackLose = (_) =>
        { },
        CallbackWin = (_) =>
        { },
        LevelRequirement = 6,
        Common = false,
        DestroyOnFinish = true,
    };
    public static TaskCardData BuyRevolts3 = new TaskCardData()
    {
        Name = "Bribe revolt leaders",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 4,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(BuyRevolts3);
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.AddWinPoint();
            EmpireController.ChangeStability(3);
        },
        LevelRequirement = 10,
        Common = false,
        DestroyOnFinish = true,
        WinPoint = true,
    };
    
    public static TaskCardData SpyStart = new TaskCardData()
    {
        Name = "Build spy network",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 4,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(Spy1);
        },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(Assassination);
        },
        LevelRequirement = 6,
        Important = true,
        Common = false,
        DestroyOnFinish = true,
    };
    public static TaskCardData Spy1 = new TaskCardData()
    {
        Name = "Build spy network",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 4,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(Spy1);
        },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(Assassination);
        },
        LevelRequirement = 6,
        Common = false,
        DestroyOnFinish = true,
    };
    public static TaskCardData Assassination = new TaskCardData()
    {
        Name = "Assassinate foreign leader",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 4,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(Spy1);
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.AddWinPoint();
            EmpireController.ChangeStability(3);
        },
        LevelRequirement = 12,
        Common = false,
        DestroyOnFinish = true,
        WinPoint = true,
    };
    
    //TUTORIAL
    public static TaskCardData Tutorial1 = new TaskCardData()
    {
        Name = "Wear ceremonial sword",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (_) => { },
        CallbackWin = (_) => { },
        LevelRequirement = 1,
        Common = false,
        DestroyOnFinish = true,
    };
    public static TaskCardData Tutorial2 = new TaskCardData()
    {
        Name = "Inspect treasury",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 2,
        CallbackLose = (_) => { },
        CallbackWin = (_) => { },
        LevelRequirement = 1,
        Common = false,
        DestroyOnFinish = true,
    };
    public static TaskCardData Tutorial3 = new TaskCardData()
    {
        Name = "Meet nobles",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 3,
        CallbackLose = (_) => { },
        CallbackWin = (_) => { },
        LevelRequirement = 1,
        Common = false,
        DestroyOnFinish = true,
    };
}
