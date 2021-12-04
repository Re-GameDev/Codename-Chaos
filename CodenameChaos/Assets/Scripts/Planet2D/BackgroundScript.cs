// Creation Date: December 03 2021
// Author(s): Taylor

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BackgroundScript : MonoBehaviour
{
    public GameObject CameraObject;
    
    void Start()
    {
        Assert.IsNotNull(CameraObject);
        Assert.IsNotNull(CameraObject.GetComponent<Camera>());
    }
    
    void LateUpdate()
    {
        Camera camera = CameraObject.GetComponent<Camera>();
        transform.position = new Vector3(CameraObject.transform.position.x, CameraObject.transform.position.y, 0);
        transform.rotation = CameraObject.transform.rotation;
        float cameraHeight = 2.0f * camera.orthographicSize * 100;
        transform.localScale = new Vector3(
            cameraHeight * camera.aspect,
            cameraHeight,
            1
        );        
    }
}
