using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Host_Game : MonoBehaviour {

	[SerializeField]
	private uint roomSize = 2;

	private string roomName;
	private string password;
	private NetworkManager networkManager;


	// Use this for initialization
	void Start () {
		password = "";
		networkManager = NetworkManager.singleton;
		if (networkManager.matchMaker == null) {
			networkManager.StartMatchMaker ();
		}
	}

	public void setRoomName (string _name) {
		roomName = _name;
	}

	public void setRoomPass (string _pass) {
		password = _pass;
	}

	public void createRoom() {
		if (roomName != "" && roomName != null) {
			Debug.Log ("Creatin Room: " + roomName + " with room for " + roomSize);
			networkManager.matchMaker.CreateMatch (roomName, roomSize, true, "", "","",0,0,networkManager.OnMatchCreate);
			//SceneManager.LoadScene ("Multiplayer Lobby");
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
