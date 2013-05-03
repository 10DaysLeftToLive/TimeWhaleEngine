using UnityEngine;
using System.Collections;

/// <summary>
/// Update default text action will change the default text which is displayed when the interaction is opened.
/// </summary>
public class UpdateDefaultTextAction : InteractionUpdateAction {
	public UpdateDefaultTextAction(NPC npcToUpdate, string newText) : base(npcToUpdate, newText){}
	
	public override void Perform(){
		npcToUpdate.UpdateDefaultText(newText);
	}
}