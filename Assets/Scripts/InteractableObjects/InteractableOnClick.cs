using UnityEngine;
using System.Collections;

public class InteractableOnClick : OnClickNextToPlayer {	
	protected override void DoClickNextToPlayer(){
		Debug.Log("Clicked on a " + this.name + " with tag: " + this.tag);
		playerCharacter.InteractWithObject(this.gameObject);
	}
}
