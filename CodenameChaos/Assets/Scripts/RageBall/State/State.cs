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
        // private
		StateMachine _owner;
		State _previousState;

        // public
		public State nextState;

        public virtual void Execute() { }

        public virtual void Init(StateMachine owner, State previousState = null)
        {
            this._owner = owner;
			this._previousState = previousState;
        }

        public virtual void OnEnter() { }

        public virtual void OnExit() { }

        public virtual void OnNextState() => _owner.SetState( nextState );

        public virtual void OnPreviousState() => _owner.SetState( _previousState );
    }

}