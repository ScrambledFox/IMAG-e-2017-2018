using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    public int currentScene = 0;
    public Scene[] scenes;

    public Transform transitionBlack;
    private ColorTransition blackTransition;

    public GameInput gameInput;

    private Vector2 targetPosition;

    private void Awake () {
        blackTransition = transitionBlack.GetComponent<ColorTransition>();

        gameInput.enabled = false;

        InitScenePositions();
        //InitScenes();
    }

    void InitScenePositions () {
        for (int i = 0; i < scenes.Length; i++) {
            if (scenes[i].parentScene != null) {
                Vector3 relativePosition;
                switch (scenes[i].transition) {
                    case Scene.Transition.left:
                        relativePosition = new Vector3(-1920, 0);
                        break;
                    case Scene.Transition.right:
                        relativePosition = new Vector3(1920, 0);
                        break;
                    case Scene.Transition.up:
                        relativePosition = new Vector3(0, 1080);
                        break;
                    case Scene.Transition.down:
                        relativePosition = new Vector3(0, -1080);
                        break;
                    case Scene.Transition.none:
                        relativePosition = new Vector3(0, -1080);
                        break;
                    default:
                        relativePosition = new Vector3(0, 0);
                        break;
                }

                scenes[i].scene.localPosition = scenes[i].parentScene.localPosition + relativePosition;
            }
        }
    }

    private void InitScenes ()
    {
        foreach (Scene scene in scenes) {
            scene.scene.gameObject.SetActive(false);
        }
    }

    public void SetTargetPosition (Vector2 target) {
        targetPosition = target;
    }

    public void GoToScene ( int sceneIndex ) {
        if (sceneIndex == 8) {
            gameInput.enabled = true;
        } else {
            gameInput.enabled = false;
        }
        currentScene = sceneIndex;
        DoTransition(scenes[currentScene], sceneIndex);
    }

    public void Quit () {
        Application.Quit();
    }

    private void DoTransition (Scene targetScene, int sceneIndex) {
        switch (targetScene.transition) {
            case Scene.Transition.black:
                blackTransition.HandleTransition(this, targetScene, sceneIndex);
                break;
            case Scene.Transition.none:
                targetScene.transitionSpeed = 1.0f;
                targetPosition = targetScene.scene.localPosition;
                break;
            default:
                targetPosition = targetScene.scene.localPosition;
                break;
        }
    }

    private void Update () {
        if (currentScene < 0) currentScene = 0;
        if (currentScene > scenes.Length - 1) currentScene = scenes.Length - 1;

        transform.localPosition = Vector2.Lerp(transform.localPosition, -targetPosition, scenes[currentScene].transitionSpeed);
    }

}

[System.Serializable]
public struct Scene {

    public enum Transition {
        left, right, up, down, black, none
    }

    public string name;
    public Transform scene;
    public Transform parentScene;
    public Transition transition;
    public float transitionSpeed;

    public Scene ( string name, Transform scene, Transform parentScene, Transition transition, float transitionSpeed ) {
        this.name = name;
        this.scene = scene;
        this.parentScene = parentScene;
        this.transition = transition;
        this.transitionSpeed = transition > 0 ? transitionSpeed : 1.0f;
    }

}