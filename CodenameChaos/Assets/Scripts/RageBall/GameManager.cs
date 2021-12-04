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
        [SerializeField] GameEvent playerDeath;
        
        public enum GameState { GameMenu, Preround, InGame, Intermission, GameOver }
        private static GameManager _instance;
        public static GameManager Instance
        {
            get => _instance ?? new GameObject(nameof(GameManager)).AddComponent<GameManager>();
        }

        public static bool Exists() => _instance != null;

        public bool waitForAllPlayers = false;

        public float preroundTimer = 5f;

        public float countdown = 5f;

        public float gameDuration = 60 * 10f; // 10 minutes

        public float endGameTimer = 10f;    // end game show leaderboards

        [SerializeField] GameEvent roundStart;
        [SerializeField] GameEvent roundEnd;
        [SerializeField] GameEvent newRound;
        [SerializeField] GameEvent gameOver;
        [SerializeField] GameEvent playerJoins;
        [SerializeField] GameEvent playerLeft;
        [SerializeField] PlayerController avatar; 
        // [SerializeField] GameManagerEvent onPlayerJoined;
        // [SerializeField] GameManagerEvent onPlayerLeft;

        GameState currentState = GameState.GameMenu;



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

        void OnSceneLoaded( Scene scene, LoadSceneMode mode )
        {
            Debug.Log("Scene loaded!");
            if( players.Count > 0 )
                StartGame();
        }

        public void OnPlayerJoin( PlayerInput player )
        {
            Debug.Log("A player has joiend");
            if( player.TryGetComponent<PlayerInputHandler>(out PlayerInputHandler handler ))
            {
                players.Add( handler );
                if( currentState == GameState.GameMenu )
                {
                    StartGame();
                }
            }
        }

        public void OnPlayerJoin( PlayerInputHandler handler )
        {
            playerJoins?.Invoke();
            players.Add( handler );
            if( currentState == GameState.GameMenu )
            {
                StartGame();
            }
            else if ( currentState == GameState.Preround )
            {
                var spawnPoints = SpawnManager.instance.GetSpawnPoints();
                int pos = players.Count % spawnPoints.Count;
                Spawner spawn = spawnPoints[pos];
                RespawnPlayer( handler, spawn.transform.position, spawn.transform.rotation );
            }
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
            Spawner[] spawners = FindObjectsOfType<Spawner>();
            int i = 0;
            playerAlive = players.Count;
            foreach( PlayerInputHandler player in players )
            {
                RespawnPlayer( player, spawners[i].transform.position, spawners[i].transform.rotation );
                i = ++i % ( spawners.Length - 1 );
            }
        }

        public void RespawnPlayer( PlayerInputHandler player, Vector3 position, Quaternion rotation )
        {
            PlayerController _avatarController = Instantiate( avatar, position, rotation );
            player.AssignActiveController( _avatarController );
        }

        // public override void RaiseEvent()
        // {
        //     // in this case here, a player has died...
        //     playerAlive--;
        // }

        public void PlayerDied()
        {
            if( currentState == GameState.InGame )
                playerAlive--;
        }

        /// <summary>
        /// Start the game
        /// </summary>
        public void StartGame()
        {
            _timer = 0;
            currentState = GameState.Preround;
            newRound?.Invoke();
            SpawnPlayers();
        }

        public void EndGame()
        {
            _timer = 0;
            currentState = GameState.GameOver;
            roundEnd?.Invoke();
            gameOver?.Invoke();
        }

        void Update()
        {
            switch( currentState )
            {
                case GameState.Preround : Preround(); break;
                case GameState.InGame : InGame(); break;
                case GameState.Intermission : Intermission(); break;
            }
        }

        float _timer = 0f;
        void Preround()
        {
            _timer += Time.deltaTime;
            if( _timer >= preroundTimer )
            {
                _timer = 0;
                currentState = GameState.InGame;
                roundStart?.Invoke();
            }
        }

        void InGame()
        {
            _timer += Time.deltaTime;
            if( _timer >= gameDuration )
            {
                _timer = 0;
                currentState = GameState.Intermission;
                roundEnd?.Invoke();
            }

            // if( playerAlive == 1 )
            // {
            //     currentState = GameState.Intermission;
            //     roundEnd?.Invoke();
            // }
        }

        void Intermission()
        {
            _timer += Time.deltaTime;
            if( _timer >= endGameTimer )
            {
                _timer = 0;
                currentState = GameState.Preround;
                newRound?.Invoke();
                // wonder if we can just reload the map here?
                SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex ); 
            }
        }
    }
}

