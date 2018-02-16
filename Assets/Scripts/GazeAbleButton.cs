using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GazeAbleButton : GazeAbleObject {

    protected VRCanvas parentPanel;
	// Use this for initialization
	void Start () {

        parentPanel = GetComponentInParent<VRCanvas>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setButtonColor(Color buttonColor)
    {
        GetComponent<Image>().color = buttonColor;
    }

    public override void OnPress(RaycastHit hitInfo)
    {
        base.OnPress(hitInfo);
        if (parentPanel != null)
        {
            parentPanel.setActiveButton(this);
        }
        else
        {
            Debug.LogError("Button not a child of object with VRPaneel Component.", this);
        }
    }
}
