using UnityEngine;

namespace Marsminerwa
{
    public class Health : MonoBehaviour
    {
        public delegate void HealthEvent(Health source, int previousHealth);

        public event HealthEvent OnHealthChange;
        public event HealthEvent OnDeath;

        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;

        [SerializeField]
        private int maxHealth;

        [SerializeField]
        private int currentHealth;

        public void TakeDamage(uint value)
        {
            if (value == 0) return;

            int previousHealth = currentHealth;
            currentHealth -= (int)value;
            if (currentHealth < 0) currentHealth = 0;

            OnHealthChange?.Invoke(this, previousHealth);

            if (currentHealth == 0)
                OnDeath?.Invoke(this, previousHealth);
        }

        public void Heal(uint value)
        {
            if (value == 0) return;

            int previousHealth = currentHealth;
            currentHealth += (int)value;
            if (currentHealth > maxHealth) currentHealth = maxHealth;

            OnHealthChange?.Invoke(this, previousHealth);
        }
    }
}