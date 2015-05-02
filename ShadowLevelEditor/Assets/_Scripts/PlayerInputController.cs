using UnityEngine;
using System.Collections;
using InControl;
using System.Collections.Generic;

public class PlayerInputController : MonoBehaviour {
	public static bool Jumping;
	public static int jumpOK;
	public static int playerStatus;
	public static bool restart;
	public static Vector3 platformVelXY;
	public static Vector3 platformVelZY;
	public List<GameObject> pXYhitObj = new List<GameObject>();
	public List<GameObject> pZYhitObj = new List<GameObject>();
	public Transform pXY;
	public Transform pZY;
	// movement config
	public float gravity = -25f;
	public float runSpeed = 3.8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float jumpHeight = 1.65f;

	public float normalizedHorizontalSpeed = 0;

	[System.Serializable]
	struct PlayerInfo2D {
		public Transform spriteTransform;
		public Animator animator;
		public CharacterController2D controller;
		// [HideInInspector]
		// public RaycastHit2D lastControllerColliderHit;
		[HideInInspector]
		public Vector3 velocity;
	}


	[SerializeField]
	PlayerInfo2D _playerXY;
	[SerializeField]
	PlayerInfo2D _playerZY;
	[SerializeField]
	private Character3D _playerXYZ;

	private PlayerInfo2D[] _all2DPlayers;
	public int _characterIndexUnderControl;

	void Awake()
	{
		jumpOK=2;
		restart=false;
		platformVelXY=new Vector3(0,0,0);
		platformVelZY=new Vector3(0,0,0);

		_all2DPlayers = new PlayerInfo2D[2];
		_all2DPlayers[0] = _playerXY;
		_all2DPlayers[1] = _playerZY;


		// listen to some events for illustration purposes
		//_controller.onControllerCollidedEvent += onControllerCollider;
		//_controller.onTriggerEnterEvent += onTriggerEnterEvent;
		//_controller.onTriggerExitEvent += onTriggerExitEvent;
	}

// TODO(Julian): Re-enable events and add a ref to the related controller
	// #region Event Listeners

	// void onControllerCollider( RaycastHit2D hit )
	// {
	// 	// bail out on plain old ground hits cause they arent very interesting
	// 	if( hit.normal.y == 1f )
	// 		return;

	// 	// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
	// 	//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	// }


	// void onTriggerEnterEvent( Collider2D col )
	// {
	// 	Debug.Log( "onTriggerEnterEvent: " + col.gameObject.name );
	// }


	// void onTriggerExitEvent( Collider2D col )
	// {
	// 	Debug.Log( "onTriggerExitEvent: " + col.gameObject.name );
	// }

	// #endregion


	//void FixedUpdate(){
//		if(pXYhitObj && pXYhitObj.GetComponent<toParent>() && pXYhitObj.GetComponent<toParent>().myVel!=new Vector3(0,0,0)){
//			pXY.transform.parent=pXYhitObj.transform;
//			_all2DPlayers[0].velocity.y=0;
//		}else{
//			pXY.transform.parent=GameObject.Find("Players").transform;
//		}
//		if(pZYhitObj && pZYhitObj.GetComponent<toParent>() && pZYhitObj.GetComponent<toParent>().myVel!=new Vector3(0,0,0)){
//			pZY.transform.parent=pZYhitObj.transform;
//			_all2DPlayers[0].velocity.y=0;
//		}else{
//			pZY.transform.parent=GameObject.Find("Players").transform;
//		}

	//}
	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void Update () {
		pXY.transform.position=pXY.transform.position+platformVelXY;
		pZY.transform.position=pZY.transform.position+platformVelZY;

		if(restart)
			Application.LoadLevel(Application.loadedLevel);

		if(InputManager.ActiveDevice.Action3.WasPressed || Input.GetKeyDown(KeyCode.R)){
			restart=true;
		}

		if (InputManager.ActiveDevice.Action2.WasPressed  || Input.GetKeyDown(KeyCode.DownArrow)) {
			_characterIndexUnderControl = (_characterIndexUnderControl + 1) % (_all2DPlayers.Length);
		}

		if (_playerXYZ.DoesExist) {
			if (InputManager.ActiveDevice.LeftBumper.WasPressed || InputManager.ActiveDevice.LeftTrigger.WasPressed || Input.GetKey(KeyCode.Z)) {
				_playerXYZ.Rotate(-1);
			}
			if (InputManager.ActiveDevice.RightBumper.WasPressed || InputManager.ActiveDevice.RightTrigger.WasPressed || Input.GetKey(KeyCode.X)) {
				_playerXYZ.Rotate(1);
			}
		}

		for (int i = 0; i < _all2DPlayers.Length; i++) {
			bool isInputAllowed = (i == _characterIndexUnderControl);
//use player status
			if(isInputAllowed)
				playerStatus=i;

			if(Character3D._pRotDirection!=0)
				isInputAllowed=false;

			SimulatePlayer(i,_all2DPlayers[i], isInputAllowed);
		}
	}



	void SimulatePlayer(int index, PlayerInfo2D player, bool isInputAllowed)
	{
		CharacterController2D _controller = player.controller;
		Transform spriteTransform = player.spriteTransform;

		// grab our current _velocity to use as a base for all calculations
		player.velocity = _controller.velocity;

		if( _controller.isGrounded )
			player.velocity.y = 0;


		if( isInputAllowed && (InputManager.ActiveDevice.DPadLeft.IsPressed || Input.GetKey(KeyCode.LeftArrow)))
		{
			normalizedHorizontalSpeed = 1;
			if( spriteTransform.localScale.x < 0f )
				spriteTransform.localScale = new Vector3( -spriteTransform.localScale.x, spriteTransform.localScale.y, spriteTransform.localScale.z );

			if( _controller.isGrounded ){
				player.animator.Play( Animator.StringToHash( "Run" ) );
			}
		}
		else if( isInputAllowed && (InputManager.ActiveDevice.DPadRight.IsPressed || Input.GetKey(KeyCode.RightArrow)))
		{
			normalizedHorizontalSpeed = -1;
			if( spriteTransform.localScale.x > 0f )
				spriteTransform.localScale = new Vector3( -spriteTransform.localScale.x, spriteTransform.localScale.y, spriteTransform.localScale.z );

			if( _controller.isGrounded ){
				player.animator.Play( Animator.StringToHash( "Run" ) );
			}

		}
		else
		{
			normalizedHorizontalSpeed = 0;

			if( _controller.isGrounded ){
				player.animator.Play( Animator.StringToHash( "Idle" ) );
			
			}
		}


		// we can only jump whilst grounded
		if(jumpOK!=index && jumpOK!=10)
		if( isInputAllowed && _controller.isGrounded && (InputManager.ActiveDevice.DPadUp.WasPressed || InputManager.ActiveDevice.Action1.WasPressed || Input.GetKeyDown(KeyCode.UpArrow)) )
		{
			player.velocity.x=0;
			player.velocity.y = Mathf.Sqrt( 2f * jumpHeight * -gravity );
			player.animator.Play( Animator.StringToHash( "Jump" ) );
			Jumping=true;

		}


		// apply horizontal speed smoothing it
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		player.velocity.x = Mathf.Lerp( player.velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor );

		// apply gravity before moving
		player.velocity.y += gravity * Time.deltaTime;

		_controller.move( player.velocity * Time.deltaTime );
	}
}
