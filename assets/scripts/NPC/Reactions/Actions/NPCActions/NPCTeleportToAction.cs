using UnityEngine;
using System.Collections;

/// <summary>
/// Will teleport the given npc to a given position. Used for chaning positions in other ages
/// </summary>
public class NPCTeleportToAction : NPCValueUpdateAction {
	Vector3 positionToGo;
	
	public NPCTeleportToAction(NPC _npcToUpdate, Vector3 _positionToGo) : base(_npcToUpdate) {
		positionToGo = _positionToGo;
	}
	
	public override void Perform(){
		npcToUpdate.transform.position = positionToGo;
	}
}