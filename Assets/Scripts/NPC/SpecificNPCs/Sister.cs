using UnityEngine;
using System.Collections;

public class Sister : npcClass {
	
	//TODO: This line needs a better getter/setter
	public bool inLove;
	
	public GameObject paperBoy;
	
	protected override void DoReaction(string itemToReactTo) {
		Debug.Log("Doing reaction between " + name + " and " + itemToReactTo);
		
		if (itemToReactTo == Strings.tag_Player) {
			PlayerController playerController = player.GetComponent<PlayerController>();
			if (playerController.HasItem()) {
				GameObject playersItem = playerController.GetItem();
				Debug.Log ("Player gives this to sister: " + playersItem.tag);
				if (playersItem.tag == "Flower") {
					inLove = true;
				}
			}
		}
		
		
		if (npcDisposition > 5){
			// blah
		}
	}
	
	
	void fallInLove() {
		//if (paperBoy.inLove && inLove) {
			//Show emoticon
		//}
	}
	
	void onTriggerEnter(Collider collide) {
		DoReaction(collide.tag);
	}
	
	
}
