using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PirateShooter
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] string _startGameScene;
        [SerializeField] string _winGameScene;
        [SerializeField] string _gameOverScene;
        [SerializeField] string _mainMenuScene;

        private SceneController _instance;

        private void Awake()
        {
            if(_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            GameController.Instance.OnStartGame += OnStartGameHandler;
            GameController.Instance.OnWinGame += OnWinGameHandler;
            GameController.Instance.OnGameOver += OnGameOverHandler;
            GameController.Instance.OnMainMenu += OnMainMenuHandler;
        }
        private void OnDisable()
        {
            GameController.Instance.OnStartGame -= OnStartGameHandler;
            GameController.Instance.OnWinGame -= OnWinGameHandler;
            GameController.Instance.OnGameOver -= OnGameOverHandler;
            GameController.Instance.OnMainMenu -= OnMainMenuHandler;
        }

        private void OnStartGameHandler()
        {
            SceneManager.LoadScene(_startGameScene);
        }
        private void OnGameOverHandler()
        {
            SceneManager.LoadScene(_gameOverScene);
        }
        private void OnWinGameHandler()
        {
            SceneManager.LoadScene(_winGameScene);
        }
        private void OnMainMenuHandler()
        {
            SceneManager.LoadScene(_mainMenuScene);
        }
    }
}
