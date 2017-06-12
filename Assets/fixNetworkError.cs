using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class fixNetworkError : MonoBehaviour {

	NetworkIdentity net;

	// Use this for initialization
	void Start () {
		net = GetComponent<NetworkIdentity> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
