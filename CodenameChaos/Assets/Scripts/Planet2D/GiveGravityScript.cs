using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveGravityScript : MonoBehaviour
{
	/*public void Attract(MagnetScript PlanetX)
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
	}*/
}
