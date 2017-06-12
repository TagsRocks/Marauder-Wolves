using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StoreCredit : MonoBehaviour {

    public static int coinscore;        // The player's score.
    public static int armor;

    Text text;                      // Reference to the Text component.


    void Awake()
    {
        // Set up the reference.
        text = GetComponent<Text>();

        // Reset the score.
        coinscore = 1200;
        armor = coinscore-200;
    }


    public void BuyItemButtonClicked()
    {
        // Set the displayed text to be the word "Score" followed by the score value.
        text.text = "Coins: " + armor;
        
    }
}