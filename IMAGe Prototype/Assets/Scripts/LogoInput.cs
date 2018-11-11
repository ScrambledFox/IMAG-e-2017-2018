using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoInput : MonoBehaviour {

    SceneManager manager;

    private void Awake () {
        manager = transform.parent.GetComponent<SceneManager>();
    }

    private void Update () {

        if (Input.touchCount > 0) {
            manager.GoToScene(1);
            Destroy(this);
        }

        if (Input.GetMouseButtonDown(0)) {
            manager.GoToScene(1);
            Destroy(this);
        }

    }
}
