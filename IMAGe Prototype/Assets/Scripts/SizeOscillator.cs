using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeOscillator : MonoBehaviour {

    public float sizeOscillation = 1.0f;
    public float speed = 1.0f;

    Vector3 defaultScale;

    private void Start () {
        defaultScale = transform.localScale;
    }

    // Update is called once per frame
    void Update () {
        transform.localScale = defaultScale + new Vector3(sizeOscillation * Mathf.Sin(Time.time * speed), sizeOscillation * Mathf.Sin(Time.time * speed), sizeOscillation * Mathf.Sin(Time.time * speed));
	}
}
