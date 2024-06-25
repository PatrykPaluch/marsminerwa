using System;
using UnityEditor;
using UnityEngine;

namespace Marsminerwa
{
    [RequireComponent(typeof(Connection))]
    public class BufferConnection : MonoBehaviour
    {
        [SerializeField]
        private Connection input;

        private void OnEnable()
        {
            input.OnChange += OnInputChange;
        }
        
        private void OnDisable()
        {
            input.OnChange -= OnInputChange;
        }

        private void OnInputChange(Connection source)
        {
            GetComponent<Connection>().State = source.State;
        }

        private void OnDrawGizmos()
        {
            if (!input) return;
            
            Gizmos.color = input.State ? Color.green : Color.red;
            GizmosExtras.Arrow(input.transform.position, transform.position, 0.5f);
        }
    }
}