using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControllerScript : MonoBehaviour
{
	public GameObject PlantToGrow;
	public int typeID = 0;
    float FallSpeed = 50;
	float lifetime = 500;
    public Transform SpriteTransform;
	
	//float Deceleration = 0.01f;
	
	Rigidbody2D m_rigidbody;
	Collider2D m_collider;
	
	private void Awake()
	{
		m_rigidbody = GetComponent<Rigidbody2D>();
		m_collider = GetComponent<Collider2D>();
	}

    private void Update()
    {
        float headingDir = Mathf.Atan2(m_rigidbody.velocity.y, m_rigidbody.velocity.x) * Mathf.Rad2Deg;
        float headingSpeed = m_rigidbody.velocity.magnitude;
        headingSpeed = Mathf.Clamp(headingSpeed / 600, 0, 1);

        SpriteTransform.localScale = new Vector3(Mathf.Lerp(1.0f, 10.0f, headingSpeed), SpriteTransform.localScale.y, SpriteTransform.localScale.z);
        SpriteTransform.rotation = Quaternion.AngleAxis(headingDir, Vector3.forward);
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
		if (lifetime <= 0) 
		{
			//print("I have lived too long");
			Destroy(gameObject);
		}
		
	}
	
	public void OnTriggerEnter2D(Collider2D ThingHit)
	{
		//print("I am dying!");
		if (ThingHit.gameObject.layer == 9 && typeID == 2)
		{
			//print("I hit a plant");
            ThingHit.gameObject.GetComponentInParent<TreeScript>().GetWatered();
		}
		Destroy(gameObject);
	}
	
	public void OnCollisionEnter2D(Collision2D ThingHit)
	{
		//NOTES
		//Need animation for death
		//print("I am dying!");
		if (PlantToGrow != null && typeID == 1)
		{
			GameObject bulletShot = Instantiate(PlantToGrow, transform.position, transform.rotation);
		}
		Destroy(gameObject);
	}
}
