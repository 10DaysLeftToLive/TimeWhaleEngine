using UnityEngine;
using System.Collections;

public class UpdateNPCDispositionAction : NPCValueUpdateAction {
	int deltaDisposition;
	
	public UpdateNPCDispositionAction(NPC _npcToUpdate, int _deltaDisposition) : base(_npcToUpdate) {
		deltaDisposition = _deltaDisposition;
	}
	
	public override void Perform(){
		npcToUpdate.UpdateDisposition(deltaDisposition);
	}
}