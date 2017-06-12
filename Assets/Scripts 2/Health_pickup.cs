//name: Michael Royal
//course: CST 306
using UnityEngine;
using System.Collections;

public class Health_pickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	//Pickup healthpotion
	void OnTriggerEnter2D (Collider2D health)
	{


		if (health.gameObject.CompareTag ("Pickup")) {

			health.gameObject.SetActive (false);
			Debug.Log ("Health collected");
		} 
	}
}
