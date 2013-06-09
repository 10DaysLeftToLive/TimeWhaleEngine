using UnityEngine;
using System.Collections;

public abstract class Sibling : NPC {
	private string gender;
	
	public void ChangeGender(string genderToChangeTo, SmoothMoves.BoneAnimation genderAnimation){
		gender = genderToChangeTo;
		this.SetCharacterPortrait("");
		ChangeAnimation(genderAnimation);
	}
	
	private void ChangeAnimation(SmoothMoves.BoneAnimation newAnimation){
		newAnimation.transform.position = transform.position;
		newAnimation.transform.parent = transform;
		
		if (animationData != null) {
			Utils.SetActiveRecursively(animationData.gameObject, false);
		}
		
		animationData = newAnimation;
		Utils.SetActiveRecursively(animationData.gameObject, true);
	}
	
	public override void SetCharacterPortrait(string emotion){
		if (gender == null) gender = (LevelManager.playerGender == CharacterGender.MALE ? "Female" : "Male");
		base.SetCharacterPortrait(emotion + gender);
	}
}