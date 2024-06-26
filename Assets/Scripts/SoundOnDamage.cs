using UnityEngine;

namespace Marsminerwa
{
    public class SoundOnDamage : MonoBehaviour
    {
        [SerializeField]
        private bool playerSound = false;
        
        private Health health;

        private void Awake()
        {
            health = GetComponent<Health>();
        }
        
        private void OnEnable()
        {
            health.OnHealthChange += OnHealthChange;
        }

        private void OnDisable()
        {
            health.OnHealthChange -= OnHealthChange;
        }

        private void OnHealthChange(Health source, int previousHealth)
        {
            if(source.CurrentHealth >= previousHealth) return;
            
            if (playerSound)
            {
                GameManager.PlayPlayerHitSound();
            }
            else
            {
                GameManager.PlayEnemyHitSound();
            }
        }

    }
}