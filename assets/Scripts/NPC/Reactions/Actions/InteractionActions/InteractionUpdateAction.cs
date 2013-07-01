using UnityEngine;
using System.Collections;

public abstract class InteractionUpdateAction : Action {
	protected string newText;
	protected NPC npcToUpdate;
	
	protected InteractionUpdateAction(){}
	
	protected InteractionUpdateAction(NPC _npcToUpdate, string _newText){
		newText = _newText;	
		npcToUpdate = _npcToUpdate;
	}
}