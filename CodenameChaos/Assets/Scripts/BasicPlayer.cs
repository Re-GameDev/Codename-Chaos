using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BasicPlayer : MonoBehaviour
{
    public float MoveForce = 1.0f;

    private Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody>();
        Assert.IsNotNull(body);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            body.AddForce(new Vector3(0, 0, 1) * MoveForce);
        }
        if (Input.GetKey(KeyCode.S))
        {
            body.AddForce(new Vector3(0, 0, -1) * MoveForce);
        }
        if (Input.GetKey(KeyCode.D))
        {
            body.AddForce(new Vector3(1, 0, 0) * MoveForce);
        }
        if (Input.GetKey(KeyCode.A))
        {
            body.AddForce(new Vector3(-1, 0, 0) * MoveForce);
        }
    }
}
