using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PirateShooter
{
    public class EndGameView : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Text _finalScoreText = null;
        [SerializeField] private Button _playAgainButton = null;
        [SerializeField] private Button _mainMenuButton = null;

        private void OnEnable()
        {
            _finalScoreText.text = GameController.Instance.Score.ToString();
            _playAgainButton.onClick.AddListener(PlayAgain);
            _mainMenuButton.onClick.AddListener(MainMenu);
        }

        private void PlayAgain()
        {
            GameController.Instance.StartNewGame(GameController.Instance.GameSetting);
        }
        private void MainMenu()
        {
            GameController.Instance.MainMenu();
        }
    }
}
