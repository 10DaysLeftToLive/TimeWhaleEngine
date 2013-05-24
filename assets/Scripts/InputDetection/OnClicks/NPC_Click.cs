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
		if (NPCIsOpenToInteracting()){
			player.EnterState(new TalkState(player, npcAttachedTo));
		} else if (npcAttachedTo.IsInteracting()) {
			GUIManager.Instance.CloseInteractionMenu();
		} else {
			DebugManager.instance.Log("Tried to talk to " + npcAttachedTo + " but it didn't want to talk", "NPC", npcAttachedTo.name);	
		}	
	}
	
	private bool NPCIsOpenToInteracting(){
		return (!npcAttachedTo.IsInteracting() && npcAttachedTo.CanTalk()); 		
	}
}