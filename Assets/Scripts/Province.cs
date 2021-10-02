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

    private readonly List<TaskCard> _tasks = new List<TaskCard>();
    private int _currentIdleTime;

    private void Awake()
    {
        GameController.Instance.OnTurnAdvanced.AddListener(OnNewTurn);
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
            AddCard(taskData, true);
            return;
        }

        if (_currentIdleTime == 0)
        {
            taskData = DeckManager.Instance.Get();
            AddCard(taskData);
            _currentIdleTime = Random.Range(_maxIdleTime, _maxIdleTime);
        }
    }

    private void AddCard(TaskCardData data, bool top = false)
    {
        var card = Instantiate(_cardPrefab, this.transform);

        if (top)
        {
            card.transform.SetAsLastSibling();
            _tasks.Insert(0, card);
        }
        else
        {
            card.transform.SetAsFirstSibling();
            _tasks.Add(card);
        }

        card.Initialize(data);
        
        card.OnTaskFailed.AddListener(() => FailedTask(card));
        card.OnCardDestroyed.AddListener(() => _tasks.Remove(card));
    }

    private void FailedTask(TaskCard task)
    {
        if (task != _tasks[0])
        {
            EmpireController.ChangeStability(-1);
        }
    }
}
