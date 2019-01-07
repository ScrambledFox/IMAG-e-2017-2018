using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public bool active = true;

    private int state = 0;
    private int currentState = -1;

    public List<GameObject> state1Objects = new List<GameObject>();

    TextMeshProUGUI text;
    string[] texts = new string[13];

    Image image;
    public Sprite[] backgrounds = new Sprite[13];

    bool savedValue;
    float value;

    private void Awake () {
        text = GetComponentInChildren<TextMeshProUGUI>();

        image = GetComponentInChildren<Image>();

        texts[0] = "We need you to find some nodules!";
        texts[1] = "Nodules are the weird looking white spots that you'll find.";
        texts[2] = "It's easy to mistake nodules with 'alveoli', the smaller white spots.";
        texts[3] = "We'll call those 'good spots' for now.";

        texts[4] = "To tag harmful nodules, Tap and Hold that spot until a circle appears.";
        texts[5] = "You can always edit the tags by clicking on them.";
        texts[6] = "Lungs with nodules have a higher chance of having lung cancer.";

        texts[7] = "Swipe up and down with two fingers to scroll through the lung!";
        texts[8] = "Try it now!";
        texts[9] = "Good Job!";

        texts[10] = "You might see some artifacts as well. Please tap these as they give you points!";
        texts[11] = "Artifacts look like this! Tap it now!";

        texts[12] = "Good luck doing research!";
    }

    private void Update () {

        if (currentState != 8) {
            if (Input.GetMouseButtonDown(0)) {
                Next();
            }
        } else {
            if (!savedValue) {
                value = GameManager.INSTANCE.value;
                savedValue = true;
            }
            if (Mathf.Abs(GameManager.INSTANCE.value - value) > 0.1f) {
                Next();
            }
        }

        if (state != currentState) {
            if (state >= texts.Length) {
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
            GoToBackground(currentState);
        }
    }

    public void GoToText (int i) {
        text.text = texts[i];
    }

    public void GoToBackground ( int i ) {
        image.sprite = backgrounds[i];
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
