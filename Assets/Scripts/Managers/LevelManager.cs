using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
	
	public PlayerController playerCharacter;
	public InteractionManager interactionManager;
	
	public TimeSwitchObject[] timeSwitchObjects;
	
	public PlayerController.CharacterAgeState CurrentTimePlaying{
		get{return playerCharacter.currentCharacterAge;}
	}
	
	//Make these private later: Also assign these targets based off prior choices / dispositions
	public Transform youngSectionTarget;
	public Transform middleSectionTarget;
	public Transform oldSectionTarget;
	
	public string levelDataName;
	
	Dictionary<string, Dictionary<string, float>> npcDispositionDict;
	
	void Awake(){
		if(playerCharacter == null){
			Debug.LogWarning("Warning: No PlayerCharacter attached to LevelManager");
		}
		
		if(interactionManager == null){
			Debug.LogWarning("Warning: No InteractionManager attached to LevelManager");	
		}
			
		//Put Section targetting / starting age here
		playerCharacter.SetAge(PlayerController.CharacterAgeState.YOUNG, youngSectionTarget.position);	
	}
	
	// Use this for initialization
	void Start () {
		
		LoadDispositionTable();
	}
	
	// Update is called once per frame
	void Update () {
		HandleInput();
	}
	
	void LoadDispositionTable(){
		string fileWithPath =  Application.dataPath + "/LevelData/" + levelDataName + ".xml";
		interactionManager.InitilizeNPCDispositionDict(npcDispositionDict, fileWithPath);
	}
	
	void HandleInput(){
		/*
		if(Input.GetButtonDown(Strings.ButtonYoungAge)){
			ShiftToAge(PlayerController.CharacterAgeState.YOUNG, youngSectionTarget.position);
		}
		else if (Input.GetButtonDown(Strings.ButtonMiddleAge)){
			ShiftToAge(PlayerController.CharacterAgeState.MIDDLE, middleSectionTarget.position);
		}
		else if(Input.GetButtonDown(Strings.ButtonOldAge)){
			ShiftToAge(PlayerController.CharacterAgeState.OLD, oldSectionTarget.position);
		}*/
		
		if(Input.GetButtonDown(Strings.ButtonAgeShiftDown)){
			ShiftDownAge();
		}
		else if(Input.GetButtonDown(Strings.ButtonAgeShiftUp)){
			ShiftUpAge();
		}
		
	}
	
	void ShiftUpAge(){
		switch(playerCharacter.currentCharacterAge){
			case PlayerController.CharacterAgeState.YOUNG:
				//Switch to middle
				ShiftToAge(PlayerController.CharacterAgeState.MIDDLE, middleSectionTarget.position);
				foreach(TimeSwitchObject tsObject in timeSwitchObjects){
					if(!tsObject.staticInYoung){
						tsObject.middleTimeObject.transform.localPosition = tsObject.youngTimeObject.transform.localPosition;
					}
				}
				break;
			case PlayerController.CharacterAgeState.MIDDLE:
				//Switch to Old
				ShiftToAge(PlayerController.CharacterAgeState.OLD, oldSectionTarget.position);
				foreach(TimeSwitchObject tsObject in timeSwitchObjects){
					if(!tsObject.staticInMiddle){
						tsObject.oldTimeObject.transform.localPosition = tsObject.middleTimeObject.transform.localPosition;
					}
				}
				break;
			case PlayerController.CharacterAgeState.OLD:
				//Switch to Young
				ShiftToAge(PlayerController.CharacterAgeState.YOUNG, youngSectionTarget.position);
				break;
		}
	}
	
	void ShiftDownAge(){
		switch(playerCharacter.currentCharacterAge){
			case PlayerController.CharacterAgeState.YOUNG:
				//Switch to Old
				ShiftToAge(PlayerController.CharacterAgeState.OLD, oldSectionTarget.position);
				foreach(TimeSwitchObject tsObject in timeSwitchObjects){
					if(!tsObject.staticInYoung){
						tsObject.oldTimeObject.transform.localPosition = tsObject.middleTimeObject.transform.localPosition;
					}
				}
				break;
			case PlayerController.CharacterAgeState.MIDDLE:
				//Switch to Young
				ShiftToAge(PlayerController.CharacterAgeState.YOUNG, youngSectionTarget.position);
				break;
			case PlayerController.CharacterAgeState.OLD:
				//Switch to Middle
				ShiftToAge(PlayerController.CharacterAgeState.MIDDLE, middleSectionTarget.position);
				break;
		}
	}
	
	void ShiftToYoung(){
		ShiftToAge(PlayerController.CharacterAgeState.YOUNG, youngSectionTarget.position);
	}
	
	void ShiftToMiddle(){
		ShiftToAge(PlayerController.CharacterAgeState.MIDDLE, middleSectionTarget.position);
		
	}
	
	void ShiftToOld(){
		ShiftToAge(PlayerController.CharacterAgeState.OLD, oldSectionTarget.position);
		
	}
	
	void ShiftToAge(PlayerController.CharacterAgeState age, Vector3 frameOriginRelativeToWorld){
		//Handle Player Character
		SwitchPlayerAge(frameOriginRelativeToWorld);
		playerCharacter.SetAge(age, frameOriginRelativeToWorld);	
		
		//Handle Objects
		
	}
	
	void SwitchPlayerAge(Vector3 sectionPosRelativeToWorld){
		
		Vector3 deltaPlayerToCurrentFrame = playerCharacter.transform.position - playerCharacter.currentFrameOriginPos;
		
		playerCharacter.transform.position = new Vector3(sectionPosRelativeToWorld.x + deltaPlayerToCurrentFrame.x,
											sectionPosRelativeToWorld.y + deltaPlayerToCurrentFrame.y,
											sectionPosRelativeToWorld.z);
	}
	
	void GetTimeSwitchedPosition(Vector3 newSectionPosRelativeToWorld, Vector3 objectCurrentLocalPosition){
		
	}
}
