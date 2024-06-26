using System.Collections;
using UnityEngine;

namespace Marsminerwa
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField]
        private float speed = 1f;
        [SerializeField]
        private uint dmg = 1;
        [SerializeField]
        private int dmgCooldown = 1;

        private Rigidbody2D rb;
        private Animator animator;

        private Coroutine dmgCoroutine;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Vector2 playerPosition = PlayerMovement.PlayerPosition;
            Vector2 direction = (playerPosition - (Vector2)transform.position).normalized;
            rb.velocity = direction * speed;

            animator.SetFloat(AnimatorProps.Velocity, rb.velocity.magnitude);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                dmgCoroutine = StartCoroutine(DealDamage());
            }
        }
        
        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                StopCoroutine(dmgCoroutine);
            }
        }

        private IEnumerator DealDamage()
        {
            while (enabled)
            {
                PlayerMovement.PlayerObject.GetComponent<Health>()?.TakeDamage(dmg);
                animator.SetTrigger(AnimatorProps.Attack);
                yield return new WaitForSeconds(dmgCooldown);
            }
        }
    }
}