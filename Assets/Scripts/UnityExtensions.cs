using System;
using System.Collections;
using UnityEngine;

namespace Marsminerwa
{
    public static class UnityExtensions
    {
        /// <summary>
        /// Before destroying the GameObject, checks if it is managed by a pool and releases it from the pool instead.
        /// </summary>
        public static void SafeDestroy(this GameObject go)
        {
            if (go is null) return;
            PooledObjectController poolObjController = go.GetComponent<PooledObjectController>();
            if (poolObjController)
            {
                poolObjController.ReleaseFromPool();
            }
            else
            {
                UnityEngine.Object.Destroy(go);
            }
        }
        
        /// <summary>
        /// Sets the position of the transform in 2D space without changing the z coordinate.
        /// </summary>
        public static void SetPosition2D(this Transform transform, Vector2 position)
        {
            transform.position = new Vector3(position.x, position.y, transform.position.z);
        }

        public static Vector2 TransformDirection2D(this Transform transform, Vector2 direction)
        {
            return transform.TransformDirection(direction);
        }
        
        public static void CallAfter(this MonoBehaviour mb, float time, Action action)
        {
            mb.StartCoroutine(CallAfterCoroutine(time, action));
        }
        
        private static IEnumerator CallAfterCoroutine(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action();
        }
    }
}