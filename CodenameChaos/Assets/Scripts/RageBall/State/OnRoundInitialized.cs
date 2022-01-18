// Creation Date: January 10 2022
// Author(s): Regretful

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace RageBall
{
	// probably don't need this class?
	public class OnRoundInitialized : GameState
	{
		public float timer = 10f;
		float _t = 0f;

        public override void Execute()
        {
			_t += Time.deltaTime;
			if( _t > timer )
				OnNextState();	
        }
    }
}