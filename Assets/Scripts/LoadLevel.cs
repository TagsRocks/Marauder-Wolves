//Michael Royal CST 306

using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {

	public string levelToLoad;

	IEnumerator waitThen(){
        // Wait 9 seconds to load next level
		yield return new WaitForSeconds(9);
		Application.LoadLevel (levelToLoad);

	}


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		StartCoroutine (waitThen ());

	}
}
