// Creation Date: January 12 2022
// Author(s): Jordan Bejar

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace RageBall
{
    public class State : MonoBehaviour, IState
    {
        private StateMachine _owner;
        private State _previousState;
        public State nextState;

        public virtual void Init(StateMachine owner, State previousState = null)
        {
            _owner = owner;
            _previousState = previousState;
        }

        public virtual void OnEnter()
        {
        }

        public virtual void Execute()
        {
        }

        public virtual void OnExit()
        {
        }

        public virtual void OnNextState() => _owner.SetState(nextState);
        public virtual void OnPreviousState() => _owner.SetState(_previousState);
    }
}