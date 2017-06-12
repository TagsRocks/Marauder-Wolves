//name: Michael Royal
//course: CST 306
using UnityEngine;
using System.Collections;

public class demon2_health : MonoBehaviour {

	// Update is called once per frame
	void Update()
	{

	}
	void OnTriggerEnter(Collider Demon2) 
	{
		if (Demon2.gameObject.CompareTag ("Enemy"))
		{
			Demon2.gameObject.SetActive (false);
		}
	}
}