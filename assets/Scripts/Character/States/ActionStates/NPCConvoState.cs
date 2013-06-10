using UnityEngine;
using System.Collections;

public class NPCConvoState : AbstractState {
	Character _toTalkWith;
	string _textToShow;
	NPCChat _chatToPerform;
	
	public NPCConvoState(Character toControl, Character toTalkWith, NPCChat chatToPerform) : base(toControl){
		_toTalkWith = toTalkWith;
		_chatToPerform = chatToPerform;
	}
	
	public override void Update(){
		if (_chatToPerform.GetCurrentInfo().GetTime() <= 0) {
			if (character is NPC) {
				((NPC)character).NextTask();
			} 
		}
	}
	
	public void FaceEachOther() {
		((NPC)character).LookAt(_toTalkWith.gameObject);
		((NPC)_toTalkWith).LookAt(character.gameObject);
	}
	
	public override void OnEnter(){
		if (((NPC)character).IsInteracting()){
			GUIManager.Instance.CloseInteractionMenu();
		}
		GUIManager.Instance.AddNPCChat(_chatToPerform);
		FaceEachOther();
		
		Debug.Log(character.name + ": NPCConvoState Enter");
		character.PlayAnimation(Strings.animation_stand); // Should be a talk animation
		_toTalkWith.PlayAnimation(Strings.animation_stand);
	}
	
	public override void OnExit(){
		Debug.Log(character.name + ": TalkState Exit");
	}
}
