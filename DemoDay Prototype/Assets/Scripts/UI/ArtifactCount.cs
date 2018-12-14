using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArtifactCount : MonoBehaviour{

    TextMeshProUGUI text;

    private void Awake () {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update () {
        text.text = GameManager.INSTANCE.artifactsFound.Count.ToString();
    }

}