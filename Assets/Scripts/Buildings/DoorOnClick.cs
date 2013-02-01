using UnityEngine;
using System.Collections;

public class DoorOnClick : OnClickNextToPlayer {
	public Building building;
	
	protected override void DoClickNextToPlayer(){
		building = this.transform.parent.GetComponent<Building>();
		
		building.ToggleBuilding();
	}
}
