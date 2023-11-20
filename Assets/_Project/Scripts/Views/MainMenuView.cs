using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PirateShooter
{
    public class MainMenuView : MonoBehaviour
    {
        public UnityAction<float, float> OnClickPlay;
        public UnityAction OnClickOptions;
        public UnityAction OnClickQuit;

        [SerializeField] private GameObject _options = null;
        [SerializeField] private Slider _gameTimeSlider = null;
        [SerializeField] private Slider _spawnTimeSlider = null;
        [SerializeField] private Slider _volumeSlider = null;
        [SerializeField] private TMPro.TMP_Text _gameTimeText = null;
        [SerializeField] private TMPro.TMP_Text _spawnTimeText = null;

        private void OnEnable()
        {
            _gameTimeSlider.onValueChanged.AddListener(OnGameTimeValueChangedHandler);
            _spawnTimeSlider.onValueChanged.AddListener(OnSpawnTimeValueChangedHandler);
            _volumeSlider.onValueChanged.AddListener(OnVolumeValueChangedHandler);
            _volumeSlider.value = SoundService.Instance.Volume;
            SetGameTimeText(_gameTimeSlider.value);
            SetSpawnTimeText(_spawnTimeSlider.value);
        }
        private void OnDisable()
        {
            _gameTimeSlider.onValueChanged.RemoveListener(OnGameTimeValueChangedHandler);
            _spawnTimeSlider.onValueChanged.RemoveListener(OnSpawnTimeValueChangedHandler);
            _volumeSlider.onValueChanged.RemoveListener(OnVolumeValueChangedHandler);
        }

        public void Play()
        {
            OnClickPlay?.Invoke(_gameTimeSlider.value, _spawnTimeSlider.value);
        }
        public void Options()
        {
            OnClickOptions?.Invoke();
            _options.SetActive(true);
        }
        public void Quit()
        {
            OnClickQuit?.Invoke();
        }
        private void SetGameTimeText(float value)
        {
            if (value == 1)
            {
                _gameTimeText.text = "1 minute";
                return;
            }
            _gameTimeText.text = $"{value} minutes";
        }
        private void SetSpawnTimeText(float value)
        {
            if (value == 1)
            {
                _spawnTimeText.text = "1 second";
                return;
            }
            _spawnTimeText.text = $"{value} seconds";
        }
        private void OnGameTimeValueChangedHandler(float value)
        {
            SetGameTimeText(value);
        }
        private void OnSpawnTimeValueChangedHandler(float value)
        {
            SetSpawnTimeText(value);
        }
        private void OnVolumeValueChangedHandler(float value)
        {
            SoundService.Instance.UpdateVolume(value);
        }
    }

}