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
	
	public enum CharacterAgeState{
		YOUNG,
		MIDDLE,
		OLD,
	}
	public CharacterAgeState CurrentCharacterAge{
		get {return currentCharacterAge;}
	}
	public CharacterAgeState currentCharacterAge;
	
	public Vector3 CurrentFrameOriginPos{
		get {return currentFrameOriginPos;}
	}
	public Vector3 currentFrameOriginPos; 
	
	public bool isControllable = true;
	public bool isAffectedByGravity = true;
	public bool isTouchingGrowableUp = false;
	
	public BoneAnimation youngBoneAnimation;
	public BoneAnimation middleBoneAnimation;
	public BoneAnimation oldBoneAnimation;
	
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(isControllable){
			UpdateMovementControls();
			
			if(isAffectedByGravity){
			ApplyGravity();
			}
			
		}
			
		
		
		MoveCharacter();
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
		if(IsClimbable(trigger)){
			isAffectedByGravity = false;
			youngBoneAnimation.animation.Play("Climb");
		}
		else if(trigger.tag == Strings.tag_GrowableUp){
			isTouchingGrowableUp = true;	
		}
	}
	
	void OnTriggerStay(Collider trigger){
		if(IsClimbable(trigger)){
			isAffectedByGravity = false;
			youngBoneAnimation.animation.Play("Climb");
		}
		else if(trigger.tag == Strings.tag_GrowableUp){
			isTouchingGrowableUp = true;	
		}
	}
	
	void OnTriggerExit(Collider trigger){
		if(IsClimbable(trigger)){
			isAffectedByGravity = true;
			youngBoneAnimation.animation.Play("Walk");
		}
		else if(trigger.tag == Strings.tag_GrowableUp){
			isTouchingGrowableUp = false;	
		}
	}
	
	private bool IsClimbable(Collider trigger){
		return (trigger.CompareTag(Strings.tag_Climbable) && trigger.renderer.enabled);
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit){
		if(hit.transform.tag == Strings.tag_Pushable){
			PushPushableObject(hit);
		}
	
	}
	
	void PushPushableObject(ControllerColliderHit pushableObject){
		//NOTE THIS IS POOR PUSHABLE CODE!
		if(currentCharacterAge == CharacterAgeState.MIDDLE){
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
		
		CharacterController controller = GetComponent<CharacterController>();
		lastReturnedCollisionFlags = controller.Move(movement);
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
	
	void DisableAllBoneAnimations(){
		youngBoneAnimation.gameObject.SetActiveRecursively(false);
		middleBoneAnimation.gameObject.SetActiveRecursively(false);
		oldBoneAnimation.gameObject.SetActiveRecursively(false);
	}
	
	public void SetAge(CharacterAgeState age, Vector3 frameOriginPos){
		currentCharacterAge = age;
		currentFrameOriginPos = frameOriginPos;
		
		DisableAllBoneAnimations();
		
		switch(age){
			case CharacterAgeState.YOUNG:
				youngBoneAnimation.gameObject.SetActiveRecursively(true);
				break;
			case CharacterAgeState.MIDDLE:
				middleBoneAnimation.gameObject.SetActiveRecursively(true);
				break;
			case CharacterAgeState.OLD:
				oldBoneAnimation.gameObject.SetActiveRecursively(true);
				break;
		}
	}
	
	public void PickUpObject(GameObject toPickUp){
		if (HasItem()){
			Debug.Log("Already Have an item");
			// Play can't do that animation
		} else {
			pickedUpObject = toPickUp;
			
			// This is just to make it go above his head, should go into one of the character's hands
			Vector3 playerPos = transform.position;
			toPickUp.transform.position = new Vector3(playerPos.x, playerPos.y+.5f, playerPos.z);
			
			toPickUp.transform.parent = transform;
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
	
	public void DropItem(){
		pickedUpObject.transform.parent = null;
		pickedUpObject = null;	
	}
	
	public void DisableHeldItem(){
		pickedUpObject.SetActiveRecursively(false);
		pickedUpObject = null;	
	}
}
