using UnityEngine;
using System.Collections;

/// <summary>
/// NPC remove schedule action will remove the schedule in every NPC that has the flag provided.
/// </summary>
public class NPCAllRemoveScheduleAction : Action {
	private string schedulesWithFlagToRemove;
	
	public NPCAllRemoveScheduleAction(string _schedulesWithFlagToRemove) : base(){
		schedulesWithFlagToRemove = _schedulesWithFlagToRemove;
	}
	
	public override void Perform(){
		NPCManager.instance.RemoveSchedulesWithFlag(schedulesWithFlagToRemove);
	}
}