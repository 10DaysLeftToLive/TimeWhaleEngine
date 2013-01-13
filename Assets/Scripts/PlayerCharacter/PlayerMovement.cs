using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {
	
	public float walkSpeed = 2.0f;
	public float gravity = 20.0f;
	
	private float currentVerticalSpeed = 0.0f;
	private float currentHorizontalSpeed = 0.0f;
	
	private CollisionFlags lastReturnedCollisionFlags;

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
}
