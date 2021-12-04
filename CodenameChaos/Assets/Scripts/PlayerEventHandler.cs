using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RageBall
{
    [RequireComponent(typeof(PlayerInputManager)), DisallowMultipleComponent]
    public class PlayerEventHandler : MonoBehaviour
    {
        void Start()
        {
            PlayerInputManager.instance.onPlayerJoined += HandlePlayerJoined;
            PlayerInputManager.instance.onPlayerLeft += HandlePlayerLeft;
        }

        void OnDestroy() 
        {
            if( PlayerInputManager.instance != null )
            {
                PlayerInputManager.instance.onPlayerJoined -= HandlePlayerJoined;
                PlayerInputManager.instance.onPlayerLeft -= HandlePlayerLeft;
            }    
        }

        private void HandlePlayerLeft(PlayerInput obj)
        {
            GameManager.Instance.OnPlayerLeft( obj );
        }

        private void HandlePlayerJoined(PlayerInput obj)
        {
            GameManager.Instance.OnPlayerJoin( obj );
        }
    }
}
