using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer3 : MonoBehaviour
{

    public Animator animColorFade; 					                      //Reference to animator which will fade to and from black when starting game.
    private float fastFadeIn = .01f;                                     //Very short fade time (10 milliseconds) to start playing music immediately without a click/glitch
    public Animator animMenuAlpha;                                      //Reference to animator that will fade out alpha of MenuPanel canvas group
    public AnimationClip fadeColorAnimationClip;                       //Animation clip fading to color (black default) when changing scenes
    public AnimationClip fadeAlphaAnimationClip;                      //Animation clip fading out UI elements alpha

    IEnumerator Start()
    {
        yield return new WaitForSeconds(8f);

        SceneManager.LoadScene(17);

    }
}
