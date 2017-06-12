using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.Animations;

public class AnimationStateCheck : MonoBehaviour {
	Animator animator;
	private Player_Controller player;
	private bool jump = false;
	float timeRem = 50f;
	float ogTime = 400f;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		player = transform.GetComponentInParent<Player_Controller> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		/*if (player.grounded)
			jump = false;
		else
			jump = true;*/
		if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && !jump) {
			animator.SetBool ("Walk", true);
			animator.SetBool ("Run", false);
			animator.SetBool ("Idle", false);
			animator.SetBool ("Jump", false);
			//			Debug.Log ("time remaining: " + timeRem);
			if(timeRem >= 0)
				timeRem -= Time.time;


		}/*else if(Input.GetKey(KeyCode.Space) || jump) {
			animator.SetBool ("Walk", false);
			animator.SetBool ("Run", false);
			animator.SetBool ("Idle", false);
			animator.SetBool ("Jump", true);
		}*/else{
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

