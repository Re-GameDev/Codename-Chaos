using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenderScript : MonoBehaviour
{
    public LayerMask PlayerLayer;
	bool sellIconVisible;
	GameObject theVenderIcon;

    void Awake()
    {
        sellIconVisible = false;
		theVenderIcon = this.transform.GetChild(0).gameObject;
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
    }
    
    /*void FixedUpdate()
    {
        AttractObjects();
    }*/
	
	private void OnTriggerEnter2D(Collider2D collision)
    {
        ThePlayer fruit = collision.GetComponent<ThePlayer>();
        if (fruit != null)
        {
            print("what would you like to buy?");
			sellIconVisible = true;
        }
    }
	
	private void OnTriggerExit2D(Collider2D collision)
    {
        ThePlayer fruit = collision.GetComponent<ThePlayer>();
        if (fruit != null)
        {
            print("Goodbye for now!");
			sellIconVisible = false;
        }
    }
}
