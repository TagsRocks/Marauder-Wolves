using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class needleStick : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D col)
	{
		//Debug.Log ("hit");
		if (col.gameObject.tag == "Player" && gameObject.tag == "Spear") {
			PlayerHealth ph = col.gameObject.GetComponent<PlayerHealth> ();
			MP_PlayerHealth MP_ph = col.gameObject.GetComponent<MP_PlayerHealth> ();
			if (ph != null)
				ph.health -= 10;
			else if (MP_ph != null)
				MP_ph.health -= 10;
			
			Destroy (gameObject);
			//Debug.Log ("spear hit player");
			//transform.parent = col.transform;
			//transform.GetComponent<CapsuleCollider2D> ().enabled = false;
			//Destroy (transform.GetComponent<Rigidbody2D> ());
		} else if (gameObject.tag == "Spear") {
			//transform.parent = col.transform;
			transform.GetComponent<CapsuleCollider2D> ().enabled = false;
			Destroy (transform.GetComponent<Rigidbody2D> ());
			StartCoroutine (Die ());

		}
	}

	IEnumerator Die() {
		yield return new WaitForSeconds (3f);
		Destroy (gameObject);
	}
}
