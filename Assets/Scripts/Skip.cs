using UnityEngine;
using System.Collections;

 //Michael Royal CST 306

public class Skip : MonoBehaviour {

	public string levelToLoad;

	void Update () {
	
        //Press Space key to skip to next level
		if (Input.GetKeyDown (KeyCode.Space)) {

			Application.LoadLevel(levelToLoad);

		}


	}
}
