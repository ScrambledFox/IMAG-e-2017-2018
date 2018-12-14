using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NoduleTagging : MonoBehaviour {

    public GameObject tagPrefab;

    GameObject tagLoaderGameObject;
    TagLoader tagLoader;

    public float minimumTouchTime = 0.5f;
    public float maximumTouchTime = 1.0f;

    float currentTime;
    float progress;

    bool touching;

    Vector2 lastTappedPos;

    private void Awake () {
        tagLoaderGameObject = GameObject.FindGameObjectWithTag("TagLoader");
        tagLoader = tagLoaderGameObject.GetComponent<TagLoader>();
    }

    private void Update () {
        if (Input.touchCount > 0) {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began) {
                if (CanTag()) {
                    touching = true;
                }
            }

            if (touch.phase == TouchPhase.Stationary) {
                
            }

            if (touch.phase == TouchPhase.Moved) {

            }

            if (touch.phase == TouchPhase.Ended) {
                touching = false;
            }
        }

        if (Input.GetMouseButtonDown(0)) {
            lastTappedPos = Input.mousePosition;

            if (CanTag()) {
                touching = true;
            }
            
        }

        if (Input.GetMouseButtonUp(0)) {
            touching = false;
        }

        if (touching) {
            currentTime += Time.deltaTime;

            progress = currentTime / maximumTouchTime;

            if (currentTime >= minimumTouchTime) {
                ShowTagLoader();
            }
            tagLoader.value = Mathf.Clamp01((currentTime - minimumTouchTime) / (maximumTouchTime - minimumTouchTime));

            if (Vector2.Distance(lastTappedPos, Input.mousePosition) > 75.0f) {
                touching = false;
                Debug.Log("Canceled Placement");
            }

            if (currentTime >= maximumTouchTime) {
                Debug.Log("PLACED");
                GameObject tag = Instantiate(tagPrefab, lastTappedPos, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
                GameManager.INSTANCE.tags.Add(tag);
                tag.GetComponent<LayerFader>().layer = GameManager.INSTANCE.value;
                touching = false;
            }
        } else if (currentTime > 0.0f) {
            currentTime = 0.0f;
            HideTagLoader();
        } else {
            HideTagLoader();
        }
    }

    private void ShowTagLoader () {
        tagLoaderGameObject.SetActive(true);
        tagLoaderGameObject.transform.position = Input.mousePosition;
    }

    private void HideTagLoader () {
        tagLoader.value = 0.0f;
        tagLoader.SetDirty();
        tagLoaderGameObject.SetActive(false);
    }

    private bool CanTag () {
        foreach (GameObject tag in GameManager.INSTANCE.tags) {
            if (tag.GetComponent<Tag>().CanMove()) {
                return false;
            }
        }

        return true;
    }

}