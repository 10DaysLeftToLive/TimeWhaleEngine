using UnityEngine;
using System.Collections;

public class LockedDoor : InteractableObject {
	public GameObject keyThatUnlocks;
	
	public override void Interact(GameObject toInteractWith){
		if(toInteractWith == keyThatUnlocks){
			transform.renderer.enabled = false;
			playerCharacter.DisableHeldItem();
		}
	}
}

