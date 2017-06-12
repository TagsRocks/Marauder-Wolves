using UnityEngine;

public class Gun : MonoBehaviour
{
	public Rigidbody2D rocket;				// Prefab of the rocket.
	public float speed = 20f;				// The speed the rocket will fire at.
	public GameObject rocket2;
	public GameObject Weapon;
	private const string PLAYER_TAG = "Player";
	private Player_Controller playerCtrl;		// Reference to the PlayerControl script.
	private Animator anim;					// Reference to the Animator component.


	void Awake()
	{
		// Setting up the references.
		anim = transform.root.gameObject.GetComponent<Animator>();
		playerCtrl = transform.root.GetComponent<Player_Controller>();
	}

	void Start()
	{
		//Weapon = transform.FindChild ("Gun").gameObject;
	}
	void Update ()
	{
		//Debug.Log (transform.parent.transform.GetComponent<NetworkIdentity>().isLocalPlayer);
		// If the fire button is pressed...
		if(Input.GetButtonDown("Fire1"))
		{
			GetComponent<AudioSource>().Play();
			CmdPlayerShot();
			//Shoot ();
		}
	}

	void Shoot() {
		// ... set the animator Shoot trigger parameter and play the audioclip.
		//anim.SetTrigger("Shoot");

		// If the player is facing right...
		if(playerCtrl.facingRight)
		{
			// ... instantiate the rocket facing right and set it's velocity to the right. 
			Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(speed, 0);
		}
		else
		{
			// Otherwise instantiate the rocket facing left and set it's velocity to the left.
			Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0,0,180f))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(-speed, 0);
		}
	}

	void CmdPlayerShot() {
		Debug.Log ("Shooting");
		//GameObject bullet = (GameObject)Instantiate (
		//	rocket2, 
		//	Weapon.transform.position,
		//	Weapon.transform.rotation);
		Debug.Log(playerCtrl.facingRight);
		if(playerCtrl.facingRight)
		{
			// ... instantiate the rocket facing right and set it's velocity to the right. 
			GameObject bullet = (GameObject)Instantiate (
				rocket2, 
				Weapon.transform.position, 
				Quaternion.Euler (new Vector3 (0, 0, 0)));
			bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
			//Instantiate (bullet);
		}
		else
		{
			Debug.Log ("backwards");
			// Otherwise instantiate the rocket facing left and set it's velocity to the left.
			GameObject bullet = (GameObject)Instantiate (
				rocket2, 
				Weapon.transform.position, 
				Quaternion.Euler (new Vector3 (0, 0, 180f)));
			bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
			//Instantiate (bullet);
			//RpcflipBullet (bullet);
		}

	}

	void RpcflipBullet(GameObject bullet){
		bullet.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 180f));
	}
}
