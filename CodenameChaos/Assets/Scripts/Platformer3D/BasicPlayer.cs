using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BasicPlayer : MonoBehaviour
{
    public float MoveSpeed = 6.0f;
    public float JumpSpeed = 4.0f;
    public float SlopeLimit = 45; //degrees

    private Rigidbody body;
    private CapsuleCollider capsuleCollider;

    private bool isGrounded = false;
    private bool doJump = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        body = this.GetComponent<Rigidbody>();
        Assert.IsNotNull(body);
        capsuleCollider = this.GetComponent<CapsuleCollider>();
        Assert.IsNotNull(capsuleCollider);
    } 

    private void CheckGrounded()
    {
        this.isGrounded = false;
        float capsuleHeight = Mathf.Max(capsuleCollider.radius * 2, capsuleCollider.height);
        Vector3 capsuleBottom = transform.TransformPoint(capsuleCollider.center + (Vector3.down * (capsuleHeight / 2)));
        float radius = transform.TransformVector(capsuleCollider.radius, 0, 0).magnitude;

        Ray ray = new Ray(capsuleBottom + transform.up * 0.01f, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, radius * 5))
        {
            float normalAngle = Vector3.Angle(hit.normal, transform.up);
            if (normalAngle < SlopeLimit)
            {
                float maxDist = radius / Mathf.Cos(Mathf.Deg2Rad * normalAngle) - radius + 0.02f;
                if (hit.distance < maxDist)
                {
                    isGrounded = true;
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            doJump = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveVec = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            moveVec += new Vector3(0, 0, 1) * MoveSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveVec += new Vector3(0, 0, -1) * MoveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveVec += new Vector3(1, 0, 0) * MoveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveVec += new Vector3(-1, 0, 0) * MoveSpeed;
        }

        body.MovePosition(transform.position + (moveVec * Time.fixedDeltaTime));

        CheckGrounded();

        if (doJump)
        {
            body.AddForce(new Vector3(0, 1, 0) * JumpSpeed, ForceMode.VelocityChange);
            doJump = false;
        }
    }
}
