using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
	
	public PlayerController playerCharacter;
	public InteractionManager interactionManager;
	
	//Make these private later: Also assign these targets based off prior choices / dispositions
	public Transform youngSectionTarget;
	public Transform middleSectionTarget;
	public Transform oldSectionTarget;
	
	Dictionary<string, float> npcDispositionDict;
	
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
		interactionManager.InitilizeNPCDispositionDict(npcDispositionDict);
		
	}
	
	// Update is called once per frame
	void Update () {
		HandleInput();
	}
	
	void LoadDispositionTable(){
		npcDispositionDict = new Dictionary<string, float>();
		
		//Load stuff here
		
	}
	
	void HandleInput(){
		if(Input.GetButtonDown(Strings.ButtonYoungAge)){
			ShiftToAge(PlayerController.CharacterAgeState.YOUNG, youngSectionTarget.position);
		}
		else if (Input.GetButtonDown(Strings.ButtonMiddleAge)){
			ShiftToAge(PlayerController.CharacterAgeState.MIDDLE, middleSectionTarget.position);
		}
		else if(Input.GetButtonDown(Strings.ButtonOldAge)){
			ShiftToAge(PlayerController.CharacterAgeState.OLD, oldSectionTarget.position);
		}
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
}
