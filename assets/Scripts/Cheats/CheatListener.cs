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
	
	// Use this for initialization
	void Start() {
		playerController = gameObject.GetComponent<PlayerController>();
		levelManager = gameObject.GetComponent<LevelManager>();
		characterController = gameObject.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update() {
		Fly();
		MoveFast();
		ToggleSound();
	}
	
	// Toggle flying
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
	
	// Toggle moving twice as fast
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
	
	// Toggle noClip *Seems to be impossible unless all possible collisions are turned off for the player's character controller*
	void NoClip() {		
		if (Input.GetKeyDown(KeyCode.N)) {
			isNoClip = !isNoClip;
			
			if (isNoClip) {
				characterController.detectCollisions = false;
			} else {
				characterController.detectCollisions = true;	
			}
		}	
	}
	
	// Toggle sound
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
