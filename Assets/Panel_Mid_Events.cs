using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_Mid_Events : MonoBehaviour {
	[SerializeField]
	private GameObject host_game, join_game, login;
	// Use this for initialization
	void Start () {
		//host_game = GameObject.Find ("Host_Game");
		//join_game = GameObject.Find ("Join_Game");
		//login = GameObject.Find ("Player_Login");
	}
	
	public void Host() {
		host_game.SetActive (true);
		gameObject.SetActive (false);
	}
	public void Join() {
		join_game.SetActive (true);
		gameObject.SetActive (false);
	}
	public void Login() {
		login.SetActive (true);
		gameObject.SetActive (false);
	}
}
