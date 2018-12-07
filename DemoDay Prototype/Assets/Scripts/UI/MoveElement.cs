using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveElement : MonoBehaviour {

    public Vector2 moveOffset;

    public float moveSpeed = 1;

    RectTransform rectTransform;

    Vector2 startPosition, endPosition;

    bool moving = false;
    bool inverse = true;

    private void Awake () {
        rectTransform = GetComponent<RectTransform>();

        startPosition = rectTransform.anchoredPosition;
        endPosition = startPosition + moveOffset;
    }

    public void DoMove () {
        moving = true;
        inverse = false;
    }

    public void DoInverseMove () {
        moving = true;
        inverse = true;
    }

    public void ToggleState () {
        if (inverse) {
            DoMove();
        } else {
            DoInverseMove();
        }
    }

    private void Update () {

        ///// Input testing
        if (Input.GetKeyDown(KeyCode.F)) DoMove();
        if (Input.GetKeyDown(KeyCode.G)) DoInverseMove();
        ///

        if (moving) {
            Vector2 targetPosition;

            if (inverse) {
                targetPosition = startPosition;
            } else {
                targetPosition = endPosition;
            }

            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, moveSpeed);

            if (Vector2.Distance(rectTransform.anchoredPosition, targetPosition) < 0.01f) {
                moving = false;
            }
        }

    }
}
