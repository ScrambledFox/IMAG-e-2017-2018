using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinchStretchInput : MonoBehaviour {

    ImageZooming dataZooming;

    private void Start () {
        dataZooming = GetComponent<ImageZooming>();
    }

    private void Update () {

        // Check for input
        if (Input.touchCount == 2) {
            // Store Touches
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            // Find the position in previous frame
            Vector2 firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            Vector2 secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            // Finding magnitude of the Vectors in both frames
            float prevTouchDeltaMag = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            float touchDeltaMag = (firstTouch.position - secondTouch.position).magnitude;

            // Find the difference
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // ZoomLevel
            dataZooming.zoomLevel -= deltaMagnitudeDiff / 1000.0f;

            // Adding it up to the value
            //GameManager.INSTANCE.value -= deltaMagnitudeDiff / 5000.0f;
        }

        // Pc input
        //dataZooming.zoomLevel += Input.mouseScrollDelta.y / 50.0f;
    }

}
