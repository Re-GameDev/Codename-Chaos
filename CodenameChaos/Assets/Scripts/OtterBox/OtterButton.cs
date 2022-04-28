// Creation Date: January 16 2022
// Author(s): rhsha

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using TMPro;

public class OtterButton : MonoBehaviour
{

	static int totalScore = 0;

	public bool deactive = true;
	public Material deactiveMaterial;
	public Material activeMaterial;
	public int scoreOnHit = 0;

	public GameObject otherButtonRef;
	public TextMeshPro scoreTextRef;

	public bool isResetButton = false;

	public OtterButton[] btnList;

	void Awake()
	{
		
	}

	void Start()
	{
		
	}

	void Update()
	{
		
	}

	void FixedUpdate()
	{
		
	}

	public void Click()
    {
		if (deactive)
        {
			ActionButton(true);
		}
	}

	// Call when button is interacted
	// callFromUser is true if user clicked
	// false if paired button was clicked
	public void ActionButton(bool callFromUser)
    {
		if (isResetButton)
        {
			OtterButton.totalScore = 0;
			scoreTextRef.text = "Score: " + OtterButton.totalScore.ToString();
			foreach (OtterButton btn in btnList)
            {
				btn.Reset();
            }


        } 
		else
        {
			deactive = false;
			GetComponent<Renderer>().material = activeMaterial;
			
			if (callFromUser)
			{
				OtterButton.totalScore += scoreOnHit;
				scoreTextRef.text = "Score: " + OtterButton.totalScore.ToString();
				otherButtonRef.GetComponent<OtterButton>().ActionButton(false);
			}
		}
    }

	// Quiz reset. Reset this button
	public void Reset()
    {
		deactive = true;
		GetComponent<Renderer>().material = deactiveMaterial;
	}
}
