using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputTest : MonoBehaviour {

    public GameObject test;
    TextMeshProUGUI textObj;

    private void Awake () {
        textObj = GetComponent<TextMeshProUGUI>();
    }

    private void Update () {

        if (Input.touches.Length > 0) {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began) {
                Vector3 pos = new Vector3(touch.position.x, touch.position.y, 10.0f);

                textObj.text = touch.position.ToString();
                Instantiate(test, Camera.main.ScreenToWorldPoint(pos), Quaternion.identity, transform.root);
            }
        } else {
            textObj.text = "Waiting";
        }

    }

}
