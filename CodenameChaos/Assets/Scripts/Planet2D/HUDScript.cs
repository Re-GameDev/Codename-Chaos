using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
	public int SeedAmmo = 10;
	Text MyText;
	
	private void Awake()
	{
		MyText = GetComponent<Text>();
		MyText.text = $"{SeedAmmo}";
	}
	
	public void SeedShot()
	{
		SeedAmmo--;
		if (SeedAmmo < 0)
		{
			SeedAmmo = 0;
		}
	}
	
	public void SeedAmmoPickup()
	{
		SeedAmmo++;
	}
	
	private void Update()
	{
		MyText.text = $"{SeedAmmo}";
	}
}
