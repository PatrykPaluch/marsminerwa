using System.Collections.Generic;
using UnityEngine;

namespace Marsminerwa
{
    [RequireComponent(typeof(Connection))]
    public class TriggerConnectionActivator : MonoBehaviour
    {
        private HashSet<Collider2D> colliders = new HashSet<Collider2D>();
    
        private void OnTriggerEnter2D(Collider2D col)
        {
            colliders.Add(col);
            GetComponent<Connection>().State = true;
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            colliders.Remove(col);
            if (colliders.Count == 0)
                GetComponent<Connection>().State = false;
        }
    }
}