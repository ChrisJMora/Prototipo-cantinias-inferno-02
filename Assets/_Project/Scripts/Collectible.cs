using UnityEngine;

namespace CantuniasInferno
{
    public class Collectible : Entity
    {
        [SerializeField] int score = 1;
        [SerializeField] IntEventChannel scoreChannel;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                scoreChannel.Invoke(score);
                Destroy(gameObject);
            }
        }
    }
}
