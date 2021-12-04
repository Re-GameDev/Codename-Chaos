using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaarthEventScript : MonoBehaviour
{
	
	public CircleCollider2D PlanetCircleCollider;
	public GameObject PlantToGrow;
	public GameObject CloudToSpawn;
	float radiusOfPlanet;
	float timeToSpawnPlant = 0;
    
	void Awake()
    {
        radiusOfPlanet = PlanetCircleCollider.radius;
		timeToSpawnPlant = Random.Range(30.0f, 120.0f);
		
		int numToSpawn = Random.Range(10, 20);
		for (int i = 0; i < numToSpawn; i++)
		{
			float spawnAngle = Random.Range(0.0f, 360.0f);
			float randCloudDist = Random.Range(10.0f, 45.0f);
			Vector3 spawnPos = new Vector3(Mathf.Cos(spawnAngle)*(radiusOfPlanet + randCloudDist), Mathf.Sin(spawnAngle)*(radiusOfPlanet + randCloudDist), 0);
			Instantiate(CloudToSpawn, spawnPos, transform.rotation);
		}
		
		numToSpawn = Random.Range(3, 6);
		for (int i = 0; i < numToSpawn; i++)
		{
			float spawnAngle = Random.Range(0.0f, 360.0f);
			Vector3 spawnPos = new Vector3(Mathf.Cos(spawnAngle)*radiusOfPlanet, Mathf.Sin(spawnAngle)*radiusOfPlanet, 0);
			Instantiate(PlantToGrow, spawnPos, transform.rotation);
		}
    }
	
	void FixedUpdate()
    {
		if (timeToSpawnPlant <= 0)
		{
			timeToSpawnPlant = Random.Range(30.0f, 120.0f);
			int numToSpawn = Random.Range(0, 3);
			
			for (int i = 0; i < numToSpawn; i++)
			{
				float spawnAngle = Random.Range(0.0f, 360.0f);
				Vector3 spawnPos = new Vector3(Mathf.Cos(spawnAngle)*radiusOfPlanet, Mathf.Sin(spawnAngle)*radiusOfPlanet, 0);
				Instantiate(PlantToGrow, spawnPos, transform.rotation);
			}
		}
		else
		{
			timeToSpawnPlant -= Time.fixedDeltaTime;
		}
    }
}
