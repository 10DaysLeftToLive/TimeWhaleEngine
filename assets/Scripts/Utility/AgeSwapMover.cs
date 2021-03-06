using UnityEngine;
using System.Collections;

// Calculates and moves the character to the next age section
public class AgeSwapMover : ManagerSingleton<AgeSwapMover> {
	private Player playerCharacter;

	public void SetPlayer(Player _playerCharacter) { 
		playerCharacter = _playerCharacter;
	}

	// calculate new position
	public Vector3 GetNewAgeWorldPosition(Vector3 position, CharacterAge newAge, CharacterAge previousAge, bool isPositionCheck){
		Vector3 deltaPlayerToCurrentFrame = position - previousAge.sectionTarget.position;

		Vector3 newPlayerPosition = new Vector3(newAge.sectionTarget.position.x + deltaPlayerToCurrentFrame.x,
										  newAge.sectionTarget.position.y + deltaPlayerToCurrentFrame.y,
										 newAge.sectionTarget.position.z + deltaPlayerToCurrentFrame.z);
		return newPlayerPosition;
	}

	//	if true, move object and update timeswitchobjects
	public void ChangeAgePosition(CharacterAge newAge, CharacterAge previousAge){
		playerCharacter.transform.position = GetNewAgeWorldPosition(playerCharacter.transform.position, newAge, previousAge, false);
	}
}