using UnityEngine;
using System.Collections;
using SmoothMoves;

[RequireComponent(typeof(CharacterController))]
public class Player : Character {
	
	public float walkSpeed = 2.0f;
	public float gravity = 20.0f;
	public float pushPower = 2.0f;
	
	public CollisionFlags lastReturnedCollisionFlags;
	
	public bool isAffectedByGravity = true;
	
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
	
	// Use this for initialization
	protected override void Init(){
		EventManager.instance.mOnClickOnObjectAwayFromPlayerEvent += new EventManager.mOnClickOnObjectAwayFromPlayerDelegate (OnClickToInteract);
		EventManager.instance.mOnClickNoObjectEvent += new EventManager.mOnClickedNoObjectDelegate (OnClickToMove);
		EventManager.instance.mOnClickOnPlayerEvent += new EventManager.mOnClickOnPlayerDelegate (OnClickOnPlayer);
		AgeSwapMover.instance.SetPlayer(this);
		
	}
	
	// We want to be able to switch to move at any state when the player clicks
	private void OnClickToMove (EventManager EM, ClickPositionArgs e){
		Vector3 pos = Camera.main.ScreenToWorldPoint(e.position);
		
		pos.z = this.transform.position.z;
	
		touchParticleEmitter.transform.position = pos;
		touchParticleEmitter.Play();
		Debug.Log("Click on no object  at point " + pos);
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
    }
	
	private void OnClickToInteract(EventManager EM, ClickedObjectArgs e){
		Debug.Log("Click on " + e.clickedObject.name + " with tag " + e.clickedObject.tag + " at point " + e.clickedObject.transform.position);
		
		string tag = e.clickedObject.tag;
		
		Vector3 goal = e.clickedObject.transform.position;
		goal.z = this.transform.position.z;
		
		if (tag == Strings.tag_CarriableItem){
			Debug.Log (name + "Picking up item " + e.clickedObject.name);
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
			Debug.Log("Goal was " + toTalkWith.transform.position + " infront = " + goalPosInfront);
			EnterState(new MoveThenDoState(this, goalPosInfront, new TalkState(this, toTalkWith)));
		}
	}
	
	private void OnClickOnPlayer(EventManager EM){
		if (currentState.GetType() == typeof(TalkState)){ // if we are talking exit before doing anything else.
			EnterState(new IdleState(this));
		} else {
			if (Inventory.HasItem()){
				Inventory.DropItem(GetFeet());
			}
		}
	}
	
	// Update is called once per frame
	protected override void UpdateObject () {		
		if(isAffectedByGravity){
			ApplyGravity();
		}
		base.UpdateObject();
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
		}
		return false;
	}
	
	public void OnTriggerExit(Collider trigger){
		if(IsClimbable(trigger)){
			isAffectedByGravity = true;
		}
		else if(trigger.tag == Strings.tag_GrowableUp){
			isTouchingGrowableUp = false;	
		}
		
		isTouchingTrigger = false;
	}
	
	private bool IsClimbable(Collider trigger){
		return ((trigger.CompareTag(Strings.tag_Climbable) || trigger.CompareTag(Strings.tag_LadderTop)) && trigger.renderer.enabled);
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

		AgeSwapMover.instance.ChangeAgePosition(newAge, previousAge);

		ChangeHitBox(newAge, previousAge);
		ChangeAnimation(newAge.boneAnimation);
		Inventory.SwapItemWithCurrentAge(newAge.boneAnimation);
		
		isAffectedByGravity = true;
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
		Debug.Log("Animation data active in hiearchy: " + animationData.gameObject.activeInHierarchy);
		Debug.Log ("Is animation data enabled: " + animationData.enabled);
	}
	
	
}
