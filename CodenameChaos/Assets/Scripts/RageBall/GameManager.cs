using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;

namespace RageBall
{
    [Serializable]
    public class GameManagerEvent : UnityEvent<GameObject>{ }

    public class GameManager //: Singleton<GameManager>
    {
        public static StateMachine GameStateMachine { get; set; }
        public static byte MaxPlayers = 4;
        public static event Action PlayerJoins;
        public static event Action PlayerLeft;

        /*
            Hold players reference until a new round starts
        */
        private static readonly HashSet<PlayerInputHandler> Players = new HashSet<PlayerInputHandler>();

        public GameManager()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            GameStateMachine = new StateMachine();
        }

        ~GameManager()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void Start()
        {
            StartGameStateMachine();
            
            // for this sake, let's spawn four AI. We'll make client join after keyboard was detected. we would consume control of one ai.
            for( var i = 0; i < MaxPlayers; ++i )
            {
                var ai = new AIInputHandler();
                Players.Add( ai );
            }
        }

        private void OnSceneLoaded( Scene scene, LoadSceneMode mode )
        {
            // in case we do have a game state machine available in the level, then use that to play the game.
            StartGameStateMachine();
        }

        private static void StartGameStateMachine()
        {
            if( GameStateMachine == null )
                return;
            GameStateMachine.Initialize();
        }

        public static HashSet<PlayerInputHandler> GetPlayers() => Players;

        public static void OnPlayerJoin( PlayerInput player )
        {
            if( player.TryGetComponent<PlayerInputHandler>( out var handler ))
                Players.Add( handler );
        }

        public static void OnPlayerJoin( PlayerInputHandler handler )
        {
            PlayerJoins?.Invoke();
            Players.Add( handler );
        }

        public static void OnPlayerLeft( PlayerInput player )
        {
            PlayerLeft?.Invoke();
            if( player.TryGetComponent<PlayerInputHandler>(out PlayerInputHandler handler ))
            {
                handler.OnPlayerDisconnected();
                Players.Remove( handler );
            }
        }

        public static void OnPlayerLeft( PlayerInputHandler handler )
        {
            PlayerLeft?.Invoke();
            handler.OnPlayerDisconnected();
            Players.Remove( handler );
        }
        
        // I think we need to delegate this to a different class?
        // public void SpawnPlayers()
        // {
        //     // Spawner[] spawners = FindObjectsOfType<Spawner>();
        //     var spawners = SpawnManager.instance.GetSpawnPoints();
        //     var i = 0;
        //     foreach( var player in Players )
        //     {
        //         RespawnPlayer( player, spawners[i].transform.position, spawners[i].transform.rotation );
        //         i = ++i % ( spawners.Count - 1 );
        //     }
        // }
        
        // hmm?
        // public void RespawnPlayer( PlayerInputHandler player, Vector3 position, Quaternion rotation )
        // {
        //     var _avatarController = Instantiate( avatar, position, rotation );
        //     player.AssignActiveController( _avatarController );
        // }
    }
}

