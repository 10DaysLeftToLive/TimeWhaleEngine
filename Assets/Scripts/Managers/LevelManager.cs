using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	
	public PlayerController playerCharacter;
	
	
	
	//Make these private later: Also assign these targets based off interactionManager
	public Transform youngSectionTarget;
	public Transform middleSectionTarget;
	public Transform oldSectionTarget;
	
	void Awake(){
		if(playerCharacter == null){
				Debug.LogWarning("WARNING NO PLAYER CHARACTER ATTACHED");
			}
			
			//Put Section targetting / starting age here
			playerCharacter.SetAge(PlayerController.CharacterAgeState.YOUNG, youngSectionTarget.position);	
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		HandleInput();
	}
	
	void HandleInput(){
		if(Input.GetButtonDown(Strings.ButtonYoungAge)){
			SwitchPlayerAge(youngSectionTarget.position);
			playerCharacter.SetAge(PlayerController.CharacterAgeState.YOUNG, youngSectionTarget.position);
		}
		else if (Input.GetButtonDown(Strings.ButtonMiddleAge)){
			SwitchPlayerAge(middleSectionTarget.position);
			playerCharacter.SetAge(PlayerController.CharacterAgeState.MIDDLE, middleSectionTarget.position);
		}
		else if(Input.GetButtonDown(Strings.ButtonOldAge)){
			SwitchPlayerAge(oldSectionTarget.position);
			playerCharacter.SetAge(PlayerController.CharacterAgeState.OLD, oldSectionTarget.position);
		}
		
		
	}
	
	void SwitchPlayerAge(Vector3 sectionPosRelativeToWorld){
		
		Vector3 deltaPlayerToCurrentFrame = playerCharacter.transform.position - playerCharacter.currentFrameOriginPos;
		
		playerCharacter.transform.position = new Vector3(sectionPosRelativeToWorld.x + deltaPlayerToCurrentFrame.x,
											sectionPosRelativeToWorld.y + deltaPlayerToCurrentFrame.y,
											sectionPosRelativeToWorld.z);
	}
}
