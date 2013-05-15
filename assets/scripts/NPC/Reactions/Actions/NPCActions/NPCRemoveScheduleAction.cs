using UnityEngine;
using System.Collections;

/// <summary>
/// NPC remove schedule action will remove the schedule in the NPC that has the flag provided.
/// </summary>
public class NPCRemoveScheduleAction : Action {
	private string schedulesWithFlagToRemove;
	private NPC npcHoldingSchedule;
	
	public NPCRemoveScheduleAction(NPC _npcHoldingSchedule, string _schedulesWithFlagToRemove) : base(){
		schedulesWithFlagToRemove = _schedulesWithFlagToRemove;
		npcHoldingSchedule = _npcHoldingSchedule;
	}
	
	public override void Perform(){
		npcHoldingSchedule.RemoveScheduleWithFlag(schedulesWithFlagToRemove);
	}
}