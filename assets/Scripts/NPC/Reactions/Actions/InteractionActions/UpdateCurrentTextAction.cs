using UnityEngine;
using System.Collections;

public class UpdateCurrentTextAction : InteractionUpdateAction {
	public override void Perform(){
		GUIManager.Instance.UpdateInteractionDisplay(newText);
	}
}
