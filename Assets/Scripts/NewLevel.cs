//Michael Royal
//CST 306

using UnityEngine;
using System.Collections;

public class NewLevel : MonoBehaviour {

		
		public float resetSpeed = 0.025f;		

		private float resetSpeedSqr;			


		void Start ()
		{
			
			resetSpeedSqr = resetSpeed * resetSpeed;

		}

		void Update () {
			
			if (Input.GetKeyDown (KeyCode.G)) {
				
			Gates_of_Hell ();
			}

		if (Input.GetKeyDown (KeyCode.C)) {
			
			Castle ();
		}
		}

		void Gates_of_Hell () {
        //loading the new level
        UnityEngine.SceneManagement.SceneManager.LoadScene ("Gates of Hell");
		}

		void Castle () {
        //loading the new level
        UnityEngine.SceneManagement.SceneManager.LoadScene("Apocalyptic Castle Landscape Level");
						}
}		