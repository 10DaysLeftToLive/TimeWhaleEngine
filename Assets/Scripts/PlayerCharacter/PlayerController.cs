using UnityEngine;
using System.Collections;
using SmoothMoves;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
	public float walkSpeed = 2.0f;
	public float gravity = 20.0f;
	public float pushPower = 2.0f;
	
	public CollisionFlags lastReturnedCollisionFlags;
	private GameObject pickedUpObject = null;
	
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
	
	public Capsule smallHitBox;
	public Capsule bigHitbox;
	
	CharacterController controller;
	
	public float currentVerticalSpeed = 0.0f;
	
	private Inventory inventory; 
	
	// Use this for initialization
	void Start () {
		controller = this.GetComponent<CharacterController>();
		Transform rightHandTransform = currentAnimation.GetSpriteTransform("Right Hand");
		inventory = new Inventory(rightHandTransform);
		AgeSwapMover.instance.SetPlayer(this);
	}
	
	// Update is called once per frame
	void Update () {		
		if(isControllable){
			if(isAffectedByGravity){
				ApplyGravity();
			}	
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

		AgeSwapMover.instance.ChangeAgePosition(newAge, previousAge);

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
	
	public void PickUpObject(GameObject toPickUp){
		inventory.PickUpObject(toPickUp);
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
		return (inventory.HasItem());
	}
	
	public GameObject GetItem(){
		return (inventory.GetItem());
	}
	
	public void DropItem(Vector3 toPlace) {
		inventory.DropItem(toPlace);
	}
	
	public void DisableHeldItem(){
		inventory.DisableHeldItem();
	}
	
	public bool CheckTransitionPositionSuccess(CharacterAge newAge, CharacterAge previousAge){
		return AgeSwapMover.instance.CheckTransitionPositionSuccess(newAge, previousAge);
	}
	
	public void ChangeAnimation(BoneAnimation newAnimation){
		if (currentAnimation != null){
			currentAnimation.gameObject.SetActiveRecursively(false);
		}
		
		currentAnimation = newAnimation;
		currentAnimation.gameObject.SetActiveRecursively(true);
	}
}
