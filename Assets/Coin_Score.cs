using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Coin_Score : MonoBehaviour
{
	public int coins;        // The player's coin score.



	Text text;                      // Reference to the Text component.


	void Awake ()
	{
		// Set up the reference.
		text = GetComponent <Text> ();

		// Sets players coins to 500 for testing purposes. Remove when game is published. 
		PlayerPrefs.SetInt ("coins", 500);

		// Reset the coin score.
		coins = PlayerPrefs.GetInt("coins");
	}


	void Update ()
	{
		// keeps player coins displayed at its current value
		coins = PlayerPrefs.GetInt("coins");
		// Set the displayed text to be the word "coins" followed by the score value.
		text.text = "Coins: " + coins;
	}
}
