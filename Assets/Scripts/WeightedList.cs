using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class WeightedList<T> where T : class, IWeightedItem
{
    private List<T> _list = new List<T>();
    
    public List<T> List => _list;

    public void Add(T item)
    {
        if (!_list.Contains(item))
        {
            _list.Add(item);
        }
        else
        {
            var listed = _list.Find(x => x == item);
            listed.Priority += item.Priority;
        }
    }
    
    public void UpdatePriority(T item, float priorityDelta)
    {
        var listed = _list.Find(x => x == item);
        if (listed == null) throw new System.ArgumentException("Item not in th list");
        
        if (listed.Priority + priorityDelta < 0)
            priorityDelta = -listed.Priority;
            
        listed.Priority += priorityDelta;
    }
    
    public void Remove(T item)
    {
        if (!_list.Contains(item)) throw new System.ArgumentException($"Item not in the list");
        
        _list.Remove(item);
    }

    public bool Contains(T item) => _list.Contains(item);
    
    public int Count => _list.Count;
    
    public T GetRandom()
    {
        //TODO cache sum (but it changes when I draw uncommon card)
        float rndPriority = Random.Range(0, WeightSum);
        int i = 0;
        while (rndPriority >= _list[i].Priority)
        {
            rndPriority -= _list[i].Priority;
            i += 1;
        }
        return _list[i];
    }

    private float WeightSum => _list.Sum(item => item.Priority);
}

public interface IWeightedItem
{
    float Priority { get; set; }
}

