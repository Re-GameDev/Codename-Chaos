using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gun_Script : MonoBehaviour
{
	public Camera cameraViewRef;
	public Transform playerRotation;
	public GameObject playerHoldingGun;
	public GameObject WaterBullet;
	public GameObject SeedBullet;
	public GameObject TheAmmo;
	public GameObject TheCharge;
	public GameObject TheWater;
    public List<Collider2D> SuctionObjects = new List<Collider2D>();
    public LayerMask SeedLayer;
    public float SuctionRange = 15;
	
	float chargeAmount = 0;
	float maxChargeAmount = 100;
	float chargeRate = 0.5f;
	float chargePush = 0.15f;
	int timeCharging = 0;
	float suctionSize = 15;
	float suctionSpeed = 6;
	
	bool ShootPressed = false;
	bool ShootCharging = false;
	int GunMode = 0;
	
	SpriteRenderer MySprite;
	
	private void Awake()
	{
		MySprite = GetComponent<SpriteRenderer>();
		MySprite.color = new Color(0.5f,0.5f,1,1);
	}
	
	void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, SuctionRange);
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
		
		if (ShootCharging)
		{
			if (GunMode == 0)
			{
				//NOTES
				//Need animation when ammo depleted
			}
			else if (GunMode == 1)
			{
				//NOTES
				SuctionObjects = Physics2D.OverlapCircleAll(transform.position, SuctionRange, SeedLayer).ToList();
				for (int i = 0; i < SuctionObjects.Count; i++)
				{
					GameObject fruitFound = SuctionObjects[i].gameObject;
					if (fruitFound.activeInHierarchy)
					{
						Vector3 fruitDir = fruitFound.transform.position - transform.position;
						float fruitAngle = Mathf.Atan2(fruitDir.y, fruitDir.x) * Mathf.Rad2Deg;
						if (fruitAngle < (ShootAngle + suctionSize) && fruitAngle > (ShootAngle - suctionSize))
						{
							if (SuctionObjects[i].isTrigger)
							{
								fruitFound.GetComponentInParent<TreeScript>().GatherFruit(fruitFound);
							}
							else
							{
								Vector2 suctionEffect= new Vector2(Mathf.Cos(fruitAngle * Mathf.Deg2Rad),Mathf.Sin(fruitAngle * Mathf.Deg2Rad)) * suctionSpeed;
								fruitFound.GetComponent<FruitScript>().FruitSuction(-suctionEffect);
							}
						}
					}
				}
			}
			else if (GunMode == 2)
			{
				//NOTES
				//future, need to push you based on how long you have charged the shot (up to 100% only)
				chargeAmount = chargeAmount + chargeRate;
				if (chargeAmount > maxChargeAmount) {chargeAmount = maxChargeAmount;}
				TheCharge.GetComponent<HUDScript>().SeedAmmoPickup(chargeRate);
			}
		}
		
		if (ShootPressed || (ShootCharging && GunMode == 0))
		{
			ShootPressed = false;
			Rigidbody2D thePlayerVel = playerHoldingGun.GetComponent<Rigidbody2D>();
			if (GunMode == 0)
			{
				//NOTES
				//Need animation when ammo depleted
				//need to allow for upgrades to how many droplets are fired per shot
				for (int i = 0; i < 1; i++)
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
				//NOTES
				//Need fizzle when ammo empty
				//Need only shoot when released but when held sucks in nearby fruit
					float CurrAmmo = TheAmmo.GetComponent<HUDScript>().DisplayValue;
					//print($"ammo left {CurrAmmo}");
					if (CurrAmmo > 0)
					{
						TheAmmo.GetComponent<HUDScript>().SeedShot(1);
						GameObject bulletShot = Instantiate(SeedBullet, transform.position + (shootDirVec.normalized * 1.5f), transform.rotation);
						Rigidbody2D bulletRigidBody = bulletShot.GetComponent<Rigidbody2D>();
						bulletRigidBody.velocity = new Vector2(Mathf.Cos(ShootAngle * Mathf.Deg2Rad),Mathf.Sin(ShootAngle * Mathf.Deg2Rad)) * 30;
						bulletRigidBody.velocity = bulletRigidBody.velocity + thePlayerVel.velocity;
					}
			}
			else if (GunMode == 2)
			{
				//NOTES
				//future, need to push you based on how long you have charged the shot (up to 100% only)
				//burst from shot can push things away eventually (and maybe make fruit fall)
				//Need only shoot when released but when held charges how big the push is
				float CurrCharge = TheCharge.GetComponent<HUDScript>().DisplayValue;
				thePlayerVel.velocity += new Vector2(-Mathf.Cos(ShootAngle * Mathf.Deg2Rad),-Mathf.Sin(ShootAngle * Mathf.Deg2Rad)) * chargePush * CurrCharge;
				TheCharge.GetComponent<HUDScript>().ChargeShot();
			}
		}
	}
	
	private void Update()
    {
        ShootCharging = false;
        if (Input.GetMouseButton(0))
		{
			ShootCharging = true;
			timeCharging++;
			if (timeCharging > 999) {timeCharging = 999;}
		}

        if (Input.GetMouseButtonUp(0))
		{
			ShootPressed = true;
			if (timeCharging > 100 && GunMode == 1){ShootPressed = false;}
			timeCharging = 0;
		}
		
		if (Input.GetMouseButtonDown(1))
		{
			if (!ShootCharging) {GunMode++;}
			
			if (GunMode > 2) {GunMode = 0;}
			//print($"Gun Mode Changed to {GunMode}");
			
			if (GunMode == 0)
			{
				MySprite.color = new Color(0.5f,0.5f,1,1);
			}
			else if (GunMode == 1)
			{
				MySprite.color = new Color(0.5f,1,0.5f,1);
			}
			else if (GunMode == 2)
			{
				MySprite.color = new Color(1,0.5f,0.5f,1);
			}
		}
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FruitScript fruit = collision.GetComponent<FruitScript>();
        if (fruit != null)
        {
            int amountGained = fruit.typeID;
            fruit.FruitCollection();
            TheAmmo.GetComponent<HUDScript>().SeedAmmoPickup(amountGained);
        }
    }
}
