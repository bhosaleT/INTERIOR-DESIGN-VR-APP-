using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeAbleObject : MonoBehaviour
{
    public bool isTransformable = false;
    private  Vector3 initialObjectRotation;
    private Vector3 initialPlayerRotation;

    private Vector3 initialObjectScale;
    //when users start looking at it.
    private int objectLayer;

    private const int IGNORE_RAYCAST_LAYER = 2;

    public virtual void OnGazeEnter(RaycastHit hitInfo)
    {
        Debug.Log("Gaze Entered on " + gameObject.name);
    }

    public virtual void OnGaze(RaycastHit hitInfo)
    {
        Debug.Log("Gaze hold on " + gameObject.name);
    }

    public virtual void OnGazeExit()
    {
        Debug.Log("Gaze Exited on " + gameObject.name);
    }

    public virtual void OnPress(RaycastHit hitInfo)
    {
        Debug.Log("Button Pressed On " + gameObject.name);

        if (isTransformable)
        {
            objectLayer = gameObject.layer;

            gameObject.layer = IGNORE_RAYCAST_LAYER;

            initialObjectRotation = transform.rotation.eulerAngles;
            initialPlayerRotation = Camera.main.transform.eulerAngles;

            initialObjectScale = transform.localScale;
        }
    }

    public virtual void OnHold(RaycastHit hitInfo)
    {
        Debug.Log("Button is held on " + gameObject.name);
        if (isTransformable)
        {
            GazeTransform(hitInfo);
        }
    }

    public virtual void OnRelease(RaycastHit hitInfo)
    {
        Debug.Log("Button press left On " + gameObject.name);

        if (isTransformable)
        {
            gameObject.layer = objectLayer;
        }
    }

    public virtual void GazeTransform(RaycastHit hitInfo)
    {
        //Call the correct transform function.
        switch (Player.instance.activeMode)
        {

            //call different cases for active mode
            case InputMode.TRANSLATE:
                GazeTranslate(hitInfo);
                break;

            case InputMode.ROTATE:
                GazeRotate(hitInfo);
                break;

            case InputMode.SCALE:
                GazeScale(hitInfo);
                break;
        }
    }

    public virtual void GazeTranslate(RaycastHit hitInfo)
    {
        // move the object position.

        if (hitInfo.collider != null && hitInfo.collider.GetComponent<FloorScript>())
        {
            transform.position = hitInfo.point;// changing the position of the prefab to the position I am looking at.
        }

    }

    public virtual void GazeRotate(RaycastHit hitInfo)
    {
        // change the objects rotation.

        float rotationSpeed = 10.0f;
        Vector3 currentPlayerRotation = Camera.main.transform.rotation.eulerAngles;

        Vector3 currentObjectRotation = transform.rotation.eulerAngles;

        Vector3 rotationDelta = currentPlayerRotation - initialPlayerRotation;

        Vector3 newRotation = new Vector3(currentObjectRotation.x, initialObjectRotation.y + (rotationDelta.y * rotationSpeed), currentObjectRotation.z);

        transform.rotation = Quaternion.Euler(newRotation);
    }

    public virtual void GazeScale(RaycastHit hitInfo)
    {
        //change the scale of the objects.

        float scaleSpeed = 0.1f;
        float scaleFactor = 1;

        Vector3 currentPlayerRotation = Camera.main.transform.rotation.eulerAngles;

        Vector3 rotationDelta = currentPlayerRotation - initialPlayerRotation;

        //if looking up
        if (rotationDelta.x < 0 && rotationDelta.x > -180.0f || rotationDelta.x > 180.0f && rotationDelta.x < 360.0f)
        {
            // If greater than 180, map it between 0-180
            if (rotationDelta.x > 180.0f)
            {
                rotationDelta.x = 360.0f - rotationDelta.x;
            }
            scaleFactor = 1.0f + Mathf.Abs(rotationDelta.x) * scaleSpeed;

        }
        else
        {
            if (rotationDelta.x < 180.0f)
            {
                rotationDelta.x = 360.0f + rotationDelta.x;
            }

            scaleFactor = Mathf.Max(0.1f, 1.0f - (Mathf.Abs(rotationDelta.x) *(1.0f / scaleSpeed)) / 180.0f) ;
        }

        transform.localScale = scaleFactor * initialObjectScale;
    }
}


