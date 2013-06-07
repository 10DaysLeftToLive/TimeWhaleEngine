using UnityEngine;
using System.Collections;

public class NPCLookAtPlayer : Action {
	private NPC _npcToLook;
	
	public NPCLookAtPlayer(){}
	
	public NPCLookAtPlayer(NPC npcToLook){
		_npcToLook = npcToLook;
	}

	public override void Perform(){
		((NPC)_npcToLook).LookAtPlayer();
	}
}
