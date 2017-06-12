using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnOnEffect : MonoBehaviour {

	GameObject child;
	// Use this for initialization
	void Start () {
		child = transform.GetChild (0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void turnOn() {
		StartCoroutine (partOn ());
	}

	IEnumerator partOn() {
		child.SetActive (true);
		yield return new WaitForSeconds (5f);
		child.SetActive (false);
	}
}
