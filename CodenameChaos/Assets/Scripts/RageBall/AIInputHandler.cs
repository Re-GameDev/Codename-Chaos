// Creation Date: January 15 2022
// Author(s): Regretful

using UnityEngine;

namespace RageBall
{
	public class AIInputHandler : PlayerInputHandler
	{
		GameObject _target;
		public GameObject target
		{
			get => _target;
			private set => _target = value;
		}

		public void SetTarget(GameObject target)
		{
			this.target = target;
		}

		
	}
}