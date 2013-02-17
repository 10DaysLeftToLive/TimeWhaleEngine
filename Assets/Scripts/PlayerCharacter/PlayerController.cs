using UnityEngine;
using System.Collections;
using SmoothMoves;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
	public float walkSpeed = 2.0f;
	public float gravity = 20.0f;
	public float pushPower = 2.0f;
	
	private float currentVerticalSpeed = 0.0f;
	private float currentHorizontalSpeed = 0.0f;
	
	private CollisionFlags lastReturnedCollisionFlags;
	private GameObject pickedUpObject = null;
	
	private Path path;
	private float speed = 5f;
	private float stuckTimer;
	private bool moving = false;
	
	public bool isControllable = true;
	public bool isAffectedByGravity = true;
	
	public bool IsTouchingGrowableUp{
		get{return isTouchingGrowableUp;}
	}
	public bool isTouchingGrowableUp = false;
	
	public GameObject CurrentTouchedGrowableUp{
		get{return currentTouchedGrowableUp;}
	}
	private GameObject currentTouchedGrowableUp;
	
	public bool isTouchingTrigger = false;
	
	private static readonly float TELEPORT_ABOVE_GROWABLE_DISTANCE = .750f;
	
	private BoneAnimation currentAnimation = null;
	
	private static float RIGHT = 1;
	private static float LEFT = -1;
	
	private static float RAYCAST_Z_OFFSET = -2;
	
	public Capsule smallHitBox;
	public Capsule bigHitbox;

    public AudioSource ClimbLadderSFX;
    public AudioSource DoorCloseSFX;
    public AudioSource GiveItemSFX;
    public AudioSource PickUpItemSFX;
    public AudioSource PutDownItemSFX;
	
	CharacterController controller;
	
	private static Vector3 ZEROVELOCITY = new Vector3(0f,0f,0f);
	
	// Use this for initialization
	void Start () {
		EventManager.instance.mOnClickOnObjectAwayFromPlayerEvent += new EventManager.mOnClickOnObjectAwayFromPlayerDelegate (OnClickToMove);
		EventManager.instance.mOnClickNoObjectEvent += new EventManager.mOnClickedNoObjectDelegate (OnClickToMove);
		controller = this.GetComponent<CharacterController>();
	}
	
	private void OnClickToMove (EventManager EM, ClickPositionArgs e){	
		//currentAnimation.Play(Strings.animation_walk);
		
		Vector3 pos = Camera.main.ScreenToWorldPoint(e.position);
		pos.z = this.transform.position.z;
		int mask = (1 << 9);
		RaycastHit hit;
		if (Physics.Raycast(pos, Vector3.down , out hit, Mathf.Infinity, mask)) {
			Vector3 hitPos = hit.point;
			hitPos.y += controller.height/2;
			path = new Path();
			moving = false;
			if (PathFinding.StartPath(this.transform.position, hitPos, controller.height/2)){
				path = PathFinding.GetPath();
				moving = true;
			}
		}
    }
	
	// Update is called once per frame
	void Update () {/*		
		if (controller.velocity.Equals(ZEROVELOCITY)){
			currentAnimation.Play(Strings.animation_stand);
		} else {
			currentAnimation.Play(Strings.animation_walk);
		}*/
		
		if(isControllable){
			UpdateMovementControls();
			if(isAffectedByGravity){
				ApplyGravity();
			}	
		}
		
		if(currentHorizontalSpeed == 0 && currentVerticalSpeed == 0  && !isTouchingTrigger){
			//currentAnimation.Play(Strings.animation_stand);
		}
		
		if(currentHorizontalSpeed != 0 && !isTouchingTrigger){
			//currentAnimation.Play(Strings.animation_walk);	
		}
		
		MoveCharacter();
		if (moving) MoveCharacter(this.transform.position);
	}
	
	void UpdateMovementControls(){
		float verticalMovement = Input.GetAxisRaw(Strings.ButtonVertical);
		float horizontalMovement = Input.GetAxisRaw(Strings.ButtonHorizontal);	
		
		currentHorizontalSpeed = walkSpeed * horizontalMovement;
		
		if(!isAffectedByGravity){
			currentVerticalSpeed = walkSpeed * verticalMovement;	
		}
	}
	
	void OnTriggerEnter(Collider trigger){		
		isTouchingTrigger = CheckTriggers(trigger);
	}
	
	void OnTriggerStay(Collider trigger){
		isTouchingTrigger = CheckTriggers(trigger);
	}
	
	bool CheckTriggers(Collider trigger){
		if(IsClimbable(trigger)){
			isAffectedByGravity = false;
			//currentAnimation.Play(Strings.animation_climb);

            if(ClimbLadderSFX.timeSamples == 0){
                ClimbLadderSFX.Play();
            }
			return true;
		}
		else if(trigger.tag == Strings.tag_GrowableUp){
			SetTouchingGrowableUp(true, trigger.gameObject);	
			return true;
		}
		return false;
	}
	
	void OnTriggerExit(Collider trigger){
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
	
	void OnControllerColliderHit(ControllerColliderHit hit){
		if(hit.transform.tag == Strings.tag_Pushable && hit.transform.renderer.enabled == true){
			PushPushableObject(hit);
		}
	}
	
	void PushPushableObject(ControllerColliderHit pushableObject){
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
	
		void MoveCharacter(){
			// Calculate actual motion
			Vector3 movement = new Vector3(currentHorizontalSpeed, currentVerticalSpeed, 0 );
			movement *= Time.deltaTime;
		
			if(movement.x != 0){
				if(movement.x < 0){
					LookLeft();
				}else{
					LookRight();
				}
			}
			
			Move(movement);
		}
	
	void MoveCharacter(Vector3 pastPosition){
		Vector3 pos = this.transform.position;
		Vector3 movement = new Vector3(0,0,0);
		if (path.GetDirection() == 0){
			if (pos.x < path.GetPoint().x){
			movement.x += speed;
			LookRight();
			}else if (pos.x > path.GetPoint().x){
			movement.x -= speed;	
			LookLeft();
			}
		}else {
			if (pos.y < path.GetPoint().y){
				movement.y += speed;
			}else if (pos.y > path.GetPoint().y){
				movement.y -= speed;	
			}
		}
		movement *= Time.deltaTime;
		if (NearPoint(path.GetPoint(), path.GetDirection())){
			if (!path.NextNode()){
				moving = false;
			}
		}
		Move(movement);
		
		if (pastPosition == this.transform.position){
			stuckTimer += Time.deltaTime;
			//Debug.Log("Stuck");	
		}else{
			stuckTimer = 0;	
		}
		
		if (stuckTimer > .5f){
			moving = false;
		}
	}
	
	private void LookRight(){
		this.transform.localScale = new Vector3(RIGHT, 1, 1);
	}
	
	private void LookLeft(){
		this.transform.localScale = new Vector3(LEFT, 1, 1);
	}
	
	bool NearPoint(Vector3 point, int dir){
		Vector3 pos = this.transform.position;
		float difference = speed*Time.deltaTime;
		if ((dir == 0 || dir == 1) && (pos.x  < point.x + difference && pos.x > point.x - difference))
			return true;
		if ((dir == 2 || dir == 3) && (pos.y  < point.y + difference && pos.y > point.y - difference))
			return true;
	return false;
	}
	
	void ApplyGravity(){
		if (IsGrounded ()){
			currentVerticalSpeed = 0.0f;
		}
		else{
			currentVerticalSpeed -= gravity * Time.deltaTime;	
		}
	}
	
	bool IsGrounded () {
		return (lastReturnedCollisionFlags & CollisionFlags.CollidedBelow) != 0;
	}
	
	void SetTouchingGrowableUp(bool flag, GameObject growableUpTransform){
		isTouchingGrowableUp = flag;
		currentTouchedGrowableUp = growableUpTransform;
	}
	
	public void ChangeAge(CharacterAge newAge, CharacterAge previousAge){
		CheckItemSwapWithAge(newAge);
		
		if (isTouchingGrowableUp){
			Transform growableUpTSO = FindGrowableUp(currentTouchedGrowableUp);
			TeleportCharacterAbove(growableUpTSO);
		} else {
			ChangeAgePosition(newAge, previousAge);
		}
		
		ChangeHitBox(newAge, previousAge);
		ChangeAnimation(newAge.boneAnimation);
		SwapItemWithCurrentAge();
		
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
	
	Transform FindGrowableUp(GameObject currentlyTouchingGrowableUp){
		GrowableTree[] treeBaseObjects = (GrowableTree[]) GameObject.FindObjectsOfType(typeof(GrowableTree));
		
		foreach(GrowableTree treeBase in treeBaseObjects){
			if(treeBase.tree.objectLabel == currentlyTouchingGrowableUp.name){
				return treeBase.tree.GetTimeObjectAt(CharacterAgeManager.GetCurrentAgeState()).transform.GetChild(0);
			}
		}
		return null;
	}
	
	public void PickUpObject(GameObject toPickUp){
		if (HasItem()){
			SwapItems(toPickUp);
		} else {
			pickedUpObject = toPickUp;
			
			ItemManager item = new ItemManager();
			Debug.Log(item.FirstAppearance(toPickUp));
			// This is just to make it go above his head, should go into one of the character's hands
			
			
			Vector3 playerPos = transform.position;
			
			Transform rightHandTransform = currentAnimation.GetSpriteTransform("Right Hand");
			toPickUp.transform.position = new Vector3(rightHandTransform.position.x, 
				rightHandTransform.position.y, rightHandTransform.position.z);
			
			toPickUp.transform.parent = rightHandTransform;
			pickedUpObject.GetComponent<InteractableOnClick>().Disable();
		}
        PickUpItemSFX.Play();
	}
	
	private void SwapItems(GameObject toSwapIn){
		Vector3 positionToPlace = toSwapIn.transform.position;
		
		DropItem(positionToPlace);
		PickUpObject(toSwapIn);
	}
	
	
	private void CheckItemSwapWithAge(CharacterAge newAge){
		/*if (pickedUpObject != null) {
			ItemManager item = new ItemManager();
			if (item.FirstAppearance(pickedUpObject) > (int)newAge.stateName){
				DropItem(new Vector3(this.transform.position.x, this.transform.position.y - 
						(GetComponent<CharacterController>().height/2),this.transform.position.z));
					Debug.Log("Dropped at " + this.transform.position);
			}
		}*/
	}
	
	protected void SwapItemWithCurrentAge() {
		if (pickedUpObject != null) {
			Vector3 oldScale = pickedUpObject.transform.localScale;
			pickedUpObject.transform.parent = null;
			Transform rightHand = currentAnimation.GetSpriteTransform("Right Hand");
			pickedUpObject.SetActiveRecursively(true);
			pickedUpObject.transform.position = rightHand.position;
			pickedUpObject.transform.parent = rightHand;
			pickedUpObject.transform.localScale = oldScale;
			Debug.Log("Carrying item with us through age: " + pickedUpObject);
		}
	}
	
	public void InteractWithObject(GameObject toInteractWith){
		if(toInteractWith.tag.Equals(Strings.tag_CarriableItem)){
			PickUpObject(toInteractWith);	
		}
		else if (toInteractWith.tag.Equals(Strings.tag_Interactable)){
			if(pickedUpObject != null){
				toInteractWith.GetComponent<InteractableObject>().Interact(pickedUpObject);
			}
		}
	}
	
	public bool HasItem(){
		return (pickedUpObject != null);
	}
	
	public GameObject GetItem(){
		return (pickedUpObject);
	}
	
	public void DropItem(Vector3 toPlace) {
		pickedUpObject.GetComponent<InteractableOnClick>().Enable();
		pickedUpObject.transform.parent = null;
		pickedUpObject.transform.position = toPlace;
		pickedUpObject = null;
        PutDownItemSFX.Play();
	}
	
	public void DisableHeldItem(){
		pickedUpObject.SetActiveRecursively(false);
		pickedUpObject = null;	
	}
	
	public void DestroyHeldItem() {
		Destroy(pickedUpObject);
	}
	
	public void TeleportCharacterAbove(Transform toTeleportAbove){
		transform.position = toTeleportAbove.position + new Vector3(0,TELEPORT_ABOVE_GROWABLE_DISTANCE,0);
	}	
	
	public bool CheckTransitionPositionSuccess(CharacterAge newAge, CharacterAge previousAge){
		Vector3 playerCenter = GetNewAgeWorldPosition(newAge, previousAge);
		
		CharacterController charControl = GetComponent<CharacterController>();
		
		Vector3 linecastTopLeftStart = new Vector3(playerCenter.x - charControl.radius, playerCenter.y + (charControl.height * 0.5f),playerCenter.z - charControl.radius + RAYCAST_Z_OFFSET);
		Vector3 linecastTopLeftEnd = new Vector3(playerCenter.x - charControl.radius, playerCenter.y + (charControl.height * 0.5f), playerCenter.z + charControl.radius);
		
		Vector3 linecastTopRightStart = new Vector3(playerCenter.x + charControl.radius, playerCenter.y + (charControl.height * 0.5f),playerCenter.z - charControl.radius + RAYCAST_Z_OFFSET);
		Vector3 linecastTopRightEnd = new Vector3(playerCenter.x + charControl.radius, playerCenter.y + (charControl.height * 0.5f), playerCenter.z + charControl.radius);
		
		Vector3 linecastBottomLeftStart = new Vector3(playerCenter.x - charControl.radius, playerCenter.y - (charControl.height * 0.5f),playerCenter.z - charControl.radius + RAYCAST_Z_OFFSET);
		Vector3 linecastBottonLeftEnd = new Vector3(playerCenter.x - charControl.radius, playerCenter.y - (charControl.height * 0.5f), playerCenter.z + charControl.radius);
		
		Vector3 linecastBottomRightStart = new Vector3(playerCenter.x + charControl.radius, playerCenter.y - (charControl.height * 0.5f),playerCenter.z - charControl.radius + RAYCAST_Z_OFFSET);
		Vector3 linecastBottomRightEnd = new Vector3(playerCenter.x + charControl.radius, playerCenter.y - (charControl.height * 0.5f), playerCenter.z + charControl.radius);
		
		RaycastHit hit = new RaycastHit();
		/*
		Debug.DrawLine(linecastTopLeftStart, linecastTopLeftEnd,Color.red, 100);
		Debug.DrawLine(linecastTopRightStart, linecastTopRightEnd,Color.red, 100);
		Debug.DrawLine(linecastBottomLeftStart, linecastBottonLeftEnd,Color.red, 100);
		Debug.DrawLine(linecastBottomRightStart, linecastBottomRightEnd,Color.red, 100);
		*/
		if(Physics.Linecast(linecastTopLeftStart, linecastTopLeftEnd, out hit) ||
			Physics.Linecast(linecastTopRightStart, linecastTopRightEnd, out hit) ||
			Physics.Linecast(linecastBottomLeftStart, linecastBottonLeftEnd, out hit) ||
			Physics.Linecast(linecastBottomRightStart, linecastBottomRightEnd, out hit)){
			Debug.Log ("PHASERS TARGETED: " + hit.transform.name);
			if(hit.transform.GetComponent<MeshRenderer>().enabled == true){
				if(hit.transform.tag == Strings.tag_Pushable ||
					hit.transform.tag == Strings.tag_Block ){
					return false;	
				}
			}
		}
		
		return true;	
		
	}
	
	public void ChangeAgePosition(CharacterAge newAge, CharacterAge previousAge){
		transform.position = GetNewAgeWorldPosition(newAge, previousAge);
	}
	
	private Vector3 GetNewAgeWorldPosition(CharacterAge newAge, CharacterAge previousAge){
		Vector3 deltaPlayerToCurrentFrame = transform.position - previousAge.sectionTarget.position;
		
		
		return new Vector3(newAge.sectionTarget.position.x + deltaPlayerToCurrentFrame.x,
										 newAge.sectionTarget.position.y + deltaPlayerToCurrentFrame.y - Mathf.Abs(previousAge.capsule.height/2 - newAge.capsule.height/2),
										 newAge.sectionTarget.position.z + deltaPlayerToCurrentFrame.z);
		
	}
	
	public void ChangeAnimation(BoneAnimation newAnimation){
		if (currentAnimation != null){
			currentAnimation.gameObject.SetActiveRecursively(false);
		}
		
		currentAnimation = newAnimation;
		currentAnimation.gameObject.SetActiveRecursively(true);
	}
	
	public void Move(Vector3 toMove){
		lastReturnedCollisionFlags = controller.Move(toMove);
	}
}
