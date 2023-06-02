using UnityEngine;

namespace RPG.Stats
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100f;
        [field: SerializeField] private float currentHealth;

        public delegate void HealthChanged(float currentHealth);
        public event HealthChanged OnHealthChanged;

        public delegate void Death();
        public event Death OnDeath;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;

            // Clamp the current health between 0 and maxHealth
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            // Notify listeners about the health change
            OnHealthChanged?.Invoke(currentHealth);

            // Check if the entity has died
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            // Notify listeners about the death
            OnDeath?.Invoke();

            // Destroy the game object
            Destroy(gameObject);
        }

        public float GetCurrentHealth()
        {
            return currentHealth;
        }

        public float GetMaxHealth()
        {
            return maxHealth;
        }
    }
}
