using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageZooming : MonoBehaviour {

    public float zoomLevel = 1.0f;
    public Vector2 zoomLevels;

    public void SetZoomLevel (float zoomLevel) {
        this.zoomLevel = zoomLevel;
    }

    private void Update () {
        zoomLevel = Mathf.Clamp(zoomLevel, zoomLevels.x, zoomLevels.y);

        transform.localScale = Vector3.one * zoomLevel;
    }

}
