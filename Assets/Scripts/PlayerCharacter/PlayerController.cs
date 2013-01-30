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
	
	public GameObject destination;
	
	private GameObject finish;
	private PathFinding pathFinding;
	private Vector3[] path;
	private int pathIndex;
	private float speed = 5f;
	
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
	
	// Use this for initialization
	void Start () {
		EventManager.instance.mOnClickOnObjectAwayFromPlayerEvent += new EventManager.mOnClickOnObjectAwayFromPlayerDelegate (OnClickToMove);
		EventManager.instance.mOnClickNoObjectEvent += new EventManager.mOnClickedNoObjectDelegate (OnClickToMove);
	}
	
	private void OnClickToMove (EventManager EM, ClickPositionArgs e){	
		// you now have the position of a click that is either on an object and too far from the player
		// or on no object
		Vector3 pos = Camera.main.ScreenToWorldPoint(e.position);
		Debug.Log("Clicked to Move to " + pos);
		pathFinding = null;
		if (finish != null) Destroy(finish);
		int mask = (1 << 9);
		RaycastHit hit;
		if (Physics.Raycast(new Vector3(pos.x, pos.y, e.position.z), Vector3.down , out hit, Mathf.Infinity, mask)) {
			//Debug.Log("hit " + hit.transform.tag);
			Vector3 hitPos = hit.transform.position;
			finish = (GameObject)Instantiate(destination,new Vector3(pos.x, hitPos.y +1.5f, e.position.z),this.transform.rotation);
			pathFinding = new PathFinding();
			pathFinding.StartPath(this.transform.position ,new Vector3(pos.x, hitPos.y -.5f, .5f));
		}
    }
	
	// Update is called once per frame
	void Update () {
		if(isControllable){
			UpdateMovementControls();
			if(isAffectedByGravity){
				ApplyGravity();
			}	
		}
		
		if (pathFinding != null){
			pathFinding.Update();
			if (pathFinding.foundPath == 2){
				path = pathFinding.FoundPath();
				pathIndex = 1;
				Destroy(finish);
				pathFinding = null;
			}else if (pathFinding.foundPath == 1){
				Debug.Log("no path found");
				Destroy(finish);
				pathFinding = null;
			}
		}
		
		if (path != null)
			MoveCharacter(path[pathIndex]);
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
			currentAnimation.Play(Strings.animation_climb);
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
			currentAnimation.Play(Strings.animation_walk);
		}
		else if(trigger.tag == Strings.tag_GrowableUp){
			isTouchingGrowableUp = false;	
		}
	}
	
	private bool IsClimbable(Collider trigger){
		return (trigger.CompareTag(Strings.tag_Climbable) && trigger.renderer.enabled);
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
	
	void MoveCharacter(Vector3 dest){
		// Calculate actual motion
		//Vector3 movement = new Vector3(currentHorizontalSpeed, currentVerticalSpeed, 0 );
		//movement *= Time.deltaTime;
		
		Vector3 pos = this.transform.position;
		Vector3 movement = new Vector3(0,0,0);
		
		if (pos.x < dest.x){
			movement.x += speed;
		}else if (pos.x > dest.x){
			movement.x -= speed;	
		}
		if (pos.y < dest.y){
			movement.y += speed;
		}else if (pos.y > dest.y){
			movement.y -= speed;	
		}
		movement *= Time.deltaTime;

		if (NearPoint(dest)){
			pathIndex++;
			if (pathIndex >= path.Length)
				path = null;
		}
		
		Move(movement);
	}
	
	bool NearPoint(Vector3 point){
		Vector3 pos = this.transform.position;
		float difference = .1f;
		if (pos.x  < point.x + difference && pos.x > point.x - difference){
			if (pos.y  < point.y + difference && pos.y > point.y - difference)
				return true;
		}
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
	
	public void ChangeAge(CharacterAge newAge){
		if (isTouchingGrowableUp){
			Transform growableUpTSO = FindGrowableUp(currentTouchedGrowableUp);
			TeleportCharacterAbove(growableUpTSO);
		} else {
			ChangeAgePosition(newAge.sectionTarget);
		}
		
		ChangeAnimation(newAge.boneAnimation);
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
			
			// This is just to make it go above his head, should go into one of the character's hands
			Vector3 playerPos = transform.position;
			toPickUp.transform.position = new Vector3(playerPos.x, playerPos.y+.5f, playerPos.z);
			
			toPickUp.transform.parent = transform;
			pickedUpObject.GetComponent<InteractableOnClick>().Disable();
		}
	}
	
	private void SwapItems(GameObject toSwapIn){
		Vector3 positionToPlace = toSwapIn.transform.position;
		
		DropItem(positionToPlace);
		PickUpObject(toSwapIn);
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
	
	public void DropItem(Vector3 toPlace){
		pickedUpObject.GetComponent<InteractableOnClick>().Enable();
		pickedUpObject.transform.position = toPlace;
		pickedUpObject.transform.parent = null;
		pickedUpObject = null;	
	}
	
	public void DisableHeldItem(){
		pickedUpObject.SetActiveRecursively(false);
		pickedUpObject = null;	
	}
	
	public void TeleportCharacterAbove(Transform toTeleportAbove){
		transform.position = toTeleportAbove.position + new Vector3(0,TELEPORT_ABOVE_GROWABLE_DISTANCE,0);
	}	
	
	public void ChangeAgePosition(Transform newSectrionTarget){
		Vector3 newTargetPos = newSectrionTarget.position;
		Vector3 deltaPlayerToCurrentFrame = transform.position - newTargetPos;
		
		transform.position = new Vector3(newTargetPos.x + deltaPlayerToCurrentFrame.x,
										 newTargetPos.y + deltaPlayerToCurrentFrame.y,
										 newTargetPos.z);
	}
	
	public void ChangeAnimation(BoneAnimation newAnimation){
		if (currentAnimation != null){
			currentAnimation.gameObject.SetActiveRecursively(false);
		}
		
		currentAnimation = newAnimation;
		currentAnimation.gameObject.SetActiveRecursively(true);
	}
	
	public void Move(Vector3 toMove){
		CharacterController controller = GetComponent<CharacterController>();
		lastReturnedCollisionFlags = controller.Move(toMove);
	}
}
