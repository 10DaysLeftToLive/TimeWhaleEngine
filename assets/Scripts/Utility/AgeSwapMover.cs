using UnityEngine;
using System.Collections;

// Calculates and moves the character to the next age section
public class AgeSwapMover : ManagerSingleton<AgeSwapMover> {
	private Player playerCharacter;

	public void SetPlayer(Player _playerCharacter) { 
		playerCharacter = _playerCharacter;
	}

	// calculate new position
	private Vector3 GetNewAgeWorldPosition(CharacterAge newAge, CharacterAge previousAge, bool isPositionCheck){
		Vector3 deltaPlayerToCurrentFrame = playerCharacter.transform.position - previousAge.sectionTarget.position;


		Vector3 newPlayerPosition = new Vector3(newAge.sectionTarget.position.x + deltaPlayerToCurrentFrame.x,
										 newAge.sectionTarget.position.y + deltaPlayerToCurrentFrame.y - Mathf.Abs(previousAge.capsule.height / 2 - newAge.capsule.height / 2),
										 newAge.sectionTarget.position.z + deltaPlayerToCurrentFrame.z);

		return newPlayerPosition;
	}
	
	// check if new position will work
	public bool CheckTransitionPositionSuccess(CharacterAge newAge, CharacterAge previousAge){
		Vector3 playerCenter = GetNewAgeWorldPosition(newAge, previousAge, true);

		CharacterController charControl = playerCharacter.GetComponent<CharacterController>();

		return (AgeSwapDetector.CheckTransitionPositionSuccess(playerCenter, charControl));
	}

	//	if true, move object and update timeswitchobjects
	public void ChangeAgePosition(CharacterAge newAge, CharacterAge previousAge){
		playerCharacter.transform.position = GetNewAgeWorldPosition(newAge, previousAge, false);
	}
}