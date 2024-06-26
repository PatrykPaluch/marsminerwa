using UnityEngine;

namespace Marsminerwa
{
    public class Box : MonoBehaviour, Interactable
    {
        [SerializeField]
        private float moveAnimationTime = 0.25f;
        
        private bool isMoving = false;
        private float moveT = 0;

        private Vector2 startPosition;
        private Vector2 targetPosition;
        
        public void Interact(Direction interactionDirection)
        {
            MoveBox(interactionDirection);
        }

        public void MoveBox(Direction direction)
        {
            if(isMoving) return;
            
            Vector2 vector = direction.ToVector();
            Vector2 currentPosition = transform.position;
            Vector2 targetPosition = currentPosition + vector;
            Collider2D collision = Physics2D.OverlapBox(targetPosition, Vector2.one * 0.95f, 0);
            if (collision != null && !collision.isTrigger && collision != GetComponent<Collider2D>())
                return;
            
            isMoving = true;
            moveT = 0;
            startPosition = currentPosition;
            this.targetPosition = targetPosition;
        }
        
        private void Update()
        {
            if (!isMoving) return;
            moveT += Time.deltaTime / moveAnimationTime;
            if (moveT >= 1)
            {
                transform.SetPosition2D(targetPosition);
                isMoving = false;
                return;
            }
            
            Vector2 currentPosition = Vector2.Lerp(startPosition, targetPosition, moveT);
            transform.SetPosition2D(currentPosition);
        }
    }
}