//name: Michael Royal
//course: CST 306
using UnityEngine;
using System.Collections;

public class demon_health : MonoBehaviour {
	

		// Update is called once per frame
		void Update()
		{

		}
		void OnTriggerEnter(Collider Demon) 
		{
		if (Demon.gameObject.CompareTag ("Enemy"))
			{
			Demon.gameObject.SetActive (false);
			}
		}
	}
