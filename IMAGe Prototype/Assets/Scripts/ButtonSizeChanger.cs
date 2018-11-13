using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSizeChanger : MonoBehaviour {

    public string toolButtonName;

    Vector3 normalSize;
    public Vector2 sizes;

    private void Awake () {
        normalSize = transform.localScale;
    }

    public void ChangeSize (string currentTool) {
        if (currentTool != toolButtonName) {
            transform.localScale = normalSize * sizes.x;
        }
        else {
            transform.localScale = normalSize * sizes.y;
        }
    }

}
