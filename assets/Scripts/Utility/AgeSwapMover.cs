using UnityEngine;
using System.Collections;

// Calculates and moves the character to the next age section
public class AgeSwapMover : ManagerSingleton<AgeSwapMover> {

	private float growableCheckDistance = 7.0f;

	private PlayerController playerCharacter;

	public void SetPlayer(PlayerController _playerCharacter) { 
		playerCharacter = _playerCharacter;
	}

	// calculate new position
	private Vector3 GetNewAgeWorldPosition(CharacterAge newAge, CharacterAge previousAge, bool isPositionCheck)
	{
		Vector3 deltaPlayerToCurrentFrame = playerCharacter.transform.position - previousAge.sectionTarget.position;


		Vector3 newPlayerPosition = new Vector3(newAge.sectionTarget.position.x + deltaPlayerToCurrentFrame.x,
										 newAge.sectionTarget.position.y + deltaPlayerToCurrentFrame.y - Mathf.Abs(previousAge.capsule.height / 2 - newAge.capsule.height / 2),
										 newAge.sectionTarget.position.z + deltaPlayerToCurrentFrame.z);

		if (playerCharacter.isTouchingGrowableUp) {
			return CalculateGrowablePosition(newAge, newPlayerPosition, isPositionCheck);
		}
		else {
			return newPlayerPosition;
		}
	}

	private Vector3 CalculateGrowablePosition(CharacterAge newAge, Vector3 playerPositionGrowable, bool isPositionCheck)
	{
		RaycastHit hitUp;
		RaycastHit hitDown;

		playerPositionGrowable.y -= newAge.capsule.height / 2;
		Physics.Raycast(playerPositionGrowable, Vector3.up * growableCheckDistance, out hitUp);
		playerPositionGrowable.y += newAge.capsule.height;
		Physics.Raycast(playerPositionGrowable, Vector3.down * growableCheckDistance, out hitDown);

		if (hitUp.collider.tag == "GrowableUp") {
			return new Vector3(playerPositionGrowable.x,
										playerPositionGrowable.y + Mathf.Abs(playerPositionGrowable.y - hitUp.collider.transform.position.y),
										playerPositionGrowable.z);
		}
		else if (hitDown.collider.tag == "GrowableUp") {
			return new Vector3(playerPositionGrowable.x,
										playerPositionGrowable.y - Mathf.Abs(playerPositionGrowable.y - hitDown.collider.transform.position.y) + newAge.capsule.height / 1.788f,
										playerPositionGrowable.z);
		}
		else {
			if (!isPositionCheck) {
				playerCharacter.isTouchingGrowableUp = false;
			}

			// put player on the first platform below
			return  new Vector3(playerPositionGrowable.x,
										playerPositionGrowable.y - Mathf.Abs(playerPositionGrowable.y - hitDown.collider.transform.position.y) + newAge.capsule.height * 1.25f,
										playerPositionGrowable.z);
		}
	}

	// check if new position will work
	public bool CheckTransitionPositionSuccess(CharacterAge newAge, CharacterAge previousAge)
	{
		Vector3 playerCenter = GetNewAgeWorldPosition(newAge, previousAge, true);

		CharacterController charControl = playerCharacter.GetComponent<CharacterController>();

		return (AgeSwapDetector.CheckTransitionPositionSuccess(playerCenter, charControl));
	}

	//	if true, move object and update timeswitchobjects
	public void ChangeAgePosition(CharacterAge newAge, CharacterAge previousAge)
	{
		playerCharacter.transform.position = GetNewAgeWorldPosition(newAge, previousAge, false);
	}
}
