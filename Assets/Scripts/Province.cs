using System.Collections.Generic;
using UnityEngine;

public class Province : MonoBehaviour
{
    public bool Empty => _tasks.Count == 0;
    public bool Full => _tasks.Count >= _depth;
    
    public TaskCardData FirstTask => _tasks[0].GetData();

    [SerializeField] private TaskCard _cardPrefab;
    [SerializeField] private int _depth = 1;
    [SerializeField] private int _minIdleTime;
    [SerializeField] private int _maxIdleTime;
    
    [SerializeField] private TaskCard _blockerCard;

    private readonly List<TaskCard> _tasks = new List<TaskCard>();
    private int _currentIdleTime;

    private void Awake()
    {
        if (_blockerCard != null)
        {
            _blockerCard.OnCardDestroyed.AddListener(
                () => GameController.Instance.OnTurnAdvanced.AddListener(OnNewTurn)
            );
        }
        else
        {
            GameController.Instance.OnTurnAdvanced.AddListener(OnNewTurn);
        }

    }

    private void OnNewTurn()
    {
        if (_currentIdleTime > 0)
            _currentIdleTime -= 1;
        
        if (Full) return;

        var taskData = DeckManager.Instance.GetImportant();
        if (taskData != null && 
            (Empty || !FirstTask.Important))
        {
            AddCard(taskData);
            return;
        }

        if (_currentIdleTime == 0)
        {
            taskData = DeckManager.Instance.Get();
            AddCard(taskData);
            _currentIdleTime = Random.Range(_maxIdleTime, _maxIdleTime);
        }
    }

    private void AddCard(TaskCardData data)
    {
        var card = Instantiate(_cardPrefab, this.transform);
        
        card.transform.SetAsLastSibling();
        _tasks.Insert(0, card);

        card.Initialize(data);

        card.OnCardDestroyed.AddListener(() => _tasks.Remove(card));
    }

}
