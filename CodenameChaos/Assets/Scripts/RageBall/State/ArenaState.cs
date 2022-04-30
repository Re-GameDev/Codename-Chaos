// Creation Date: January 10 2022
// Author(s): Jordan Bejar

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RageBall
{
    public class ArenaState : GameState
    {
		private int _remainingPlayers = 0;
		[SerializeField] private PlayerController playerController;

        public override void OnEnter()
		{
			var players = GameManager.GetPlayers();			
			base.OnEnter();
			foreach( var player in players )
			{
				player.onPlayerDeath += HandlePlayerDeath;
				player.onPlayerSpawn += HandlePlayerSpawn;
			}

			SpawnPlayers(players);

			// if( remainingPlayers == 0 )
			// 	OnNextState();
		}

        private void SpawnPlayers(HashSet<PlayerInputHandler> players)
        {
	        var spawners = SpawnManager.instance.GetSpawnPoints();
	        var i = 0;
	        foreach (var player in players)
	        {
		        var controller = Instantiate(playerController, spawners[i].transform.position, spawners[i].transform.rotation);
		        player.AssignActiveController(controller);
		        i = ++i % (spawners.Count - 1);
	        }
        }

        public override void OnExit()
		{
			var players = GameManager.GetPlayers();
			_remainingPlayers = players.Count(x => x.IsAlive());
			foreach( var player in players )
			{
				player.onPlayerDeath -= HandlePlayerDeath;
				player.onPlayerSpawn -= HandlePlayerSpawn;
			}
			base.OnExit();
		}

        private void HandlePlayerDeath() 
		{
			--_remainingPlayers;
			if( _remainingPlayers == 1 )
				OnNextState();
		} 

        private void HandlePlayerSpawn() => ++_remainingPlayers;
    }
}
