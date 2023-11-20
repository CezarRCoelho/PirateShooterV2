using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateShooter
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private MainMenuView _mainMenuViewPrefab = null;

        private MainMenuView _mainMenuView = null;
        private GameSetting _gameSetting = null;

        private void OnEnable()
        {
            _mainMenuView = Instantiate(_mainMenuViewPrefab);

            _mainMenuView.OnClickPlay += OnClickPlayHandler;
            _mainMenuView.OnClickOptions += OnClickOptionsHandler;
            _mainMenuView.OnClickQuit += OnClickQuitHandler;
        }

        private void OnDisable()
        {
            _mainMenuView.OnClickPlay -= OnClickPlayHandler;
            _mainMenuView.OnClickOptions -= OnClickOptionsHandler;
            _mainMenuView.OnClickQuit -= OnClickQuitHandler;

            if(_mainMenuView)
                Destroy(_mainMenuView.gameObject);
        }

        private void OnClickPlayHandler(float gameTime, float spawnTime)
        {
            _gameSetting = new GameSetting(gameTime, spawnTime);
            GameController.Instance.StartNewGame(_gameSetting);
        }
        private void OnClickOptionsHandler()
        {

        }
        private void OnClickQuitHandler()
        {

        }

    }
}
