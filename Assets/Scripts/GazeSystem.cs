using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeSystem : MonoBehaviour {

    public GameObject reticle;

    public Color inactiveReTicleColor = Color.gray;

    public Color activeRecticleColor = Color.green;

    private GazeAbleObject currentGazeObject;

    private GazeAbleObject currentSelectedObject;

    private RaycastHit lastHit;
	// Use this for initialization
	void Start () {
        
        SetReticleColor(inactiveReTicleColor);
	}
	
	// Update is called once per frame
	void Update () {
		
        ProcessGaze();
        CheckForInput(lastHit);
	}

    public void ProcessGaze()
    {
        
        Ray raycastRay = new Ray(transform.position, transform.forward);

        RaycastHit hitInfo;
        Debug.DrawRay(raycastRay.origin, raycastRay.direction * 100);


        if(Physics.Raycast(raycastRay, out hitInfo))
        {

            //do something


            //check if the object is interactable

            //Get the gameObject from the hitInfo
            GameObject hitObj = hitInfo.collider.gameObject;

            //Get the GazeableObject from the hit object
            GazeAbleObject gazeObj = hitObj.GetComponentInParent<GazeAbleObject>();

            //Object has a gazeAble component
            if (gazeObj != null)
            {
                //object we are looking at is different
                if (gazeObj != currentGazeObject)
                {
                    
                    ClearGazeObject();
                    currentGazeObject = gazeObj; // get the gazeobject from the hitinfo
                    currentGazeObject.OnGazeEnter(hitInfo);
                    SetReticleColor(activeRecticleColor); // change the recticlecolor to activecolor.

                }
                else
                {
                    currentGazeObject.OnGaze(hitInfo);
                }

            }
            else
            {
                ClearGazeObject();
            }

            lastHit = hitInfo;

            }

        else
        {
            ClearGazeObject();
        }
              
               //check if the object is a newObject.


            //set the reticle color

        }


    private void SetReticleColor(Color reticleColor)
    {
        //set the color of the reticle.
        reticle.GetComponent<Renderer>().material.SetColor("_Color", reticleColor);
    }

    private void CheckForInput(RaycastHit hitInfo)
    {
        
        //check for down
        if(Input.GetMouseButtonDown(0) && currentGazeObject != null)
        {
            currentSelectedObject = currentGazeObject;
            currentSelectedObject.OnPress(hitInfo);
        }

        //check for hold
        if(Input.GetMouseButton(0) && currentSelectedObject != null)
        {
            currentSelectedObject.OnHold(hitInfo);
        }

        //check for release
        if(Input.GetMouseButtonUp(0) && currentSelectedObject != null)
        {
            currentSelectedObject.OnRelease(hitInfo);
            currentSelectedObject = null;
        }

    }

    private void ClearGazeObject()
    {
        if(currentGazeObject != null)
        {
            currentGazeObject.OnGazeExit();
            SetReticleColor(inactiveReTicleColor);
            currentGazeObject = null;
        }
    }
}
