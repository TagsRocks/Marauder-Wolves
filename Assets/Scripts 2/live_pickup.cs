using UnityEngine;
using System.Collections;

public class live_pickup : MonoBehaviour {

    public float healthBonus;               // How much health to give the player.

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D (Collider2D Live)
	{


		if (Live.gameObject.CompareTag ("Player")) {

			//Live.gameObject.SetActive (false);
			Debug.Log ("Heart collected");
            // Get a reference to the player health script.
			PlayerHealth playerHealth = Live.GetComponent<PlayerHealth>();

            // Increase the player's health by the health bonus but clamp it at 100.
            playerHealth.health += healthBonus;
            playerHealth.health = Mathf.Clamp(playerHealth.health, 0f, 100f);

            // Update the health bar.
            playerHealth.UpdateHealthBar();
        } 
	}
}

