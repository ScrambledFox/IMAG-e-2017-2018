using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SubmissionScreen : MonoBehaviour {

    public TextMeshProUGUI textArt;
    public TextMeshProUGUI textNod;

    public string textArtString = "404";
    public string textNodString = "404";

    private void Update ()
    {
        textArt.text = textArtString;
        textNod.text = textNodString;
    }

}
