using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//using UnityEditor.Animations;
//Name: Brandon Woodard
//Course: CST306
public class AnimationStateCheck_Multiplayer : NetworkBehaviour {
	Animator animator;

	float timeRem = 50f;
	float ogTime = 400f;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();

	}

	// Update is called once per frame
	void FixedUpdate () {
		if (!transform.parent.transform.GetComponent<NetworkIdentity>().isLocalPlayer)
			return;
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
			animator.SetBool ("Walk", true);
			animator.SetBool ("Run", false);
			animator.SetBool ("Idle", false);
			animator.SetBool ("Jump", false);
			//			Debug.Log ("time remaining: " + timeRem);
			if(timeRem >= 0)
				timeRem -= Time.time;


		}else{
			do {
				if (animator.GetBool ("Run") == true) {
					animator.SetBool ("Idle", false);
					animator.SetBool ("Run", false);
					animator.SetBool ("Walk", true);
					animator.SetBool ("Jump", false);

				} else {
					animator.SetBool ("Run", false);
					animator.SetBool ("Walk", false);
					animator.SetBool ("Idle", true);
					animator.SetBool ("Jump", false);
				}
			} while(animator.GetBool ("Idle") == false);

			timeRem = Time.time +400f;
		}

		if (timeRem <= 0 && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))) {
			animator.SetBool ("Walk", false);
			animator.SetBool ("Run", true);
			animator.SetBool ("Idle", false);
			animator.SetBool ("Jump", false);
			//	Debug.Log ("Should be running");
		}
	}
}

