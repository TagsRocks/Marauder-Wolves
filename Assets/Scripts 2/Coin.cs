using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Coin : MonoBehaviour {
	
	private Text levelText;									//Text to display.
	private Coin_Score coins;				// Reference to the Score script.
	
	void Start(){
		

			//levelText = GameObject.Find("Coin_Pickup").GetComponent<Text>();
	   
			//levelText.text = "Coin Collected ";
	}
	

	//Pickup Coins
	void OnTriggerEnter2D (Collider2D Coin_Pickup)
	{
		

		if (Coin_Pickup.gameObject.CompareTag ("Pickup")) {
			
			Coin_Pickup.gameObject.SetActive (false);
			// Increase the coins count by 1
			PlayerPrefs.SetInt("coins",PlayerPrefs.GetInt("coins")+1);
			//Debug.Log ("Coin collected");
		} 
	}
		
}