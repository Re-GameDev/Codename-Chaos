using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Script : MonoBehaviour
{
	public Camera cameraViewRef;
	public Transform playerRotation;
	public GameObject playerHoldingGun;
	public GameObject WaterBullet;
	public GameObject SeedBullet;
	public GameObject TheAmmo;
	
	bool ShootPressed = false;
	int GunMode = 0;
	
	SpriteRenderer MySprite;
	
	private void Awake()
	{
		MySprite = GetComponent<SpriteRenderer>();
		MySprite.color = new Color(0.5f,0.5f,1,1);
	}
	
	private void LateUpdate()
	{
		Vector3 whereTheMouseIs = Input.mousePosition;
		whereTheMouseIs.z = 1;
		whereTheMouseIs = cameraViewRef.ScreenToWorldPoint(whereTheMouseIs);
		Vector3 shootDirVec = whereTheMouseIs - transform.position;
		float ShootAngle = Mathf.Atan2(shootDirVec.y, shootDirVec.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(ShootAngle, Vector3.forward);
		if (Vector3.Dot(shootDirVec, playerRotation.right) < 0)
		{
			MySprite.flipY = true;
		}
		else
		{
			MySprite.flipY = false;
		}
		if (ShootPressed)
		{
			ShootPressed = false;
			Rigidbody2D thePlayerVel = playerHoldingGun.GetComponent<Rigidbody2D>();
			if (GunMode == 0)
			{
				for (int i = 0; i < 3; i++)
				{
					GameObject bulletShot = Instantiate(WaterBullet, transform.position + (shootDirVec.normalized * 1.5f), transform.rotation);
					Rigidbody2D bulletRigidBody = bulletShot.GetComponent<Rigidbody2D>();
					float randShootAngle = ShootAngle + Random.Range(-2,2);
					bulletRigidBody.velocity = new Vector2(Mathf.Cos(randShootAngle * Mathf.Deg2Rad),Mathf.Sin(randShootAngle * Mathf.Deg2Rad)) * Random.Range(18,22);
					bulletRigidBody.velocity = bulletRigidBody.velocity + thePlayerVel.velocity;
				}
			}
			else if (GunMode == 1)
			{
					int CurrAmmo = TheAmmo.GetComponent<HUDScript>().SeedAmmo;
					//print($"ammo left {CurrAmmo}");
					if (CurrAmmo > 0)
					{
						TheAmmo.GetComponent<HUDScript>().SeedShot();
						GameObject bulletShot = Instantiate(SeedBullet, transform.position + (shootDirVec.normalized * 1.5f), transform.rotation);
						Rigidbody2D bulletRigidBody = bulletShot.GetComponent<Rigidbody2D>();
						bulletRigidBody.velocity = new Vector2(Mathf.Cos(ShootAngle * Mathf.Deg2Rad),Mathf.Sin(ShootAngle * Mathf.Deg2Rad)) * 30;
						bulletRigidBody.velocity = bulletRigidBody.velocity + thePlayerVel.velocity;
					}
			}
			else if (GunMode == 2)
			{
				print("Pew Pew");
			}
		}
	}
	
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			ShootPressed = true;
		}
		if (Input.GetMouseButtonDown(1))
		{
			GunMode++;
			if (GunMode > 2) {GunMode = 0;}
			//print($"Gun Mode Changed to {GunMode}");
			
			if (GunMode == 0)
			{
				MySprite.color = new Color(0.5f,0.5f,1,1);
			}
			else if (GunMode == 1)
			{
				MySprite.color = new Color(1,0.5f,0.5f,1);
			}
			else if (GunMode == 2)
			{
				MySprite.color = new Color(0.5f,1,0.5f,1);
			}
		}
	}
}
