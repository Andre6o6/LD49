using System.Collections.Generic;
using Power.Factory;
using UnityEngine;

namespace Utils.Pool
{
    public abstract class PoolSO<T> : ScriptableObject, IPool<T>
    {
        protected readonly Queue<T> Available = new Queue<T>();
        protected abstract IFactory<T> Factory { get; set; }
        protected bool HasBeenPrewarmed { get; set; }
        
        public virtual void OnDisable()
        {
            Available.Clear();
            HasBeenPrewarmed = false;
        }
        
        protected virtual T Create()
        {
            return Factory.Create();
        }
        
        public virtual void Prewarm(int num)
        {
            if (HasBeenPrewarmed)
            {
                Debug.LogWarning($"Pool {name} has already been prewarmed.");
                return;
            }
            for (int i = 0; i < num; i++)
            {
                Available.Enqueue(Create());
            }
            HasBeenPrewarmed = true;
        }

        public virtual T Get()
        {
            return Available.Count > 0 ? Available.Dequeue() : Create();
        }
        
        public virtual IEnumerable<T> Get(int num)
        {
            List<T> members = new List<T>(num);
            for (int i = 0; i < num; i++)
            {
                members.Add(Get());
            }
            return members;
        }

        public virtual void Return(T member)
        {
            Available.Enqueue(member);
        }

        public virtual void Return(IEnumerable<T> members)
        {
            foreach (T member in members)
            {
                Return(member);
            }
        }
    }
}
