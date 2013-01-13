using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	
	public Transform playerCharacter;
	
	public enum CharacterAgeState{
		YOUNG,
		MIDDLE,
		OLD,
	}
	public CharacterAgeState characterAgeState;
	
	//Make these private later: Also assign these targets based off interactionManager
	public Transform youngSectionTarget;
	public Transform middleSectionTarget;
	public Transform oldSectionTarget;

	// Use this for initialization
	void Start () {
		if(playerCharacter == null){
			Debug.LogWarning("WARNING NO PLAYER CHARACTER ATTACHED");
		}
		
		//Put Section targetting / starting age here
		characterAgeState = CharacterAgeState.YOUNG;
	}
	
	// Update is called once per frame
	void Update () {
		HandleInput();
	}
	
	void HandleInput(){
		if(Input.GetButtonDown(Strings.ButtonYoungAge)){
			SwitchPlayerAge(youngSectionTarget.position);
			characterAgeState = CharacterAgeState.YOUNG;
		}
		else if (Input.GetButtonDown(Strings.ButtonMiddleAge)){
			SwitchPlayerAge(middleSectionTarget.position);
			characterAgeState = CharacterAgeState.MIDDLE;
		}
		else if(Input.GetButtonDown(Strings.ButtonOldAge)){
			SwitchPlayerAge(oldSectionTarget.position);
			characterAgeState = CharacterAgeState.OLD;
		}
		
		
	}
	
	void SwitchPlayerAge(Vector3 sectionPosRelativeToWorld){
		Vector3 currentFramePosition = new Vector3(0,0,0);
		
		if(characterAgeState == CharacterAgeState.YOUNG){
			currentFramePosition = youngSectionTarget.position;
		}
		else if (characterAgeState == CharacterAgeState.MIDDLE){
			currentFramePosition = middleSectionTarget.position;
		}
		else if (characterAgeState == CharacterAgeState.OLD){
			currentFramePosition = oldSectionTarget.position;
		}
		
		Vector3 deltaPlayerToCurrentFrame = playerCharacter.position - currentFramePosition;
		
		playerCharacter.position = new Vector3(sectionPosRelativeToWorld.x + deltaPlayerToCurrentFrame.x,
											sectionPosRelativeToWorld.y + deltaPlayerToCurrentFrame.y,
											sectionPosRelativeToWorld.z);
	}
}
