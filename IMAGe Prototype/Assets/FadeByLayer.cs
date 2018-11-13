using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeByLayer : MonoBehaviour {

    public Slider slider;

    public float layer;

    public float size = 50.0f;
    public float maxOpacity;

    Image image;

    private void Awake () {
        image = GetComponentInChildren<Image>();
        slider = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
   }

    void Update () {
        //float transparancy = Mathf.Lerp(0, 1, Mathf.Min(0, -Mathf.Abs(layer - slider.value)));
        float transparancy = 1.0f - Mathf.Min(Mathf.Abs(layer - slider.value) * size, 1.0f);
        image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Min(transparancy, maxOpacity));
	}

    public void setLayer (float lay) {
        layer = lay;
    }
}
