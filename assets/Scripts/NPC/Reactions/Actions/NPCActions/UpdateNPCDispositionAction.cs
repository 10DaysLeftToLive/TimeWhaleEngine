using UnityEngine;
using System.Collections;

/// <summary>
/// Updates the NPC's disposition by the given amount
/// </summary>
public class UpdateNPCDispositionAction : NPCValueUpdateAction {
	int deltaDisposition;
	
	public UpdateNPCDispositionAction(NPC _npcToUpdate, int _deltaDisposition) : base(_npcToUpdate) {
		deltaDisposition = _deltaDisposition;
	}
	
	public override void Perform(){
		npcToUpdate.UpdateDisposition(deltaDisposition);
	}
}