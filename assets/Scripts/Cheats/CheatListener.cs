using UnityEngine;
using System.Collections;

public class CheatListener : MonoBehaviour {
	public bool isFlying = false;
	public bool isFastMovement = false;
	public bool isNoClip = false;
	public bool isSoundOff = true;
	
	public float speedIncrease = 2;
	
	protected PlayerController playerController;
	protected LevelManager levelManager;
	protected CharacterController characterController;
	
	private float currentHorizontalSpeed = 0.0f;
	
	void Start() {
		playerController = GameObject.Find("PlayerCharacter").GetComponent<PlayerController>();
		levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		characterController = GameObject.Find("PlayerCharacter").GetComponent<CharacterController>();
	}
	
	void Update() {
		Fly();
		MoveFast();
		ToggleSound();
		KeyboardMovement();
	}
	
	void KeyboardMovement(){
		float verticalMovement = Input.GetAxisRaw(Strings.ButtonVertical);
		float horizontalMovement = Input.GetAxisRaw(Strings.ButtonHorizontal);	
		
		currentHorizontalSpeed = playerController.walkSpeed * horizontalMovement;
		
		if(!playerController.isAffectedByGravity){
			playerController.currentVerticalSpeed = playerController.walkSpeed * verticalMovement;	
		}
		
		MovePlayer();
	}
	void MovePlayer(){
		// Calculate actual motion
		Vector3 movement = new Vector3(currentHorizontalSpeed, playerController.currentVerticalSpeed, 0 );
		movement *= Time.deltaTime;
		
		playerController.lastReturnedCollisionFlags = characterController.Move(movement);
	}
	
	void Fly() {
		if (isFlying){
			playerController.isAffectedByGravity = false;
		}
		
		if (Input.GetKeyDown(KeyCode.F)) {
			isFlying = !isFlying;
			
			if (!isFlying) {
				playerController.isAffectedByGravity = true;
			}
		}
	}
	
	void MoveFast() {		
		if (Input.GetKeyDown(KeyCode.M)) {
			isFastMovement = !isFastMovement;
			
			if (isFastMovement) {
				playerController.walkSpeed = playerController.walkSpeed * speedIncrease;
			} else {
				playerController.walkSpeed = playerController.walkSpeed / speedIncrease;	
			}
		}	
	}
	
	void NoClip() {		// NOTE DOES NOT WORK
		if (Input.GetKeyDown(KeyCode.N)) {
			isNoClip = !isNoClip;
			
			if (isNoClip) {
				characterController.detectCollisions = false;
			} else {
				characterController.detectCollisions = true;	
			}
		}	
	}
	
	void ToggleSound() {		
		if (Input.GetKeyDown(KeyCode.A)) {
			isSoundOff = !isSoundOff;
			
			if (isSoundOff) {
				AudioListener.pause = true;
			} else {
				AudioListener.pause = false;
			}
		}	
	}
}
