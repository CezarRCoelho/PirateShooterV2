using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PirateShooter
{
    public class HealthbarView : MonoBehaviour
    {
        [SerializeField] private Vector3 _healthbarPositionOffset = Vector3.zero;
        public Slider Healthbar
        {
            get => _healthBar;
        }
        private Transform _attachedTransform = null;

        [SerializeField] private Slider _healthBar;

        private void Update()
        {
            if(_attachedTransform != null)
                transform.position = _attachedTransform.position + _healthbarPositionOffset;
        }

        public void Attach(IDamageable damageable)
        {
            if(damageable is MonoBehaviour damageableObject)
            {
                _attachedTransform = damageableObject.transform;
            }
        }
        public void Detach()
        {
            _attachedTransform = null;
        }
    }
}

