using UnityEngine;
using System.Collections;

public class LockedDoorOnClick : InteractableObject  {
	public LockedDoor door;
	
	public override void Interact(GameObject toInteractWith){
		foreach (GameObject keyUnlock in door.keysThatUnlock){
			if(toInteractWith == keyUnlock){
				LockedDoorManager.instance.UnlockWithId(door.id);
				playerCharacter.DisableHeldItem();
				break;
			}
		}
	}
}
