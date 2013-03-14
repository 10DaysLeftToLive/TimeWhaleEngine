using UnityEngine;
using System.Collections;

public class GrabableOnClick : OnClickNextToPlayer {
	protected override void DoClickNextToPlayer(){
		Debug.Log("Grabable on click");
		GrabableObject attachedGrabable = this.GetComponent<GrabableObject>();
		
		if (attachedGrabable.AttachedToPlayer){
			//TODO use DetachFrom in character.
			player.DetachFrom(this.gameObject);
		} else {
			player.AttachTo(this.gameObject);
		}
	}
}
