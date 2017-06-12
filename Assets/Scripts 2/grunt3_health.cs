 //name: Michael Royal
//course: CST 306
using UnityEngine;
using System.Collections;

public class grunt3_health : MonoBehaviour {

	// Update is called once per frame
	void Update()
	{

	}
	void OnTriggerEnter(Collider Grunt_crawler3) 
	{
		if (Grunt_crawler3.gameObject.CompareTag ("Enemy"))
		{
			Grunt_crawler3.gameObject.SetActive (false);
		}
	}
}
