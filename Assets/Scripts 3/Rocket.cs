using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour 
{
	public GameObject explosion;		// Prefab of explosion effect.


	void Start () 
	{
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject, 2);
	}


	IEnumerator OnExplode()
	{
		// Create a quaternion with a random rotation in the z-axis.
		Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

		// Instantiate the explosion where the rocket is with the random rotation.
		GameObject clone = Instantiate(explosion, transform.position, randomRotation);
		yield return new WaitForSeconds (1f);
		Destroy (clone);
	}
	
	void OnTriggerEnter2D (Collider2D col) 
	{
		//Debug.Log ("rocket hit - " + col.name);
		// If it hits an enemy...
		if(col.tag == "Enemy")
		{
			// ... find the Enemy script and call the Hurt function.
			Enemy en = col.gameObject.GetComponent<Enemy>();
			Enemy4 en4 = col.gameObject.GetComponent<Enemy4>();
			MP_Enemy MP_en = col.gameObject.GetComponent<MP_Enemy>();
			MP_Enemy4 MP_en4 = col.gameObject.GetComponent<MP_Enemy4>();
			if (en != null)
				en.Hurt ();
			if (en4 != null)
				en4.Hurt ();
			if (MP_en != null)
				MP_en.Hurt ();
			if (MP_en4 != null)
				MP_en4.Hurt ();

			// Call the explosion instantiation.
			StartCoroutine(OnExplode());

			// Destroy the rocket.
			Destroy (gameObject);
		}
		// Otherwise if it hits a bomb crate...
		else if(col.tag == "BombPickup")
		{
			// ... find the Bomb script and call the Explode function.
			col.gameObject.GetComponent<Bomb>().Explode();

			// Destroy the bomb crate.
			Destroy (col.transform.root.gameObject);

			// Destroy the rocket.
			Destroy (gameObject);
		}
		// Otherwise if the player manages to shoot himself...
		else if(col.gameObject.tag != "Player")
		{
			// Instantiate the explosion and destroy the rocket.
			StartCoroutine(OnExplode());
			Destroy (gameObject);
		}
	}
}
