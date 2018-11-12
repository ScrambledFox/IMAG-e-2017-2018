using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    public GameObject circle;

    private Vector2 touchPosition = -Vector2.one;

    private void Update () {
        if (Input.touchCount > 0) {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began) {
                touchPosition = touch.position;
            } else if (touch.phase == TouchPhase.Ended && touchPosition.x >= 0) {
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

        if (Input.GetMouseButtonUp(0)) {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10.0f;
            SpawnCircle( Camera.main.ScreenToWorldPoint(mousePos) );
        }
    }

    private GameObject SpawnCircle ( Vector2 touchPos) {
        Vector3 position = new Vector3(touchPos.x, touchPos.y, 0);
        return Instantiate(circle, position, Quaternion.identity, gameObject.transform.root);
    }

}
