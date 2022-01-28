using UnityEngine;

public class AllGameCards
{
    //Army
    public static readonly TaskCardData TrainArmy = new TaskCardData()
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
    public static readonly TaskCardData ForceRecruit = new TaskCardData()
    {
        Name = "Recruit levies",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 3,
        CallbackLose = (minister) => { },
        CallbackWin = (_) => { },
        LevelRequirement = 1,
    };
    public static readonly TaskCardData MinisterCombat = new TaskCardData()
    {
        Name = "Duel",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (minister) =>
        {
            if (minister != null)
            {
                minister.ChangeBoredom(-10);
            }
        },
        CallbackWin = (minister) =>
        {
            minister.ChangeBoredom(-2);
            if (minister.Level < 5) minister.GainLevel();
        },
        LevelRequirement = 5,
    };
    public static readonly TaskCardData Revolt = new TaskCardData()
    {
        Name = "Revolt",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(Revolt);
        },
        CallbackWin = (_) =>
        {
            if (Random.value < 0.5f)
            {
                DeckManager.RemoveCardFromDeck(Revolt);
                DeckManager.AddCardToDeck(Revolt2);
            }
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
            DeckManager.AddCardToDeck(Revolt2);
        },
        CallbackWin = (_) =>
        { },
        LevelRequirement = 1,
        DrawMode = CardDrawMode.Important,
    };
    public static readonly TaskCardData Revolt2 = new TaskCardData()
    {
        Name = "Revolt",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 2,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(Revolt);
            DeckManager.AddCardToDeck(Revolt2);
        },
        CallbackWin = (_) =>
        {
            if (Random.value < 0.5f)
            {
                DeckManager.RemoveCardFromDeck(Revolt2);
                DeckManager.AddCardToDeck(Revolt3);
            }
        },
        LevelRequirement = 3,
        DrawMode = CardDrawMode.ReturnToDeck,
    };
    public static readonly TaskCardData Revolt3 = new TaskCardData()
    {
        Name = "Revolt",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 3,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(Revolt2);
            DeckManager.AddCardToDeck(Revolt3);
            DeckManager.AddCardToDeck(Revolt4Important);
        },
        CallbackWin = (_) =>
        {
            DeckManager.RemoveCardFromDeck(Revolt);
            DeckManager.RemoveCardFromDeck(Revolt2);
        },
        LevelRequirement = 5,
        DrawMode = CardDrawMode.ReturnToDeck,
    };
    public static TaskCardData Revolt4Important = new TaskCardData()
    {
        Name = "Revolt",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(Revolt3);
        },
        CallbackWin = (_) =>
        {
            DeckManager.RemoveCardFromDeck(Revolt);
            DeckManager.RemoveCardFromDeck(Revolt2);
            DeckManager.RemoveCardFromDeck(Revolt3);
        },
        LevelRequirement = 6,
        DrawMode = CardDrawMode.Important,
    };
    public static readonly TaskCardData Raid = new TaskCardData()
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
    public static readonly TaskCardData RoadBanditsImportant = new TaskCardData()
    {
        Name = "Road bandits",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        { },
        CallbackWin = (_) =>
        { },
        LevelRequirement = 2,
        DrawMode = CardDrawMode.Important
    };
    public static readonly TaskCardData RoadBandits = new TaskCardData()
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
        DrawMode = CardDrawMode.ReturnToDeck,
    };
    public static readonly TaskCardData RoadBandits2 = new TaskCardData()
    {
        Name = "Road bandits",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 2,
        CallbackLose = (_) =>
            { },
        CallbackWin = (_) =>
        { },
        LevelRequirement = 4,
        DrawMode = CardDrawMode.ReturnToDeck,
    };
    public static readonly TaskCardData RogueMercenaries = new TaskCardData()
    {
        Name = "Rogue mercenaries",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(RogueMercenaries2);
        },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToDeck(RoadBandits2);
        },
        LevelRequirement = 4,
        DrawMode = CardDrawMode.Important,
    };
    public static readonly TaskCardData RogueMercenaries2 = new TaskCardData()
    {
        Name = "Rogue mercenaries",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        { },
        CallbackWin = (_) =>
        { },
        LevelRequirement = 5,
        DrawMode = CardDrawMode.ReturnToDeck,
    };
    public static readonly TaskCardData SatanCult = new TaskCardData()
    {
        Name = "Satan cult",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 2,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(SatanCult);
        },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToDeck(PaganCults);
        },
        DrawMode = CardDrawMode.DestroyOnFinish,
        LevelRequirement = 7,
    };
    //TODO monster chain
    
    //MONEY
    public static readonly TaskCardData CollectTaxes = new TaskCardData()
    {
        Name = "Collect taxes",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 2,
        CallbackLose = (minister) =>
        { },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToDeck(TaxEvaders);
        },
        LevelRequirement = 1,
    };
    public static readonly TaskCardData TaxEvaders = new TaskCardData()
    {
        Name = "Tax evaders",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 2,
        CallbackLose = (minister) =>
        {
            DeckManager.AddCardToDeck(TaxEvaders);
            DeckManager.AddCardToDeck(Revolt2);
        },
        CallbackWin = (_) =>
        {
            if (Random.value < 0.5f) DeckManager.AddCardToDeck(TaxEvaders2);
        },
        DrawMode = CardDrawMode.ReturnToDeck,
        LevelRequirement = 3,
    };
    public static readonly TaskCardData TaxEvaders2 = new TaskCardData()
    {
        Name = "Tax evaders",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 2,
        CallbackLose = (minister) =>
        {
            DeckManager.AddCardToDeck(TaxEvaders2);
            DeckManager.AddCardToDeck(Revolt3);
        },
        CallbackWin = (_) => { },
        DrawMode = CardDrawMode.DestroyOnFinish,
        LevelRequirement = 5,
    };
    public static readonly TaskCardData TradeRoute = new TaskCardData()
    {
        Name = "Establish trade route",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 2,
        CallbackLose = (minister) =>
        {
            DeckManager.AddCardToDeck(RoadBanditsImportant);
        },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToDeck(TradeDeal);
        },
        LevelRequirement = 3,
    };
    public static readonly TaskCardData TradeDeal = new TaskCardData()
    {
        Name = "Shady trade deal",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 4,
        CallbackLose = (minister) =>
        { },
        CallbackWin = (_) =>
        { },
        DrawMode = CardDrawMode.DestroyOnFinish,
        LevelRequirement = 4,
    };
    public static readonly TaskCardData MintCoins = new TaskCardData()
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
    public static readonly TaskCardData Mercenaries = new TaskCardData()
    {
        Name = "Bribe mercenary band",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 1,
        CallbackLose = (minister) =>
        {
            DeckManager.AddCardToDeck(RogueMercenaries);
        },
        CallbackWin = (minister) =>
        {
            if (minister.Level >= 5 && Random.value < 0.5f) 
                DeckManager.AddCardToDeck(RogueMercenaries);
        },
        LevelRequirement = 5,
    };
    public static readonly TaskCardData BuyCropsFamine = new TaskCardData()
    {
        Name = "Import more crops",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 3,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(Hunger);
            DeckManager.AddCardToDeck(BigFamine);
        },
        CallbackWin = (_) =>
        {
            DeckManager.RemoveCardFromDeck(Famine);
            if (Random.value < 0.25f) DeckManager.RemoveCardFromDeck(Famine2);
        },
        LevelRequirement = 3,
        DrawMode = CardDrawMode.Important,
    };
    public static readonly TaskCardData BuyCropsFamine2 = new TaskCardData()
    {
        Name = "Import more crops",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 5,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(Famine);
            DeckManager.AddCardToDeck(BigFamine2);
        },
        CallbackWin = (_) =>
        {
            DeckManager.RemoveCardFromDeck(Famine2);
        },
        LevelRequirement = 5,
        DrawMode = CardDrawMode.Important,
    };
    //TODO Smugglers
    
    //MOOD
    public static readonly TaskCardData Hunger = new TaskCardData()
    {
        Name = "Hunger in villages",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 3,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(Famine);
            DeckManager.AddCardToDeck(Revolt);
        },
        CallbackWin = (_) =>
        {
            if (Random.value < 0.5f) DeckManager.AddCardToDeck(Famine);
        },
        LevelRequirement = 1,
    };
    public static readonly TaskCardData Famine = new TaskCardData()
    {
        Name = "Famine",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 2,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(Revolt2);
            DeckManager.AddCardToDeck(BigFamine);
        },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToDeck(BuyCropsFamine);
            if (Random.value < 0.5f) DeckManager.AddCardToDeck(Famine2);
            if (Random.value < 0.25f) DeckManager.AddCardToDeck(BigFamine);
        },
        DrawMode = CardDrawMode.DestroyOnFinish,
        LevelRequirement = 3,
    };
    public static readonly TaskCardData Famine2 = new TaskCardData()
    {
        Name = "Famine",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 2,
        CallbackLose = (_) =>
        {
            DeckManager.RemoveCardFromDeck(Hunger);
            DeckManager.AddCardToDeck(Famine2);
            DeckManager.AddCardToDeck(BigFamine);
        },
        CallbackWin = (_) =>
        {
            if (Random.value < 0.25f) DeckManager.AddCardToDeck(BigFamine);
        },
        DrawMode = CardDrawMode.ReturnToDeck,
        LevelRequirement = 4,
    };
    public static readonly TaskCardData BigFamine = new TaskCardData()
    {
        Name = "Famine",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(Revolt3);
            DeckManager.AddCardToDeck(Famine);
            DeckManager.AddCardToDeck(Famine2);
        },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToDeck(BuyCropsFamine2);
            if (Random.value < 0.25f) DeckManager.AddCardToDeck(BigFamine2);
        },
        DrawMode = CardDrawMode.Important,
        LevelRequirement = 5,
    };
    public static readonly TaskCardData BigFamine2 = new TaskCardData()
    {
        Name = "Famine",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            DeckManager.RemoveCardFromDeck(Hunger);
            DeckManager.RemoveCardFromDeck(Famine2);
            DeckManager.AddCardToDeck(BigFamine);
            DeckManager.AddCardToDeck(BigFamine2);
        },
        CallbackWin = (_) =>
        {
            DeckManager.RemoveCardFromDeck(Famine2);
        },
        DrawMode = CardDrawMode.ReturnToDeck,
        LevelRequirement = 6,
    };
    public static readonly TaskCardData Feud = new TaskCardData()
    {
        Name = "Reconcile a feud",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 2,
        CallbackLose = (_) =>
        {},
        CallbackWin = (_) => { },
        LevelRequirement = 2,
    };
    public static readonly TaskCardData BrewingMasses = new TaskCardData()
    {
        Name = "Quell civil unrest",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(Revolt);
        },
        CallbackWin = (_) =>
        { },
        DrawMode = CardDrawMode.ReturnToDeck,
        LevelRequirement = 3,
    };
    public static readonly TaskCardData ReligionPush = new TaskCardData()
    {
        Name = "Prosecute nonbelievers",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 3,
        CallbackLose = (_) =>
        { },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToDeck(PaganCults);
        },
        LevelRequirement = 2,
    };
    public static readonly TaskCardData PaganCults = new TaskCardData()
    {
        Name = "Pagan cults",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 2,
        CallbackLose = (minister) =>
        {
            DeckManager.AddCardToDeck(PaganCults);
        },
        CallbackWin = (_) =>
        {
            if (Random.value < 0.5f) DeckManager.AddCardToDeck(SatanCult);
        },
        DrawMode = CardDrawMode.ReturnToDeck,
        LevelRequirement = 4,
    };
    
    public static readonly TaskCardData SuperTask1 = new TaskCardData()
    {
        Name = "Invade Nomans Steppe",
        SuiteRequirement = MinisterSuite.None,
        TurnsToSolve = 0,
        CallbackLose = (_) => { },
        CallbackWin = FinishSuperTask,
        SuperTask = true,
        DrawMode = CardDrawMode.DestroyOnFinish,
        LevelRequirement = 6,
    };
    public static readonly TaskCardData SuperTask2 = new TaskCardData()
    {
        Name = "Invade Bohemian Province",
        SuiteRequirement = MinisterSuite.None,
        TurnsToSolve = 0,
        CallbackLose = (_) => { },
        CallbackWin = FinishSuperTask,
        SuperTask = true,
        DrawMode = CardDrawMode.DestroyOnFinish,
        LevelRequirement = 7,
    };
    public static readonly TaskCardData SuperTask3 = new TaskCardData()
    {
        Name = "Invade Blue Isles",
        SuiteRequirement = MinisterSuite.None,
        TurnsToSolve = 0,
        CallbackLose = (_) => { },
        CallbackWin = FinishSuperTask,
        SuperTask = true,
        DrawMode = CardDrawMode.DestroyOnFinish,
        LevelRequirement = 8,
    };

    private static void FinishSuperTask(Minister minister)
    {
        if (minister.Suite == MinisterSuite.Army)
            DeckManager.AddCardToDeck(War);
        if (minister.Suite == MinisterSuite.Money)
            DeckManager.AddCardToDeck(BuyRevolts1);
        if (minister.Suite == MinisterSuite.Mood)
            DeckManager.AddCardToDeck(SpyStart);
    }
    
    public static readonly TaskCardData War = new TaskCardData()
    {
        Name = "War declared!",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(Battle1);
            DeckManager.AddCardToDeck(BattleImportant);
        },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToDeck(Battle1);
        },
        LevelRequirement = 6,
        DrawMode = CardDrawMode.Important,
    };
    public static readonly TaskCardData Battle1 = new TaskCardData()
    {
        Name = "Battle",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(Battle1);
            DeckManager.AddCardToDeck(BattleImportant);
        },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToDeck(Battle2);
            Battle1.ResetPriority();
        },
        LevelRequirement = 7,
        DrawMode = CardDrawMode.DestroyOnFinish,
        GrowPriorityEveryTurn = true,
        PriorityChange = GameSettings.ChainEventPriorityGrowth,
    };
    public static readonly TaskCardData BattleImportant = new TaskCardData()
    {
        Name = "Town is under siege",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 2,
        CallbackLose = (_) =>
        {
            EmpireController.ChangeStability(-1);
            DeckManager.AddCardToDeck(WarRefugees);
        },
        CallbackWin = (_) =>
        {
        },
        LevelRequirement = 5,
        DrawMode = CardDrawMode.Important,
    };
    public static readonly TaskCardData WarRefugees = new TaskCardData()
    {
        Name = "War refugees",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 4,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(Famine2);
        },
        CallbackWin = (_) =>
        { },
        LevelRequirement = 6,
        DrawMode = CardDrawMode.ReturnToDeck,
    };
    public static readonly TaskCardData Battle2 = new TaskCardData()
    {
        Name = "Surround enemy army",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(Battle1);
            DeckManager.AddCardToDeck(RogueMercenaries);
            DeckManager.AddCardToDeck(RoadBandits);
            DeckManager.AddCardToDeck(WarRefugees2);
        },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToDeck(Battle3);
            Battle2.ResetPriority();
        },
        LevelRequirement = 8,
        DrawMode = CardDrawMode.DestroyOnFinish,
        GrowPriorityEveryTurn = true,
        PriorityChange = GameSettings.ChainEventPriorityGrowth,
    };
    public static readonly TaskCardData WarRefugees2 = new TaskCardData()
    {
        Name = "War refugees",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 2,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(BigFamine);
        },
        CallbackWin = (_) => { },
        LevelRequirement = 8,
        DrawMode = CardDrawMode.ReturnToDeck,
    };
    public static readonly TaskCardData Battle3 = new TaskCardData()
    {
        Name = "Storm the capital",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(Battle1);
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.AddWinPoint();
            EmpireController.ChangeStability(3);
            Battle3.ResetPriority();
            DeckManager.AddCardToDeck(WarRefugees2);
        },
        LevelRequirement = 9,
        DrawMode = CardDrawMode.DestroyOnFinish,
        WinPoint = true,
        GrowPriorityEveryTurn = true,
        PriorityChange = GameSettings.ChainFinalEventPriorityGrowth,
    };
    
    public static readonly TaskCardData BuyRevolts1 = new TaskCardData()
    {
        Name = "Supply foreign revolts",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 3,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(BuyRevoltsRepeat);
            DeckManager.AddCardToDeck(HuntSpies);
        },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToDeck(BuyRevolts2);
            DeckManager.AddCardToDeck(BuyRevolts2);
            DeckManager.AddCardToDeck(BuyRevolts3);
        },
        LevelRequirement = 7,
        DrawMode = CardDrawMode.Important,
    };
    public static readonly TaskCardData HuntSpies = new TaskCardData()
    {
        Name = "Counter foreign spies",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(HuntSpies);
        },
        CallbackWin = (_) =>
        { },
        LevelRequirement = 8,
        DrawMode = CardDrawMode.ReturnToDeck,
    };
    public static readonly TaskCardData BuyRevoltsRepeat = new TaskCardData()
    {
        Name = "Supply foreign revolts",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 3,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(BuyRevoltsRepeat);
            DeckManager.AddCardToDeck(Revolt2);
            DeckManager.AddCardToDeck(HuntSpies);
        },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToDeck(BuyRevolts3);
            BuyRevoltsRepeat.ResetPriority();
        },
        LevelRequirement = 7,
        DrawMode = CardDrawMode.DestroyOnFinish,
        GrowPriorityEveryTurn = true,
        PriorityChange = GameSettings.ChainEventPriorityGrowth,
    };
    
    public static readonly TaskCardData BuyRevolts2 = new TaskCardData()
    {
        Name = "Smuggle troops",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 2,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(HuntSpies2);
        },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToDeck(HuntSpies);
        },
        LevelRequirement = 6,
        DrawMode = CardDrawMode.DestroyOnFinish,
    };
    public static readonly TaskCardData HuntSpies2 = new TaskCardData()
    {
        Name = "Distribute propaganda",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 4,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(BrewingMasses);
        },
        CallbackWin = (_) =>
            { },
        LevelRequirement = 8,
        DrawMode = CardDrawMode.ReturnToDeck,
    };
    public static readonly TaskCardData BuyRevolts3 = new TaskCardData()
    {
        Name = "Bribe revolt leaders",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 4,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(BuyRevolts3);
            BuyRevolts3.Priority = 0;
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.AddWinPoint();
            EmpireController.ChangeStability(3);
            BuyRevolts3.ResetPriority();
            DeckManager.AddCardToDeck(HuntSpies2);
            DeckManager.AddCardToDeck(AfterBuyRevolts);
        },
        LevelRequirement = 10,
        DrawMode = CardDrawMode.DestroyOnFinish,
        WinPoint = true,
        GrowPriorityEveryTurn = true,
        PriorityChange = 1,
    };
    public static readonly TaskCardData AfterBuyRevolts = new TaskCardData()
    {
        Name = "Dispose of revolt leaders",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 2,
        CallbackLose = (_) =>
        {
            AfterBuyRevolts.ResetPriority();
        },
        CallbackWin = (_) =>
        {
            AfterBuyRevolts.Priority = 0;
            DeckManager.RemoveCardFromDeck(AfterBuyRevolts);
        },
        LevelRequirement = 12,
        GrowPriorityEveryTurn = true,
        DrawMode = CardDrawMode.ReturnToDeck,
    };
    
    public static readonly TaskCardData SpyStart = new TaskCardData()
    {
        Name = "Build spy network",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 4,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(Spy1);
        },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToDeck(Assassination);
            DeckManager.AddCardToDeck(BribeSpy);
        },
        LevelRequirement = 6,
        DrawMode = CardDrawMode.Important,
    };
    public static readonly TaskCardData BribeSpy = new TaskCardData()
    {
        Name = "Bribe foreign minister",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 2,
        CallbackLose = (_) =>
        { },
        CallbackWin = (_) =>
            { },
        LevelRequirement = 7,
        DrawMode = CardDrawMode.ReturnToDeck,
    };
    public static readonly TaskCardData Spy1 = new TaskCardData()
    {
        Name = "Build spy network",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 4,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(Spy1);
        },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToDeck(Assassination);
            DeckManager.AddCardToDeck(BribeSpy);
            Spy1.ResetPriority();
        },
        LevelRequirement = 6,
        DrawMode = CardDrawMode.DestroyOnFinish,
        GrowPriorityEveryTurn = true,
        PriorityChange = GameSettings.ChainEventPriorityGrowth,
    };
    public static readonly TaskCardData BribeWitness = new TaskCardData()
    {
        Name = "Bribe assassination witness",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            EmpireController.ChangeStability(-1);
            BribeWitness.ResetPriority();
        },
        CallbackWin = (_) =>
        {
            BribeWitness.ResetPriority();
        },
        LevelRequirement = 10,
        GrowPriorityEveryTurn = true,
        DrawMode = CardDrawMode.ReturnToDeck,
    };
    public static readonly TaskCardData SilenceWitness = new TaskCardData()
    {
        Name = "Silence assassination witness",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            EmpireController.ChangeStability(-1);
            SilenceWitness.ResetPriority();
        },
        CallbackWin = (_) =>
        {
            SilenceWitness.ResetPriority();
        },
        LevelRequirement = 10,
        GrowPriorityEveryTurn = true,
        DrawMode = CardDrawMode.ReturnToDeck,
    };
    public static readonly TaskCardData Assassination = new TaskCardData()
    {
        Name = "Assassinate foreign leader",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 4,
        CallbackLose = (minister) =>
        {
            DeckManager.AddCardToDeck(Spy1);
            if (minister != null)
            {
                DeckManager.AddCardToDeck(BribeWitness);
                DeckManager.AddCardToDeck(SilenceWitness);
            }
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.AddWinPoint();
            EmpireController.ChangeStability(3);
            Assassination.ResetPriority();
            DeckManager.AddCardToDeck(AfterAssassination);
        },
        LevelRequirement = 12,
        DrawMode = CardDrawMode.DestroyOnFinish,
        WinPoint = true,
        GrowPriorityEveryTurn = true,
        PriorityChange = GameSettings.ChainFinalEventPriorityGrowth,
    };
    public static readonly TaskCardData AfterAssassination = new TaskCardData()
    {
        Name = "Police annexed territories",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 4,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(Revolt3);
            DeckManager.AddCardToDeck(Revolt4Important);
            AfterAssassination.ResetPriority();
        },
        CallbackWin = (_) =>
        {
            AfterAssassination.ResetPriority();
        },
        LevelRequirement = 12,
        GrowPriorityEveryTurn = true,
        DrawMode = CardDrawMode.ReturnToDeck,
    };
    
    //TUTORIAL
    public static readonly TaskCardData Tutorial1 = new TaskCardData()
    {
        Name = "Wear ceremonial sword",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (_) => { },
        CallbackWin = (_) => { },
        LevelRequirement = 1,
        DrawMode = CardDrawMode.DestroyOnFinish,
    };
    public static readonly TaskCardData Tutorial2 = new TaskCardData()
    {
        Name = "Inspect treasury",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 2,
        CallbackLose = (_) => { },
        CallbackWin = (_) => { },
        LevelRequirement = 1,
        DrawMode = CardDrawMode.DestroyOnFinish,
    };
    public static readonly TaskCardData Tutorial3 = new TaskCardData()
    {
        Name = "Meet nobles",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 3,
        CallbackLose = (_) => { },
        CallbackWin = (_) => { },
        LevelRequirement = 1,
        DrawMode = CardDrawMode.DestroyOnFinish,
    };
}
