using UnityEngine;
using System.Collections;

public abstract class NPCValueUpdateAction : Action {
	protected NPC npcToUpdate;
	
	public NPCValueUpdateAction(){}
	
	public NPCValueUpdateAction(NPC _npcToUpdate){
		npcToUpdate = _npcToUpdate;	
	}
}