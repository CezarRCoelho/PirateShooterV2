using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateShooter
{
    public class Cannon : MonoBehaviour
    {
        public Vector2 Target
        {
            get => _target;
            set => _target = value;
        }

        [SerializeField] private CannonBall _cannonBallPrefab = null;
        [SerializeField] private Transform _bulletSpawnPosition = null;
        [SerializeField] private GameObject _cannonShootSmoke = null;
        [SerializeField] private float _shotCooldown = 1.5f;

        private Quaternion _originalRotation = Quaternion.identity;
        private Vector2 _target = Vector2.zero;
        private float _nextShotTime = 0;
        

        private void Start()
        {
            _originalRotation = transform.localRotation;
        }

        public void AimAtTarget(Vector2 target)
        {
            float targetAngle = Vector2.SignedAngle(transform.up, target - new Vector2(transform.position.x, transform.position.y));
            transform.Rotate(transform.forward, targetAngle);
        }

        public void ResetCannonRotation()
        {
            transform.localRotation = _originalRotation;
        }

        public void Shoot(float damage)
        {
            if (Time.time < _nextShotTime)
            {
                return;
            }
            _nextShotTime = Time.time + _shotCooldown;

            SoundService.Instance.PlaySound(SoundService.SoundType.CannonShoot);

            CannonBall cannonBall = Instantiate(_cannonBallPrefab);
            cannonBall.BallDamage = damage;
            cannonBall.transform.position = _bulletSpawnPosition.position;
            cannonBall.transform.rotation = _bulletSpawnPosition.rotation;

            GameObject shootSmoke = Instantiate(_cannonShootSmoke);
            shootSmoke.transform.position = _bulletSpawnPosition.position;
            shootSmoke.transform.rotation = _bulletSpawnPosition.rotation;
        }
    }
}
