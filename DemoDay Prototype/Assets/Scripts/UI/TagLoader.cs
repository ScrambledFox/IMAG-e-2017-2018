using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagLoader : MonoBehaviour {

    public float value = 0.0f;

    Image loader;

    private void Awake () {
        loader = GetComponentInChildren<Image>();
    }

    public void SetDirty () {
        SetFillAmount();
    }

    private void Update () {
        SetFillAmount();
    }

    private void SetFillAmount () {
        loader.fillAmount = value;
    }
}
