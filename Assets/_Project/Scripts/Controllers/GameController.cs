using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PirateShooter
{
    public class GameController : MonoBehaviour
    {
        public UnityAction<int> OnScoreChanged;
        public UnityAction OnStartGame;
        public UnityAction OnGameOver;
        public UnityAction OnWinGame;
        public UnityAction OnMainMenu;
        public static GameController Instance
        {
            get;
            private set;
        }

        public Ship PlayerShip
        {
            get => _playerShip;
            set
            {
                _playerShip = value;
                _playerShip.OnShipDestroyed += OnPlayerDestroyedHandler;
            }
        }
        public GameSetting GameSetting 
        {
            get => _gameSetting;
            private set => _gameSetting = value;
        }
        public float TimeLeft
        {
            get;
            private set;
        }
        public int Score
        {
            get;
            private set;
        }

        private GameSetting _gameSetting = null;
        private Ship _playerShip = null;

        private float _gameStartTime = 0;
        private bool _running = false;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void FixedUpdate()
        {
            if (!_running) return;
            float timePassed = Time.time - _gameStartTime;
            TimeLeft = GameSetting.GameTime * 60 - timePassed;
            if (timePassed >= GameSetting.GameTime * 60)
            {
                WinGame();
            }
        }

        public void AddScore(int amount)
        {
            Score += amount;
            OnScoreChanged?.Invoke(Score);
        }
        public void StartNewGame(GameSetting gameSetting)
        {
            if(gameSetting != null)
            {
                GameSetting = gameSetting;
            }
            else
            {
                GameSetting = new GameSetting(3, 10);
            }
            Score = 0;
            OnStartGame?.Invoke();
            _gameStartTime = Time.time;
            _running = true;
        }
        public void GameOver()
        {
            OnGameOver?.Invoke();
            _running = false;
        }
        public void MainMenu()
        {
            OnMainMenu?.Invoke();
            _running = false;
        }
        private void WinGame()
        {
            OnWinGame?.Invoke();
            _running = false;
        }
        private void OnPlayerDestroyedHandler()
        {
            GameOver();
        }
    }
}