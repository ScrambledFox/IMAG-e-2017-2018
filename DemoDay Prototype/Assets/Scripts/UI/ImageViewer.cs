using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageViewer : MonoBehaviour {

    RawImage image;

    Texture[] images;

    public Texture[] ImageSet;

    private void Awake () {
        images = ImageSet;

        image = gameObject.GetComponent<RawImage>();
    }

    private void Update () {
        GameManager.INSTANCE.value = Mathf.Clamp01(GameManager.INSTANCE.value);

        int index = (int)(GameManager.INSTANCE.value * (images.Length - 1));
        SetTexture(images[images.Length - 1 - index]);
    }

    private void SetTexture (Texture texture) {
        if (image.texture != texture) {
            image.texture = texture;
        }
    }

    public void ImageUpdate () {

    }

}
