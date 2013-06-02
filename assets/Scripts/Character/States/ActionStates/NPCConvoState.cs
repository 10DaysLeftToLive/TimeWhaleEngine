using UnityEngine;
using System.Collections;

public class NPCConvoState : AbstractState {
	Character _toTalkWith;
	string _textToShow;
	NPCChat _chatToPerform;
	float timeLeft;
	
	public NPCConvoState(Character toControl, Character toTalkWith, NPCChat chatToPerform) : base(toControl){
		_toTalkWith = toTalkWith;
		_chatToPerform = chatToPerform;
		timeLeft = chatToPerform.GetCurrentInfo().GetTime();
	}
	
	public override void Update(){
		timeLeft -= Time.deltaTime;
		if (timeLeft <= 0) {
			if (character is NPC) {
				((NPC)character).NextTask();
			} 
		}
	}
	
	public void FaceEachOther() {
		if (character.transform.position.x > _toTalkWith.transform.position.x) {
			character.LookLeft();
			_toTalkWith.LookRight();
		} else {
			character.LookRight();
			_toTalkWith.LookLeft();
		}
	}
	
	public override void OnEnter(){
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
