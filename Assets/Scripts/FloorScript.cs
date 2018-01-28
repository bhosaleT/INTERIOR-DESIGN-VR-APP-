using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : GazeAbleObject {

    public override void OnPress(RaycastHit hitInfo)
    {
        base.OnPress(hitInfo);

        if (Player.instance.activeMode == InputMode.TELEPORT)
        {
            Vector3 destinationLocation = hitInfo.point;

            destinationLocation.y = Player.instance.transform.position.y;

            Player.instance.transform.position = destinationLocation;
        }
    }
}
