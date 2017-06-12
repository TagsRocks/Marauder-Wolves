using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back_To_Multiplayer : MonoBehaviour {

	[SerializeField]
	private GameObject menu;

	public void back() {
		menu.SetActive (true);
		gameObject.transform.parent.gameObject.SetActive (false);
	}
}
