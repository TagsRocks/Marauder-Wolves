//name: Michael Royal
//course: CST 306
using UnityEngine;
using System.Collections;

public class Boss_health : MonoBehaviour {

	// Update is called once per frame
	void Update()
	{

	}
	void OnTriggerEnter(Collider Demon_Boss) 
	{
		if (Demon_Boss.gameObject.CompareTag ("Enemy"))
		{
			Demon_Boss.gameObject.SetActive (false);
		}
	}
}
