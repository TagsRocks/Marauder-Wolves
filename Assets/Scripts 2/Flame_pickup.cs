//name: Michael Royal
//course: CST 306
using UnityEngine;
using System.Collections;

public class Flame_pickup : MonoBehaviour {

	//Pickup flames
	void OnTriggerEnter(Collider Flame_Idle) 
	{
		if (Flame_Idle.gameObject.CompareTag ("Pickup"))
		{
			Flame_Idle.gameObject.SetActive (false);
		}
	}

}
