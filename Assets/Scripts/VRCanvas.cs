using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCanvas : MonoBehaviour {

  public GazeAbleButton currentActiveButton;

    public Color unselectedColor = Color.white;
    public Color selectedColor = Color.green;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setActiveButton(GazeAbleButton activeButton)
    {
        //If there was a previously selected button this will disable it.
        if(currentActiveButton != null)
        {

            currentActiveButton.setButtonColor(unselectedColor);
        }

        if (activeButton != null && currentActiveButton != activeButton)

        {
            currentActiveButton = activeButton;
            currentActiveButton.setButtonColor(selectedColor);
        }

        else
        {
            Debug.Log("Resetting");
            currentActiveButton = null;
            Player.instance.activeMode = InputMode.NONE;
        }
    }
}
