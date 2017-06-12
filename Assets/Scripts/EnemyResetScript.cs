using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//name:Michael Royal
//course:CST 306

//This script ensures that the enemy return to the proper positions each wave
public class EnemyResetScript : MonoBehaviour 
{
	Vector3 originalPosition;			//Original position of the enemy
	Quaternion originalRotation;		//Original rotation of the enemy


	void OnEnable()
	{
		//Record the position and rotation
		//originalPosition = transform.position;
		//originalRotation = transform.rotation;

	}

	void OnDisable()
	{
		//Return to original position and rotation
		//transform.position = originalPosition;
		//transform.rotation = originalRotation;
		Application.LoadLevel (Application.loadedLevel);
	}


}
