using UnityEngine;
using System.Collections;

public class UpdateNPCDispositionAction : NPCValueUpdateAction {
	int newDisposition;
	
	public UpdateNPCDispositionAction(NPC _npcToUpdate, int _newDisposition) : base(_npcToUpdate) {
		newDisposition = _newDisposition;
	}
	
	public override void Perform(){
		npcToUpdate.UpdateDisposition(newDisposition);
	}
}