using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PirateShooter
{
    public class Attack : MonoBehaviour
    {
        public UnityAction OnAttackCompleted;

        public float MaxAttackDistance
        {
            get => _maxAttackDistance;
        }
        public float MinAttackDistance
        {
            get => _minAttackDistance;
        }

        [SerializeField] protected TargetRadar _targetRadar = null;
        [SerializeField] protected float _attackRate = 0;
        [SerializeField] protected float _minAttackDistance = 0;
        [SerializeField] protected float _maxAttackDistance = 0;
        
        protected bool _isAttacking = false;
        protected float _lastAttackTime = 0;

        public virtual void DoAttack(Transform target) { }

        protected virtual void AttackCompleted()
        {
            _isAttacking = false;
            _lastAttackTime = Time.time;
            OnAttackCompleted?.Invoke();
        }
    }
}
