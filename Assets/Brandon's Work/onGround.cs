using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onGround : MonoBehaviour {

	void OnTriggerStay2D(Collider2D col) {
		Mult_P_Cont mp_cont = transform.parent.GetComponent<Mult_P_Cont> ();
		Player_Controller cont = transform.parent.GetComponent<Player_Controller> ();
		if (mp_cont != null) {
			mp_cont.grounded = true;
			mp_cont.doubleJump = false;
		} else if (cont != null) {
			cont.grounded = true;
			cont.doubleJump = false;
		}
	}
	void OnTriggerExit2D(Collider2D col) {
		Mult_P_Cont mp_cont = transform.parent.GetComponent<Mult_P_Cont> ();
		Player_Controller cont = transform.parent.GetComponent<Player_Controller> ();
		if (mp_cont != null) {
			mp_cont.grounded = false;
		} else if (cont != null) {
			cont.grounded = false;
		}
	}

}
