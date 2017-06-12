using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignoreSelf : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col) {
		Physics2D.IgnoreCollision (gameObject.GetComponent<Collider2D> (), col.collider);

	}
}
