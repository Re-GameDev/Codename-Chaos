using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenderScript : MonoBehaviour
{
    public LayerMask PlayerLayer;
	bool sellIconVisible;
	public GameObject playerSeedCount;
	GameObject theVenderIcon;
	GameObject theVenderSpring;

    void Awake()
    {
        sellIconVisible = false;
		theVenderIcon = this.transform.GetChild(0).gameObject;
		theVenderSpring = this.transform.GetChild(1).gameObject;
    }
    
    void Update()
    {
        if (sellIconVisible)
		{
			theVenderIcon.SetActive(true);
		}
		else
		{
			theVenderIcon.SetActive(false);
		}
		
		if (Input.GetKeyDown(KeyCode.E) && sellIconVisible)
		{
			BuyAThing();
		}
    }
    
    /*void FixedUpdate()
    {
        AttractObjects();
    }*/
	
	private void OnTriggerEnter2D(Collider2D collision)
    {
        ThePlayer MainPlayer = collision.GetComponent<ThePlayer>();
        if (MainPlayer != null)
        {
            print("What would you like to buy?");
			sellIconVisible = true;
        }
    }
	
	private void OnTriggerExit2D(Collider2D collision)
    {
        ThePlayer MainPlayer = collision.GetComponent<ThePlayer>();
        if (MainPlayer != null)
        {
            print("Goodbye for now!");
			sellIconVisible = false;
        }
    }
	
	private void BuyAThing()
    {
        if (playerSeedCount != null)
        {
			float seedCount = playerSeedCount.GetComponent<HUDScript>().DisplayValue;
			if (seedCount >= 15)
			{
				print("Good Purchase!");
				theVenderSpring.SetActive(true);
				playerSeedCount.GetComponent<HUDScript>().SeedShot(15);
			}
			else
			{
				print("Need more seeds!");
			}
        }
    }
}
