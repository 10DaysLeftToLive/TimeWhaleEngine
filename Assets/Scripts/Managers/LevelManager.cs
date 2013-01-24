using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
	public PlayerController playerCharacter;
	public InteractionManager interactionManager;
	
	public TimeSwitchObject[] timeSwitchObjects;
	
	public PlayerController.CharacterAgeState CurrentTimePlaying{
		get{return playerCharacter.CurrentCharacterAge;}
	}
	
	//Make these private later: Also assign these targets based off prior choices / dispositions
	public Transform youngSectionTarget;
	public Transform middleSectionTarget;
	public Transform oldSectionTarget;
	
	public string levelDataName;
	
	private string dispositionDataFile;
	private string levelInteractionFile;
	
	Dictionary<string, Dictionary<string, float>> npcDispositionDict;
	
	void Awake(){
		if(playerCharacter == null){
			Debug.LogWarning("Warning: No PlayerCharacter attached to LevelManager");
		}
		
		if(interactionManager == null){
			Debug.LogWarning("Warning: No InteractionManager attached to LevelManager");	
		}
		
		if (levelDataName == ""){
			Debug.LogError("Error: No Level data file given to LevelManager.");
		}
			
		//Put Section targetting / starting age here
		playerCharacter.SetAge(PlayerController.CharacterAgeState.YOUNG, youngSectionTarget.position);	
	}
	
	// Use this for initialization
	void Start () {
		SetFiles();		
		SetNPCData();
	}
	
	private void SetFiles(){
		levelInteractionFile = Application.dataPath + "/Data/LevelData/" + levelDataName + ".xml";
		dispositionDataFile = Application.dataPath + "/Data/DispositionData/" + Strings.DispositionFile + ".xml";
		
		if (!System.IO.File.Exists(levelInteractionFile)){
			Debug.LogError("Error: " + levelInteractionFile + " was not found.");
		} else if (!System.IO.File.Exists(dispositionDataFile)){
			Debug.LogError("Error: " + dispositionDataFile + " was not found.");
		}
	}
	
	// Update is called once per frame
	void Update () {
		HandleInput();
	}
	
	void SetNPCData(){		
		InteractionManager.instance.InitilizeNPCs(dispositionDataFile, levelInteractionFile);
	}	
	
	void SaveDispositions(){
		InteractionManager.instance.SaveNPCDispositions(dispositionDataFile);
	}
	
	
	void HandleInput(){	
		if(Input.GetButtonDown(Strings.ButtonAgeShiftDown)){
			ShiftDownAge();
		}
		else if(Input.GetButtonDown(Strings.ButtonAgeShiftUp)){
			ShiftUpAge();
		}
		
	}
	
	void ShiftUpAge(){
		bool isTouchingGrowableUp = playerCharacter.IsTouchingGrowableUp;
		TimeSwitchObject growableUpTSO = null;
		if(isTouchingGrowableUp){
			growableUpTSO = FindGrowableUp(playerCharacter.CurrentTouchedGrowableUp);
		}
		
		switch(playerCharacter.CurrentCharacterAge){
			case PlayerController.CharacterAgeState.YOUNG:
				//Switch to middle
				ShiftToAge(PlayerController.CharacterAgeState.MIDDLE, middleSectionTarget.position);
				foreach(TimeSwitchObject tsObject in timeSwitchObjects){
					if(!tsObject.staticInYoung){
						tsObject.middleTimeObject.transform.localPosition = tsObject.youngTimeObject.transform.localPosition;
					}
				}
			
				if(isTouchingGrowableUp){
					playerCharacter.TeleportCharacterAbove(growableUpTSO.middleTimeObject);
				}
			
				Crossfade.YoungToMiddle();
			
				break;
			case PlayerController.CharacterAgeState.MIDDLE:
				//Switch to Old
				ShiftToAge(PlayerController.CharacterAgeState.OLD, oldSectionTarget.position);
				foreach(TimeSwitchObject tsObject in timeSwitchObjects){
					if(!tsObject.staticInMiddle){
						tsObject.oldTimeObject.transform.localPosition = tsObject.middleTimeObject.transform.localPosition;
					}
				}
			
				if(isTouchingGrowableUp){
					playerCharacter.TeleportCharacterAbove(growableUpTSO.oldTimeObject);
				}
				
				Crossfade.MiddleToOld();
			
				break;
			case PlayerController.CharacterAgeState.OLD:
				//Switch to Young
				ShiftToAge(PlayerController.CharacterAgeState.YOUNG, youngSectionTarget.position);
			
				if(isTouchingGrowableUp){
					playerCharacter.TeleportCharacterAbove(growableUpTSO.youngTimeObject);
				}
				
				Crossfade.OldToYoung();
			
				break;
		}
	}
	
	void ShiftDownAge(){
		bool isTouchingGrowableUp = playerCharacter.IsTouchingGrowableUp;
		TimeSwitchObject growableUpTSO = null;
		if(isTouchingGrowableUp){
			growableUpTSO = FindGrowableUp(playerCharacter.CurrentTouchedGrowableUp);
		}
		
		switch(playerCharacter.CurrentCharacterAge){
			case PlayerController.CharacterAgeState.YOUNG:
				//Switch to Old
				ShiftToAge(PlayerController.CharacterAgeState.OLD, oldSectionTarget.position);
				foreach(TimeSwitchObject tsObject in timeSwitchObjects){
					if(!tsObject.staticInYoung){
						tsObject.oldTimeObject.transform.localPosition = tsObject.middleTimeObject.transform.localPosition;
					}
				}
			
				if(isTouchingGrowableUp){
					playerCharacter.TeleportCharacterAbove(growableUpTSO.oldTimeObject);
				}
			
				Crossfade.YoungToOld();
			
				break;
			case PlayerController.CharacterAgeState.MIDDLE:
				//Switch to Young
				ShiftToAge(PlayerController.CharacterAgeState.YOUNG, youngSectionTarget.position);
			
				if(isTouchingGrowableUp){
					playerCharacter.TeleportCharacterAbove(growableUpTSO.youngTimeObject);
				}
			
				Crossfade.MiddleToYoung();
				
				break;
			case PlayerController.CharacterAgeState.OLD:
				//Switch to Middle
				ShiftToAge(PlayerController.CharacterAgeState.MIDDLE, middleSectionTarget.position);
			
			
				if(isTouchingGrowableUp){
					playerCharacter.TeleportCharacterAbove(growableUpTSO.middleTimeObject);
				}
			
				Crossfade.OldToMiddle();
			
				break;
		}
	}
	
	void ShiftToAge(PlayerController.CharacterAgeState age, Vector3 frameOriginRelativeToWorld){
		//Handle Player Character
		SwitchPlayerAge(frameOriginRelativeToWorld);
		playerCharacter.SetAge(age, frameOriginRelativeToWorld);	
		
		//Handle Objects
		
	}
	
	void SwitchPlayerAge(Vector3 sectionPosRelativeToWorld){
		
		Vector3 deltaPlayerToCurrentFrame = playerCharacter.transform.position - playerCharacter.CurrentFrameOriginPos;
		
		playerCharacter.transform.position = new Vector3(sectionPosRelativeToWorld.x + deltaPlayerToCurrentFrame.x,
											sectionPosRelativeToWorld.y + deltaPlayerToCurrentFrame.y,
											sectionPosRelativeToWorld.z);
	}
	
	TimeSwitchObject FindGrowableUp(GameObject currentlyTouchingGrowableUp){
		foreach(TimeSwitchObject tso in timeSwitchObjects){
			if(tso.objectLabel == currentlyTouchingGrowableUp.name){
				Debug.Log("Found growable up");
				return tso;	
			}
		}
		return null;
	}
	
	void OnApplicationQuit(){
		SaveDispositions();
	}
}
