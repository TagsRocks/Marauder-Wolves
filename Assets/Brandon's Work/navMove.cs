using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navMove : MonoBehaviour {

	private UnityEngine.AI.NavMeshAgent agent;
	public GameObject player;
	private Transform parent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		parent = gameObject.transform.parent;
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		
		player = GameObject.FindGameObjectWithTag ("Player");
		transform.LookAt (player.transform);
		agent.destination = player.transform.position;
		if ((player.transform.position - transform.position).magnitude < 4)
			agent.Stop ();
		else {
			Vector3 pos = transform.position;
			pos.z = -0.1f;
			pos.y += 1f;
			parent.position = pos;
			transform.localPosition = Vector3.zero;
			agent.Resume ();
		}
		
	}
}
