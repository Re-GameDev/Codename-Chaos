using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BasicCamera : MonoBehaviour
{
    public Transform PlayerTransform = null;

    private Transform transform;
    private Vector3 initialOffset = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        this.transform = GetComponent<Transform>();
        Assert.IsNotNull(this.transform);
        if (PlayerTransform != null)
        {
            this.initialOffset = this.transform.position - PlayerTransform.position;
        }
        else
        {
            Debug.LogWarning("WARNING: PlayerTransform was not set on BasicCamera script!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerTransform != null)
        {
            this.transform.position = PlayerTransform.position + this.initialOffset;
        }
    }
}
