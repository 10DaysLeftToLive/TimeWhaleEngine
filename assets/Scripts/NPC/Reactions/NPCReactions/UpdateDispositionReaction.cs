using UnityEngine;
using System.Collections;

/// <summary>
/// Reaction that will start with an update disposition action to change the disposition of the given NPC by the given amount.
/// </summary>
public class UpdateDispositionReaction : Reaction {
	public UpdateDispositionReaction(NPC npcToUpdate, int dispositionChange) : base(){
		AddAction(new UpdateNPCDispositionAction(npcToUpdate, dispositionChange));
	}
}
