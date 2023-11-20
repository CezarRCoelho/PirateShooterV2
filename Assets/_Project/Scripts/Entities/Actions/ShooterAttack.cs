using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PirateShooter
{
    public class ShooterAttack : Attack
    {
        [SerializeField] private List<Cannon> _frontCannons;
        [SerializeField] private List<Cannon> _leftCannons;
        [SerializeField] private List<Cannon> _rightCannons;
        [SerializeField] private float _maxFrontCannonsRotation = 20f;
        [SerializeField] private float _maxSideCannonsRotation = 30f;
        [SerializeField] private float _cannonDamage = 5f;

        private bool _canAttack => Time.time > _attackRate + _lastAttackTime;

        public override void DoAttack(Transform target)
        {
            base.DoAttack(target);

            List<Cannon> aimedCannons = GetAimedCannons(target.position);
            if (aimedCannons == null) return;
            foreach (Cannon cannon in aimedCannons)
            {
                if (_canAttack)
                {
                    cannon.Shoot(_cannonDamage);
                    Debug.Log("Fired");
                }
            }
            if (_canAttack)
            {
                _lastAttackTime = Time.time;
            }
        }

        private List<Cannon> GetAimedCannons(Vector2 target)
        {
            float targetAngle = Vector2.SignedAngle(transform.up, target - (Vector2)transform.position);

            if (Mathf.Abs(targetAngle) < _maxFrontCannonsRotation)
            {
                return _frontCannons;
            }

            float rightAngle = Vector2.SignedAngle(transform.right, target - new Vector2(transform.position.x, transform.position.y));

            if (Mathf.Abs(rightAngle) < _maxSideCannonsRotation)
            {
                return _rightCannons;
            }

            float leftAngle = Mathf.Abs(Mathf.Abs(rightAngle) - 180f);

            if (leftAngle < _maxSideCannonsRotation)
            {
                return _leftCannons;
            }
            return null;
        }
    }
}
