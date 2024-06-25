using System.Collections.Generic;
using UnityEngine;

namespace Marsminerwa
{
    public class AndOrConnection : MonoBehaviour
    {
        public enum AndOrGateType { And, Or }
        
        [SerializeField]
        private AndOrGateType gateType = AndOrGateType.And;
        
        [SerializeField]
        private List<Connection> inputs;

        private void OnEnable()
        {
            foreach (var input in inputs)
            {
                input.OnChange += OnInputChange;
            }
        }
        
        private void OnDisable()
        {
            foreach (Connection input in inputs)
            {
                input.OnChange -= OnInputChange;
            }
        }

        private void OnInputChange(Connection source)
        {
            bool result = (gateType == AndOrGateType.And);
            foreach (Connection input in inputs)
            {
                if (gateType == AndOrGateType.And)
                    result &= input.State;
                else
                    result |= input.State;
            }
            GetComponent<Connection>().State = result;
        }

        private void OnDrawGizmos()
        {
            if (inputs is null) return;
            foreach (Connection input in inputs)
            {
                Gizmos.color = input.State ? Color.green : Color.red;
                GizmosExtras.Arrow(input.transform.position, transform.position, 0.5f);
            }
        }
    }
}