// Creation Date: January 10 2022
// Author(s): Regretful

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace RageBall
{
	public class OnRoundCompleted : GameState
	{
		float duration = 10f;
		float _timer = 0f;
		
        public override void Execute()
        {
			_timer += Time.deltaTime;
			if( duration < _timer )
				OnNextState();
        }
    }
}