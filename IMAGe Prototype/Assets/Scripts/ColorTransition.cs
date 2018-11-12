using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorTransition : MonoBehaviour {

    SceneManager sceneManager;
    Scene targetScene;
    int targetIndex;

    Image image;

    public Color beginColour;
    public Color endColour;

    bool transitioning = false;

    private void Awake () {
        image = GetComponentInChildren<Image>();
    }

    public void HandleTransition ( SceneManager sceneManager, Scene targetScene, int targetIndex ) {
        this.sceneManager = sceneManager;
        this.targetScene = targetScene;
        this.targetIndex = targetIndex;

        image.color = beginColour;
        transform.position = Vector2.zero;

        transitioning = true;
    }

    private void Update () {
        if (transitioning) {
            image.color = Color.Lerp(image.color, endColour, targetScene.transitionSpeed);
        } else {
            image.color = Color.Lerp(image.color, beginColour, targetScene.transitionSpeed);
        }

        if (image.color.a > 0.999 && transitioning) {
            transform.position = targetScene.scene.position;
            sceneManager.transform.localPosition = -targetScene.scene.localPosition;

            sceneManager.currentScene = targetIndex;
            sceneManager.SetTargetPosition(targetScene.scene.localPosition); 

            transitioning = false;
        }

        if (image.color.a < 0.001 && !transitioning) {
            transform.position = new Vector3(300000, 300000, 300000);
        }
    }

}
