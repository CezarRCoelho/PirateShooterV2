using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PirateShooter
{
    public class SoundService : MonoBehaviour
    {
        public enum SoundType
        {
            ShipSail,
            ShipExplode,
            CannonShoot,
            BallHitShip,
            BallHitRock,
            BallHitGround,
            BallHitWater,
            BallHitBall,
            ChaserHit,
            GameMusic,
        }

        public static SoundService Instance { get; private set; }

        public List<SoundTypeAudioClip> _soundTypeAudioClips = null;

        public float Volume => _volume;

        private List<SoundTypeAudioSource> _activeLoopedSounds = null;

        private float _volume = 0.5f;
        private bool _muted = false;

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
        private void OnEnable()
        {
            _activeLoopedSounds = new List<SoundTypeAudioSource>();
            PlaySound(SoundType.GameMusic, true);
        }

        public void PlaySound(SoundType soundType, bool loop = false)
        {
            if (_muted && !loop) return;

            GameObject go = new GameObject();
            AudioSource audioSource = go.AddComponent<AudioSource>();
            audioSource.volume = _volume;
            AudioClip audioClip = GetRandomClip(soundType);
            if (audioClip == null)
            {
                Debug.LogWarning($"No clips for sound type: {soundType}");
                return;
            }
            if (!loop)
            {
                audioSource.PlayOneShot(audioClip);
                Destroy(audioSource.gameObject, audioClip.length);
                return;
            }
            go.transform.parent = transform;
            audioSource.clip = audioClip;
            audioSource.loop = true;
            audioSource.Play();

            SoundTypeAudioSource _soundTypeAudioSource = new SoundTypeAudioSource();
            _soundTypeAudioSource.SoundType = soundType;
            _soundTypeAudioSource.AudioSource = audioSource;
            _activeLoopedSounds.Add(_soundTypeAudioSource);
        }

        public void StopSound(SoundType soundType)
        {
            for (int i = 0; i < _activeLoopedSounds.Count; i++)
            {
                if (_activeLoopedSounds[i].SoundType != soundType || _activeLoopedSounds[i].AudioSource == null) continue;

                _activeLoopedSounds[i].AudioSource.Stop();
                Destroy(_activeLoopedSounds[i].AudioSource.gameObject);
            }
        }
        public void SwitchMute()
        {
            _muted = !_muted;

            float volume = _muted ? 0 : _volume;

            UpdateLoopedSoundsVolume(volume);
        }

        public void UpdateVolume(float volume)
        {
            _volume = volume;
            _muted = volume <= 0;

            UpdateLoopedSoundsVolume(volume);
        }

        private void UpdateLoopedSoundsVolume(float volume)
        {
            for (int i = _activeLoopedSounds.Count -1; i >= 0; i--)
            {
                if (_activeLoopedSounds[i].AudioSource == null)
                {
                    _activeLoopedSounds.RemoveAt(i);
                    continue;
                }
                _activeLoopedSounds[i].AudioSource.volume = volume;
            }
        }

        private AudioClip GetRandomClip(SoundType type)
        {
            SoundTypeAudioClip soundTypeAudioClip = _soundTypeAudioClips.Where(soundTypeClip => soundTypeClip.SoundType == type).First();
            if (soundTypeAudioClip == null) return null;

            return soundTypeAudioClip.AudioClips[Random.Range(0, soundTypeAudioClip.AudioClips.Count)];
        }
    }

    [System.Serializable]
    public class SoundTypeAudioClip
    {
        public SoundService.SoundType SoundType;
        public List<AudioClip> AudioClips;
    }

    public class SoundTypeAudioSource
    {
        public SoundService.SoundType SoundType;
        public AudioSource AudioSource;
    }
}
