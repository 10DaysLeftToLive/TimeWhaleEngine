using UnityEngine;
using SmoothMoves;
using System.Collections;

public class CharacterAge {
	public CharacterAgeState stateName;
	public BoneAnimation boneAnimation;
	public Transform sectionTarget;
	public AudioSource backgroundMusic;
	public Capsule capsule;
	
	public CharacterAge(CharacterAgeState _stateName, BoneAnimation _boneAnimation, Transform _sectionTarget, AudioSource _backgroundMusic, Capsule _capsule){
		stateName = _stateName;
		boneAnimation = _boneAnimation;
		sectionTarget = _sectionTarget;
		backgroundMusic = _backgroundMusic;
		capsule = _capsule;
	}
}