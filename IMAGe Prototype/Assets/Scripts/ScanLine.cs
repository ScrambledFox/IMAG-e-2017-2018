using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanLine : MonoBehaviour {

    public Vector2 limits;

    public void GoToLayer (float layer) {
        float height;

        height = Mathf.Lerp(limits.x, limits.y, layer);

        transform.localPosition = new Vector3(transform.localPosition.x, height, transform.localPosition.z);
    }

}