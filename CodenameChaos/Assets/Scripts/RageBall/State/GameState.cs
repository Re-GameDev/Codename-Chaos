// Creation Date: January 10 2022
// Author(s): Jordan Bejar

using System;
using UnityEngine;
using UnityEngine.Events;

namespace RageBall
{
	[Serializable]
	public class GameStateEvent : UnityEvent<GameState> { }

	public class GameState : State
	{
		// public event
		public GameStateEvent onGameEventStart;
		public GameStateEvent onGameEventEnd;

#region interface implementation

        public override void OnEnter() => onGameEventStart?.Invoke( this );
		public override void Execute() { }
        public override void OnExit() => onGameEventEnd?.Invoke( this );
		
	#endregion
    }
}