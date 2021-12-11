using UnityEngine;

namespace Power.Factory
{
    public interface IFactory<T>
    {
        T Create();
    }

    public class ComponentFactory<T> : IFactory<T> where T : Component
    {
        private T _component;

        public ComponentFactory(T component)
        {
            _component = component;
        }

        public T Create()
        {
            return Object.Instantiate(_component);
        }
    }
}
