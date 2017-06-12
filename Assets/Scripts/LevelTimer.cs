//Michael Royal CST 306
using UnityEngine;
using System.Collections;

public class LevelTimer : MonoBehaviour {

    //Stes timer to 100 seconds
    float timeRemaining = 100.0f;

    void Update()
    {
        //Updates the remianing time to the counter
        timeRemaining -= Time.deltaTime;
    }

    void OnGUI()
    {
        if (timeRemaining > 0)
        {
            //GUI displays the amount of time remaining
            GUI.Label(new Rect(35, 100, 200, 100),
                         "Time Remaining : " + timeRemaining);
        }
        else
        {
            //After time has ended game is over
            GUI.Label(new Rect(100, 100, 200, 100), "Game Over! Press R to Try again");
        }
    }
}