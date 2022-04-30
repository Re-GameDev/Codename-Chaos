// Creation Date: January 10 2022
// Author(s): Jordan Bejar

using System;

namespace RageBall
{
	public interface IState
	{
		void Init( StateMachine owner, State previousState = null );
		void OnEnter();
		void Execute();
		void OnExit();
	}
}