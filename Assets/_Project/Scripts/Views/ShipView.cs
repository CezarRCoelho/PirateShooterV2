using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PirateShooter
{
    public class ShipView : MonoBehaviour
    {
        [SerializeField] private Ship _ship = null;
        [SerializeField] private SpriteRenderer _shipSprite = null;
        [SerializeField] private HealthbarView _healthBarViewPrefab = null;
        [SerializeField] private List<Sprite> _shipSprites = null;
        [SerializeField] private Sprite _shipDestroyedSprite = null;
        [SerializeField] private List<ParticleSystem> _destructionParticlePrefabs = null;

        private HealthbarView _healthBarView = null;

        private void OnEnable()
        {
            _healthBarView = Instantiate(_healthBarViewPrefab);
            _healthBarView.Attach(_ship);
            _healthBarView.Healthbar.maxValue = _ship.ShipMaxHealth;
            _healthBarView.Healthbar.minValue = 0;
            _healthBarView.Healthbar.value = _ship.ShipHealth;

            _ship.OnHealthChanged += OnHealthChangedHandler;
            _ship.OnShipDestroyed += OnShipDestroyedHandler;
            
            UpdateShipHealthSprite();
        }
        private void OnDisable()
        {
            _ship.OnHealthChanged -= OnHealthChangedHandler;
            _ship.OnShipDestroyed -= OnShipDestroyedHandler;
            _healthBarView.Detach();
            if(_healthBarView != null)
                Destroy(_healthBarView.gameObject);
        }

        /// <summary>
        /// This enables me to automate the animations, so I don't need to create one animation per Ship.
        /// </summary>
        private void UpdateShipHealthSprite()
        {
            if (_shipSprites == null || _ship.ShipHealth <= 0) return;

            float healthSpriteThreshold = _ship.ShipMaxHealth / _shipSprites.Count;
            int spriteIndex = Mathf.Min(Mathf.FloorToInt((_ship.ShipHealth / healthSpriteThreshold)), _shipSprites.Count - 1);
            Debug.Log($"Health: {_ship.ShipHealth}. Sprite Index: {spriteIndex}");
            _shipSprite.sprite = _shipSprites[spriteIndex];
        }

        private void OnHealthChangedHandler()
        {
            _healthBarView.Healthbar.value = _ship.ShipHealth;
            UpdateShipHealthSprite();
        }
        private void OnShipDestroyedHandler()
        {
            _shipSprite.sprite = _shipDestroyedSprite;
            SoundService.Instance.PlaySound(SoundService.SoundType.ShipExplode);
            if (_destructionParticlePrefabs != null)
            {
                foreach (ParticleSystem particlePrefab in _destructionParticlePrefabs)
                {
                    ParticleSystem newParticle = Instantiate(particlePrefab, transform);
                    newParticle.transform.position = transform.position;
                }
            }
        }
    }
}
