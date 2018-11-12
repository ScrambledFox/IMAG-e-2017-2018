using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fading : MonoBehaviour {

    public float startAfterSeconds = 0.0f;

    float currentA;
    public float startTransparancy = 0.0f;
    public float endTransparancy = 1.0f;

    public float speed = 1.0f;

    TextMeshProUGUI textMesh;
    float timer;

    // Use this for initialization
    void Awake () {
        textMesh = gameObject.GetComponent<TextMeshProUGUI>() ?? gameObject.AddComponent<TextMeshProUGUI>();

        timer = startAfterSeconds;
        currentA = startTransparancy;

        ToA(currentA);
    }

	// Update is called once per frame
	void Update () {
        if (timer > 0.0f) {
            timer -= Time.deltaTime;
        } else {
            currentA = Mathf.Lerp(currentA, endTransparancy, speed);
            ToA(currentA);
        }
	}

    void ToA (float a) {
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, a);
    }
}
