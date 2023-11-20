using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateShooter
{
    public class ShooterBehavior : EnemyBehavior
    {
        [SerializeField] private ShooterAttack _shooterAttack = null;

        protected override void OnEnable()
        {
            base.OnEnable();
            _shooterAttack.OnAttackCompleted += OnAttackCompletedHandler;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            _shooterAttack.OnAttackCompleted += OnAttackCompletedHandler;
        }

        protected override void Pursue()
        {
            base.Pursue();
            if (_activeTarget == null || _activeTargetDistanceSqrMagnitude > _maxPursueDistance * _maxPursueDistance)
            {
                CurrentState = BehaviorState.Search;
                return;
            }

            _movement.Target = _activeTarget.position;
            if (_activeTargetDistanceSqrMagnitude < _shooterAttack.MaxAttackDistance * _shooterAttack.MaxAttackDistance)
            {
                CurrentState = BehaviorState.Attack;
            }
        }
        protected override void Attack()
        {
            base.Attack();
            if (_activeTarget == null)
            {
                CurrentState = BehaviorState.Search;
                return;
            }

            if (_activeTargetDistanceSqrMagnitude > _shooterAttack.MaxAttackDistance * _shooterAttack.MaxAttackDistance)
            {
                CurrentState = BehaviorState.Pursue;
                return;
            }

            if(_activeTargetDistanceSqrMagnitude < _shooterAttack.MinAttackDistance * _shooterAttack.MinAttackDistance)
            {
                CurrentState = BehaviorState.Flee;
                return;
            }

            _movement.StopMovement();
            _shooterAttack.DoAttack(_activeTarget);

        }
        protected override void Search()
        {
            base.Search();
            if (Time.time > _searchStateInfo.StartTime + _searchStateInfo.MaxTime)
            {
                CurrentState = BehaviorState.Idle;
                return;
            }

            if (!_isSearching)
            {
                _targetLastSeenPosition = _movement.Target;
                _isSearching = true;
            }

            if (Time.time > _searchStartTime + _searchMaxTime && (_targetLastSeenPosition - (Vector2)transform.position).sqrMagnitude < 0.2f)
            {
                _movement.Target = _targetLastSeenPosition + new Vector2(Random.Range(3, 3), Random.Range(3, 3));
                _searchStartTime = Time.time;
            }
        }
        protected override void Flee()
        {
            base.Flee();
            if (_activeTarget == null)
            {
                CurrentState = BehaviorState.Roam;
                return;
            }

            _movement.Target = transform.position - _activeTarget.position;

            if (_activeTargetDistanceSqrMagnitude > _fleeSafetyDistance * _fleeSafetyDistance && Time.time > _fleeStateInfo.StartTime + _fleeStateInfo.MaxTime)
            {
                CurrentState = BehaviorState.Roam;
            }
        }
        protected virtual void OnAttackCompletedHandler()
        {
            CurrentState = BehaviorState.Search;
        }
    }

}