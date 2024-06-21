using System.Collections;
using TMPro;
using UnityEngine;

namespace CantuniasInferno
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText;

        void Start()
        {
            UpdateScore();
        }

        public void UpdateScore()
        {
            // Make sure all logic has run before updating the score
            StartCoroutine(UpdateScoreNextFrame());
        }

        IEnumerator UpdateScoreNextFrame()
        {
            yield return null;
            scoreText.text = GameManager.Instance.Score;
        }
    }
}
