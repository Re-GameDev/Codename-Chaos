using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RageBall
{
    [RequireComponent(typeof(Rigidbody)), DisallowMultipleComponent]
    public class PlayerController : MonoBehaviour
    {
        Rigidbody _rigidbody;
        [SerializeField] float velocity = 1f;
        // Vector2 _direction = Vector2.zero;
        float x = 0f;
        float y = 0f;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            x = Input.GetAxis("Horizontal") * velocity;
            y = Input.GetAxis("Vertical") * velocity;
        }

        void FixedUpdate()
        {
            // Vector3 vel = _rigidbody.velocity;
            Vector3 vel = Vector3.zero;
            vel += Vector3.forward * y 
                + Vector3.right * x;
            vel = vel * Time.deltaTime; // it still uses fixedDeltaTime under the hood...
            // _rigidbody.velocity = vel;
            _rigidbody.AddForce( vel, ForceMode.Acceleration );
        }
    }
}
