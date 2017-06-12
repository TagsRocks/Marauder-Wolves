using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class Movement : MonoBehaviour {
	
	public enum State
	{
		Idle,
		Init,
		Setup,
		Run
	}
	
	public enum Turn
	{
		left = -1,
		none = 0,
		right = 1
	}
	
	public enum Forward
	{
		back = -1,
		none = 0,
		forward = 1
	}
	
	public float walkSpeed = 5;
	public float runMultiplier = 2;
	public float strafeSpeed = 4;
	public float rotateSpeed = 250;
	public float gravity = 20;
	public float airTime = 0;
	public float fallTime = .5f;
	public float jumpHeight = 8;
	public float jumpTime = 1.5f;
	
	private Transform _myTransform;
	private CharacterController _controller;	
	private Vector3 _moveDirection;
	private CollisionFlags _collisionFlags;
	private Turn _turn;
	private Forward _forward;
	private Turn _strafe;
	private bool _run;
	private bool _jump;
	private State _state;
	private bool _isSwimming;
	
	void Awake()
	{
		_myTransform = transform;
		_controller = GetComponent<CharacterController>();
		_state = State.Init;
	}
	
	// Use this for initialization
	IEnumerator Start () {	
		while(true)
		{
			switch(_state)
			{
			case State.Init:
				Init();
				break;
			case State.Setup:
				Setup();
				break;
			case State.Run:
				ActionPicker();
				break;
			}
			yield return null;
		}		
	}
	
	private void Init()
	{
		if(!GetComponent<CharacterController>())
			return;
		if(!GetComponent<Animation>())
			return;
		
		_state = State.Setup;
	}
	
	private void Setup()
	{	
		_moveDirection = Vector3.zero;
		GetComponent<Animation>().Stop();		
		GetComponent<Animation>().wrapMode = WrapMode.Loop;		
		GetComponent<Animation>()["jump"].layer = 1;
		GetComponent<Animation>()["jump"].wrapMode = WrapMode.Once;		
		GetComponent<Animation>().Play("idle");
		
		_turn = Movement.Turn.none;
		_forward = Movement.Forward.none;
		_strafe = Movement.Turn.none;
		_run = true;
		_jump = false;
		_isSwimming = false;
		
		_state = State.Run;
	}
	
	private void ActionPicker()
	{
		_myTransform.Rotate(0, (int)_turn * Time.deltaTime * rotateSpeed, 0);
				
		if(_controller.isGrounded || _isSwimming)
		{	
			airTime = 0;
			_moveDirection = new Vector3((int)_strafe ,0 ,(int)_forward);
			_moveDirection = _myTransform.TransformDirection(_moveDirection).normalized;
			_moveDirection *= walkSpeed;
			
			if(_forward != Forward.none)
			{
				if(_isSwimming)
					Swim();
				else
				{
					if(_run)
					{
						_moveDirection *= runMultiplier;
						Run ();
					}
					else
					{
						Walk();
					}
				}
			}
			else if(_strafe != Turn.none)
			{		
				Strafe();
			}
			else
			{
				if(_isSwimming)
					Swim();
				else
					Idle();
			}
			
			if(_jump)
			{
				if(airTime < jumpTime)
				{
					_moveDirection.y = jumpHeight;
					Jump();
					_jump = false;
				}
			}		
		}
		else
		{
			if((_collisionFlags & CollisionFlags.CollidedBelow) == 0)
			{
				airTime += Time.deltaTime;
				
				if(airTime > fallTime)
				{
					Fall();
				}
			}			
		}
		
		if(!_isSwimming)
			_moveDirection.y -= gravity * Time.deltaTime;
		
		_collisionFlags = _controller.Move(_moveDirection * Time.deltaTime);					
	}
	
	public void MoveMeForward(Forward z)
	{
		_forward = z;
	}		
	
	public void RotateMe(Turn y)
	{
		_turn = y;
	}
	
	public void StrafeMe(Turn x)
	{
		_strafe = x;
	}
	
	public void JumpUp()
	{
		//if(_controller.isGrounded)
			_jump = true;
	}
	
	public void ToggleRun()
	{
		_run = !_run;
	}
	
	public void IsSwimming(bool swim)
	{
		_isSwimming = swim;
	}
	
	private void Run()
	{
		GetComponent<Animation>()["run"].speed = 1.7f;
		GetComponent<Animation>().CrossFade("run");			
	}
	
	private void Walk()
	{
		GetComponent<Animation>().CrossFade("walk");			
	}
	
	public void Idle()
	{
		GetComponent<Animation>().CrossFade("idle");		
	}
	
	public void Fall()
	{
		GetComponent<Animation>().CrossFade("fall");		
	}
	
	public void Strafe()
	{
		GetComponent<Animation>().CrossFade("side");		
	}
	
	public void Jump()
	{
		GetComponent<Animation>().CrossFade("jump");		
	}
	
	public void Swim()
	{
		GetComponent<Animation>().CrossFade("swim");
	}
}
