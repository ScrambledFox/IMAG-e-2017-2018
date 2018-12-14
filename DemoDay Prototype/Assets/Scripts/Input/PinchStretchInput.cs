using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinchStretchInput : MonoBehaviour {

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

            // Adding it up to the value
            GameManager.INSTANCE.value += deltaMagnitudeDiff / 10000.0f;
        }

                // Lags out app ? NOPE
        GameManager.INSTANCE.value += Input.mouseScrollDelta.y / 100.0f;
    }

}
