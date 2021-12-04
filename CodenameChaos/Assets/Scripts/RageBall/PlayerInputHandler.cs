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

        void Start()
        {
            GameManager.Instance.OnPlayerJoin( this );
        }

        public void AssignActiveController( PlayerController control )
        {
            // we can do something like destroy or disconnect
            if( _activePlayerController != null )
                _activePlayerController.OnPlayerDisconnectControl();
                
            _activePlayerController = control;
            _activePlayerController.OnPlayerConnectControl();
        }

        public void OnPlayerDisconnected()
        {
            if( _activePlayerController != null )
                _activePlayerController.OnPlayerDisconnectControl();
        }

        #region Input handlers

        void OnMovement( InputValue value ) => _activePlayerController?.Movement( value );

        void OnJump( InputValue value ) => _activePlayerController?.Jump( value );

        void OnCharge( InputValue value ) => _activePlayerController?.MainTrigger( value );

        void OnBrake( InputValue value ) => _activePlayerController?.AltTrigger( value );

        #endregion
    }   
}
