using UnityEngine;
using System.Collections;

public class Sister : npcClass {
	
	//TODO: This line needs a better getter/setter
	public bool inLove;
	
	public GameObject paperBoy;
	
	
	protected override void DoReaction(string itemToReactTo) {
		switch (itemToReactTo){
			case "Flower":
				inLove = true;
				npcDisposition += 10;
				(player.GetComponent<PlayerController>() as PlayerController).DestroyHeldItem();
				break;
			case "Plushie":
				npcDisposition += 10;
				(player.GetComponent<PlayerController>() as PlayerController).DestroyHeldItem();
				questDone = true;
				break;
			case "NoItem":
				break;
			default:
				break;
		}
		
		PlayerPrefs.SetInt("Sister", npcDisposition);
		
		FallInLove();
		print ("Seeing what I got from SmoothMoves");
		
		if (npcDisposition > 5) {
			// blah
		}
	}
	
	protected override void ShowEmoticon(string emoticon) {
		base.ShowEmoticon(emoticon);
		
	}
	
	
	void FallInLove() {
		if ((paperBoy.GetComponent<PaperBoy>() as PaperBoy).inLove && inLove) {
			Debug.Log ("Sister says: The Paper Boy and the Sister are in Love");
		}
	}
	
	
}
