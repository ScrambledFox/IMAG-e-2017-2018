using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureGrabber : MonoBehaviour {

    GameManager manager;
    RawImage image;

    bool hasCorrectTextures = false;
    Texture[] currentTextures;



    private void Awake () {
        manager = transform.parent.GetComponent<GameManager>();
        image = GetComponent<RawImage>();

        currentTextures = GetTextures();
        image.texture = currentTextures[currentTextures.Length-1];
    }

    public void SetTexture ( float sliderValue ) {
        if (!hasCorrectTextures) {
            currentTextures = GetTextures();
        }

        int index = (int)(sliderValue * (currentTextures.Length - 1));

        Texture texture = currentTextures[currentTextures.Length - 1 - index];
        image.texture = texture;

        //image.texture = currentTextures[index];
    }

    public void ResetBool () {
        hasCorrectTextures = false;
    }

    Texture[] GetTextures () {
        hasCorrectTextures = true;
        return manager.GetCurrentGameTextures();
    }

}
