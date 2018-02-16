using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureButton : GazeAbleButton {

    // Use this for initialization
    public Object Fprefab;

    public override void OnPress(RaycastHit hitInfo)
    {
        base.OnPress(hitInfo);

        //Set the playermode to place furniture
        Player.instance.activeMode = InputMode.FURNITURE;


        // Set active furniture prefab to this prefab.
        Player.instance.activeFurniturePrefab = Fprefab;
    }


}