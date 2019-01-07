using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveData : MonoBehaviour {

    RectTransform rect;
    Vector2 dataPos;

    Touch touch;
    Vector2 firstTouchPosition;

    Vector2 offset;

    public Vector2 limits;

    public bool canMove = true;

    private void Awake () {
        rect = GetComponent<RectTransform>();
    }

    private void Update () {

        /*
        if (Input.touchCount >= 1) {
            touch = Input.GetTouch(0);
            firstTouchPosition = touch.position;

            if (touch.phase == TouchPhase.Began) {
                dataPos = rect.anchoredPosition;
            }

            if (touch.phase == TouchPhase.Moved) {
                offset = firstTouchPosition - touch.position;
            }

        }
        */

        ////// PC INPUT
        /// 

        if (Input.touchCount == 1 && canMove) {
            if (Input.GetMouseButtonDown(0)) {
                firstTouchPosition = Input.mousePosition;
                dataPos = rect.anchoredPosition;
            }

            if (Input.GetMouseButton(0)) {
                offset = firstTouchPosition - (Vector2)Input.mousePosition;
            }
        }

        rect.anchoredPosition = dataPos - offset;

        rect.anchoredPosition = new Vector2(
            Mathf.Clamp(rect.anchoredPosition.x, -limits.x * transform.localScale.magnitude, limits.x * transform.localScale.magnitude),
            Mathf.Clamp(rect.anchoredPosition.y, -limits.y * transform.localScale.magnitude, limits.y * transform.localScale.magnitude)
        );

    }
}