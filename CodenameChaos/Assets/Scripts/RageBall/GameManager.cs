using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace RageBall
{
    public class GameManager : GameEventListener
    {
        [SerializeField] GameEvent playerDeath;
        
        public enum GameState { GameMenu, Preround, InGame, Intermission, GameOver }
        private static GameManager _instance;
        public static GameManager Instance
        {
            get => _instance ?? new GameObject(nameof(GameManager)).AddComponent<GameManager>();
        }

        public bool waitForAllPlayers = false;

        public float preroundTimer = 5f;

        public float countdown = 5f;

        public float gameDuration = 60 * 10f; // 10 minutes

        public float endGameTimer = 10f;    // end game show leaderboards

        [SerializeField] GameEvent roundStart;
        [SerializeField] GameEvent roundEnd;
        [SerializeField] GameEvent newRound;
        [SerializeField] GameEvent gameOver;
        [SerializeField] PlayerController avatar; 

        GameState currentState = GameState.GameMenu;
        // HashSet<PlayerInputHandler> players = new HashSet<PlayerInputHandler>();
        [SerializeField] List<PlayerInputHandler> players = new List<PlayerInputHandler>(); // for testing purposes.



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
            players.Add( handler );
            if( currentState == GameState.GameMenu )
            {
                StartGame();
            }
        }

        public void OnPlayerLeft( PlayerInput player )
        {
            if( player.TryGetComponent<PlayerInputHandler>(out PlayerInputHandler handler ))
            {
                handler.OnPlayerDisconnected();
                players.Remove( handler );
            }
        }

        public void OnPlayerLeft( PlayerInputHandler handler )
        {
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
            Debug.Log("Spawning player", _avatarController);
            player.AssignActiveController( _avatarController );
        }

        public override void RaiseEvent()
        {
            // in this case here, a player has died...
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

            if( playerAlive == 1 )
            {
                currentState = GameState.Intermission;
                roundEnd?.Invoke();
            }
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

