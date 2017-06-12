

/*== 
When spawning
NetCorrector nc = go.GetComponent<NetCorrector>();
nc.rotation = go.transform.rotation;
nc.scale = go.transform.localScale;
NetworkServer.Spawn(go);


== To Update position/scaling*/

using UnityEngine;
using UnityEngine.Networking;
using System;

public class NetCorrector : NetworkBehaviour {
	[SyncVar]
	public Vector3 scale;

	void Start () {
		scale = new Vector3(1.2f,1.2f,1.2f);
		transform.localScale = scale;
	}

	void Update() {
		//Debug.Log (NetworkServer.active);
		transform.localScale = scale;
	}
}