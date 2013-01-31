using UnityEngine;
using System.Collections;

public class LockedDoor : InteractableObject {
	public GameObject[] keysThatUnlock;
	
	public override void Interact(GameObject toInteractWith){
		foreach (GameObject keyUnlock in keysThatUnlock){
			if(toInteractWith == keyUnlock){
				transform.renderer.enabled = false;
				playerCharacter.DisableHeldItem();
			}
		}
	}
}

