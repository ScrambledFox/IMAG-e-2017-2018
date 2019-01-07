using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataScrolling : MonoBehaviour {


    /// <summary>
    /// Change a few of these things around, here we dont want the difference between the two, but we want the average upwards or downwards motion, so y-motion.
    /// </summary>

    float avgY;
    float newAvgY;

    float oldValue;

    private void Update () {
        if (Input.touchCount >= 2) {
            // Setting up touches
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            // Find the average of when touching began
            if (secondTouch.phase == TouchPhase.Began) {
                avgY = (firstTouch.position.y + secondTouch.position.y) / 2;
                oldValue = GameManager.INSTANCE.value;
            }

            // How much has the average moved?
            if (firstTouch.phase == TouchPhase.Moved || secondTouch.phase == TouchPhase.Moved) {
                newAvgY = (firstTouch.position.y + secondTouch.position.y) / 2;
            }

            float avgYDiff = newAvgY - avgY;

            GameManager.INSTANCE.value = oldValue + (avgYDiff / 5000.0f);

        }
        
        
        // Testing the system

        /*
         * 
         * bool done = false;
        if (true) {
            // Setting up touches

            // Find the average of when touching began
            if (!done) {
                done = true;
                avgY = 20;
                oldValue = GameManager.INSTANCE.value;
                newAvgY = avgY;
            }

            // How much has the average moved?
            newAvgY += 0.1f; 

            float avgYDiff = newAvgY - avgY;

            GameManager.INSTANCE.value = oldValue + avgYDiff;

        }
        */


    }

}