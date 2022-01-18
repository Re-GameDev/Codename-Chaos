// Creation Date: January 16 2022
// Author(s): rhsha

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class OtterRayCast : MonoBehaviour
{
	void Awake()
	{
		
	}

	void Start()
	{
		
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
        {

			LayerMask mask = LayerMask.GetMask("Button");

			RaycastHit hit;
			// Does the ray intersect any objects excluding the player layer
			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, mask))
			{
				//Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
				//Debug.Log("Hit");
				hit.collider.gameObject.GetComponent<OtterButton>().Click();
			}
			else
			{
				//Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
				//Debug.Log("No Hit");
			}
		}
	}

	void FixedUpdate()
	{
		
	}
}
