using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Script : MonoBehaviour
{
	public Camera cameraViewRef;
	public Transform playerRotation;
	public GameObject WaterBullet;
	
	bool ShootPressed = false;
	
	SpriteRenderer MySprite;
	
	private void Awake()
	{
		MySprite = GetComponent<SpriteRenderer>();
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
			
			for (int i = 0; i < 3; i++)
			{
				GameObject bulletShot = Instantiate(WaterBullet, transform.position + (shootDirVec.normalized * 1.5f), transform.rotation);
				Rigidbody2D bulletRigidBody = bulletShot.GetComponent<Rigidbody2D>();
				float randShootAngle = ShootAngle + Random.Range(-2,2);
				bulletRigidBody.velocity = new Vector2(Mathf.Cos(randShootAngle * Mathf.Deg2Rad),Mathf.Sin(randShootAngle * Mathf.Deg2Rad)) * Random.Range(18,22);
			}
		}
	}
	
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			ShootPressed = true;
		}
	}
}
