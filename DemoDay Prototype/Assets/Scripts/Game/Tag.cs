﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tag : MonoBehaviour {

    LayerFader fader;

    GameObject menu, moveMenu;

    bool moveable = false;

    Vector2 lastSavedPos;

    private void Awake () {
        menu = transform.GetChild(0).gameObject;
        moveMenu = transform.GetChild(1).gameObject;

        fader = GetComponentInChildren<LayerFader>();

        lastSavedPos = transform.position;
    }

    public void OnDrag () {
        if (moveable) {
            Vector2 pos = Input.mousePosition;
            transform.position = pos;
        }
    }

    public void ToggleMenu () {
        if (!moveable) {
            if (!menu.activeSelf) {
                menu.SetActive(true);
            } else {
                menu.SetActive(false);
            }
        }
    }

    private void CloseMenu () {
        menu.SetActive(false);
    }

    private void OpenMoveMenu () {
        moveMenu.SetActive(true);
    }

    public void CloseMoveMenu () {
        moveMenu.SetActive(false);
    }

    public void Move () {
        fader.MaxOpacity();

        lastSavedPos = transform.position;

        moveable = true;

        CloseMenu();
        OpenMoveMenu();
    }

    public void EndMove () {
        moveable = false;

        fader.layer = GameManager.INSTANCE.value;
        fader.ResumeUpdate();

        CloseMoveMenu();
    }

    public void CancelMove () {
        moveable = false;

        transform.position = lastSavedPos;
        fader.ResumeUpdate();

        CloseMoveMenu();
    }

    public void Delete () {
        Destroy(gameObject);
    }

    public bool CanMove () {
        return moveable;
    }

    private void OnDestroy () {
        GameManager.INSTANCE.tags.Remove(gameObject);
    }

    private void Update () {
        if (menu.activeSelf) {
            if (Mathf.Abs(fader.layer - GameManager.INSTANCE.value) > 0.025f) {
                menu.SetActive(false);
            }
        }
    }

}