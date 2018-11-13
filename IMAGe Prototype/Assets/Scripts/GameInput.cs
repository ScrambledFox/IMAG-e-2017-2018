using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameInput : MonoBehaviour {

    public GameObject circle;
    public GameObject flagPrefab;
    Image circleImage;
    float circleFillAmount = 0.0f;

    public Slider slider;

    public PlacementBar bar;

    string currentTool = "artifacts";

    public Color noduleToolColour;
    public Color artifactToolColour;

    public float maxTouchTime;
    float currentTouchTime;
    bool touching;

    private Vector3 touchPosition = -Vector3.one;

    private List<GameObject> currentFlags = new List<GameObject>();

    private void Awake () {
        circleImage = circle.GetComponentInChildren<Image>();

        ChangeAllButtonSizes();
        bar.Bar(0, GetCurrentToolColour());
    }

    private void Update () {
        if (Input.touchCount > 0) {
            Touch touch = Input.touches[0];

            /*if (touch.phase == TouchPhase.Began) {
                touchPosition = touch.position;
            } else if (touch.phase == TouchPhase.Ended && touchPosition.x >= 0) {
                Vector2 touchEnd = touch.position;

                float x = touchEnd.x - touchPosition.x;
                float y = touchEnd.y - touchPosition.y;

                touchPosition.x = -1;
                if (Mathf.Abs(x) + Mathf.Abs(y) > 10.0f) {
                    // do nothing
                } else {
                    SpawnCircle(touchEnd);
                }
            }*/

            if (touch.phase == TouchPhase.Began) {

            }

            if (touch.phase == TouchPhase.Stationary) {

            }

            if (touch.phase == TouchPhase.Moved) {

            }

            if (touch.phase == TouchPhase.Ended) {

            }


        } else {

        }

        //Vector3 pos = Input.mousePosition;
        //pos.z = 10.0f;
        //Debug.Log(Camera.main.ScreenToWorldPoint(pos));

        if (Input.GetMouseButtonDown(0)) {
            touchPosition = Input.mousePosition;

            Vector3 relativePosition = touchPosition;
            relativePosition.z = 10.0f;

            Vector3 position = Camera.main.ScreenToWorldPoint(relativePosition);

            if ((position.x > -6.0f && position.x < 8.75f) && (position.y > -3.0f && position.y < 6.0f)) {
                touching = true;
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            touching = false;

        }

        if (Input.GetMouseButtonDown(1)) {
            Vector3 poss = Input.mousePosition;
            poss.z = 10.0f;

            //Debug.Log(Camera.main.ScreenToWorldPoint(poss).ToString());

            //Debug.Log(Vector3.Distance(Camera.main.ScreenToWorldPoint(poss), ));
        }

        if (touching) {
            currentTouchTime += Time.deltaTime;

            SpawnCircle(touchPosition);

            if (currentTouchTime >= maxTouchTime) {
                touching = false;

                Vector3 relativePosition = touchPosition;
                relativePosition.z = 10.0f;

                Vector3 position = Camera.main.ScreenToWorldPoint(relativePosition);

                GameManager.Flag flag = new GameManager.Flag(position, slider.value, GetCurrentToolColour());
                GameManager.INSTANCE.AddFlagToCurrentGame(flag);

                GameManager.INSTANCE.CheckForObject(flag, currentTool);
            }

            if (Vector3.Distance(touchPosition, Input.mousePosition) > 100.0f) {
                touching = false;
            }

        } else {
            currentTouchTime = 0.0f;
            circle.transform.position = new Vector3(30000, 30000, 30000);
        }

        CircleValue((currentTouchTime / maxTouchTime), GetCurrentToolColour());
        bar.Bar((currentTouchTime / maxTouchTime), GetCurrentToolColour());


        ///////////////////////////////////////
        ///

        Vector3 poz = Input.mousePosition;
        poz.z = 10.0f;

        //GameObject.FindWithTag("Player").GetComponent<TextMeshPro>().text = Camera.main.ScreenToWorldPoint(poz).ToString();

    }

    private void SpawnCircle ( Vector3 touchPos) {
        touchPos.z = 10.0f;
        Vector3 position = Camera.main.ScreenToWorldPoint(touchPos);

        circle.transform.position = position;
    }

    private void SpawnFlag (Vector3 position) {
        Vector3 pos = position;
        pos.z = 10.0f;
        GameObject flag = Instantiate(flagPrefab, Camera.main.ScreenToWorldPoint(pos), Quaternion.identity, this.transform);
        flag.GetComponentInChildren<Image>().color = GetCurrentToolColour();
        currentFlags.Add(flag);
    }

    private void CircleValue (float value, Color colour) {
        circleImage.fillAmount = value;
        circleImage.color = colour;
    }

    public void ChangeTool (string tool) {
        switch (tool) {
            case "nodules":
                currentTool = "nodules";
                break;
            case "artifacts":
                currentTool = "artifacts";
                break;
            default:
                break;
        }
    }

    public string GetCurrentTool () {
        return currentTool;
    }

    public Color GetCurrentToolColour () {
        switch (currentTool) {
            case "nodules":
                return noduleToolColour;
            case "artifacts":
                return artifactToolColour;
            default:
                return Color.black;
        }
    }

    public void ChangeAllButtonSizes () {
        foreach (ButtonSizeChanger button in GetComponentsInChildren<ButtonSizeChanger>()) {
            button.ChangeSize(currentTool);
        }
    }

}
