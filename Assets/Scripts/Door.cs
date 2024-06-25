using UnityEngine;

namespace Marsminerwa
{
    [RequireComponent(typeof(Connection))]
    public class Door : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<Connection>().OnChange += OnChange;
        }
        
        private void OnDisable()
        {
            GetComponent<Connection>().OnChange -= OnChange;
        }
        
        private void OnChange(Connection source)
        {
            GetComponent<SpriteRenderer>().enabled = !source.State;
            GetComponent<BoxCollider2D>().enabled = !source.State;
        }
    }
}