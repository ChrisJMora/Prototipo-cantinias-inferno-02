using UnityEngine;
using UnityEngine.Events;

namespace CantuniasInferno
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int maxHealth = 100;
        [SerializeField] FloatEventChannel playerHealthChannel;

        public event UnityAction RecieveDamage = delegate { };

        int currentHealth;

        public bool IsDead => currentHealth <= 0;

        void Awake()
        {
            currentHealth = maxHealth;
        }

        void Start()
        {
            PublishHealthPercentage();
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            RecieveDamage.Invoke();
            PublishHealthPercentage();
        }

        void PublishHealthPercentage()
        {
            playerHealthChannel?.Invoke(currentHealth / (float)maxHealth);
        }
    }
}
