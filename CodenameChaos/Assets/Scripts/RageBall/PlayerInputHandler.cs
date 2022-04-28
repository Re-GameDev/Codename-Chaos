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
        private PlayerController _activePlayerController;
        public event Action onPlayerDeath;
        public event Action onPlayerSpawn;
        public bool IsAlive() => _activePlayerController;

        #region unity event

        private void Start()
        {
            GameManager.OnPlayerJoin(this);
            DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy()
        {
            GameManager.OnPlayerLeft(this);
        }

        #endregion

        #region private implementation

        private void SubscribeEvent(PlayerController control)
        {
            UnsubscribeEvent();
            _activePlayerController = control;
            _activePlayerController.OnPlayerConnectControl();
            _activePlayerController.onPlayerDeath += HandlePlayerDeath;
            _activePlayerController.onPlayerSpawn += HandlePlayerSpawn;
        }

        private void UnsubscribeEvent()
        {
            if (!_activePlayerController) return;
            _activePlayerController.onPlayerDeath -= HandlePlayerDeath;
            _activePlayerController.onPlayerSpawn -= HandlePlayerSpawn;
            _activePlayerController.OnPlayerDisconnectControl();
            _activePlayerController = null;
        }

        #endregion

        #region handlers

        private void HandlePlayerDeath()
        {
            UnsubscribeEvent();
            onPlayerDeath?.Invoke();
        }

        private void HandlePlayerSpawn() => onPlayerSpawn?.Invoke();

        #endregion

        #region public implementation

        public void AssignActiveController(PlayerController control) => SubscribeEvent(control);
        public void OnPlayerDisconnected() => UnsubscribeEvent();

        #endregion

        #region Input handlers

        public virtual void OnMovement(InputValue value) => _activePlayerController?.Movement(value);
        public virtual void OnJump(InputValue value) => _activePlayerController?.Jump(value);
        public virtual void OnCharge(InputValue value) => _activePlayerController?.MainTrigger(value);
        public virtual void OnBrake(InputValue value) => _activePlayerController?.AltTrigger(value);

        #endregion
    }
}