using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeButton : GazeAbleButton {

    [SerializeField]
    private InputMode mode; //[serializedfield] allows us to see a private variable in the editor.

    public override void OnPress(RaycastHit hitInfo)
    {
        base.OnPress(hitInfo);

        if (parentPanel.currentActiveButton != null)
        {

            Player.instance.activeMode = mode;

        }
    }
}

