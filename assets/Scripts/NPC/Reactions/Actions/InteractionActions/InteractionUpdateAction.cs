using UnityEngine;
using System.Collections;

/// <summary>
/// Interaction update action will hold the data to update the interaction display and the children will
/// set exactly what will change.
/// </summary>
public abstract class InteractionUpdateAction : Action {
	protected string newText;
	protected NPC npcToUpdate;
	
	public InteractionUpdateAction(){}
	
	public InteractionUpdateAction(NPC _npcToUpdate, string _newText){
		newText = _newText;	
		npcToUpdate = _npcToUpdate;
	}
}