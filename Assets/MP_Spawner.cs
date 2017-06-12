using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MP_Spawner : NetworkBehaviour
{
	public float spawnTime = 5f;		// The amount of time between each spawn.
	public float spawnDelay = 3f;		// The amount of time before spawning starts.
	public GameObject[] enemies;		// Array of enemy prefabs.
	[SerializeField]
	private startMPGame game;

	void Start ()
	{
		//game = (startMPGame)FindObjectOfType(typeof(startMPGame));
		// Start calling the Spawn function repeatedly after a delay .
		//InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}


	public void Spawn ()
	{
		Debug.Log ("is on the server spawn = " + isServer);
		if (game.gameReady) {
			StartCoroutine (spawnEnemy ());
			// Play the spawning effect from all of the particle systems.
			foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>()) {
				p.Play ();
			}
		}
	}

	IEnumerator spawnEnemy() {
		// Instantiate a random enemy.
		yield return new WaitForSeconds(2f);
		int enemyIndex = Random.Range(0, enemies.Length);
		if (enemyIndex == 2) {
			Vector3 pos = transform.position;
			pos.y -= 1f;
			GameObject clone = Instantiate (enemies [enemyIndex], pos, transform.rotation);
			CmdSpawnBomb(clone);
		} else {
			GameObject clone = Instantiate (enemies [enemyIndex], transform.position, transform.rotation);
			CmdSpawnBomb(clone);
		}

	}

	[Command]
	void CmdSpawnBomb(GameObject clone)
	{
		//GameObject item = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
		NetworkServer.Spawn(clone);
	}
}
