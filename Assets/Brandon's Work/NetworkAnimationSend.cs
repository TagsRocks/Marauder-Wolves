using UnityEngine;
using UnityEngine.Networking;

public class NetworkAnimationSend : NetworkBehaviour {

	public Animator anim;
	// Use this for initialization
	public override void OnStartLocalPlayer () {
		anim = transform.GetComponentInChildren<Animator> ();
		for (int i = 0; i < anim.parameterCount; i++)
			GetComponent<NetworkAnimator> ().SetParameterAutoSend(i, true);
	}

	public override void PreStartClient () {
		anim = transform.GetComponentInChildren<Animator> ();
		for (int i = 0; i < anim.parameterCount; i++)
			GetComponent<NetworkAnimator> ().SetParameterAutoSend(i, true);
	}
}
