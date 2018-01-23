using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeAbleObject : MonoBehaviour
{

    //when users start looking at it.

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
    }

    public virtual void OnHold(RaycastHit hitInfo)
    {
        Debug.Log("Button is held on " + gameObject.name);
    }

    public virtual void OnRelease(RaycastHit hitInfo)
    {
        Debug.Log("Button press left On " + gameObject.name);
    }
}


