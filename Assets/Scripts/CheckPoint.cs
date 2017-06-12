using UnityEngine;
//name: Michael Royal
//course:CST 306

public class CheckPoint : MonoBehaviour 
{
   
    // Indicate if the checkpoint is activated
    
    public bool Activated = false;
    private Animator thisAnimator;

   
    // List with all checkpoints objects in the scene
    public static GameObject[] CheckPointsList;

    // Get position of the last activated checkpoint
    public static Vector3 GetActiveCheckPointPosition()
    {
		
        // If player die without activate any checkpoint,  will return a default position
        Vector3 result = new Vector3(0, 0, 0);

        if (CheckPointsList != null)
        {
            foreach (GameObject cp in CheckPointsList)
            {
                //  Search the activated checkpoint to get its position
                if (cp.GetComponent<CheckPoint>().Activated)
                {
                    result = cp.transform.position;
                    break;
                }
            }
        }

        return result;
    }

    // Activate the checkpoint
    
    private void ActivateCheckPoint()
    {
        //  deactive all checkpoints in the scene
        foreach (GameObject cp in CheckPointsList)
        {
            cp.GetComponent<CheckPoint>().Activated = false;
            cp.GetComponent<Animator>().SetBool("Active", false);
        }

        //  activated the current checkpoint
        Activated = true;
		thisAnimator.SetBool("Active", true);
		Debug.Log ("Checkpoint Actived");
    }

    void Start()
    {
        thisAnimator = GetComponent<Animator>();

        // search all the checkpoints in the current scene
        CheckPointsList = GameObject.FindGameObjectsWithTag("CheckPoint");
    }

    void OnTriggerEnter(Collider other)
    {
        // If the player passes through the checkpoint,  activate it
        if (other.tag == "Player")
        {
            ActivateCheckPoint();
        }
    }
}