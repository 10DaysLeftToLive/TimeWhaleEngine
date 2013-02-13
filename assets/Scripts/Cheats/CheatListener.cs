using UnityEngine;
using System.Collections;

public class CheatListener : MonoBehaviour {
	
	public PlayerController playerController;
	
	protected bool isFlying = false;
	protected bool isFastMovement = false;
	
	protected float speedIncrease = 2;
	
	// Use this for initialization
	void Start () {
		playerController = gameObject.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		Fly ();
		MoveFast ();
	}
	
	// Toggle flying
	void Fly () {
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
	void MoveFast () {		
		if (Input.GetKeyDown(KeyCode.M)) {
			isFastMovement = !isFastMovement;
			
			if (isFastMovement) {
				playerController.walkSpeed = playerController.walkSpeed * speedIncrease;
			} else {
				playerController.walkSpeed = playerController.walkSpeed / speedIncrease;	
			}
		}	
	}
}
