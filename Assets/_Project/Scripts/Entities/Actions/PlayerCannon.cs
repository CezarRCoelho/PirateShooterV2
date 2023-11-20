using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PirateShooter
{
    public class PlayerCannon : MonoBehaviour
    {
        private enum ActiveAimDirection
        {
            None,
            Front,
            Left,
            Right
        }

        [SerializeField] private List<Cannon> _frontCannons;
        [SerializeField] private List<Cannon> _leftCannons;
        [SerializeField] private List<Cannon> _rightCannons;

        [SerializeField] private float _maxFrontCannonRotation = 20f;
        [SerializeField] private float _maxSideCannonsRotation = 35f;
        [SerializeField] private float _cannonDamage = 5f;

        private ActiveAimDirection _activeAimDirection = ActiveAimDirection.None;

        private void ResetCannonDirections()
        {
            foreach (Cannon cannon in _frontCannons)
                cannon.ResetCannonRotation();
            foreach (Cannon cannon in _leftCannons)
                cannon.ResetCannonRotation();
            foreach (Cannon cannon in _rightCannons)
                cannon.ResetCannonRotation();
        }
        private void UpdateAimedDirection(Vector2 target)
        {
            _activeAimDirection = ActiveAimDirection.None;
            float targetAngle = Vector2.SignedAngle(transform.up, target - (Vector2)transform.position);
            if (Mathf.Abs(targetAngle) < _maxFrontCannonRotation)
            {
                _activeAimDirection = ActiveAimDirection.Front;
            }

            float rightAngle = Vector2.SignedAngle(transform.right, target - new Vector2(transform.position.x, transform.position.y));
            if (Mathf.Abs(rightAngle) < _maxSideCannonsRotation)
            {
                _activeAimDirection = ActiveAimDirection.Right;
            }

            float leftAngle = Mathf.Abs(Mathf.Abs(rightAngle) - 180f);
            if (leftAngle < _maxSideCannonsRotation)
            {
                _activeAimDirection = ActiveAimDirection.Left;
            }
        }

        public void OnAim()
        {
            if (Mouse.current == null || Camera.main == null) return;

            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector2 target = Camera.main.ScreenToWorldPoint(mousePosition);

            ResetCannonDirections();
            UpdateAimedDirection(target);
            switch (_activeAimDirection)
            {
                case ActiveAimDirection.None:
                    break;
                case ActiveAimDirection.Front:
                    foreach (Cannon cannon in _frontCannons)
                        cannon.AimAtTarget(target);
                    break;
                case ActiveAimDirection.Left:
                    foreach (Cannon cannon in _leftCannons)
                        cannon.AimAtTarget(target);
                    break;
                case ActiveAimDirection.Right:
                    foreach (Cannon cannon in _rightCannons)
                        cannon.AimAtTarget(target);
                    break;
                default:
                    break;
            }
        }
        public void OnShoot()
        {
            switch (_activeAimDirection)
            {
                case ActiveAimDirection.None:
                    break;
                case ActiveAimDirection.Front:
                    foreach (Cannon cannon in _frontCannons)
                        cannon.Shoot(_cannonDamage);
                    break;
                case ActiveAimDirection.Left:
                    foreach (Cannon cannon in _leftCannons)
                        cannon.Shoot(_cannonDamage);
                    break;
                case ActiveAimDirection.Right:
                    foreach (Cannon cannon in _rightCannons)
                        cannon.Shoot(_cannonDamage);
                    break;
                default:
                    break;
            }
        }
        public void OnShootLeft()
        {
            foreach (Cannon cannon in _leftCannons)
            {
                cannon.ResetCannonRotation();
                cannon.Shoot(_cannonDamage);
            }
        }
        public void OnShootRight()
        {
            foreach (Cannon cannon in _rightCannons)
            {
                cannon.ResetCannonRotation();
                cannon.Shoot(_cannonDamage);
            }
        }
        public void OnShootFront()
        {
            foreach (Cannon cannon in _frontCannons)
            {
                cannon.ResetCannonRotation();
                cannon.Shoot(_cannonDamage);
            }
        }
    }
}
