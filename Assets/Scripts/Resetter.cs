using UnityEngine;
using System.Collections;

public class Resetter : MonoBehaviour {

	
	public float resetSpeed = 0.025f;		
	
	private float resetSpeedSqr;			
	
	void Start ()
	{
		//	Calculate the Reset Speed Squared from the Reset Speed
		resetSpeedSqr = resetSpeed * resetSpeed;

	}
	
	void Update () {
		//	If we hold down the "R" key...
		if (Input.GetKeyDown (KeyCode.R)) {
			//	... call the Reset() function
			Reset ();
		}
	}
	
	void Reset () {
		//	The reset function will Reset the game by reloading the same level
		Application.LoadLevel (Application.loadedLevel);
	}
}
