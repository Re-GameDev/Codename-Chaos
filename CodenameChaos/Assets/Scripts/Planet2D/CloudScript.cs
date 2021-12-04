using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
	public Sprite[] CloudSprites; //for each PlantVariant
	public GameObject WaterBullet;
	float waterPercent = 0f;
	float extraRainTimer = 0f;
	float timeBetweenDrops = 0.1f;
	float mySpeed = 0.01f;
	bool raining = false;
	SpriteRenderer MySprite;
	
	void Awake()
    {
        //get info and set sprites
		MySprite = GetComponent<SpriteRenderer>();
		mySpeed = Random.Range(-0.05f,0.05f);
    }
	
    void FixedUpdate()
    {
		if (waterPercent > 100f) 
		{ 
			waterPercent = 100f;
			raining = true;
			MySprite.sprite = CloudSprites[1];
		}
		if (waterPercent > 0f) { waterPercent -= Time.fixedDeltaTime; }
		if (waterPercent < 0f) 
		{ 
			waterPercent = 0f; 
			raining = false;
			MySprite.sprite = CloudSprites[0];
		}
		if (extraRainTimer > 0f) { extraRainTimer -= Time.fixedDeltaTime; }
		if (timeBetweenDrops > 0f) { timeBetweenDrops -= Time.fixedDeltaTime; }
		
		Vector2 upDir = transform.up;
		Vector2 rightHandRule = new Vector2(upDir.y, -upDir.x);
		//move the cloud slowly
		//rain if able
		if (raining && timeBetweenDrops <= 0)
		{
			timeBetweenDrops = 0.1f;
			if (extraRainTimer > 0) { timeBetweenDrops = 0.01f; }
			
			float randOffset = Random.Range(-2f,2f);
			Vector2 randSpawnPos = new Vector2(transform.position.x, transform.position.y) + upDir * -0.2f + rightHandRule * randOffset;
			GameObject rainDrop = Instantiate(WaterBullet, new Vector3(randSpawnPos.x, randSpawnPos.y, 0), transform.rotation);
		}
		
		float colorSatEffect = 1.0f - waterPercent/200f;
		MySprite.color = new Color(colorSatEffect, colorSatEffect, colorSatEffect,1);
		
		Vector2 newMovePos = new Vector2(transform.position.x, transform.position.y) + rightHandRule * mySpeed;
		transform.position = new Vector3(newMovePos.x, newMovePos.y, 0);
    }
	
	public void OnCollisionEnter2D(Collision2D ThingHit)
	{
		if (ThingHit.gameObject.layer == 7) // player
		{
			if (ThingHit.gameObject.activeInHierarchy)
			{
				extraRainTimer = 5f;
			}
		}
		else if (ThingHit.gameObject.layer == 8) // projectile
		{
			if (ThingHit.gameObject.activeInHierarchy)
			{
				waterPercent++; 
			}
		}
	}
}
