using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public float moveSpeed = 2f;		// The speed the enemy moves at.
	public int HP = 2;					// How many times the enemy can be hit before it dies.
	public Sprite deadEnemy;			// A sprite of the enemy when it's dead.
	public Sprite damagedEnemy;			// An optional sprite of the enemy when it's damaged.
	public AudioClip[] deathClips;		// An array of audioclips that can play when the enemy dies.
	public GameObject hundredPointsUI;	// A prefab of 100 that appears when the enemy dies.
	public float deathSpinMin = -100f;			// A value to give the minimum amount of Torque when dying
	public float deathSpinMax = 100f;			// A value to give the maximum amount of Torque when dying
	public GameObject player;
	public Animator anim;
	float agentSpeed;

	private SpriteRenderer ren;			// Reference to the sprite renderer.
	private Transform frontCheck;		// Reference to the position of the gameobject used for checking if something is in front.
	private bool dead = false;			// Whether or not the enemy is dead.
	private Score score;				// Reference to the Score script.
	private UnityEngine.AI.NavMeshAgent agent;
	public bool facingRight = false;
	private bool flipped = false;
	private Vector3 last;
	void Awake()
	{
		last = transform.position;
		// Setting up the references.
		ren = transform.Find("body").GetComponent<SpriteRenderer>();
		frontCheck = transform.Find("frontCheck").transform;
		score = GameObject.Find("Score").GetComponent<Score>();
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		agentSpeed = agent.speed;
	}
	void Start()
	{

	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (dead && col.gameObject.tag == "Player")
			Physics2D.IgnoreCollision (col.collider, gameObject.GetComponent<Collider2D> ());
		if (col.gameObject.tag == "platform" && !dead)
			Physics2D.IgnoreCollision (col.collider, gameObject.GetComponent<Collider2D> ());
	}
	void LateUpdate() {
		transform.rotation = Quaternion.Euler (Vector3.zero); // keeps rotation at zero
	}
	void FixedUpdate ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		Debug.Log (player);
		if (player != null && !dead) {
			transform.LookAt (player.transform);
			agent.destination = player.transform.position;
		

			if ((player.transform.position - transform.position).magnitude < 4) {
				agent.Stop ();
				anim.SetBool ("run", false);
				anim.SetBool ("attack", true);
				anim.SetBool ("attack2", true);
				anim.SetBool ("attack1", false);
			} else {
				if (last.x - transform.position.x > 0)
					facingRight = false;
				else if (last.x - transform.position.x < 0)
					facingRight = true;
				if (facingRight != flipped)
					Flip ();
				if ((player.transform.position - transform.position).magnitude < 14) {
					int attack = Random.Range (0, 40);
					if (attack == 2)
						anim.SetBool ("attack1", true);
					else
						anim.SetBool ("attack1", false);
				}
				last = transform.position;
				Vector3 pos = transform.position;
				pos.z = -0.2f;
				pos.y += 1f;
				transform.position = pos;
				agent.Resume ();
				anim.SetBool ("run", true);
				anim.SetBool ("attack", false);
				anim.SetBool ("attack2", false);

			}
		} else if (agent.isOnNavMesh == true)
			agent.isStopped = true;

		// if jumping up onto platform
		if (agent.currentOffMeshLinkData.offMeshLink != null) {
			if (agent.currentOffMeshLinkData.offMeshLink.tag == "platform")
				agent.speed = 2;
			else
				agent.speed = agentSpeed;
			//Debug.Log ("off mesh link == " + agent.currentOffMeshLinkData.offMeshLink.gameObject);
		}
		else
			agent.speed = agentSpeed;
		
		//Debug.Log((player.transform.position - transform.position).magnitude);
		// Create an array of all the colliders in front of the enemy.

		//GetComponent<Rigidbody2D> ().velocity = new Vector2 (transform.localScale.x * -moveSpeed,
		//GetComponent<Rigidbody2D> ().velocity.y);
		//	Debug.Log ("Inside of if statement");


		// If the enemy has one hit point left and has a damagedEnemy sprite...
		if(HP == 1 && damagedEnemy != null)
			// ... set the sprite renderer's sprite to be the damagedEnemy sprite.
			ren.sprite = damagedEnemy;

		// If the enemy has zero or fewer hit points and isn't dead yet...
		if(HP <= 0 && !dead)
			// ... call the death function.
			Death ();

		transform.rotation = Quaternion.Euler (Vector3.zero); // keeps rotation at zero
	}

	public void Hurt()
	{
		// Reduce the number of hit points by one.
		HP--;
	}

	void Death()
	{
		// Find all of the sprite renderers on this object and it's children.
		SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer>();

		// Disable all of them sprite renderers.
		foreach(SpriteRenderer s in otherRenderers)
		{
			//s.enabled = false;
		}

		// Re-enable the main sprite renderer and set it's sprite to the deadEnemy sprite.
		ren.enabled = true;
		ren.sprite = deadEnemy;

		// Increase the score by 100 points
		Score.score += 100;

		// Set dead to true.
		dead = true;

		// Allow the enemy to rotate and spin it by adding a torque.
		GetComponent<Rigidbody2D>().fixedAngle = false;
		//GetComponent<Rigidbody2D>().AddTorque(Random.Range(deathSpinMin,deathSpinMax));

		// Find all of the colliders on the gameobject and set them all to be triggers.
		Collider2D[] cols = GetComponents<Collider2D>();
		foreach(Collider2D c in cols)
		{
			c.enabled = false;
		}
		gameObject.GetComponent<Rigidbody2D> ().gravityScale = 0;
		gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
		// Play a random audioclip from the deathClips array.
		int i = Random.Range(0, deathClips.Length);
		AudioSource.PlayClipAtPoint(deathClips[i], transform.position);
		anim.Play ("death");
		//Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D> (),player.GetComponent<Collider2D>());
		agent.enabled = false;
		// Create a vector that is just above the enemy.
		Vector3 scorePos;
		scorePos = transform.position;
		scorePos.y += 1.5f;

		// Instantiate the 100 points prefab at this point.
		Instantiate(hundredPointsUI, scorePos, Quaternion.identity);
		transform.GetComponent<Enemy> ().enabled = false;
		//Destroy (gameObject);
	}


	public void Flip()
	{
		flipped = facingRight;
		// Multiply the x component of localScale by -1.
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;
	}
}
