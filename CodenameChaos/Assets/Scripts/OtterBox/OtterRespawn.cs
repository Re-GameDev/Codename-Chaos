// Creation Date: January 17 2022
// Author(s): rhsha

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class OtterRespawn : MonoBehaviour
{

	public GameObject spawn;

	void Awake()
	{
		
	}

	void Start()
	{
		
	}

	void Update()
	{
		
	}

	void FixedUpdate()
	{
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
        {
			other.gameObject.GetComponent<SC_FPSController>().WarpPlayer(spawn.transform.position);
		}
    }
}
