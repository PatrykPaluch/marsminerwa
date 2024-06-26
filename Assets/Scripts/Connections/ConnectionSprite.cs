using UnityEngine;

namespace Marsminerwa
{
    [RequireComponent(typeof(Connection))]
    public class ConnectionSprite : MonoBehaviour
    {
        
        [SerializeField]
        private Sprite spriteDisabled;
        [SerializeField]
        private Sprite spriteEnabled;
        
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
            GetComponent<SpriteRenderer>().sprite = source.State ? spriteEnabled : spriteDisabled;
        }
    }
}