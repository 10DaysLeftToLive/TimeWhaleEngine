using UnityEngine;
using System;
using System.Collections;
using SmoothMoves;

[System.Serializable]
public class PlayerAnimationContainer{
	public BoneAnimation youngBoneAnimation;
	public BoneAnimation middleBoneAnimation;
	public BoneAnimation oldBoneAnimation;
	
	public void PlayAnimation(string animationToPlay){
		try{
			youngBoneAnimation.Play(animationToPlay);
		}catch(NullReferenceException e){
			e.ToString();
		}
		
		try{
			middleBoneAnimation.Play(animationToPlay);
		}catch(NullReferenceException e){
			e.ToString();
		}
	
		try{
			oldBoneAnimation.Play(animationToPlay);	
		}catch(NullReferenceException e){
			e.ToString();
		}
	}
}
