using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour {

    public bool active = true;

    private int state = 0;
    private int currentState = -1;
    private int maxState = 6;

    public List<GameObject> state1Objects = new List<GameObject>();

    TextMeshProUGUI text;
    string[] texts = new string[6];

    private void Awake () {
        text = GetComponentInChildren<TextMeshProUGUI>();

        texts[0] = "Lungs with nodules have a higher chance of containing lung cancer.";
        texts[1] = "Nodules are the weird looking clusters that you'll find. They look quite different that the other white spots.";
        texts[2] = "Pinch and Stretch the screen to move through the data!";
        texts[3] = "Tap and hold the nodules to tag them. You can always edit the tags by clicking on them.";
        texts[4] = "You might see some artifacts as well. Please tap these as they give you points!";
        texts[5] = "Good luck doing research!";
    }

    private void Update () {
        if (state != currentState) {
            if (state >= maxState) {
                GameManager.INSTANCE.StartGame();
                return;
            }

            if (state == 1) {
                EnableList(state1Objects);
            } else {
                DisableList(state1Objects);
            }

            currentState = state;
            GoToText(currentState);
        }
    }

    public void GoToText (int i) {
        text.text = texts[i];
    }

    public void Next () {
        state++;
    }

    private void EnableList (List<GameObject> list) {
        foreach (GameObject gameObject in list) {
            gameObject.SetActive(true);
        }
    }

    private void DisableList ( List<GameObject> list ) {
        foreach (GameObject gameObject in list) {
            gameObject.SetActive(false);
        }
    }
}
