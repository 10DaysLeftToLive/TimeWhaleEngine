using UnityEngine;
using System.Collections;
using SmoothMoves;

public class CharacterAgeManager {
	private static PlayerController playerCharacter;
	private static CharacterAge[] characterAges = new CharacterAge[3];
	private static CharacterAgeState currentAge;
	
	public static void SetPlayer (PlayerController _playerCharacter) {
		playerCharacter = _playerCharacter;	
		currentAge = CharacterAgeState.YOUNG;
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
		UpdatePlayer(newAge);
		Crossfade.FadeBetween(previousAge, newAge);
	}
	
	public static void PlayCurrentSong(){
		GetCurrentAge().backgroundMusic.Play();
	}
	
	public static void UpdatePlayer(CharacterAge newAge){
		playerCharacter.ChangeAge(GetCurrentAge());
	}
	
	public static void SetupYoung(BoneAnimation _boneAnimation, Transform _sectionTarget, AudioSource _backgroundMusic){
		characterAges[(int)CharacterAgeState.YOUNG] = new CharacterAge(CharacterAgeState.YOUNG, _boneAnimation, _sectionTarget, _backgroundMusic);
	}
	
	public static void SetupMiddle(BoneAnimation _boneAnimation, Transform _sectionTarget, AudioSource _backgroundMusic){
		characterAges[(int)CharacterAgeState.MIDDLE] = new CharacterAge(CharacterAgeState.MIDDLE, _boneAnimation, _sectionTarget, _backgroundMusic);
	}
	
	public static void SetupOld(BoneAnimation _boneAnimation, Transform _sectionTarget, AudioSource _backgroundMusic){
		characterAges[(int)CharacterAgeState.OLD] = new CharacterAge(CharacterAgeState.OLD, _boneAnimation, _sectionTarget, _backgroundMusic);
	}
	
	public static CharacterAgeState GetCurrentAgeState(){
		return (currentAge);
	}
	
	public static void SetAgeStart(CharacterAgeState startingAge){
		currentAge = startingAge;
	}
	
	private static CharacterAge GetCurrentAge(){
		return (characterAges[(int)currentAge]);
	}
}

public enum CharacterAgeState{
	YOUNG = 0,
	MIDDLE = 1,
	OLD = 2,
}