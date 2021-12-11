using Power.Factory;
using UnityEngine;

namespace Utils.Pool
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Pool/GameObject Pool")]
    public class GameobjectPoolSO : PoolSO<GameObject>    //GameObject doesn't inherit from Component
    {
        public GameObject Prefab;
        
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
        
        protected override IFactory<GameObject> Factory { get; set; }    //fixme: Not used

        private Transform _parent;

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

        public override GameObject Get()
        {
            GameObject member = base.Get();
            member.gameObject.SetActive(true);
            return member;
        }

        public override void Return(GameObject member)
        {
            member.transform.SetParent(PoolRoot.transform);
            member.gameObject.SetActive(false);
            base.Return(member);
        }

        protected override GameObject Create()
        {
            GameObject newMember = Instantiate(Prefab, PoolRoot.transform);
            newMember.gameObject.SetActive(false);
            return newMember;
        }
    }
}
