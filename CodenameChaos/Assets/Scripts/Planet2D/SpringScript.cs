using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringScript : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
    {
		//print("I hit something");
		if (collision.gameObject.layer == 7)
		{
			//print("It was the player!");
			Vector2 upDir = transform.up;
			Rigidbody2D playerBody = collision.gameObject.GetComponent<Rigidbody2D>();
			playerBody.velocity += upDir * 80.0f;
		}
    }
}
