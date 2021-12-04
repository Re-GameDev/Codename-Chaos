using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RageBall
{
    [RequireComponent(typeof(Rigidbody))]
    public class BallController : PlayerController
    {
        public virtual bool isGround { get; set; } = false;
        [SerializeField] float velocity = 1f;
        [SerializeField] float jumpForce = 2f;
        [SerializeField] float jumpRecharge = 1f;
        [SerializeField] LayerMask jumpMask;
        Rigidbody _rigidbody;
        bool canJumpAgain = true;  
        
        
        void Start() => _rigidbody = GetComponent<Rigidbody>();

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
