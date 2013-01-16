using UnityEngine;
using System.Collections;
using SmoothMoves;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
	
	public float walkSpeed = 2.0f;
	public float gravity = 20.0f;
	
	private float currentVerticalSpeed = 0.0f;
	private float currentHorizontalSpeed = 0.0f;
	
	private CollisionFlags lastReturnedCollisionFlags;
	
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
	
	
	public BoneAnimation youngBoneAnimation;
	public BoneAnimation middleBoneAnimation;
	public BoneAnimation oldBoneAnimation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMovementControls();
			
		ApplyGravity();
		
		MoveCharacter();
	}
	
	void UpdateMovementControls(){
		float verticalMovement = Input.GetAxisRaw("Vertical");
		float horizontalMovement = Input.GetAxisRaw("Horizontal");	
		
		currentHorizontalSpeed = walkSpeed * horizontalMovement;
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
	
	
}
