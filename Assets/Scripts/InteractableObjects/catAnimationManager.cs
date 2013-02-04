using UnityEngine;
using System.Collections;

public class catAnimationManager : MonoBehaviour {

	bool hasJumped;
	bool gravityActivate;
	bool idolAnimation;
	float myTimer;
	float myTimerTriggerBox;
	public GameObject cat;
	private Animation boneAnimationList;
	
	public SmoothMoves.BoneAnimation catBones;
	
	void Start () {
		hasJumped = false;
		gravityActivate = false;
		idolAnimation = true;
		myTimer = 1.4f;		
		myTimerTriggerBox = 1f;
		boneAnimationList = cat.animation;
		
		foreach (AnimationState state in boneAnimationList) {
			state.speed = 10f;
		}
	}
	
	void OnTriggerExit (Collider trigger) {
		if (trigger.tag == Strings.tag_Player && !hasJumped) {
			hasJumped = true;
			//cat.collider.isTrigger = false;
			cat.rigidbody.useGravity = true;
			triggerJump();
		}
	}
	
	void OnCollisionEnter (Collision collision) {
		print ("hey! Collision");
		if (collision.gameObject.tag == "Ground" && !idolAnimation) {
			print("hello");
			idolAnimation = true;
			catBones.CrossFade("Idle",2f);
		}
	}
	
	void Update() {
		if (gravityActivate) {
			myTimer -= Time.deltaTime;
			myTimerTriggerBox -= Time.deltaTime;
		}
		if (myTimerTriggerBox <= 0) {
			cat.collider.isTrigger = false;
		}
		
		if (myTimer <= 0) {
			gravityActivate = false;
			cat.rigidbody.useGravity = false;
			cat.collider.isTrigger = true;
		}
	}
	
	void triggerJump() {
		print ("hey!2");
		gravityActivate = true;
		idolAnimation = false;
		catBones.CrossFade("Land",.5f);
		cat.rigidbody.AddForce(250f,125f,0);
		foreach (AnimationState state in boneAnimationList) {
			state.speed = 5f;
		}
	}
}
