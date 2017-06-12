using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {
	
	public bool netFacingRight = true;

	public Vector3 scale;

	public float maxSpeed = 10f;
	public bool jump = false;				// Condition for whether the player should jump.
	//private NetCorrector Scale;
	public bool facingRight = true;
	Animator anim;
	public bool grounded = false;
	//public Transform groundCheck;
	//float groundRadius = 0.2f;
	//public LayerMask whatIsGround;
	public float jumpForce = 700f;
	public float tauntProbability = 50f;    // Chance of a taunt happening.
	public float tauntDelay = 1f;			// Delay for when the taunt should happen.
	private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.

	public AudioClip[] jumpClips;           // Array of clips for when the player jumps.
	public AudioClip[] taunts;				// Array of clips for when the player taunts.

	public bool doubleJump = false;
	private float jumpHeight = 0;
	private float force = 100f;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		//Scale = GetComponent<NetCorrector> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space))
		{
			if ((grounded || !doubleJump) && Input.GetKeyDown(KeyCode.Space))
			{
				jumpHeight = 0f;
				jump = true;
				anim.SetBool("Ground", false);
				GetComponent<Rigidbody2D>().velocity = Vector3.zero;
				GetComponent<Rigidbody2D>().angularVelocity = 0f;

				if (!doubleJump && !grounded)
					doubleJump = true;
				grounded = false;
			}

			jumpHeight += force;
			if (jumpHeight < 500f)
				GetComponent<Rigidbody2D>().AddForce(new Vector2(0, force));
		}
		/*if (grounded && Input.GetKeyDown(KeyCode.LeftShift))
			{
				anim.SetBool("Crouch", true);
			}
		if (Input.GetKeyUp (KeyCode.LeftShift))
			anim.SetBool ("Crouch", true);*/


	}

	void FixedUpdate()
	{
		//grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		//anim.SetBool("Ground", grounded);

		if (grounded)
			doubleJump = false;

		anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

		//if (!grounded) return;

		float move = Input.GetAxis("Horizontal");

		anim.SetFloat("Speed", Mathf.Abs(move));

		GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		// If the input is moving the player right and the player is facing left...
		if (move > 0 && !facingRight)
			// ... flip the player.
			Flip();

		// Otherwise if the input is moving the player left and the player is facing right...
		else if (move < 0 && facingRight)
			// ... flip the player.
			Flip();

		if (jump)
		{
			// Set the Jump animator trigger parameter.
			anim.SetTrigger("Jump");

			// Play a random jump audio clip.
			int i = Random.Range(0, jumpClips.Length);
			//AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

			// Add a vertical force to the player.
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
			//Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
			//rb.AddForce(new Vector2(0f, jumpForce));

			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
		}
	}

	void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		CmdFlipSprite(facingRight);
		// Multiply the player's x local scale by -1.
		/*Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		scale = theScale;
		Debug.Log ("this is the flip");*/
		//Scale.scale = theScale;
	}


	public void CmdFlipSprite(bool facing)
	{
		netFacingRight = facing;
		if (netFacingRight)
		{
			Vector3 SpriteScale = transform.localScale;
			SpriteScale.x = 1.2f;
			transform.localScale = SpriteScale;
		}
		else
		{
			Vector3 SpriteScale = transform.localScale;
			SpriteScale.x = -1.2f;
			transform.localScale = SpriteScale;
		}
	}

	void FacingCallback(bool facing)
	{
		netFacingRight = facing;
		if (netFacingRight)
		{
			Vector3 SpriteScale = transform.localScale;
			SpriteScale.x = 1.2f;
			transform.localScale = SpriteScale;
		}
		else
		{
			Vector3 SpriteScale = transform.localScale;
			SpriteScale.x = -1.2f;
			transform.localScale = SpriteScale;
		}
	}


	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Ground")
		{
			Debug.Log("This is the ground");
			//grounded = true;
			//doubleJump = false;
		}
	}

	/*public IEnumerator Taunt()
	{
		// Check the random chance of taunting.
		float tauntChance = Random.Range(0f, 100f);
		if (tauntChance > tauntProbability)
		{
			// Wait for tauntDelay number of seconds.
			yield return new WaitForSeconds(tauntDelay);

			// If there is no clip currently playing.
			if (!GetComponent<AudioSource>().isPlaying)
			{
				// Choose a random, but different taunt.
				tauntIndex = TauntRandom();

				// Play the new taunt.
				GetComponent<AudioSource>().clip = taunts[tauntIndex];
				GetComponent<AudioSource>().Play();
			}
		}
	}*/


	int TauntRandom()
	{
		// Choose a random index of the taunts array.
		int i = Random.Range(0, taunts.Length);

		// If it's the same as the previous taunt...
		if (i == tauntIndex)
			// ... try another random taunt.
			return TauntRandom();
		else
			// Otherwise return this index.
			return i;
	}

	void OnParticleCollision2D(GameObject other) {
		Debug.Log ("Particle Collision!");
	}
}
