using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Artifact : MonoBehaviour {

    Button button;
    Image image;

    private void Awake () {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    public void Find () {
        Debug.Log("Found an Artifact!");
        GameManager.INSTANCE.artifactsFound.Add(gameObject);
        gameObject.SetActive(false);
    }

    private void Update () {
        if (image.color.a == 0.0f) {
            button.enabled = false;
        } else {
            button.enabled = true;
        }
    }

}