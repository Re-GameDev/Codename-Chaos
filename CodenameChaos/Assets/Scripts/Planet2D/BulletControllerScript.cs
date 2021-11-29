using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControllerScript : MonoBehaviour
{
    public float FallSpeed = 20;
	public GameObject PlantToGrow;
	float lifetime = 300;
	
	//float Deceleration = 0.01f;
	
	Rigidbody2D m_rigidbody;
	Collider2D m_collider;
    [SerializeField] MagnetScript currentAttractor;
	
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
		if (lifetime <= 0) 
		{
			print("I have lived too long");
			Destroy(gameObject);
		}
		
	}
	
	public void Attract(MagnetScript PlanetX)
	{
		Vector2 attractionDir = (Vector2)PlanetX.planetTransform.position - m_rigidbody.position;
		float radiusOfAttraction = PlanetX.effectionRadius;
		float DistanceOfPlanet = attractionDir.magnitude;
		float CurrGravityStrength = 1 - (DistanceOfPlanet/radiusOfAttraction);
		CurrGravityStrength = PlanetX.GravityStrength.Evaluate(CurrGravityStrength);
		if (CurrGravityStrength > 0)
		{
			m_rigidbody.AddForce(attractionDir.normalized * -PlanetX.gravity * 100 * Time.fixedDeltaTime * CurrGravityStrength);
		}
	}
	
	public void OnCollisionEnter2D(Collision2D ThingHit)
	{
		//print("I am dying!");
		if (PlantToGrow != null)
		{
			GameObject bulletShot = Instantiate(PlantToGrow, transform.position, transform.rotation);
		}
		else if (ThingHit.gameObject.layer == 9)
		{
			//print("I hit a plant");
            ThingHit.gameObject.GetComponent<TreeScript>().GetWatered();
		}
		Destroy(gameObject);
	}
}
