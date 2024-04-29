using UnityEngine;

namespace Marsminerwa
{
    public class DestroyOnDeath : MonoBehaviour
    {
        private Health health;

        private void Awake()
        {
            health = GetComponent<Health>();
        }
        
        private void OnEnable()
        {
            health.OnDeath += DestroyGameObject;
        }

        private void OnDisable()
        {
            health.OnDeath -= DestroyGameObject;
        }

        private void DestroyGameObject(Health _, int __)
        {
            gameObject.SafeDestroy();
        }
    }
}