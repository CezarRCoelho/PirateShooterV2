using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PirateShooter
{
    public class CannonBall : MonoBehaviour
    {
        public UnityAction<DeactivationReason> OnDeactivate;
        public enum DeactivationReason
        {
            Timeout,
            ShipHit,
            BallHit,
            GroundHit,
            RockHit
        }

        public float BallDamage = 1f;

        [SerializeField] private Rigidbody2D _rigidbody2D = null;
        [SerializeField] private float _ballSpeed = 50f;
        [SerializeField] private float _cannonBallDuration = 1f;

        private float _timeWhenShot = 0;

        private void OnEnable()
        {
            Activate();
        }

        private void FixedUpdate()
        {
            if (_timeWhenShot + _cannonBallDuration < Time.time)
            {
                Deactivate(DeactivationReason.Timeout);
                return;
            }
            _rigidbody2D.MovePosition(transform.position + transform.up * _ballSpeed * Time.fixedDeltaTime);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.gameObject.tag == gameObject.tag) return;

            switch (collision.collider.gameObject.tag)
            {
                case "Ground":
                    Deactivate(DeactivationReason.GroundHit);
                    break;
                case "Rocks":
                    Deactivate(DeactivationReason.RockHit);
                    break;
                case "EnemyCannonBall":
                    Deactivate(DeactivationReason.BallHit);
                    Destroy(collision.collider.gameObject);
                    break;
                case "CannonBall":
                    Deactivate(DeactivationReason.BallHit);
                    Destroy(collision.collider.gameObject);
                    break;
                default:
                    break;
            }
        }

        //To-do if possible: Change this to an ObjectPooler
        public void Activate()
        {
            _timeWhenShot = Time.time;
        }
        public void Deactivate(DeactivationReason deactivationReason)
        {
            OnDeactivate?.Invoke(deactivationReason);
            Destroy(gameObject);
        }

    }
}
