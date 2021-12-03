using UnityEngine;

public class Magnetized : MonoBehaviour
{
    [SerializeField] MagnetScript currentAttractor;
	
	public float CurrDistToPlanet = 0;
	[HideInInspector] public bool inSpace = false;
	float inSpaceTimer = 3;
	
	Transform m_transform;
	Rigidbody2D m_rigidbody;
	
	private void Awake()
	{
		m_transform = GetComponent<Transform>();
		m_rigidbody = GetComponent<Rigidbody2D>();
	}
	
	private void Update()
	{
		if (inSpaceTimer > 0)
		{
			inSpaceTimer--;
			inSpace = false;
			m_rigidbody.freezeRotation = true;
		}
		else if (inSpaceTimer <= 0)
		{
			inSpace = true;
			m_rigidbody.freezeRotation = false;
		}
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
	}
	
	public void RotateToCenter(MagnetScript PlanetX)
	{
		if (currentAttractor == null)
		{
			currentAttractor = PlanetX;
		}
		Vector2 distanceVector = (Vector2)currentAttractor.planetTransform.position - (Vector2)m_transform.position;
		float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
		m_transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
		inSpaceTimer = 3;
	}
}
