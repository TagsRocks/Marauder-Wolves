using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Name: Brandon Woodard
//Course: CST306
public class EnergyBlast_Fire : MonoBehaviour {

    public GameObject energyBlast;
    public float fireDelta = 0.5F;
    public float speed = 0.01f;
    private float nextFire = 0.5F;
    private GameObject newenergyBlast;
    private float myTime = 0.0F;
	private GameObject bossHand;
	private GameObject player;

	void Start()
	{

		bossHand = GameObject.FindGameObjectWithTag ("BossHand");

		player = GameObject.FindGameObjectWithTag ("Player");
	}
	//This function gets called by animation event in Darkblast animation
	void Blast()
	{
		newenergyBlast= Instantiate(energyBlast,bossHand.transform.position, bossHand.transform.rotation) as GameObject;

		newenergyBlast.GetComponent<Rigidbody>().AddForce
		(-player.transform.position * speed, ForceMode.VelocityChange);

	}
}
