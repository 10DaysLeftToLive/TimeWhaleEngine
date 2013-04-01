using UnityEngine;
using SmoothMoves;
using System.Collections;

public class CharacterAge {
	public CharacterAgeState stateName;
	public BoneAnimation boneAnimation;
	public Transform sectionTarget;
	public Capsule capsule;
	
	public CharacterAge(CharacterAgeState _stateName, BoneAnimation _boneAnimation, Transform _sectionTarget, Capsule _capsule){
		stateName = _stateName;
		boneAnimation = _boneAnimation;
		sectionTarget = _sectionTarget;
		capsule = _capsule;
	}
}