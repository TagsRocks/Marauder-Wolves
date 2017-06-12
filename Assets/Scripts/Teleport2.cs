//name; Michael Royal
//course: CST 306
using UnityEngine;
using System.Collections;

public class Teleport2 : MonoBehaviour {

    public bool changeScenes;						//If true, load a new scene when Start is pressed, if false, fade out UI and continue to next level.

    [HideInInspector]
    public Animator animColorFade;                  //Reference to animator which will fade to and from black when starting level.

    public AnimationClip fadeColorAnimationClip;        //Animation clip fading to color (black default) when changing scenes

    [HideInInspector]
    public AnimationClip fadeAlphaAnimationClip;        //Animation clip fading out UI elements alpha

	void OnTriggerEnter2D (Collider2D teleporter)
	{


		if (teleporter.gameObject.CompareTag("Player"))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(1);

		}

        //If changeScenes is true, start fading and change scenes halfway through animation when screen is blocked by FadeImage
        if (changeScenes)
        {
            //Use invoke to delay calling of LoadDelayed by half the length of fadeColorAnimationClip
            Invoke("LoadDelayed", fadeColorAnimationClip.length * .5f);

            //Set the trigger of Animator animColorFade to start transition to the FadeToOpaque state.
            animColorFade.SetTrigger("fade");
        }

    }
}