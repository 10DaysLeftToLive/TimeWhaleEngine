using UnityEngine;
using System.Collections;
using SmoothMoves;

[RequireComponent(typeof(CharacterController))]
public class Player : Character {
	public float walkSpeed = 2.0f;
	public float gravity = 20.0f;
	public float pushPower = 2.0f;
	
	public CollisionFlags lastReturnedCollisionFlags;
	
	public bool isAffectedByGravity = false;
	
	public bool isTouchingGrowableUp = false;
	
	public ParticleSystem touchParticleEmitter;
	
	public Capsule smallHitBox;
	public Capsule bigHitbox;
	
	public GameObject CurrentTouchedGrowableUp{
		get{return currentTouchedGrowableUp;}
	}
	private GameObject currentTouchedGrowableUp;

	public bool isTouchingTrigger = false;
	
	public float currentVerticalSpeed = 0.0f;
	
	public NPC npcTalkingWith;
	
	private float timeSinceLastHold = 1; // start moving at first moment
	private static float TIMEBETWEENHOLDMOVES = .35f;
	private static float HOLDMINDISTANCEX = 2; 
	private static float HOLDMINDISTANCEY = 1; 
	
	// Use this for initialization
	protected override void Init(){
		EventManager.instance.mOnClickOnObjectAwayFromPlayerEvent += new EventManager.mOnClickOnObjectAwayFromPlayerDelegate (OnClickToInteract);
		EventManager.instance.mOnClickNoObjectEvent += new EventManager.mOnClickedNoObjectDelegate (OnClickToMove);
		EventManager.instance.mOnClickOnPlayerEvent += new EventManager.mOnClickOnPlayerDelegate (OnClickOnPlayer);
		EventManager.instance.mOnClickHoldEvent += new EventManager.mOnClickHoldDelegate (OnHoldClick);
		EventManager.instance.mOnClickHoldReleaseEvent += new EventManager.mOnClickHoldReleaseDelegate(OnHoldRelease);
		
		AgeSwapMover.instance.SetPlayer(this);
	}
	
	private Vector3 pos;
	// We want to be able to switch to move at any state when the player clicks
	private void OnClickToMove (EventManager EM, ClickPositionArgs e){
		pos = Camera.main.ScreenToWorldPoint(e.position);
		pos.z = this.transform.position.z;
		if (currentState.GetType() == typeof(TalkState)) return;
		
		// Will need to be changed with later refactoring
		if (currentState.GetType() == typeof(IdleState) || currentState.GetType() == typeof(ClimbIdleState)){ // if we are idled or climbing idled
			EnterState(new MoveState(this, pos)); // move normaly
		} else if (currentState.GetType() == typeof(GrabIdleState)){ // if we are attached to an object 
			EnterState(new GrabMoveState(this, pos));
		} else if (currentState.GetType() == typeof(MoveState)){
			EnterState(new MoveState(this, pos));
		} else if (currentState.GetType() == typeof(GrabMoveState)){
			EnterState(new GrabMoveState(this, pos));
		}
		touchParticleEmitter.transform.localPosition = pos;
		touchParticleEmitter.Play();
    }
	
	private void OnClickToInteract(EventManager EM, ClickedObjectArgs e){
		string tag = e.clickedObject.tag;
		Vector3 goal = e.clickedObject.transform.position;
		goal.z = this.transform.position.z;
		
		if (tag == Strings.tag_CarriableItem){
			EnterState(new MoveThenDoState(this, goal, new PickUpItemState(this, e.clickedObject)));
		} else if (tag == Strings.tag_Pushable){
			if (currentState.GetType() == typeof(GrabIdleState)){
				EnterState(new LetGoOfState(this, e.clickedObject));
			} else {
				if (Inventory.HasItem()){
					Inventory.DropItem(GetFeet());
				}
				EnterState(new MoveThenDoState(this, goal, new GrabOntoState(this, e.clickedObject)));
			}
		} else if (tag == Strings.tag_NPC){
			NPC toTalkWith = (NPC)e.clickedObject.gameObject.GetComponent<NPC>();
			Vector3 currentPos = this.transform.position;
			Vector3 goalPosInfront = Utils.GetPointInfrontOf(currentPos, toTalkWith.gameObject);
			EnterState(new MoveThenDoState(this, goalPosInfront, new TalkState(this, toTalkWith)));
		}
	}
	
	private void OnClickOnPlayer(EventManager EM){
		if (currentState.GetType() == typeof(TalkState)){ // if we are talking exit before doing anything else.
			CloseInteraction();
		} else {
			if (Inventory.HasItem()){
				Inventory.DropItem(GetFeet());
			}
		}
	}
	
	private void OnHoldClick(EventManager EM, ClickPositionArgs e){
		pos = Camera.main.ScreenToWorldPoint(e.position);
		if (currentState.GetType() == typeof(TalkState) || HoldIsTooClose(pos)){
			timeSinceLastHold = 1;
			return;
		}
		if (timeSinceLastHold > TIMEBETWEENHOLDMOVES){
			pos.z = this.transform.position.z;
			
			if (!(currentState is MoveState)){
				EnterState(new MoveState(this, pos)); // move normaly
			} else {
				((MoveState) currentState).UpdateGoal(pos);
			}
			
			timeSinceLastHold = 0;
		} else {
			timeSinceLastHold += Time.deltaTime;
		}
	}
	
	Vector3 playerScreenPos;
	private bool HoldIsTooClose(Vector3 clickPos){
		playerScreenPos = transform.position;
		return (Utils.CalcDistance(playerScreenPos.x, clickPos.x) < HOLDMINDISTANCEX && Utils.CalcDistance(playerScreenPos.y, clickPos.y) < HOLDMINDISTANCEY);
	}
	
	private void OnHoldRelease(EventManager EM){
		timeSinceLastHold = 1; // Make it so on the next hold we start right up
		EnterState(new IdleState(this));
	}
	
	// Update is called once per frame
	protected override void UpdateObject () {		
		if(isAffectedByGravity){
			ApplyGravity();
		}
		base.UpdateObject();
	}
	
	public void OnCollisionEnter(Collision collision) {
		//if (collision.collider == Strings.tag_Ground)
			Debug.Log("hit ground at collision");// + collision.collider);
	}
	
	public void OnCollisionStay(Collision collision){
		Debug.Log("hit ground at collision");
	}
	
	public void OnTriggerEnter(Collider trigger) {		
		isTouchingTrigger = CheckTriggers(trigger);
	}
	
	public void OnTriggerStay(Collider trigger){
		isTouchingTrigger = CheckTriggers(trigger);
	}
	
	protected bool CheckTriggers(Collider trigger){
		if(IsClimbable(trigger)){
			isAffectedByGravity = false;
			//animationData.Play(Strings.animation_climb);

            if(SoundManager.instance.ClimbLadderSFX.timeSamples == 0){
                SoundManager.instance.ClimbLadderSFX.Play();
            }
			return true;
		}
		else if(trigger.tag == Strings.tag_GrowableUp){
			SetTouchingGrowableUp(true, trigger.gameObject);	
			return true;
		}else if (trigger.tag == Strings.tag_Ground){
			Debug.Log("hit ground at trigger");
		}
		return false;
	}
	
	public void OnTriggerExit(Collider trigger){
		if(IsClimbable(trigger)){
			//isAffectedByGravity = true;
		}
		else if(trigger.tag == Strings.tag_GrowableUp){
			isTouchingGrowableUp = false;	
		}
		
		isTouchingTrigger = false;
	}
	
	private bool IsClimbable(Collider trigger){
		return ((trigger.CompareTag(Strings.tag_Climbable)) && trigger.renderer.enabled);
	}
	
	protected void OnControllerColliderHit(ControllerColliderHit hit){
		if(hit.transform.tag == Strings.tag_Pushable && hit.transform.renderer.enabled == true){
			PushPushableObject(hit);
		}
	}
	
	protected void PushPushableObject(ControllerColliderHit pushableObject){
		//NOTE THIS IS POOR PUSHABLE CODE!
		if(CharacterAgeManager.GetCurrentAgeState() == CharacterAgeState.MIDDLE){
		    Rigidbody body = pushableObject.collider.attachedRigidbody;
		
		    // no rigidbody
		    if (body == null || body.isKinematic) { return; }
		
		    // We dont want to push objects below us
		    if (pushableObject.moveDirection.y < -0.3) { return; }
			
		    Vector3 pushDir = new Vector3 (pushableObject.moveDirection.x, 0, 0); //Can only push along x-axis
		
		    body.velocity = pushDir * pushPower;
		}
	}

	protected void ApplyGravity(){
		if (IsGrounded ()){
			currentVerticalSpeed = 0.0f;
			isAffectedByGravity = false;
		}
		else{
			currentVerticalSpeed -= gravity * Time.deltaTime;	
		}
	}
	
	public bool IsGrounded () {
		return (lastReturnedCollisionFlags & CollisionFlags.CollidedBelow) != 0;
	}
	
	void SetTouchingGrowableUp(bool flag, GameObject growableUpTransform){
		isTouchingGrowableUp = flag;
		currentTouchedGrowableUp = growableUpTransform;
	}
	
	public void ChangeAge(CharacterAge newAge, CharacterAge previousAge){
		if (IsInteracting()){
			CloseInteraction();	
		}
		
		AgeSwapMover.instance.ChangeAgePosition(newAge, previousAge);

		ChangeHitBox(newAge, previousAge);
		ChangeAnimation(newAge.boneAnimation);
		Inventory.SwapItemWithCurrentAge(newAge.boneAnimation);
	}
	
	private void ChangeHitBox(CharacterAge newAge, CharacterAge previousAge){
		CharacterController charControl = GetComponent<CharacterController>();	
		charControl.radius = newAge.capsule.radius;
		charControl.height = newAge.capsule.height;
		charControl.center = newAge.capsule.center;
		
		if(previousAge.stateName == CharacterAgeState.YOUNG){
			if(newAge.stateName == CharacterAgeState.MIDDLE || newAge.stateName == CharacterAgeState.OLD){
				transform.position = new Vector3(transform.position.x, transform.position.y + newAge.capsule.height/2, + transform.position.z);
			}
		}
	}
	
	public void PickUpObject(GameObject toPickUp){
		Debug.Log ("Player transform: " + animationData.mLocalTransform);
		Inventory.PickUpObject(toPickUp);
	}
	
	
	public void DisableHeldItem(){
		Inventory.DisableHeldItem();
	}
	
	public bool CheckTransitionPositionSuccess(CharacterAge newAge, CharacterAge previousAge){
		return AgeSwapMover.instance.CheckTransitionPositionSuccess(newAge, previousAge);
	}
	
	public void ChangeAnimation(BoneAnimation newAnimation){
		if (animationData != null){
			Utils.SetActiveRecursively(animationData.gameObject, false);
		}
		
		animationData = newAnimation;
		Utils.SetActiveRecursively(animationData.gameObject, true);
		if (Inventory != null) {
			Transform rightHand = animationData.GetSpriteTransform("Right Hand");
			Inventory.ChangeRightHand(rightHand);
		}
	}
	
	private bool IsInteracting(){
		return (currentState is TalkState);	
	}
	
	private void CloseInteraction(){
		Debug.Log("Leaving interaction");
		GUIManager.Instance.CloseInteractionMenu();
	}
	
	public void LeaveInteraction(){
		EnterState(new IdleState(this));
	}
}
