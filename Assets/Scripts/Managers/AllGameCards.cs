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
        Description = "Train army description (need 100 ppl)",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 2,
        CallbackLose = (_) =>
        {
            EmpireController.Instance.Army -= 50;
            EmpireController.Instance.Mood -= 1;
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.Army = (int)(1.2f * EmpireController.Instance.Army);
        },
        LevelRequirement = 2,
        ResourceSpawnConditions = () => EmpireController.Instance.Army > 100,
    };
    public static TaskCardData ForceRecruit = new TaskCardData()
    {
        Name = "Recruit villagers",
        Description = "Recruit villagers description (<100 ppl)",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 3,
        CallbackLose = (minister) =>
        {
            if (minister != null)
            {
                EmpireController.Instance.Army -= 10;
                EmpireController.Instance.Mood -= 10;
                //TODO can add some card
            }
            EmpireController.Instance.Mood -= 5;
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.Army += 50;
            EmpireController.Instance.Mood -= 10;
        },
        LevelRequirement = 1,
        ResourceSpawnConditions = () => EmpireController.Instance.Army < 75,
    };
    public static TaskCardData MinisterCombat = new TaskCardData()
    {
        Name = "Duel",
        Description = "Duel description",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (minister) =>
        {
            if (minister != null)
            {
                minister.ChangeBoredom(-5);
            }
            else
            {
                EmpireController.Instance.ArmyMinister.LoseLevel();
            }
            EmpireController.Instance.Mood -= 5;
        },
        CallbackWin = (minister) =>
        {
            minister.ChangeBoredom(-5);
            minister.GainLevel();
        },
        LevelRequirement = 5,
        ResourceSpawnConditions = () => EmpireController.Instance.ArmyMinister != null && 
                                        EmpireController.Instance.ArmyMinister.Level >= 4,
    };
    public static TaskCardData Revolt = new TaskCardData()
    {
        Name = "Revolt",
        Description = "Small peasant revolt, 10 ppl to suppress",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            EmpireController.Instance.Army -= 5;
            EmpireController.Instance.Mood -= 5;
            DeckManager.AddCardToGlobalDeck(Revolt2);
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.Army -= 10;
        },
        LevelRequirement = 1,
        ResourceSpawnConditions = () => EmpireController.Instance.Mood < 30,
        ResourceWinConditions = () => EmpireController.Instance.Army >= 10
    };
    public static TaskCardData RevoltImportant = new TaskCardData()
    {
        Name = "Revolt",
        Description = "Small peasant revolt, 10 ppl to suppress",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            EmpireController.Instance.Army -= 5;
            EmpireController.Instance.Mood -= 5;
            DeckManager.AddCardToGlobalDeck(Revolt2);
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.Army -= 10;
        },
        LevelRequirement = 1,
        Common = false,
        Important = true,
        DestroyOnFinish = true,
        ResourceWinConditions = () => EmpireController.Instance.Army >= 10
    };
    public static TaskCardData Revolt2 = new TaskCardData()
    {
        Name = "Revolt",
        Description = "Peasant revolt, 25 ppl to suppress",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 2,
        CallbackLose = (_) =>
        {
            EmpireController.Instance.Army -= 10;
            EmpireController.Instance.Mood -= 10;
            DeckManager.AddCardToGlobalDeck(Revolt3);
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.Army -= 25;
        },
        LevelRequirement = 2,
        Common = false,
        ResourceSpawnConditions = () => EmpireController.Instance.Mood < 20,
        ResourceWinConditions = () => EmpireController.Instance.Army >= 25
    };
    public static TaskCardData Revolt3 = new TaskCardData()
    {
        Name = "Revolt",
        Description = "Big peasant revolt, 50 ppl to suppress",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 3,
        CallbackLose = (_) =>
        {
            EmpireController.Instance.Army -= 25;
            EmpireController.Instance.Mood -= 10;
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.Army -= 50;
            DeckManager.AddCardToGlobalDeck(Revolt2);
        },
        LevelRequirement = 4,
        Common = false,
        Important = true,
        ResourceSpawnConditions = () => EmpireController.Instance.Mood < 10,
        ResourceWinConditions = () => EmpireController.Instance.Army >= 50
    };
    public static TaskCardData Raid = new TaskCardData()
    {
        Name = "Raid",
        Description = "Raid neighbours with 100 ppl",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 3,
        CallbackLose = (minister) =>
        {
            if (minister != null)
            {
                EmpireController.Instance.Army -= 50;
                EmpireController.Instance.Mood -= 4;
            }
            EmpireController.Instance.Mood -= 1;
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.Army -= 25;
            EmpireController.Instance.Gold += 100;
            EmpireController.Instance.Mood += 10;    //FIXME ?
        },
        LevelRequirement = 3,
        ResourceSpawnConditions = () => EmpireController.Instance.Army >= 100,
    };
    public static TaskCardData RoadBanditsImportant = new TaskCardData()
    {
        Name = "Bandits",
        Description = "Bandits roam the roads",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            EmpireController.Instance.Gold -= 50;
            EmpireController.Instance.Mood -= 5;
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.Army -= 10;
        },
        LevelRequirement = 2,
        Common = false,
        Important = true,
        DestroyOnFinish = true,
        ResourceWinConditions = () => EmpireController.Instance.Army >= 10,
    };
    public static TaskCardData RoadBandits = new TaskCardData()
    {
        Name = "Bandits",
        Description = "Bandits roam the roads",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            EmpireController.Instance.Gold -= 50;
            EmpireController.Instance.Mood -= 5;
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.Army -= 10;
        },
        LevelRequirement = 2,
        Common = false,
        ResourceWinConditions = () => EmpireController.Instance.Army >= 10,
    };
    public static TaskCardData RogueMercenaries = new TaskCardData()
    {
        Name = "Mercenaries went rogue",
        Description = "Bandits roam the roads",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            EmpireController.Instance.Mood -= 10;
            DeckManager.AddCardToGlobalDeck(RoadBandits);
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.Army -= 75;
        },
        LevelRequirement = 4,
        Common = false,
        Important = true,
        DestroyOnFinish = true,
        ResourceWinConditions = () => EmpireController.Instance.Army >= 75,
    };
    public static TaskCardData SatanCult = new TaskCardData()
    {
        Name = "SatanCult",
        Description = "Massacre",
        SuiteRequirement = MinisterSuite.Army,
        TurnsToSolve = 2,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(SatanCult);
            EmpireController.Instance.Mood -= 10;
            EmpireController.Instance.Army -= 75;
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.Mood -= 10;
            EmpireController.Instance.Army -= 50;
        },
        Common = false,
        DestroyOnFinish = true,
        LevelRequirement = 6,
        ResourceWinConditions = () => EmpireController.Instance.Army >= 75,
    };
    //TODO monster chain
    
    //MONEY
    public static TaskCardData CollectTaxes = new TaskCardData()
    {
        Name = "Taxes",
        Description = "Collect taxes",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 2,
        CallbackLose = (minister) =>
        {
            if (minister != null)
                EmpireController.Instance.Mood -= 10;
            else
                DeckManager.AddCardToGlobalDeck(TaxEvaders);
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.Gold += 50;
        },
        LevelRequirement = 1,
    };
    public static TaskCardData TaxEvaders = new TaskCardData()
    {
        Name = "Tax evaders",
        Description = "Tax evaders desc",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 2,
        CallbackLose = (minister) =>
        {
            DeckManager.AddCardToGlobalDeck(TaxEvaders);
            DeckManager.AddCardToGlobalDeck(Revolt2);
            EmpireController.Instance.Mood -= 5;
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.Mood -= 10;
            EmpireController.Instance.Gold += 75;
        },
        Common = false,
        DestroyOnFinish = true,
        LevelRequirement = 3,
    };
    public static TaskCardData TradeRoute = new TaskCardData()
    {
        Name = "Trade route",
        Description = "Establish new trade route",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 2,
        CallbackLose = (minister) =>
        {
            if (minister != null)
            {
                EmpireController.Instance.Gold -= 100;
            }
            DeckManager.AddCardToGlobalDeck(RoadBanditsImportant);
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.Gold -= 100;
            DeckManager.AddCardToGlobalDeck(TradeDeal);
        },
        LevelRequirement = 3,
        ResourceWinConditions = () => EmpireController.Instance.Gold >= 100,
    };
    public static TaskCardData TradeDeal = new TaskCardData()
    {
        Name = "Trade deal",
        Description = "New trade opportunity",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 3,
        CallbackLose = (minister) =>
        {
            if (minister)
            {
                EmpireController.Instance.Gold -= 100;
            }
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.Gold += 200;
        },
        Common = false,
        DestroyOnFinish = true,
        LevelRequirement = 4,
    };
    public static TaskCardData MintCoins = new TaskCardData()
    {
        Name = "Mint coins",
        Description = "Add more copper to gold coins",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 3,
        CallbackLose = (minister) =>
        {
            if (minister != null)
            {
                EmpireController.Instance.Mood -= 15;
                EmpireController.Instance.Gold += 50;
            }
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.Mood -= 10;
            EmpireController.Instance.Gold += 100;
        },
        LevelRequirement = 2,
    };
    public static TaskCardData Mercenaries = new TaskCardData()
    {
        Name = "Hire mercenaries",
        Description = "Hire mercenaries",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 3,
        CallbackLose = (minister) =>
        {
            DeckManager.AddCardToGlobalDeck(RogueMercenaries);
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.Gold -= 100;
            EmpireController.Instance.Army += 100;
        },
        LevelRequirement = 4,
        ResourceWinConditions = () => EmpireController.Instance.Gold >= 100,
    };
    public static TaskCardData BuyCropsFamine = new TaskCardData()
    {
        Name = "Buy crops",
        Description = "Cure famine",
        SuiteRequirement = MinisterSuite.Money,
        TurnsToSolve = 3,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(Hunger);
            DeckManager.AddCardToGlobalDeck(Famine);
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.Mood += 10;
            EmpireController.Instance.Gold -= 50;
        },
        LevelRequirement = 3,
        Important = true,
        DestroyOnFinish = true,
        ResourceWinConditions = () => EmpireController.Instance.Gold >= 50,
    };
    //TODO Smugglers
    
    //MOOD
    public static TaskCardData Hunger = new TaskCardData()
    {
        Name = "Hunger",
        Description = "Hunger desc",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 3,
        CallbackLose = (_) =>
        {
            EmpireController.Instance.Mood -= 5;
            DeckManager.AddCardToGlobalDeck(Famine);
            DeckManager.AddCardToGlobalDeck(Revolt);    //? RevoltImportant
        },
        CallbackWin = (_) =>
        {
            
        },
        LevelRequirement = 1,
    };
    public static TaskCardData Famine = new TaskCardData()
    {
        Name = "Famine",
        Description = "Famine desc",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 2,
        CallbackLose = (_) =>
        {
            EmpireController.Instance.Mood -= 10;
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
        Description = "Big famine desc",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 1,
        CallbackLose = (_) =>
        {
            EmpireController.Instance.Mood -= 10;
            DeckManager.AddCardToGlobalDeck(Revolt3);
            DeckManager.AddCardToGlobalDeck(Famine);
        },
        CallbackWin = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(Famine);
            EmpireController.Instance.Gold -= 100;
        },
        Common = false,
        Important = true,
        DestroyOnFinish = true,
        LevelRequirement = 5,
        ResourceWinConditions = () => EmpireController.Instance.Gold >= 100,
    };
    public static TaskCardData BrewingMasses = new TaskCardData()
    {
        Name = "BrewingMasses",
        Description = "Protests, etc",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 3,
        CallbackLose = (_) =>
        {
            DeckManager.AddCardToGlobalDeck(Revolt);
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.Mood -= 5;
        },
        Common = false,
        LevelRequirement = 3,
        ResourceSpawnConditions = () => EmpireController.Instance.Mood < 50,
    };
    public static TaskCardData FreeSpeech = new TaskCardData()
    {
        Name = "FreeSpeech",
        Description = "Desc",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 4,
        CallbackLose = (_) =>
        {
            EmpireController.Instance.Mood += 10;
            DeckManager.AddCardToGlobalDeck(BrewingMasses);
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.Mood -= 5;
        },
        Common = false,
        LevelRequirement = 1,
        ResourceSpawnConditions = () => EmpireController.Instance.Mood < 50,
    };
    public static TaskCardData ReligionPush = new TaskCardData()
    {
        Name = "Religion",
        Description = "",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 3,
        CallbackLose = (_) =>
        {
            EmpireController.Instance.Mood += 5;
            DeckManager.AddCardToGlobalDeck(PaganCults);
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.Mood -= 5;
        },
        LevelRequirement = 2,
    };
    public static TaskCardData PaganCults = new TaskCardData()
    {
        Name = "PaganCults",
        Description = "",
        SuiteRequirement = MinisterSuite.Mood,
        TurnsToSolve = 2,
        CallbackLose = (minister) =>
        {
            EmpireController.Instance.Mood += 10;
            DeckManager.AddCardToGlobalDeck(PaganCults);
            DeckManager.AddCardToGlobalDeck(SatanCult);
        },
        CallbackWin = (_) =>
        {
            EmpireController.Instance.Mood -= 10;
        },
        Common = false,
        LevelRequirement = 4,
    };
    
}
