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

    public class GameManager : MonoBehaviour
    {
        [SerializeField] StateMachine gameStateMachine;
        private static GameManager _instance;
        public static GameManager Instance
        {
            get => _instance ?? new GameObject(nameof(GameManager)).AddComponent<GameManager>();
        }

        public static bool Exists() => _instance != null;
        public bool waitForAllPlayers = false;
        [SerializeField] GameEvent playerJoins;
        [SerializeField] GameEvent playerLeft;
        [SerializeField] PlayerController avatar; 

        /*
            Hold players reference until a new round starts
        */
        HashSet<PlayerInputHandler> players = new HashSet<PlayerInputHandler>();

        int playerAlive = 0;

        void Awake()
        {
            if( _instance != null )
            {
                DestroyImmediate( gameObject );
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            StartGameStateMachine();
        }

        void OnSceneLoaded( Scene scene, LoadSceneMode mode )
        {
            // in case we do have a game state machine available in the level, then use that to play the game.
            gameStateMachine = FindObjectOfType<StateMachine>();
            StartGameStateMachine();
        }

        void StartGameStateMachine()
        {
            if( gameStateMachine == null )
                return;
            gameStateMachine.Initialize();
        }

        public HashSet<PlayerInputHandler> GetPlayers() => players;

        public void OnPlayerJoin( PlayerInput player )
        {
            if( player.TryGetComponent<PlayerInputHandler>( out PlayerInputHandler handler ))
                players.Add( handler );
        }

        public void OnPlayerJoin( PlayerInputHandler handler )
        {
            playerJoins?.Invoke();
            players.Add( handler );
            // else if ( currentState == GameState.Preround )
            // {
            //     var spawnPoints = SpawnManager.instance.GetSpawnPoints();
            //     int pos = players.Count % spawnPoints.Count;
            //     Spawner spawn = spawnPoints[pos];
            //     RespawnPlayer( handler, spawn.transform.position, spawn.transform.rotation );
            // }
        }

        public void OnPlayerLeft( PlayerInput player )
        {
            playerLeft?.Invoke();
            if( player.TryGetComponent<PlayerInputHandler>(out PlayerInputHandler handler ))
            {
                handler.OnPlayerDisconnected();
                players.Remove( handler );
            }
        }

        public void OnPlayerLeft( PlayerInputHandler handler )
        {
            playerLeft?.Invoke();
            handler.OnPlayerDisconnected();
            players.Remove( handler );
        }

        public void SpawnPlayers()
        {
            // Spawner[] spawners = FindObjectsOfType<Spawner>();
            List<Spawner> spawners = SpawnManager.instance.GetSpawnPoints();
            int i = 0;
            playerAlive = players.Count;
            foreach( PlayerInputHandler player in players )
            {
                RespawnPlayer( player, spawners[i].transform.position, spawners[i].transform.rotation );
                i = ++i % ( spawners.Count - 1 );
            }
        }

        public void RespawnPlayer( PlayerInputHandler player, Vector3 position, Quaternion rotation )
        {
            PlayerController _avatarController = Instantiate( avatar, position, rotation );
            player.AssignActiveController( _avatarController );
        }
    }
}

