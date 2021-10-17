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
    public static readonly TaskCardData Revolt = new TaskCardData()
    {
        Name = "Revolt",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(Revolt2);
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
            DeckManager.AddCardToDeck(Revolt2);
        },
        CallbackWin = (_) =>
        {},
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
            DeckManager.AddCardToDeck(Revolt3);
        },
        CallbackWin = (_) =>
        {},
        LevelRequirement = 2,
        DrawMode = CardDrawMode.ReturnToDeck,
    };
    public static readonly TaskCardData Revolt3 = new TaskCardData()
    {
        Name = "Revolt",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 3,
        CallbackLose = (_) =>
        {},
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToDeck(Revolt2);
        },
        LevelRequirement = 4,
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
    public static readonly TaskCardData RogueMercenaries = new TaskCardData()
    {
        Name = "Rogue mercenaries",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(RoadBandits);
        },
        CallbackWin = (_) =>
        { },
        LevelRequirement = 4,
        DrawMode = CardDrawMode.Important,
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
        },
        DrawMode = CardDrawMode.DestroyOnFinish,
        LevelRequirement = 6,
    };
    //TODO monster chain
    
    //MONEY
    public static readonly TaskCardData CollectTaxes = new TaskCardData()
    {
        Name = "Collect taxes",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 2,
        CallbackLose = (minister) =>
        {
            DeckManager.AddCardToDeck(TaxEvaders);
        },
        CallbackWin = (_) =>
        { },
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
        { },
        DrawMode = CardDrawMode.DestroyOnFinish,
        LevelRequirement = 3,
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
        TurnsToSolve = 3,
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
        TurnsToSolve = 3,
        CallbackLose = (minister) =>
        {
            DeckManager.AddCardToDeck(RogueMercenaries);
        },
        CallbackWin = (_) =>
        { },
        LevelRequirement = 4,
    };
    public static readonly TaskCardData BuyCropsFamine = new TaskCardData()
    {
        Name = "Import more crops",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 3,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(Hunger);
            DeckManager.AddCardToDeck(Famine);
        },
        CallbackWin = (_) =>
        { },
        LevelRequirement = 3,
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
        { },
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
        },
        DrawMode = CardDrawMode.DestroyOnFinish,
        LevelRequirement = 3,
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
        },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToDeck(Famine);
        },
        DrawMode = CardDrawMode.Important,
        LevelRequirement = 5,
    };
    public static readonly TaskCardData Feud = new TaskCardData()
    {
        Name = "Reconcile a feud",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {},
        CallbackWin = (_) => { },
        LevelRequirement = 2,
    };
    public static readonly TaskCardData BrewingMasses = new TaskCardData()
    {
        Name = "Quell civil unrest",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 3,
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
        {
            DeckManager.AddCardToDeck(PaganCults);
        },
        CallbackWin = (_) =>
        { },
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
            DeckManager.AddCardToDeck(SatanCult);
        },
        CallbackWin = (_) =>
        { },
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
        },
        CallbackWin = (_) =>
        {
        },
        LevelRequirement = 5,
        DrawMode = CardDrawMode.Important,
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
    public static readonly TaskCardData BuyRevoltsRepeat = new TaskCardData()
    {
        Name = "Supply foreign revolts",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 3,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(BuyRevoltsRepeat);
            DeckManager.AddCardToDeck(Revolt2);
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
        { },
        CallbackWin = (_) =>
        { },
        LevelRequirement = 6,
        DrawMode = CardDrawMode.DestroyOnFinish,
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
        },
        LevelRequirement = 10,
        DrawMode = CardDrawMode.DestroyOnFinish,
        WinPoint = true,
        GrowPriorityEveryTurn = true,
        PriorityChange = 1,
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
        },
        LevelRequirement = 6,
        DrawMode = CardDrawMode.Important,
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
            Spy1.ResetPriority();
        },
        LevelRequirement = 6,
        DrawMode = CardDrawMode.DestroyOnFinish,
        GrowPriorityEveryTurn = true,
        PriorityChange = GameSettings.ChainEventPriorityGrowth,
    };
    public static readonly TaskCardData Assassination = new TaskCardData()
    {
        Name = "Assassinate foreign leader",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 4,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToDeck(Spy1);
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.AddWinPoint();
            EmpireController.ChangeStability(3);
            Assassination.ResetPriority();
        },
        LevelRequirement = 12,
        DrawMode = CardDrawMode.DestroyOnFinish,
        WinPoint = true,
        GrowPriorityEveryTurn = true,
        PriorityChange = GameSettings.ChainFinalEventPriorityGrowth,
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
