using UnityEngine;

namespace CantuniasInferno
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] int scoreForWin = 3;

        public static GameManager Instance { get; private set; }

        int score;

        public string Score => $"{score}/{scoreForWin}";

        public void Pause(bool value)
        {
            Time.timeScale = value ? 0 : 1;
        }

        void Awake()
        {
            Pause(true);
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        void Update()
        {
            if (score >= scoreForWin)
            {
                Debug.Log("You win!");
            }
        }

        public void AddScore(int amount)
        {
            score += amount;
        }
    }
}
