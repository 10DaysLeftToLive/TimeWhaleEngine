using UnityEngine;
using System.Collections;

public class InteractableOnClick : OnClickNextToPlayer {	
	protected override void DoClickNextToPlayer(){
		Debug.Log("click");
		playerCharacter.PickupObject(this.gameObject);
	}
}
