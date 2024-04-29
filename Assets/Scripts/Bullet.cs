using UnityEngine;

namespace Marsminerwa
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour, PoolEventListener
    {
        public uint Damage = 1;
        
        [SerializeField]
        private float speed = 10f;
        
        [Tooltip("Y+ is front, X+ is right")]
        [SerializeField]
        private Vector2 direction = Vector2.up;

        [SerializeField]
        private float lifeTime = 2;
        
        public void OnTookFromPool()
        {
            this.CallAfter(lifeTime, () => gameObject.SafeDestroy());
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = transform.TransformDirection2D(direction.normalized) * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.GetComponent<Health>()?.TakeDamage(Damage);
            gameObject.SafeDestroy();
        }

    }
}