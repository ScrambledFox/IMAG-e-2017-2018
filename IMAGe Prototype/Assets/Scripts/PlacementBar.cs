using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementBar : MonoBehaviour {

    Image image;

    private void Awake () {
        image = GetComponentsInChildren<Image>()[1];
    }

    public void Bar (float value, Color colour) {
        image.fillAmount = value;
        image.color = colour;
    }

}
