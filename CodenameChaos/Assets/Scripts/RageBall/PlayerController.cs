using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RageBall
{
    [RequireComponent(typeof(Rigidbody)), DisallowMultipleComponent]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float velocity = 1f;
        [SerializeField] float jumpForce = 2f;
        [SerializeField] float jumpRecharge = 1f;
        [SerializeField] LayerMask jumpMask;
        Rigidbody _rigidbody;
        bool _isGround = false;
        float x = 0f;
        float y = 0f;

        bool canJumpAgain = true;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        void OnEnable()
        {
            canJumpAgain = true;
        }

        void Update()
        {
            x = Input.GetAxis("Horizontal") * velocity;
            y = Input.GetAxis("Vertical") * velocity;
            if( _isGround && Input.GetButtonDown("Jump") )
            {
                Jump();
                _isGround = false;
                canJumpAgain = false;
                Invoke(nameof( CanJumpAgain ), jumpRecharge);
            }
        }

        void Jump()
        {
            _rigidbody.AddForce( -Physics.gravity * jumpForce, ForceMode.Impulse );
        }

        void CanJumpAgain()
        {
            canJumpAgain = true;
        }

        void FixedUpdate()
        {
            Vector3 vel = Vector3.zero;
            vel += Vector3.right * y 
                + Vector3.back * x;
            vel = vel * Time.deltaTime; // it still uses fixedDeltaTime under the hood...
            _rigidbody.AddTorque( vel, ForceMode.Acceleration );

            if( canJumpAgain )
                _isGround = Physics.SphereCast( transform.position, 1f, Physics.gravity, out RaycastHit hit, 1f, jumpMask);
        }
    }
}
