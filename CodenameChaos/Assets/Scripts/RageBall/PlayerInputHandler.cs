using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RageBall
{

    [DisallowMultipleComponent]
    public class PlayerInputHandler : MonoBehaviour
    {
        PlayerController _activePlayerController;
        public event Action onPlayerDeath;
        public event Action onPlayerSpawn;
        public bool IsAlive() => _activePlayerController != null;

#region unity event

        void Start()
        {
            GameManager.Instance.OnPlayerJoin( this );
            DontDestroyOnLoad(gameObject);
        }

        void OnDestroy()
        {
            if( GameManager.Exists() )
                GameManager.Instance.OnPlayerLeft( this );
        }

#endregion

#region private implementation
        void SubscribeEvent( PlayerController control )
        {
            UnsubscribeEvent();
            _activePlayerController = control;
            _activePlayerController.OnPlayerConnectControl();
            _activePlayerController.onPlayerDeath += HandlePlayerDeath;
            _activePlayerController.onPlayerSpawn += HandlePlayerSpawn;
        }

        void UnsubscribeEvent()
        {
            if( _activePlayerController == null ) return;
            _activePlayerController.onPlayerDeath -= HandlePlayerDeath;
            _activePlayerController.onPlayerSpawn -= HandlePlayerSpawn;
            _activePlayerController.OnPlayerDisconnectControl();
            _activePlayerController = null;
        }

#endregion

#region handlers

        void HandlePlayerDeath()
        {
            UnsubscribeEvent();
            onPlayerDeath?.Invoke();
        }

        void HandlePlayerSpawn() => onPlayerSpawn?.Invoke();
        
#endregion

#region public implementation

        public void AssignActiveController( PlayerController control ) => SubscribeEvent( control );
        public void OnPlayerDisconnected() => UnsubscribeEvent();

#endregion

#region Input handlers

        void OnMovement( InputValue value ) => _activePlayerController?.Movement( value );
        void OnJump( InputValue value ) => _activePlayerController?.Jump( value );
        void OnCharge( InputValue value ) => _activePlayerController?.MainTrigger( value );
        void OnBrake( InputValue value ) => _activePlayerController?.AltTrigger( value );

#endregion

    }   
}
