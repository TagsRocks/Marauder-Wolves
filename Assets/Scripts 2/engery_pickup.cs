using UnityEngine;
using System.Collections;

public class engery_pickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//Pickup healthpotion
	void OnTriggerEnter2D (Collider2D Energy)
	{


		if (Energy.gameObject.CompareTag ("Pickup")) {

			Energy.gameObject.SetActive (false);
			//Debug.Log ("Heart collected");
		} 
	}
}
