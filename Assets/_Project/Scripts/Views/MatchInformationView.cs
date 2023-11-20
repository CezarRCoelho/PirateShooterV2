using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateShooter
{
    public class MatchInformationView : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Text _scoreText = null;
        [SerializeField] private TMPro.TMP_Text _timeLeftText = null;

        private void OnEnable()
        {
            GameController.Instance.OnScoreChanged += OnScoreChangedHandler;
            InvokeRepeating(nameof(UpdateTime), 0, 1);
            _scoreText.text = GameController.Instance.Score.ToString();
        }
        private void OnDisable()
        {
            GameController.Instance.OnScoreChanged -= OnScoreChangedHandler;
            CancelInvoke();
        }

        private void OnScoreChangedHandler(int score)
        {
            _scoreText.text = $"Score: {score}";
        }
        private void UpdateTime()
        {
            float timeLeft = GameController.Instance.TimeLeft;
            float minutes = Mathf.FloorToInt(timeLeft / 60);
            float seconds = Mathf.FloorToInt(timeLeft % 60);
            _timeLeftText.text = string.Format("{0}:{1:00}", minutes, seconds);
        }
    }
}