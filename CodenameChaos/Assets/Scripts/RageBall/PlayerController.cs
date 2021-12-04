using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RageBall
{
    [DisallowMultipleComponent]
    public abstract class PlayerController : NetworkBehaviour
    {
        public virtual Vector2 move { get; set; } = Vector2.zero;
        public virtual Vector2 look { get; set; } = Vector2.zero;

        /// <summary>
        /// How fast should the user accelerate
        /// </summary>
        /// <value></value>
        public virtual float velocity { get; set; } = 5f;

        /// <summary>
        /// How fast should the user look around
        /// </summary>
        /// <value></value>
        public virtual float sensitivity { get; set; } = 5f;

        public virtual float mainTrigger { get; set; } = 0f;
        public virtual float altTrigger { get; set; } = 0f;

        public virtual void Movement( InputValue value ) => move = value.Get<Vector2>() * velocity;

        public virtual void Look( InputValue value ) => look = value.Get<Vector2>() * sensitivity;

        public virtual void MainTrigger( InputValue value ) => mainTrigger = value.Get<float>();

        public virtual void AltTrigger( InputValue value ) => altTrigger = value.Get<float>();

        public virtual void Jump( InputValue value ) { }

        public virtual void OnPlayerDisconnectControl() { }

        public virtual void OnPlayerConnectControl() { }
    }
}
