using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {

    public GameObject circle;

    private Vector2 touchPosition = -Vector2.one;

    private void Update () {
        if (Input.touchCount > 0) {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began) {
                touchPosition = touch.position;
            } else if ( touch.phase == TouchPhase.Ended && touchPosition.x >= 0 ) {
                Vector2 touchEnd = touch.position;

                float x = touchEnd.x - touchPosition.x;
                float y = touchEnd.y - touchPosition.y;

                touchPosition.x = -1;
                if (Mathf.Abs(x) + Mathf.Abs(y) > 10.0f) {
                    // do nothing
                } else {
                    SpawnCircle(touchEnd);
                }
                
            }
        }
    }

    private void SpawnCircle (Vector2 position) {
        Instantiate(circle, position, Quaternion.identity);
    }

}
