// Creation Date: January 10 2022
// Author(s): molen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
	//https://docs.unity3d.com/Packages/com.unity.ugui@1.0/api/UnityEngine.UI.DefaultControls.html
	void Awake()
	{
		var mC = CreateCanvasInWolrd();
		CreateButtonInWorld(mC, new Vector3(100, 100, 0));

        CreateButtonInWorld(mC, new Vector3(400, 200, 0));
    }

	/*
	 * TODO - Researsh styling https://docs.unity3d.com/Manual/UIE-USS.html
	 * TODO - Expand method features
	 * TODO - create dataType for list UIs (maybe make use of inheritance?)
	 * TODO - add coroute to "animate" UI
	 */


	public GameObject CreateButtonInWorld(Canvas mCanvas, Vector3 mPosition)
    {
		GameObject button = new GameObject();
		button.AddComponent<CanvasRenderer>();
		button.AddComponent<RectTransform>();
		Button mButton = button.AddComponent<Button>();
		Image mImage = button.AddComponent<Image>();
		mButton.targetGraphic = mImage;

		button.transform.position = mPosition;
		button.transform.SetParent(mCanvas.transform);
		return button;
	}

	public Canvas CreateCanvasInWolrd()
    {
		GameObject myGO;
		GameObject myText;
		Canvas myCanvas;
		Text text;
		RectTransform rectTransform;

		// Canvas
		myGO = new GameObject();
		myGO.name = "TestCanvas";
		myGO.AddComponent<Canvas>();

		myCanvas = myGO.GetComponent<Canvas>();
		myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
		myGO.AddComponent<CanvasScaler>();
		myGO.AddComponent<GraphicRaycaster>();

		// Text
		myText = new GameObject();
		myText.transform.parent = myGO.transform;
		myText.name = "wibble";

		text = myText.AddComponent<Text>();
		text.font = (Font)Resources.Load("MyFont");
		text.text = "wobble";
		text.fontSize = 100;

		// Text position
		rectTransform = text.GetComponent<RectTransform>();
		rectTransform.localPosition = new Vector3(0, 0, 0);
		rectTransform.sizeDelta = new Vector2(400, 200);

		return myCanvas;
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
}
