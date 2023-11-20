using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PirateShooter
{
    public class Behavior : MonoBehaviour
    {
        public UnityAction OnStateChanged;
        protected enum BehaviorState
        {
            Roam,
            Idle,
            Pursue,
            Attack,
            Search,
            Flee
        }
        
        [System.Serializable] 
        protected struct BehaviorStateInfo
        {
            public float MaxTime;
            public float StartTime;
        }

        protected BehaviorState CurrentState
        {
            get => _currentState;
            set
            {
                _currentState = value;
                OnStateChanged?.Invoke();
            }
        }

        private BehaviorState _currentState = BehaviorState.Idle;

        protected virtual void FixedUpdate()
        {
            Behave();
        }

        protected virtual void Behave()
        {
            switch (_currentState)
            {
                case BehaviorState.Roam:
                    Roam();
                    break;
                case BehaviorState.Idle:
                    Idle();
                    break;
                case BehaviorState.Pursue:
                    Pursue();
                    break;
                case BehaviorState.Attack:
                    Attack();
                    break;
                case BehaviorState.Search:
                    Search();
                    break;
                case BehaviorState.Flee:
                    Flee();
                    break;
                default:
                    Idle();
                    break;
            }
        }

        protected virtual void Roam()
        {

        }
        protected virtual void Idle()
        {

        }
        protected virtual void Pursue()
        {

        }
        protected virtual void Attack()
        {

        }
        protected virtual void Search()
        {

        }
        protected virtual void Flee()
        {

        }
    }

}