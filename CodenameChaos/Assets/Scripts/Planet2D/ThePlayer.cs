using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePlayer : MonoBehaviour
{	
    public float WalkAcceleration = 25;
    public float Deceleration = 0.05f;
    public float WalkSpeed = 15;
    public float FallSpeed = 40;
    public float JumpStrength = 20;
	public float CurrDistToPlanet = 0;
	public float MyHSpeed = 0;
	public float MyVSpeed = 0;
	public GameObject TheAmmo;
	
	Vector3 RestartPos;
	bool foundFloor = false;	
	bool jumpPressed = false;
	bool doubleJumped = false;
	float maxRotationSpeed = 360.0f;
	public bool FacingLeft = false;
	
	Transform m_transform;
	Rigidbody2D m_rigidbody;
	SpriteRenderer PlayerSprite;
	
	private void Awake()
	{
		m_transform = GetComponent<Transform>();
		m_rigidbody = GetComponent<Rigidbody2D>();
		PlayerSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
		RestartPos = m_transform.position;
	}
	
	private void Update()
	{
		RaycastHit2D FeetPlanted = Physics2D.Raycast(transform.position, new Vector2(-m_transform.up.x, -m_transform.up.y), 1.5f, LayerMask.GetMask("Planet"));
		RaycastHit2D FeetOnHead = Physics2D.Raycast(transform.position, new Vector2(-m_transform.up.x, -m_transform.up.y), 1.5f, LayerMask.GetMask("NPC"));
		if (FeetPlanted.collider != null || FeetOnHead.collider != null)
		{
			doubleJumped = false;
			foundFloor = true;
		}
		else
		{
			foundFloor = false;
		}
		
		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
		{
			jumpPressed = true;
			//print("Jump");
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			m_transform.position = RestartPos;
			m_rigidbody.velocity = Vector3.zero;
		}
	}
	
	void FixedUpdate()
    {
		Vector2 upDir = m_transform.up;
		Vector2 rightHandRule = new Vector2(upDir.y, -upDir.x);
		Vector2 leftHandRule = new Vector2(-upDir.y, upDir.x);
		float WalkVelocity = Vector2.Dot(m_rigidbody.velocity, rightHandRule);
		float FallVelocity = Vector2.Dot(m_rigidbody.velocity, upDir);
		bool CurrentlyWalking = false;
		bool inSpace = this.GetComponent<Magnetized>().inSpace;
        
		//move/rotate left
		if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))) // check for max velocity
		{
			Vector3 tmpVect3 = new Vector3(0, 0, 0.01f);
			if (inSpace)
			{
				m_rigidbody.AddTorque(m_rigidbody.inertia * 10 * Mathf.Deg2Rad, ForceMode2D.Impulse);
			}
			else
			{
				WalkVelocity -= WalkAcceleration * Time.fixedDeltaTime;
			}
			CurrentlyWalking = true;
			FacingLeft = true;
		}
		
        //move/rotate right
		if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
		{
			if (inSpace)
			{
				m_rigidbody.AddTorque(m_rigidbody.inertia * -10 * Mathf.Deg2Rad, ForceMode2D.Impulse);
			}
			else
			{
				WalkVelocity += WalkAcceleration * Time.fixedDeltaTime;
			}
			CurrentlyWalking = true;
			FacingLeft = false;
		}
		
		//handle the jump
        if (jumpPressed)
		{
			jumpPressed = false;

			if (foundFloor)
			{
				FallVelocity += JumpStrength;
			}
			else if (!doubleJumped)
			{
				if (FallVelocity < 0)
				{
					FallVelocity = 0;
				}
				doubleJumped = true;
				FallVelocity += JumpStrength/2;
			}
		}
		
		//face in the right direction
		if (FacingLeft)
		{
			PlayerSprite.flipX = true;
		}
		else
		{
			PlayerSprite.flipX = false;
		}
		
		//Deceleration section
		if (!CurrentlyWalking)
		{
			if (inSpace)
			{
				m_rigidbody.angularVelocity = m_rigidbody.angularVelocity * 0.95f;
			}
			else
			{
				WalkVelocity *= 1 - Deceleration;
			}
		}
		
		//clamp your speeds
		if (!inSpace)
		{
			if (FallVelocity < -FallSpeed) { FallVelocity = -FallSpeed; }
			if (WalkVelocity > WalkSpeed) { WalkVelocity = WalkSpeed; }
			if (WalkVelocity < -WalkSpeed) { WalkVelocity = -WalkSpeed; }
		}
		m_rigidbody.velocity = (WalkVelocity * rightHandRule) + (FallVelocity * upDir);
		
		MyHSpeed = WalkVelocity;
		MyVSpeed = FallVelocity;
		if (inSpace)
		{
			if (m_rigidbody.angularVelocity > maxRotationSpeed) {m_rigidbody.angularVelocity = maxRotationSpeed;}
			if (m_rigidbody.angularVelocity < -maxRotationSpeed) {m_rigidbody.angularVelocity = -maxRotationSpeed;}
		}
    }
	
	//public void OnCollisionEnter2D(Collision2D ThingHit)
	//{
	//	if (ThingHit.gameObject.layer == 10)
	//	{
	//		if (ThingHit.gameObject.activeInHierarchy)
	//		{
	//			int amountGained = ThingHit.gameObject.GetComponent<FruitScript>().typeID;
	//			ThingHit.gameObject.GetComponent<FruitScript>().FruitCollection();
	//			TheAmmo.GetComponent<HUDScript>().SeedAmmoPickup(amountGained);
	//		}
	//	}
	//}
}
