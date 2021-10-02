using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckManager : Singleton<DeckManager>
{
    private List<TaskCardData> _globalDeck;
    
    public static void AddCardToGlobalDeck(TaskCardData card)
    {
        if (card.Common) Debug.LogWarning($"Adding common card {card}");
        Instance._globalDeck.Add(card);
    }
    
    protected override void Awake()
    {
        base.Awake();
        
        _globalDeck = new List<TaskCardData>();
        _globalDeck.Add(AllGameCards.TrainArmy);
        _globalDeck.Add(AllGameCards.ForceRecruit);
        _globalDeck.Add(AllGameCards.MinisterCombat);
        _globalDeck.Add(AllGameCards.Revolt);
        _globalDeck.Add(AllGameCards.Raid);
        _globalDeck.Add(AllGameCards.CollectTaxes);
        _globalDeck.Add(AllGameCards.TradeRoute);
        _globalDeck.Add(AllGameCards.MintCoins);
        _globalDeck.Add(AllGameCards.Mercenaries);
        _globalDeck.Add(AllGameCards.Hunger);
        _globalDeck.Add(AllGameCards.BrewingMasses);
        _globalDeck.Add(AllGameCards.FreeSpeech);
        _globalDeck.Add(AllGameCards.ReligionPush);
    }

    public TaskCardData Get()
    {
        var nonImportantDeck = _globalDeck.Where(x => !x.Important && x.ResourceSpawnConditions.Invoke()).ToList();
        var cardData = nonImportantDeck[Random.Range(0, nonImportantDeck.Count)];
        if (!cardData.Common)
        {
            _globalDeck.Remove(cardData);
        }

        return cardData;
    }
    
    public TaskCardData GetImportant()
    {
        var cardData = _globalDeck.Find(x => x.Important);
        _globalDeck.Remove(cardData);    //Importants never common
        return cardData;
    }
}
