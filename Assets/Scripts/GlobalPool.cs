using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Pool;

namespace Marsminerwa
{
    public abstract class GlobalPool : MonoBehaviour
    {
        [SerializeField]
        private int defaultPoolCapacity = 10;
        [SerializeField]
        private int maxPoolSize = 100;
        [SerializeField]
        private GameObject prefab;
        
        protected int DefaultPoolCapacity => defaultPoolCapacity;
        protected int MaxPoolSize => maxPoolSize;
        protected GameObject Prefab => prefab;
        
        private ObjectPool<GameObject> pool;

        public GameObject Get([CanBeNull] Action<GameObject> beforeTakeFromPoolEventAction = null)
        {
            GameObject go = pool.Get();
            PooledObjectController poolObjController = go.GetComponent<PooledObjectController>();
            if (poolObjController is null)
            {
                poolObjController = go.AddComponent<PooledObjectController>();
            }
            
#pragma warning disable 0618
            poolObjController.SetParentPool(this);
#pragma warning restore 0618

            if (beforeTakeFromPoolEventAction is not null)
                beforeTakeFromPoolEventAction(go);

            foreach (PoolEventListener listener in go.GetComponents<PoolEventListener>())
                listener.OnTookFromPool();
            
            return go;
        }
        
        public void Release(GameObject go)
        {
            pool.Release(go);
        }
        
        protected void InitPool()
        {
            pool = new ObjectPool<GameObject>(
                CreateGameObject, ActionOnGet, ActionOnRelease, ActionOnDestroy,
                true, defaultPoolCapacity, maxPoolSize);
        }

        protected void DisposePool()
        {
            pool.Dispose();
            pool = null;
        }

        protected virtual GameObject CreateGameObject()
        {
            return Instantiate(prefab);
        }

        protected virtual void ActionOnGet(GameObject go)
        {
            go.SetActive(true);
        }

        protected virtual void ActionOnRelease(GameObject go)
        {
            go.SetActive(false);
        }
        
        protected virtual void ActionOnDestroy(GameObject go)
        {
            if(!go) return;
            
#pragma warning disable 0618
            go.GetComponent<PooledObjectController>().SetParentPool(null);
#pragma warning restore 0618
            Destroy(go);
        }
    }
}