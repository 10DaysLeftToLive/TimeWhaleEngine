using UnityEngine;
using System.Collections;

/// <summary>
/// NPC add schedule action will add the given schedule to the given NPC, the NPC's schedule will be respoinsible for prioritizing
/// the schedule.
/// </summary>
public class NPCAddScheduleAction : Action {
	private Schedule scheduleToAdd;
	private NPC npcToAddScedule;
	
	public NPCAddScheduleAction(NPC _npcToAddScedule, Schedule _scheduleToAdd) : base(){
		scheduleToAdd = _scheduleToAdd;
		npcToAddScedule = _npcToAddScedule;
	}
	
	public override void Perform(){
		npcToAddScedule.AddSchedule(scheduleToAdd);
	}
}