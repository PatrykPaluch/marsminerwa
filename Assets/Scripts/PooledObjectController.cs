using System;
using UnityEngine;

namespace Marsminerwa
{
    public class PooledObjectController : MonoBehaviour
    {
        private GlobalPool parentPool;

        public void ReleaseFromPool()
        {
            parentPool.Release(gameObject);
        }

        public static void ReleaseFromPool(GameObject target)
        {
            target.GetComponent<PooledObjectController>()?.ReleaseFromPool();
        }
        
        [Obsolete("Should not be used directly")]
        internal void SetParentPool(GlobalPool pool)
        {
            parentPool = pool;
        }
        
        private void OnDestroy()
        {
            if(!parentPool) return;
            if(!gameObject.scene.isLoaded) return;

            Debug.LogError(
                $"'{gameObject.name}' is managed by '{parentPool.name}' pool and should not be destroyed directly. " + 
                       $"Use '{parentPool.name}'s 'GlobalPool.Release(gameObject)' instead. ");
            
            parentPool.Release(gameObject);
        }
    }
}