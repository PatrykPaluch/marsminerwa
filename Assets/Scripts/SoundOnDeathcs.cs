using UnityEngine;

namespace Marsminerwa
{
    public class SoundOnDeath : MonoBehaviour
    {
        
        private Health health;

        private void Awake()
        {
            health = GetComponent<Health>();
        }
        
        private void OnEnable()
        {
            health.OnDeath += OnDeath;
        }

        private void OnDisable()
        {
            health.OnDeath -= OnDeath;
        }

        private void OnDeath(Health source, int previousHealth)
        {
            GameManager.PlayEnemyDeathSound();
        }

    }
}