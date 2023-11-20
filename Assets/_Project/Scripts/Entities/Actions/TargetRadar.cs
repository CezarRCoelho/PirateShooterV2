using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PirateShooter
{
    public class TargetRadar : MonoBehaviour
    {
        public UnityAction OnTargetsUpdated;
        public List<Transform> TargetShips
        {
            get => _targetShips;
        }
        public Transform ClosestTarget
        {
            get
            {
                Transform closest = null;
                float closestDistance = float.MaxValue;

                foreach (Transform target in _targetShips)
                {
                    float targetDistance = (target.position - transform.position).sqrMagnitude;
                    if (targetDistance < closestDistance)
                    {
                        closestDistance = targetDistance;
                        closest = target;
                    }
                }
                return closest;
            }
        }
        public LayerMask TargetLayerMask
        {
            get => _targetLayerMask;
        }

        [SerializeField] private float _radarRadius = 2f;
        [SerializeField] LayerMask _targetLayerMask;

        private List<Transform> _targetShips = null;

        private void OnEnable()
        {
            _targetShips = new List<Transform>();
            InvokeRepeating(nameof(CheckForTargets), 0, .2f);
        }

        private void OnDisable()
        {
            _targetShips = null;
            CancelInvoke();
        }

        private void CheckForTargets()
        {
            List<Transform> updatedTargetList = new List<Transform>();
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, _radarRadius, transform.up, 0f, _targetLayerMask);
            foreach(RaycastHit2D hit in hits)
            {
                updatedTargetList.Add(hit.collider.transform);
            }
            _targetShips = updatedTargetList;
            OnTargetsUpdated?.Invoke();
        }
    }
}
