using UnityEngine;
using System.Collections;

public class CatLady : npcClass {
	
	public GameObject key;
	
	bool hasKeyBeenPickedUp = false;
	
	
	protected override void DoReaction(string itemToReactTo){
		Debug.Log("Doing reaction between " + name + " and " + itemToReactTo);
		
		
		
		if (itemToReactTo == "Interactable" && !hasKeyBeenPickedUp) {
			key.transform.position = new Vector3(this.transform.position.x - 1, this.transform.position.y - 1.1f, 0f);
			hasKeyBeenPickedUp = true;
			questDone = true;
		}
		
		if (npcDisposition > 5){
			// blah
		}
	}
	
	void OnTriggerEnter(Collider trigger) {	
		DoReaction(trigger.gameObject.tag);
	}
}
