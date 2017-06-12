using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {
	[SerializeField]
	Behaviour[] componentsToDisable;

	[SerializeField]
	string remoteLayerName = "remotePlayer";
	Camera mainCam;


	// Use this for initialization
	void Start () {
		RegisterPlayer ();
		if (!isLocalPlayer) {
			DisableComponents ();
			AssignRemoteLayer ();
		} else {
			mainCam = Camera.main;
			if (mainCam != null) {
				mainCam.gameObject.SetActive (false);
			}
				
		}

	}

	void RegisterPlayer() {
		string _ID = "Player" + GetComponent<NetworkIdentity>().netId;
		transform.name = _ID;
		base.OnStartLocalPlayer ();
	}
	void AssignRemoteLayer () {
		//gameObject.layer = LayerMask.NameToLayer (remoteLayerName);
	}

	void DisableComponents () {
		for (int i = 0; i < componentsToDisable.Length; i++) {
			componentsToDisable [i].enabled = false;
		
		}
		/*Renderer[] rens = GetComponentsInChildren<Renderer> ();
		foreach (Renderer ren in rens) {
			ren.enabled = false;
		}*/
	}
	// Update is called once per frame
	void Update () {
		
	}
}
