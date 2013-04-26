using UnityEngine;
using System.Collections;

public class NPCEmotionUpdateAction : NPCValueUpdateAction {
	EmotionState newEmotionState;
	
	public NPCEmotionUpdateAction(NPC _npcToUpdate, EmotionState _newEmotionState) : base(_npcToUpdate) {
		newEmotionState = _newEmotionState;
	}
	
	public override void Perform(){
		npcToUpdate.UpdateEmotionState(newEmotionState);
		GUIManager.Instance.RefreshInteraction();
	}
}