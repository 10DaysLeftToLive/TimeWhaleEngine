using UnityEngine;
using System.Collections;

public class NPCClearSchedulesAction : NPCValueUpdateAction {
	Schedule _newSchedule;
	
	public NPCClearSchedulesAction(NPC _npcToUpdate, Schedule newSchedule) : base(_npcToUpdate) {
		_newSchedule = newSchedule;
	}
	
	public override void Perform(){
		npcToUpdate.ClearAndReplaceSchedule(_newSchedule);
	}
}
