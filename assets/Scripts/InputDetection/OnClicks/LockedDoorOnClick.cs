using UnityEngine;
using System.Collections;

public class LockedDoorOnClick : InteractableOnClick  {
	public LockedDoor door;
	
	protected override void InteractWithPlayer(){
		if (door == null) door = this.gameObject.GetComponent<LockedDoor>();
		
		GameObject playerItem = player.Inventory.GetItem();
		
		if (playerItem == null) return;
		
		foreach (GameObject keyUnlock in door.keysThatUnlock){
			if(playerItem == keyUnlock){
				LockedDoorManager.instance.UnlockWithId(door.id);
				player.Inventory.DisableHeldItem();
				break;
			}
		}
	}
}
