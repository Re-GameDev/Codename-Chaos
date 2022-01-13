// Creation Date: January 10 2022
// Author(s): Jordan Bejar

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace RageBall
{
	public class StateMachine : MonoBehaviour
	{
		[SerializeField] List<State> gameStates = new List<State>();
		State _currentState;

		bool CanExecute()
		{
			bool containElements = gameStates.Count > 0;
			if( !containElements )
				Debug.Log("No states assigned! Disabling!");
			return containElements;
		}

		void Awake()
		{
			if( !CanExecute() )
				this.enabled = false;
		}

		void Update()
		{
			if( _currentState == null )
				return;

			_currentState.Execute();	
		} 

		public void Initialize()
		{ 
			if( _currentState != null )
			{
				Debug.LogWarning("State Machine has already initialized!");
				return;
			} 

			if( CanExecute() )
				SetState( gameStates[0] );
		}

		/// <summary>
		/// Return the current active state
		/// </summary>
		/// <returns></returns>
		public State GetCurrentState() => this._currentState;

		/// <summary>
		/// Set next state to invoke
		/// </summary>
		/// <param name="state"></param>
		public void SetState( State state )
		{
			// Invoke On Exit call()
			if( _currentState != null )
				_currentState.OnExit();
			
			// Initialize new state information
			state.Init( this, _currentState );
			
			// set our current state for execution
			_currentState = state;
			
			// invoke On Enter call()
			if( _currentState != null )
				_currentState.OnEnter();
		}
	}
}