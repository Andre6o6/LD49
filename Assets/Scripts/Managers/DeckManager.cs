using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckManager : Singleton<DeckManager>
{
    private WeightedList<TaskCardData> _globalDeck;
    private List<TaskCardData> _importantDeck;
    
    public static void AddCardToDeck(TaskCardData card)
    {
        if (card.Common && Instance._globalDeck.Contains(card))
        {
            Instance._globalDeck.UpdatePriority(card, card.PriorityChange);
            Debug.Log($"{card.Name}'s priority set to {card.Priority}");
            return;
        }

        if (card.Important)
        {
            Instance._importantDeck.Add(card);
        }
        else
        {
            Instance._globalDeck.Add(card);
        }
    }

    public static void RemoveCardFromDeck(TaskCardData card)
    {
        if (!Instance._globalDeck.Contains(card)) return;
        
        if (card.Common)
        {
            Instance._globalDeck.UpdatePriority(card, -card.PriorityChange);
            Debug.Log($"{card.Name}'s priority set to {card.Priority}");
        }
        else if (!card.Important)
        {
            Instance._globalDeck.Remove(card);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        
        _importantDeck = new List<TaskCardData>();
        _globalDeck = new WeightedList<TaskCardData>();
        _globalDeck.Add(AllGameCards.TrainArmy);
        _globalDeck.Add(AllGameCards.ForceRecruit);
        _globalDeck.Add(AllGameCards.MinisterCombat);
        _globalDeck.Add(AllGameCards.Revolt);
        _globalDeck.Add(AllGameCards.RoadBandits);
        _globalDeck.Add(AllGameCards.Raid);
        _globalDeck.Add(AllGameCards.CollectTaxes);
        _globalDeck.Add(AllGameCards.TradeRoute);
        _globalDeck.Add(AllGameCards.MintCoins);
        _globalDeck.Add(AllGameCards.Mercenaries);
        _globalDeck.Add(AllGameCards.Hunger);
        _globalDeck.Add(AllGameCards.BrewingMasses);
        _globalDeck.Add(AllGameCards.Feud);
        _globalDeck.Add(AllGameCards.ReligionPush);

        GameController.Instance.OnAfterTurnAdvanced.AddListener(GrowPriorityEveryTurn);
    }

    public TaskCardData Get()
    {
        var cardData = _globalDeck.GetRandom();
        if (!cardData.Common)
        {
            _globalDeck.Remove(cardData);    //TODO ???
        }

        return cardData;
    }
    
    public TaskCardData GetImportant()
    {
        if (_importantDeck.Count == 0)
            return null;
        
        var cardData = _importantDeck[0];
        _importantDeck.Remove(cardData);    //Importants never common
        return cardData;
    }

    public void GrowPriorityEveryTurn()
    {
        _globalDeck.List.ForEach(x =>
            {
                if (x.GrowPriorityEveryTurn) _globalDeck.UpdatePriority(x, x.PriorityChange);
            }
        );
    }
}
