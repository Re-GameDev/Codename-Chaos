using UnityEngine;

public class Magnetized : MonoBehaviour
{
    [SerializeField] MagnetScript currentAttractor;
	
    public float WalkAcceleration = 25;
    public float Deceleration = 0.1f;
    public float WalkSpeed = 5;
    public float FallSpeed = 20;
    public float JumpStrength = 75;
	public float CurrDistToPlanet = 0;
	public GameObject TheAmmo;
	
	Vector3 RestartPos;
	bool foundFloor = false;	
	bool jumpPressed = false;
	bool doubleJumped = false;
	public bool FacingLeft = false;
	
	Transform m_transform;
	Collider2D m_collider;
	Rigidbody2D m_rigidbody;
	SpriteRenderer PlayerSprite;
	
	private void Awake()
	{
		m_transform = GetComponent<Transform>();
		m_collider = GetComponent<Collider2D>();
		m_rigidbody = GetComponent<Rigidbody2D>();
		PlayerSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
		RestartPos = m_transform.position;
	}
	
	private void Update()
	{
		RaycastHit2D FeetPlanted = Physics2D.Raycast(transform.position, new Vector2(-m_transform.up.x, -m_transform.up.y), 1.5f, LayerMask.GetMask("Planet"));
		if (FeetPlanted.collider != null)
		{
			doubleJumped = false;
			foundFloor = true;
		}
		else
		{
			foundFloor = false;
		}
		
		if (currentAttractor != null)
		{
			if (!currentAttractor.AttractedObjects.Contains(m_collider)) currentAttractor = null;
			RotateToCenter();
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
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && WalkVelocity > -WalkSpeed) // check for max velocity
		{
			WalkVelocity -= WalkAcceleration * Time.fixedDeltaTime;
			CurrentlyWalking = true;
			FacingLeft = true;
		}
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && WalkVelocity < WalkSpeed)
		{
			WalkVelocity += WalkAcceleration * Time.fixedDeltaTime;
			CurrentlyWalking = true;
			FacingLeft = false;
		}
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
		if (FacingLeft)
		{
			PlayerSprite.flipX = true;
		}
		else
		{
			PlayerSprite.flipX = false;
		}
		if (!CurrentlyWalking)
		{
			WalkVelocity *= 1 - Deceleration;
		}
		if (FallVelocity < -FallSpeed)
		{
			FallVelocity = -FallSpeed;
		}
		m_rigidbody.velocity = (WalkVelocity * rightHandRule) + (FallVelocity * upDir);
    }
	
	public void Attract(MagnetScript PlanetX)
	{
		Vector2 attractionDir = (Vector2)PlanetX.planetTransform.position - m_rigidbody.position;
		float radiusOfAttraction = PlanetX.effectionRadius;
		float DistanceOfPlanet = attractionDir.magnitude;
		float CurrGravityStrength = 1 - (DistanceOfPlanet/radiusOfAttraction);
		CurrDistToPlanet = CurrGravityStrength;
		CurrGravityStrength = PlanetX.GravityStrength.Evaluate(CurrGravityStrength);
		if (CurrGravityStrength > 0)
		{
			m_rigidbody.AddForce(attractionDir.normalized * -PlanetX.gravity * 100 * Time.fixedDeltaTime * CurrGravityStrength);
		}
		
		if (currentAttractor == null)
		{
			currentAttractor = PlanetX;
		}
		//add gravity velocity max
	}
	
	void RotateToCenter()
	{
		Vector2 distanceVector = (Vector2)currentAttractor.planetTransform.position - (Vector2)m_transform.position;
		float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
		m_transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
	}
	
	public void OnTriggerEnter2D(Collider2D ThingHit)
	{
		if (ThingHit.gameObject.layer == 10)
		{
			//print("I hit a fruit");
            ThingHit.gameObject.GetComponentInParent<TreeScript>().GatherFruit(ThingHit.gameObject);
			TheAmmo.GetComponent<HUDScript>().SeedAmmoPickup();
		}
	}
}
