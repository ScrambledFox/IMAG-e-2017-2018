using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayerFader : MonoBehaviour {

    public float layer;

    public float size = 50.0f;
    public float maxOpacity = 1.0f;

    bool haltUpdate = false;

    Image image;

    private void Awake () {
        image = GetComponentInChildren<Image>();
    }

    private void Update () {
        if (!haltUpdate) {
            float transparancy = 1.0f - Mathf.Min(Mathf.Abs(layer - GameManager.INSTANCE.value) * size, 1.0f);
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Min(transparancy, maxOpacity));
        }
    }

    public void ResumeUpdate () {
        haltUpdate = false;
    }

    public void MaxOpacity () {
        haltUpdate = true;
        image.color = new Color(image.color.r, image.color.g, image.color.b, maxOpacity);
    }

}
