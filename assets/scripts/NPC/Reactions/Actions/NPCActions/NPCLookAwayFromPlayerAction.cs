using UnityEngine;
using System.Collections;

public class NPCLookAwayFromPlayer : Action {
	private NPC _npcToLook;
	
	public NPCLookAwayFromPlayer(){}
	
	public NPCLookAwayFromPlayer(NPC npcToLook){
		_npcToLook = npcToLook;
	}

	public override void Perform(){
		if (Utils.CalcDifference(_npcToLook.transform.position.x, _npcToLook.player.transform.position.x) < 0){
			_npcToLook.LookLeft();
		} else {
			_npcToLook.LookRight();
		}
	}
}
