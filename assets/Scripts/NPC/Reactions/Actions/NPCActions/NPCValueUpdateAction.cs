using UnityEngine;
using System.Collections;

/// <summary>
/// NPCValueUpdateAction will be implemented by children who need an npc so that they can perform on it
/// </summary>
public abstract class NPCValueUpdateAction : Action {
	protected NPC npcToUpdate;
	
	public NPCValueUpdateAction(){}
	
	public NPCValueUpdateAction(NPC _npcToUpdate){
		npcToUpdate = _npcToUpdate;	
	}
}