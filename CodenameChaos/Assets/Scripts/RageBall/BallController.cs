using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace RageBall
{
    [RequireComponent(typeof(Rigidbody))]
    public class BallController : PlayerController
    {
        public virtual bool isGround { get; set; } = false;
        // [SerializeField] float velocity = 1f;
        [SerializeField] float jumpForce = 2f;
        [SerializeField] float jumpRecharge = 1f;
        [SerializeField] LayerMask jumpMask;
        Rigidbody _rigidbody;
        Health _health;
        bool canJumpAgain = true;
        // bool isStunned = false;

        [SerializeField] UnityEvent onStun = new UnityEvent();
        [SerializeField] UnityEvent onRecovery = new UnityEvent(); 
        
        void Start() 
        {
            _rigidbody = GetComponent<Rigidbody>();
            if( TryGetComponent<Health>(out Health health ))
            {
                _health = health;
                _health.OnHealthDepleted += HandleHealthDepleted;
            }
            OnSpawn();
        }

        public override void OnDestroy()
        {
            if( _health != null )
                _health.OnHealthDepleted -= HandleHealthDepleted;
        }

        private void HandleHealthDepleted(object sender) => OnDeath();

        void OnEnable() => canJumpAgain = true;

        public override void Jump( InputValue value )
        {
            if( !isGround || !canJumpAgain )
                return;
            
            Jump();
            isGround = false;
            canJumpAgain = false;
            Invoke( nameof( CanJumpAgain ), jumpRecharge );
        }

        void Jump()
        {
            _rigidbody.AddForce( -Physics.gravity * jumpForce, ForceMode.Impulse );
            // we can then add animations here
        }

        void CanJumpAgain() => canJumpAgain = true;

        // wonder how I can do the speedboost?
        public override void MainTrigger(InputValue value)
        {
            float t = value.Get<float>();
            // move.y = 
        }

        void FixedUpdate()
        {
            Vector3 vel = Vector3.zero;
            vel += Vector3.right * move.y 
                + Vector3.back * move.x;
            vel = vel * Time.deltaTime; // it still uses fixedDeltaTime under the hood...
            _rigidbody.AddTorque( vel, ForceMode.Acceleration );
            isGround = Physics.SphereCast( transform.position, 1f, Physics.gravity, out RaycastHit hit, 1f, jumpMask);
        }
    }
}
