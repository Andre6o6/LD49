using Power.Factory;
using UnityEngine;

namespace Utils.Pool
{
    public abstract class ComponentPoolSO<T> : PoolSO<T> where T : Component
    {
        public T Prefab;
        
        private Transform _poolRoot;
        private Transform PoolRoot
        {
            get
            {
                if (_poolRoot == null)
                {
                    _poolRoot = new GameObject(name).transform;
                    _poolRoot.SetParent(_parent);
                }
                return _poolRoot;
            }
        }
        
        protected override IFactory<T> Factory
        {
            get => _factory;
            set => _factory = value as ComponentFactory<T>;
        }

        private Transform _parent;
        private ComponentFactory<T> _factory;
        
        private void OnEnable()
        {
            _factory = new ComponentFactory<T>(Prefab);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            if (_poolRoot != null)
            {
#if UNITY_EDITOR
                DestroyImmediate(_poolRoot.gameObject);
#else
				Destroy(_poolRoot.gameObject);
#endif
            }
        }
        
        public void SetParent(Transform t)
        {
            _parent = t;
            PoolRoot.SetParent(_parent);
        }
        
        public override T Get()
        {
            T member = base.Get();
            member.gameObject.SetActive(true);
            return member;
        }

        public override void Return(T member)
        {
            member.transform.SetParent(PoolRoot.transform);
            member.gameObject.SetActive(false);
            base.Return(member);
        }

        protected override T Create()
        {
            T newMember = base.Create();
            newMember.transform.SetParent(PoolRoot.transform);
            newMember.gameObject.SetActive(false);
            return newMember;
        }
    }
}
