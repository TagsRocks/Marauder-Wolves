using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class World_Map : MonoBehaviour
{

    public int sceneToStart = 14;                                        //Index number in build settings of scene to load if changeScenes is true
    public bool changeScenes;                                           //If true, load a new scene when Start is pressed, if false, fade out UI and continue in single scene



    [HideInInspector]
    public bool inMainMenu = true;                                      //If true, pause button disabled in main menu (Cancel in input manager, default escape key)
    public Animator animColorFade; 					                    //Reference to animator which will fade to and from black when starting game.
    private ShowPanels showPanels;                                      //Reference to ShowPanels script on UI GameObject, to show and hide panels


    void Awake()
    {
        //Get a reference to ShowPanels attached to UI object
        showPanels = GetComponent<ShowPanels>();

        //Get a reference to PlayMusic attached to UI object
        // playMusic = GetComponent<PlayMusic>();
    }


    public void StartButtonClicked()
    {
        //Load the selected scene, by scene index number in build settings
        SceneManager.LoadScene(sceneToStart);


        //If changeScenes is true, start fading and change scenes halfway through animation when screen is blocked by FadeImage
        if (changeScenes)
        {

            //Set the trigger of Animator animColorFade to start transition to the FadeToOpaque state.
            // animColorFade.SetTrigger("fade");

        }

        //Hide the main menu UI element
        showPanels.HideMenu();
    }

    //Once the level has loaded
    void OnLevelWasLoaded()
    {

    }


    public void LoadDelayed()
    {
        //Pause button now works if escape is pressed since we are no longer in Main menu.
        inMainMenu = false;

        //Hide the main menu UI element
        showPanels.HideMenu();

        //Load the selected scene, by scene index number in build settings
        SceneManager.LoadScene(sceneToStart);
    }


    public void StartGameInScene()
    {
        //Pause button now works if escape is pressed since we are no longer in Main menu.
        inMainMenu = false;

        //  Debug.Log("Game started in same scene! Put your game starting stuff here.");
    }



}