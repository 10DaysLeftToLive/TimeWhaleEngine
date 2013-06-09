using UnityEngine;
using System.Collections;

/*
 * DoorOnClick.cs
 * 	Implements DoClickNextToPlayer and will toggle the assosiated building, it will also play the sound
 */

public class DoorOnClick : OnClickNextToPlayer {
	public Building building;
	
	protected override void DoClickNextToPlayer(){
		building = this.transform.parent.GetComponent<Building>();
		
		building.ToggleBuilding();

        SoundManager.instance.PlaySFX("OpenDoor");
	}
}
