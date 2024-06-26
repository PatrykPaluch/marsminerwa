using System;
using UnityEditor;
using UnityEngine;

namespace Marsminerwa
{
    public class Connection : MonoBehaviour
    {
        public delegate void ChangeEvent(Connection source);

        public bool State
        {
            get => state;
            set
            {
                if (state == value) return;
                state = value;
                OnChange?.Invoke(this);
            }
        }
        
        public event ChangeEvent OnChange;
        
        private bool state = false;
        
        private void OnDrawGizmos()
        {
            Gizmos.color = State ? Color.green : Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }
    }
}
