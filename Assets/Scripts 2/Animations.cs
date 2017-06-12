 using UnityEngine;
 using System.Collections;
    
   public class Animations : MonoBehaviour
	{
		
	public Animator anim;
  
     void Start() {
        anim = GetComponent<Animator> ();
     }


	void Update() {
       
       if(Input.GetKeyUp("space"))
       {
         anim.Play("dana_01_jump_02");
       }
	    else if (Input.GetKeyDown(KeyCode.A)) {
         anim.Play("dana_01_walk_02");
     } else if (Input.GetKeyDown(KeyCode.D)) {
         anim.Play("dana_01_walk_02");
     } else if (Input.GetKeyDown(KeyCode.S)) {
         anim.Play("dana_01_crouch");
     } else {
         anim.Play("dana_01_idle_02");
     }
     }
	}