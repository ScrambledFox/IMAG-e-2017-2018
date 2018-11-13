using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameText : MonoBehaviour {

    public string source;
    TextMeshProUGUI textObj;

    private void Awake () {
        textObj = GetComponent<TextMeshProUGUI>();

        UpdateText();
    }

    private void Update () {
        UpdateText();
    }

    public void UpdateText () {
        string newText;

        switch (source) {
            case "nodules":
                newText = GameManager.INSTANCE.GetNoduleAmount().ToString();
                break;
            case "artifacts":
                newText = GameManager.INSTANCE.GetArtifactAmount().ToString() + "/" + GameManager.INSTANCE.GetArtifactMaxAmount();
                break;
            default:
                newText = "ERROR";
                break;
        }

        textObj.text = newText;
    }

}
