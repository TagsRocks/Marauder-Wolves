using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class startMPGame : MonoBehaviour {

	public bool gameReady = false;

	public float spawnTime = 12f;		// The amount of time between each spawn.
	public float spawnDelay = 3f;		// The amount of time before spawning starts.
	public GameObject gameText;

	[SerializeField]
	private GameObject[] startEnemies = new GameObject[4];

	[SerializeField]
	private GameObject[] players;

	[SerializeField]
	private MP_Spawner[] spawners = new MP_Spawner[10];
	private NetworkManager nm;
	private bool endGame = false;

	void Start() {
		//gameText = transform.GetChild (0).gameObject;

		//spawners = GameObject.FindGameObjectsWithTag ("EnemySpawner");
		//spawners = (MP_Spawner)FindObjectsOfType(typeof(MP_Spawner));
		nm = NetworkManager.singleton;
		InvokeRepeating("newEnemys", spawnDelay, spawnTime);
	}
	// Update is called once per frame
	void Update () {
		players = GameObject.FindGameObjectsWithTag ("Player");
		if (players.Length > 1) {
			gameReady = true;
			gameText.GetComponent<Text> ().text = "";
			if ((players [0].GetComponent<MP_PlayerHealth> ().health < 0.1f) && (players [1].GetComponent<MP_PlayerHealth> ().health < 0.1f)) {
				gameText.GetComponent<Text> ().text = "Game Over";
				StartCoroutine (gameOver ());
			}
		} else if (gameReady) {
			gameText.GetComponent<Text> ().text = "Game Over";
			StartCoroutine (gameOver ());
		}
	}
	IEnumerator gameOver() {
		yield return new WaitForSeconds (3f);
		if (!endGame) {
			endGame = true;
			MatchInfo matchInfo = nm.matchInfo;
			nm.matchMaker.DropConnection (matchInfo.networkId, matchInfo.nodeId, 0, nm.OnDropConnection);
			nm.StopHost ();
		}

	}

	void newEnemys() {
		Debug.Log ("calling newEnemys scripts" + spawners.Length);
		if (gameReady && (spawners.Length>3)) {
			int spawn1 = Random.Range (0, spawners.Length);
			int spawn2 = Random.Range (0, spawners.Length);
			int spawn3 = Random.Range (0, spawners.Length);
			int spawn4 = Random.Range (0, spawners.Length);
			/*for (var i = 0; spawn2 == spawn1 || i < 30; i++) {
				spawn2 = Random.Range (0, spawners.Length - 1);
			}
			for (var i = 0; spawn3 == spawn1 || spawn3 == spawn2 || i < 30; i++) {
				spawn2 = Random.Range (0, spawners.Length - 1);
			}
			for (var i = 0; spawn4 == spawn1 || spawn4 == spawn2 || spawn4 == spawn3 || i < 30; i++) {
				spawn2 = Random.Range (0, spawners.Length - 1);
			}*/
			while (spawn2 == spawn1)
				spawn2 = Random.Range (0, spawners.Length);
			while (spawn3 == spawn1 || spawn3 == spawn2)
				spawn3 = Random.Range (0, spawners.Length);
			while (spawn4 == spawn1 || spawn4 == spawn2 || spawn4 == spawn3)
				spawn4 = Random.Range (0, spawners.Length);
			Debug.Log ("spawning enemies in pos: " + spawn1 + ", " + spawn2 + ", " + spawn3 + ", " + spawn4);
			spawners [spawn1].Spawn ();
			spawners [spawn2].Spawn ();
			spawners [spawn3].Spawn ();
			spawners [spawn4].Spawn ();
		}
	}
}
