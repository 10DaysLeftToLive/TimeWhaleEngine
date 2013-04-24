using UnityEngine;
using System.Collections;

public class UpdateDefaultTextAction : InteractionUpdateAction {
	public override void Perform(){
		GUIManager.Instance.UpdateInteractionDisplay(newText);
	}
}
