using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateShooter
{
    public class EnemyBehavior : Behavior
    {
        [SerializeField] protected Transform _activeTarget = null;
        [SerializeField] protected EnemyMovement _movement = null;
        [SerializeField] protected TargetRadar _targetRadar = null;
        [SerializeField] protected BehaviorStateInfo _idleStateInfo;
        [SerializeField] protected BehaviorStateInfo _roamStateInfo;
        [SerializeField] protected BehaviorStateInfo _pursueStateInfo;
        [SerializeField] protected BehaviorStateInfo _attackStateInfo;
        [SerializeField] protected BehaviorStateInfo _searchStateInfo;
        [SerializeField] protected BehaviorStateInfo _fleeStateInfo;
        [SerializeField] protected float _fleeSafetyDistance = 0;
        [SerializeField] protected float _maxPursueDistance = 0;

        [SerializeField] private Ship _controlledShip = null;

        protected Vector2 _targetLastSeenPosition = Vector2.zero;
        protected float _activeTargetDistanceSqrMagnitude
        {
            get
            {
                if (_activeTarget != null)
                    return (_activeTarget.transform.position - transform.position).sqrMagnitude;
                return 0;
            }
        }
        protected float _searchStartTime = 0;
        protected float _searchMaxTime = 0;
        protected bool _isSearching = false;

        private Vector2 _currentRoamTarget = Vector2.zero;

        protected virtual void OnEnable()
        {
            OnStateChanged += OnStateChangedHandler;
            _controlledShip.OnDamageTaken += OnDamageTakenHandler;
            _targetRadar.OnTargetsUpdated += OnTargetsUpdatedHandler;
            _idleStateInfo.StartTime = 0;
            _roamStateInfo.StartTime = 0;
            _searchStateInfo.StartTime = 0;
        }
        protected virtual void OnDisable()
        {
            OnStateChanged -= OnStateChangedHandler;
            _controlledShip.OnDamageTaken -= OnDamageTakenHandler;
            _targetRadar.OnTargetsUpdated -= OnTargetsUpdatedHandler;
        }
        protected override void Roam()
        {
            base.Roam();
            if(_activeTarget != null)
            {
                CurrentState = BehaviorState.Pursue;
                return;
            }

            if(_currentRoamTarget == Vector2.zero || (_currentRoamTarget - (Vector2)transform.position).sqrMagnitude < 0.1f)
            {
                _currentRoamTarget = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
                _movement.Target = _currentRoamTarget;
            }

            if(Time.time > _roamStateInfo.StartTime + _roamStateInfo.MaxTime)
            {
                CurrentState = BehaviorState.Idle;
            }
        }
        protected override void Idle()
        {
            base.Idle();
            if (_activeTarget != null)
            {
                CurrentState = BehaviorState.Pursue;
                return;
            }

            _movement.Target = Vector2.zero;
            if(Time.time > _idleStateInfo.StartTime + _idleStateInfo.MaxTime)
            {
                CurrentState = BehaviorState.Roam;
            }
        }
        protected override void Pursue()
        {
            base.Pursue();
            //Enemy Specific
        }
        protected override void Attack()
        {
            base.Attack();
            //Enemy Specific
        }
        protected override void Search()
        {
            base.Search();
            //Enemy Specific
        }
        protected override void Flee()
        {
            base.Flee();
            //Enemy Specific
        }
        protected virtual void OnStateChangedHandler()
        {
            Debug.Log($"Changed to state {CurrentState}");
            switch (CurrentState)
            {
                case BehaviorState.Roam:
                    _roamStateInfo.StartTime = Time.time;
                    break;
                case BehaviorState.Idle:
                    _idleStateInfo.StartTime = Time.time;
                    break;
                case BehaviorState.Pursue:
                    _pursueStateInfo.StartTime = Time.time;
                    break;
                case BehaviorState.Attack:
                    _attackStateInfo.StartTime = Time.time;
                    break;
                case BehaviorState.Search:
                    _searchStateInfo.StartTime = Time.time;
                    break;
                case BehaviorState.Flee:
                    _fleeStateInfo.StartTime = Time.time;
                    break;
                default:
                    break;
            }
            _currentRoamTarget = Vector2.zero;
        }
        protected virtual void OnDamageTakenHandler()
        {
            if(_controlledShip.Alive && _controlledShip.ShipHealth < _controlledShip.ShipMaxHealth * 0.4f)
            {
                int shouldFlee = Random.Range(0, 10);
                if(shouldFlee < 4)
                {
                    CurrentState = BehaviorState.Flee;
                }
            }
        }
        protected virtual void OnTargetsUpdatedHandler()
        {
            _activeTarget = _targetRadar.ClosestTarget;
        }
    }
}
