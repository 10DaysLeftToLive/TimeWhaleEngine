using UnityEngine;
using System.Collections;

/*
 * NPC_Click.cs
 * 	Implements the DoClickNextToPlayer and will call the InteractionManager's perform interaction
 */

public class NPC_Click : OnClickNextToPlayer {
	NPC npcAttachedTo;
	
	void Start(){
		npcAttachedTo = this.gameObject.GetComponent<NPC>();
		if (npcAttachedTo == null){
			Debug.LogError("Error: No npc script attached to " + this.name);
		}
		base.InitEvent();
	}
	
	protected override void DoClickNextToPlayer(){
		npcAttachedTo.OpenChat();
	}
}