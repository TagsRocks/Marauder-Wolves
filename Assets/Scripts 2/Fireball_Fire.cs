//name: Michael Royal
//course: CST 306
using UnityEngine;
using System.Collections;

public class Fireball_Fire : MonoBehaviour {

	public GameObject fireShot;
	public float fireDelta = 0.5F;
	public float speed= 0.5f;
	private float nextFire = 0.5F;
	private GameObject newfireShot;
	private float myTime = 0.0F;


	void Update() {

		myTime = myTime + Time.deltaTime;

		if (Input.GetButton("Fire2") && myTime > nextFire) {
			nextFire = myTime + fireDelta;
			newfireShot = Instantiate(fireShot, transform.position, transform.rotation) as GameObject;
			//engeryBlast.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.VelocityChange);
			nextFire = nextFire - myTime;
			myTime = 0.0F;
		}
	}
}
