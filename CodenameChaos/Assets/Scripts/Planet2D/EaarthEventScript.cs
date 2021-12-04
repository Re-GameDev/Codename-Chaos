using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaarthEventScript : MonoBehaviour
{
	
	public CircleCollider2D PlanetCircleCollider;
	public GameObject PlantToGrow;
	float radiusOfPlanet;
	float timeToSpawnPlant = 0;
    
	void Awake()
    {
        radiusOfPlanet = PlanetCircleCollider.radius;
		timeToSpawnPlant = Random.Range(30.0f, 120.0f);
    }
	
	void FixedUpdate()
    {
		if (timeToSpawnPlant <= 0)
		{
			timeToSpawnPlant = Random.Range(30.0f, 120.0f);
			float spawnAngle = Random.Range(0.0f, 360.0f);
			Vector3 spawnPos = new Vector3(Mathf.Cos(spawnAngle)*radiusOfPlanet, Mathf.Sin(spawnAngle)*radiusOfPlanet, 0);
			Instantiate(PlantToGrow, spawnPos, transform.rotation);
		}
		else
		{
			timeToSpawnPlant -= Time.fixedDeltaTime;
		}
    }
}
