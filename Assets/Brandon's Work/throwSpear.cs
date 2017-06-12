using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwSpear : MonoBehaviour {
	[SerializeField]
	private GameObject spear;

	void turnOn () {
		spear.SetActive (true);
	}

	public void thrown () {
		GameObject clone = Instantiate (spear);
		clone.transform.position = spear.transform.position;
		clone.transform.rotation = spear.transform.rotation;
		clone.transform.parent = null;
		Vector3 scale = -transform.parent.localScale;
		scale.x /= 2;
		scale.y /= 2;
		clone.transform.localScale = scale;
		clone.tag = "Spear";
		spear.SetActive (false);
		clone.AddComponent<Rigidbody2D> ();
		Physics2D.IgnoreCollision (clone.GetComponent<CapsuleCollider2D>(), transform.parent.GetComponent<Collider2D> ());
		bool right = transform.parent.GetComponent<Enemy> ().facingRight;

		if (right == false)
			clone.GetComponent<Rigidbody2D> ().AddForce (clone.transform.right * 1000f);
		else
			clone.GetComponent<Rigidbody2D> ().AddForce (clone.transform.right * (-1 * 1000f));
	}
}
