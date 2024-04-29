using UnityEngine;
using UnityEngine.UI;

namespace Marsminerwa
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Health health;
        
        [SerializeField]
        private Slider uiHealthBar;

        void OnEnable()
        {
            health.OnHealthChange += HealthChanges;
        }

        void OnDisable()
        {
            health.OnHealthChange -= HealthChanges;
        }
        
        private void HealthChanges(Health source, int previousHealth)
        {
            int diff = source.CurrentHealth - previousHealth;
            float percentHealth = (float)source.CurrentHealth / (float)source.MaxHealth;
            uiHealthBar.value = percentHealth;

            Transform sourceTransform = source.transform;
            DamageNumbersManager.ShowDamageNumber(sourceTransform.position, diff);
        }
    }
}