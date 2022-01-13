// Creation Date: January 10 2022
// Author(s): Jordan Bejar

using System.Linq;

namespace RageBall
{
    public class ArenaState : GameState
    {
		int remainingPlayers = 0;

        public override void OnEnter()
		{
			var players = GameManager.Instance.GetPlayers();			
			base.OnEnter();
			foreach( var player in players )
			{
				player.onPlayerDeath += HandlePlayerDeath;
				player.onPlayerSpawn += HandlePlayerSpawn;
			}

			if( remainingPlayers == 0 )
				OnNextState();
		}
		
		public override void OnExit()
		{
			var players = GameManager.Instance.GetPlayers();
			remainingPlayers = players.Count(x => x.IsAlive());
			foreach( var player in players )
			{
				player.onPlayerDeath -= HandlePlayerDeath;
				player.onPlayerSpawn -= HandlePlayerSpawn;
			}
			base.OnExit();
		}

        private void HandlePlayerDeath() 
		{
			--remainingPlayers;
			if( remainingPlayers == 1 )
				OnNextState();
		} 

        private void HandlePlayerSpawn() => ++remainingPlayers;
    }
}
