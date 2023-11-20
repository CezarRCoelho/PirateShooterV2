using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateShooter
{
    public class EnemyMovement : Movement
    {
        public Vector2 Target
        {
            get => _target;
            set => _target = value;
        }

        private Vector2 _target = Vector2.zero;

        protected override void FixedUpdate()
        {
            UpdateMovementDirection();
            base.FixedUpdate();
        }
        public void StopMovement()
        {
            _movementDirection = new Vector2(0, -1);
        }
        private void UpdateMovementDirection()
        {
            if (_target != null)
            {
                if (_target == Vector2.zero)
                {
                    _movementDirection = Vector2.zero;
                    return;
                }
                
                float targetAngle = Vector2.SignedAngle(transform.up, _target - (Vector2)transform.position);

                if(Mathf.Abs(targetAngle) < 45)
                {
                    _movementDirection.y = 1;
                }
                else
                {
                    _movementDirection.y = -1;
                }
                _movementDirection.x = -targetAngle;
            }
            else
                _movementDirection = Vector2.zero;
        }
    }

}