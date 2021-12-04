using UnityEngine;

public class Magnetized : MonoBehaviour
{
    [SerializeField] MagnetScript currentAttractor;
	
	public float CurrDistToPlanet = 0;
	[HideInInspector] public bool inSpace = false;
    public bool ShouldRotate = true;
    public bool Shouldfall = true;
	float inSpaceTimer = 0; //seconds
	
	Transform m_transform;
	Rigidbody2D m_rigidbody;
	
	private void Awake()
	{
		m_transform = GetComponent<Transform>();
		m_rigidbody = GetComponent<Rigidbody2D>();
	}

    public void FixedUpdate()
    {
        if (currentAttractor != null)
        {
            inSpaceTimer = 0.1f;
            if (inSpace)
            {
                inSpace = false;
                m_rigidbody.freezeRotation = true;
            }
        }

        if (inSpaceTimer > 0 && currentAttractor == null)
        {
            inSpaceTimer -= Time.fixedDeltaTime;
            if (inSpaceTimer <= 0)
            {
                inSpace = true;
                m_rigidbody.freezeRotation = false;
            }
        }

        //clear this so we can detect if Attract and/or RotateToCenter get called between now and next FixedUpdate
        currentAttractor = null;
    }
		
	public void Attract(MagnetScript Planet)
    {
        currentAttractor = Planet;
		
		if (Shouldfall)
        {
			Vector2 attractionDir = (Vector2)Planet.planetTransform.position - m_rigidbody.position;
			float radiusOfAttraction = Planet.effectionRadius;
			float DistanceOfPlanet = attractionDir.magnitude;
			float CurrGravityStrength = 1 - (DistanceOfPlanet/radiusOfAttraction);
			CurrDistToPlanet = CurrGravityStrength;
			CurrGravityStrength = Planet.GravityStrength.Evaluate(CurrGravityStrength);
			if (CurrGravityStrength > 0)
			{
				m_rigidbody.AddForce(attractionDir.normalized * -Planet.gravity * 100 * Time.fixedDeltaTime * CurrGravityStrength);
			}
		}
	}
	
	public void RotateToCenter(MagnetScript Planet)
    {
        currentAttractor = Planet;

        if (ShouldRotate)
        {
            Vector2 distanceVector = (Vector2)currentAttractor.planetTransform.position - (Vector2)m_transform.position;
            float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
            m_transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        }
	}
}
