using UnityEngine;
using System.Collections;

public class UpdateDefaultTextAction : InteractionUpdateAction {
	public UpdateDefaultTextAction(NPC npcToUpdate, string newText) : base(npcToUpdate, newText){}
	
	public override void Perform(){
		//Debug.Log("Updating default text");
		npcToUpdate.UpdateDefaultText(newText);
	}
}