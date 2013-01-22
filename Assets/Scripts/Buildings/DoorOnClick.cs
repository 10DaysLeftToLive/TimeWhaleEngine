using UnityEngine;
using System.Collections;

public class DoorOnClick : OnClickNextToPlayer {
	public House house;
	
	protected override void DoClickNextToPlayer(){
		house = this.transform.parent.GetComponent<House>();
		
		house.ToggleHouse();
	}
}
