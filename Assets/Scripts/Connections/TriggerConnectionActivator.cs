using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Marsminerwa
{
    [RequireComponent(typeof(Connection))]
    public class TriggerConnectionActivator : MonoBehaviour
    {
        private HashSet<Collider2D> colliders = new HashSet<Collider2D>();

        public List<GameObject> go = new List<GameObject>();
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            colliders.Add(col);
            go = colliders.Select(c => c.GameObject()).ToList();
            
            GetComponent<Connection>().State = true;
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            colliders.Remove(col);
            go = colliders.Select(c => c.GameObject()).ToList();
            
            if (colliders.Count == 0)
                GetComponent<Connection>().State = false;
        }
    }
}