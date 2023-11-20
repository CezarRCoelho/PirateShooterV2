using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PirateShooter
{
    public class ChaserAttack : Attack
    {
        [SerializeField] private Ship _ship = null;
        [SerializeField] private EnemyMovement _movement = null;
        [SerializeField] private float _chaserDamageOnHit = 0;

        private Vector2 _telegraphedAttackPosition = Vector2.zero;
        private float _startTelegraphTime = 0f;
        private float _telegraphDuration = 1f;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(IsLayerInLayerMask(collision.gameObject.layer, _targetRadar.TargetLayerMask) && collision.gameObject.tag != "Enemy")
            {
                PlayerShip playerShip = collision.gameObject.GetComponent<PlayerShip>();
                if (playerShip != null)
                {
                    playerShip.TakeDamage(_chaserDamageOnHit);
                    _ship.TakeDamage(_ship.ShipMaxHealth);
                    AttackCompleted();
                }
            }
        }

        public override void DoAttack(Transform target)
        {
            base.DoAttack(target);
            if (!_isAttacking && Time.time > _attackRate + _lastAttackTime)
            {
                _isAttacking = true;
            }

            if (_isAttacking)
            {
                if (Time.time > _startTelegraphTime + _telegraphDuration)
                {
                    _movement.Target = _telegraphedAttackPosition;
                    return;
                }
                else
                {
                    _movement.Target = target.position;
                    _telegraphedAttackPosition = target.position;
                }
            }
        }

        private bool IsLayerInLayerMask(int layer, LayerMask layerMask)
        {
            return layerMask == (layerMask | (1 << layer));
        }
    }
}
