using UnityEngine;
using System.Collections;

public abstract class InteractionUpdateAction : Action {
	protected string newText;
	protected NPC npcToUpdate;
	
	public InteractionUpdateAction(){}
	
	public InteractionUpdateAction(NPC _npcToUpdate, string _newText){
		newText = _newText;	
		npcToUpdate = _npcToUpdate;
	}
}