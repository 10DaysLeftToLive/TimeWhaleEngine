using UnityEngine;
using System.Collections;
using SmoothMoves;

public class CharacterAgeManager {
	private static Player playerCharacter;
	private static CharacterAge[] characterAges = new CharacterAge[3];
	public static CharacterAgeState currentAge;
	
	public static void SetPlayer (Player _playerCharacter) {
		playerCharacter = _playerCharacter;	
		currentAge = CharacterAgeState.YOUNG;
	}
	
	public static CharacterAge GetAgeTransitionUp(){
		CharacterAgeState tempAge = currentAge;
		
		if (currentAge < CharacterAgeState.OLD){
			tempAge++;
		} else {
			tempAge = CharacterAgeState.YOUNG;
		}
		return GetAgeOf(tempAge);
	}
	
	public static CharacterAge GetAgeTransitionDown(){	
		CharacterAgeState tempAge = currentAge;
			
		if (currentAge > CharacterAgeState.YOUNG){
			tempAge--;
		} else {
			tempAge = CharacterAgeState.OLD;
		}
		
		return GetAgeOf(tempAge);
	}
	
	public static void TransistionUp(){
		CharacterAge previousAge = GetCurrentAge();
		
		if (currentAge < CharacterAgeState.OLD){
			currentAge++;
		} else {
			currentAge = CharacterAgeState.YOUNG;
		}
		
		CharacterAge newAge = GetCurrentAge();
		
		Transition(previousAge, newAge);
	}
	
	public static void TransistionDown(){
		CharacterAge previousAge = GetCurrentAge();
		
		if (currentAge > CharacterAgeState.YOUNG){
			currentAge--;
		} else {
			currentAge = CharacterAgeState.OLD;
		}
		
		CharacterAge newAge = GetCurrentAge();
		
		Transition(previousAge, newAge);
	}
	
	private static void Transition(CharacterAge previousAge, CharacterAge newAge){
		UpdatePlayer(previousAge);
		//Crossfade.FadeBetween(previousAge, newAge);
	}
	
	public static void PlayCurrentSong(){
		GetCurrentAge().backgroundMusic.Play();
	}
	
	public static void UpdatePlayer(CharacterAge previousAge){
		playerCharacter.ChangeAge(GetCurrentAge(), previousAge);
		playerCharacter.DetachObject();
	}
	
	public static void SetupYoung(BoneAnimation _boneAnimation, Transform _sectionTarget, AudioSource _backgroundMusic){
		characterAges[(int)CharacterAgeState.YOUNG] = new CharacterAge(CharacterAgeState.YOUNG, _boneAnimation, _sectionTarget, _backgroundMusic, playerCharacter.smallHitBox);
	}
	
	public static void SetupMiddle(BoneAnimation _boneAnimation, Transform _sectionTarget, AudioSource _backgroundMusic){
		characterAges[(int)CharacterAgeState.MIDDLE] = new CharacterAge(CharacterAgeState.MIDDLE, _boneAnimation, _sectionTarget, _backgroundMusic, playerCharacter.bigHitbox);
	}
	
	public static void SetupOld(BoneAnimation _boneAnimation, Transform _sectionTarget, AudioSource _backgroundMusic){
		characterAges[(int)CharacterAgeState.OLD] = new CharacterAge(CharacterAgeState.OLD, _boneAnimation, _sectionTarget, _backgroundMusic, playerCharacter.bigHitbox);
	}
	
	public static CharacterAgeState GetCurrentAgeState(){
		return (currentAge);
	}
	
	public static void SetAgeStart(CharacterAgeState startingAge){
		currentAge = startingAge;
	}
	
	public static CharacterAge GetCurrentAge(){
		return (characterAges[(int)currentAge]);
	}
	
	private static CharacterAge GetAgeOf(CharacterAgeState age){
		return (characterAges[(int) age]);
	}
}

public enum CharacterAgeState{
	YOUNG = 0,
	MIDDLE = 1,
	OLD = 2,
}