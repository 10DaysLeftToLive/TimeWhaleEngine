using UnityEngine;
using System.Collections;

// Calculates and moves the character to the next age section
public class AgeSwapMover : ManagerSingleton<AgeSwapMover> {
	private Player playerCharacter;

	public void SetPlayer(Player _playerCharacter) { 
		playerCharacter = _playerCharacter;
	}
	
	private float GetY(Vector3 currentPos, float height){
		int mask = (1 << 9);
        RaycastHit hitDown;
        
        if (Physics.Raycast(currentPos, Vector3.down , out hitDown, 10, mask)) {
			float difference = hitDown.point.y + (height/2);
			return (difference);
        } else {
			return (currentPos.y);	
		}
	}

	// calculate new position
	public Vector3 GetNewAgeWorldPosition(Vector3 position, CharacterAge newAge, CharacterAge previousAge, bool isPositionCheck){
		Vector3 deltaPlayerToCurrentFrame = position - previousAge.sectionTarget.position;

		Vector3 newPlayerPosition = new Vector3(newAge.sectionTarget.position.x + deltaPlayerToCurrentFrame.x,
										 GetY(position, playerCharacter.collider.bounds.size.y),
										 newAge.sectionTarget.position.z + deltaPlayerToCurrentFrame.z);
		return newPlayerPosition;
	}

	//	if true, move object and update timeswitchobjects
	public void ChangeAgePosition(CharacterAge newAge, CharacterAge previousAge){
		playerCharacter.transform.position = GetNewAgeWorldPosition(playerCharacter.transform.position, newAge, previousAge, false);
	}
}