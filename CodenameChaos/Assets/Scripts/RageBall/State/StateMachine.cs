// Creation Date: January 10 2022
// Author(s): Jordan Bejar

using System.Collections.Generic;
using UnityEngine;

namespace RageBall
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private List<State> gameStates = new List<State>();
        private State _currentState;
        public State GetCurrentState() => _currentState;

        private bool CanExecute()
        {
            var containElements = gameStates.Count > 0;
            if (!containElements)
                Debug.Log("No states assigned! Disabling!");
            return containElements;
        }

        private void Awake()
        {
            if (!CanExecute())
                enabled = false;
            else
            {
                GameManager.GameStateMachine = this;
                Initialize();
            }
        }

        private void Update()
        {
            if (!_currentState)
                return;
            _currentState.Execute();
        }

        public void Initialize()
        {
            if ( _currentState != null )
            {
                Debug.LogWarning("State Machine has already initialized!");
                return;
            }

            if (CanExecute())
                SetState(gameStates[0]);
        }

        /// <summary>
        /// Set next state to invoke
        /// </summary>
        /// <param name="state"></param>
        public void SetState(State state)
        {
            // Invoke On Exit call()
            _currentState?.OnExit();

            // Initialize new state information
            state?.Init(this, _currentState);

            // set our current state for execution
            _currentState = state;

            // invoke On Enter call()
            _currentState?.OnEnter();
        }
    }
}