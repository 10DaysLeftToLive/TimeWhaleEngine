using UnityEngine;
using System.Collections;

public class LockedDoor : InteractableObject {
	public GameObject[] keysThatUnlock;
	public int id;
	
	public override void Interact(GameObject toInteractWith){
		foreach (GameObject keyUnlock in keysThatUnlock){
			if(toInteractWith == keyUnlock){
				LockedDoorManager.instance.UnlockeWithId(id);
				playerCharacter.DestroyHeldItem();
			}
		}
	}
	
	public void Unlock(){
		transform.renderer.enabled = false;
		transform.collider.enabled = false;
	}
}

