using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeReactionManager : ManagerSingleton<TimeReactionManager> {
	private List<NPC> npcsWithTimeReaction;
	
	public override void Init(){
		npcsWithTimeReaction = new List<NPC>();
	}
	
	public void Add(NPC npc) {
		if (!npcsWithTimeReaction.Contains(npc)) {
			npcsWithTimeReaction.Add(npc);
		}
	}
	
	public void Remove(NPC npc) {
		if (!npcsWithTimeReaction.Contains(npc)) {
			npcsWithTimeReaction.Remove(npc);
		}
	}
	
	public void Update(int gameDayTime) {
		foreach(NPC npc in npcsWithTimeReaction) {
			npc.ReactToTime(gameDayTime);
		}
	}
}
