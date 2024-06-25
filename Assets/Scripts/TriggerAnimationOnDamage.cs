using UnityEngine;

namespace Marsminerwa
{
    [RequireComponent(typeof(Health))]
    public class TriggerAnimationOnDamage : MonoBehaviour
    {
        
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
            if (source.CurrentHealth < previousHealth)
            {
                GetComponent<Animator>().SetTrigger(AnimatorProps.Damage);
            }
        }
        
    }
}