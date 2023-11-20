using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateShooter
{
    public class Movement : MonoBehaviour
    {
        protected Vector2 _movementDirection = Vector2.zero;

        [SerializeField] private Rigidbody2D _rigidbody2D = null;
        [SerializeField] private Ship _ship = null;

        [SerializeField] private float _boatSpeed = 0;
        [SerializeField] private float _boatMaxSpeed = 0;
        [SerializeField] private float _boatAcceleration = 0;
        [SerializeField] private float _boatStoppingPower = 0;
        [SerializeField] private float _boatDeacceleration = 0;
        [SerializeField] private float _boatRotationPower = 0;

        protected virtual void FixedUpdate()
        {
            Move();
            if(_boatSpeed > 0f)
            {
                MoveForward();
            }
            _rigidbody2D.angularVelocity = 0f;
        }

        private void Accelerate()
        {
            _boatSpeed = Mathf.Lerp(_boatSpeed, _boatMaxSpeed, _boatAcceleration * Time.fixedDeltaTime);
        }
        private void Stop()
        {
            _boatSpeed = Mathf.Lerp(_boatSpeed, 0f, _boatStoppingPower * Time.fixedDeltaTime);
        }

        private void Rotate(float angle)
        {
            _rigidbody2D.MoveRotation(_rigidbody2D.rotation + angle * _boatRotationPower * Time.fixedDeltaTime);
        }

        private void Move()
        {
            if (!_ship.Alive)
            {
                Stop();
                return;
            }
            if(_movementDirection == Vector2.zero)
            {
                _boatSpeed = Mathf.Lerp(_boatSpeed, 0f, _boatDeacceleration * Time.fixedDeltaTime);
                return;
            }
            switch (_movementDirection.y)
            {
                case > 0f:
                    Accelerate();
                    break;
                case < 0f:
                    Stop();
                    break;
                default:
                    break;
            }

            switch (_movementDirection.x)
            {
                case > 0:
                    Rotate(-45);
                    break;
                case < 0:
                    Rotate(45);
                    break;
                default:
                    break;
            }
        }
        private void MoveForward()
        {
            _rigidbody2D.MovePosition(_rigidbody2D.position + new Vector2(_rigidbody2D.transform.up.x, _rigidbody2D.transform.up.y) * _boatSpeed * Time.fixedDeltaTime);
        }

        private void OnDrawGizmos()
        {
            
        }
    }
}
