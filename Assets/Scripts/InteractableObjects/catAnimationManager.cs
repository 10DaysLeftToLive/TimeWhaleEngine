using UnityEngine;
using System.Collections;

public class catAnimationManager : MonoBehaviour {
	bool hasJumped;
	bool idolAnimation;
	public GameObject cat;
	private Animation boneAnimationList;
	
	public SmoothMoves.BoneAnimation catBones;
	
	void Start () {
		hasJumped = false;
		idolAnimation = true;
		boneAnimationList = cat.animation;
		foreach (AnimationState state in boneAnimationList) {
			state.speed = 10f;
		}
	}
	
	void OnTriggerEnter (Collider trigger) {
		if (trigger.gameObject.tag == "Player" && !hasJumped) {
			hasJumped = true;
			triggerJump();
		}
	}
	
	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.tag == "Ground" && !idolAnimation) {
			print("hello");
			idolAnimation = true;
			catBones.CrossFade("Idle",2f);
		}
	}
	
	void Update () {
	}
	
	void triggerJump() {
		idolAnimation = false;
		catBones.CrossFade("Land",.5f);
		cat.rigidbody.AddForce(250f,125f,0);
		foreach (AnimationState state in boneAnimationList) {
			state.speed = 5f;
		}
	}
}
