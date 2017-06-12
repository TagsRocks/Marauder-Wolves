//name: Michael Royal
//course: CST 306
using UnityEngine;
using System.Collections;

public class grunt2_health : MonoBehaviour {

	// Update is called once per frame
	void Update()
	{

	}
	void OnTriggerEnter(Collider Grunt_crawler2) 
	{
		if (Grunt_crawler2.gameObject.CompareTag ("Enemy"))
		{
			Grunt_crawler2.gameObject.SetActive (false);
		}
	}
}