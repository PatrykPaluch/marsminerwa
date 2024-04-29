using UnityEngine;

namespace Marsminerwa
{
    [DefaultExecutionOrder(ExecutionOrder.Pool)]
    public class StandardGlobalPool : GlobalPool
    {
        private void Awake()
        {
            InitPool();
        }
        
        private void OnDestroy()
        {
            DisposePool();
        }
    }
}