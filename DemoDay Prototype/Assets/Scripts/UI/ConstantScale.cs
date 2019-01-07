using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantScale : MonoBehaviour {

    public Vector3 scale;

    private void Awake () {
//        transform.parent = transform.root;
    }

    // Update is called once per frame
    void Update () {

        transform.localScale = (1 / transform.parent.parent.localScale.magnitude) * scale;

	}
}
