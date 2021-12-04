using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
	public float DisplayValue = 10;
	public float MaxValue = 99999;
	string textModifier = "";
	Text MyText;
	
	private void Awake()
	{
		MyText = GetComponent<Text>();
		if (MyText.text == "seeds")
		{
			textModifier = "$";
			DisplayValue = 10;
			MaxValue = 99999;
		}
		else if (MyText.text == "water")
		{
			DisplayValue = 50;
			MaxValue = 50;
		}
		else if (MyText.text == "boost")
		{
			textModifier = "%";
			DisplayValue = 0;
			MaxValue = 100;
		}			
		
		MyText.text = $"{DisplayValue}";
	}
	
	public void SeedShot(float amount)
	{
		DisplayValue = DisplayValue - amount;
		if (DisplayValue < 0)
		{
			DisplayValue = 0;
		}
	}
	
	public void ChargeShot()
	{		
		DisplayValue = 0;
	}
	
	public void SeedAmmoPickup(float amount)
	{
		DisplayValue = DisplayValue + amount;
		if (DisplayValue > MaxValue)
		{
			DisplayValue = MaxValue;
		}
	}
	
	private void Update()
	{
		MyText.text = $"{DisplayValue}{textModifier}";
	}
}
