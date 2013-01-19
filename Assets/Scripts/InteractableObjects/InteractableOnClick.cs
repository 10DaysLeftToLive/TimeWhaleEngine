using UnityEngine;
using System.Collections;

public class InteractableOnClick : OnClick {
	public PlayerController playerCharacter;
	public float pickupDistance = 1.5f;
	
	void Start(){
		base.InitEvent();
		Physics.IgnoreCollision(collider, playerCharacter.collider);
	}
	
	protected override void DoClick(){
		if (Vector3.Distance(playerCharacter.transform.position, transform.position) < pickupDistance){		
			playerCharacter.PickupObject(this.gameObject);
		}
	}
}
