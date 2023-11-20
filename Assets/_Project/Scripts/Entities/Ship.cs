using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PirateShooter
{
    public class Ship : MonoBehaviour, IDamageable
    {
        public UnityAction OnHealthChanged;
        public UnityAction OnDamageTaken;
        public UnityAction OnShipDestroyed;

        public float ShipMaxHealth => _shipMaxHealth;
        public float ShipHealth => _shipHealth;
        public bool Alive => _alive;

        [SerializeField] private float _shipMaxHealth = 0;
        [SerializeField] private float _shipHealth = 0;
        [SerializeField] private bool _alive = false;

        protected virtual void OnEnable()
        {
            _shipHealth = _shipMaxHealth;
            _alive = true;
        }
        protected virtual void OnDisable() { }

        public virtual void TakeDamage(float amount)
        {
            _shipHealth -= amount;
            OnHealthChanged?.Invoke();
            OnDamageTaken?.Invoke();
            if(ShipHealth <= 0)
            {
                _alive = false;
                OnShipDestroyed?.Invoke();
                Invoke(nameof(DestroyShip), 1f);
            }
        }
        public virtual void DestroyShip()
        {
            Destroy(gameObject);
        }
    }
}
