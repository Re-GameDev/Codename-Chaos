using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour
{
	public int typeID = 0;
    float FallSpeed = 50;
	float lifetime = 1500;
	bool canDie = false;
	
	//float Deceleration = 0.01f;
	
	Rigidbody2D m_rigidbody;
	Collider2D m_collider;
	
	private void Awake()
	{
		m_rigidbody = GetComponent<Rigidbody2D>();
		m_collider = GetComponent<Collider2D>();
	}
	
	void FixedUpdate()
    {
		Vector2 upDir = transform.up;
		Vector2 rightHandRule = new Vector2(upDir.y, -upDir.x);
		float WalkVelocity = Vector2.Dot(m_rigidbody.velocity, rightHandRule);
		float FallVelocity = Vector2.Dot(m_rigidbody.velocity, upDir);
		
		//WalkVelocity *= 1 - Deceleration;
		
		if (FallVelocity < -FallSpeed)
		{
			FallVelocity = -FallSpeed;
		}
		
		m_rigidbody.velocity = (WalkVelocity * rightHandRule) + (FallVelocity * upDir);
		
		lifetime--;
		if (lifetime <= 0 && canDie) 
		{
			//print("I have lived too long");
			Destroy(gameObject);
		}
		canDie = true;
		
	}
	
	public void FruitSuction(Vector2 SuctionDirection)
	{				
		m_rigidbody.velocity = SuctionDirection;
		canDie = false;
	}
	
	public void FruitCollection()
	{
		Destroy(gameObject);
	}
}
