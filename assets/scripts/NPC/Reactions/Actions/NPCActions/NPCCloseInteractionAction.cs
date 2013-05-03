using UnityEngine;
using System.Collections;

/// <summary>
/// NPC close interaction action.
///  Will close the interaction and that will make both the npc and player leave the interaction
///  WARNING: Putting a close and update interaction actions in the same reaction will cause problems
/// </summary>
public class NPCCloseInteractionAction : Action{
	public NPCCloseInteractionAction(){}
	
	public override void Perform(){
		GUIManager.Instance.CloseInteractionMenu();
	}
}