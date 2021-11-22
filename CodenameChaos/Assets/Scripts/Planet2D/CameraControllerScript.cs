using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{
	public Transform PlayerTransform;
	public Magnetized PlayerCameraZoom;
	
	Camera TheCamera;
    // Update is called once per frame
    private void Awake()
	{
		TheCamera = GetComponent<Camera>();
	}
	
	void LateUpdate()
    {
        transform.position = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, transform.position.z);
		transform.rotation = Quaternion.Euler(0,0, Mathf.Atan2(PlayerTransform.up.y,PlayerTransform.up.x) * Mathf.Rad2Deg - 90);
		
		TheCamera.orthographicSize = Mathf.Lerp(20, 5, PlayerCameraZoom.CurrDistToPlanet);
    }
}