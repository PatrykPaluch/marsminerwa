using UnityEngine;

namespace Marsminerwa
{
    [RequireComponent(typeof(Connection))]
    public class Lever : MonoBehaviour, Interactable
    {
        [SerializeField]
        private Sprite spriteLeft;
        [SerializeField]
        private Sprite spriteRight;

        [SerializeField]
        private bool direction;
        
        private void Start()
        {
            GetComponent<SpriteRenderer>().sprite = direction ? spriteRight : spriteLeft;
        }
        
        public void Interact(Direction interactionDirection)
        {
            if(interactionDirection == Direction.Right && !direction)
                direction = true;
            if(interactionDirection == Direction.Left && direction)
                direction = false;
            
            GetComponent<SpriteRenderer>().sprite = direction ? spriteRight : spriteLeft;
            GetComponent<Connection>().State = direction;
        }
    }
}