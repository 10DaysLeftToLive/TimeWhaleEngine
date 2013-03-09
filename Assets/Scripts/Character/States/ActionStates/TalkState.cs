using UnityEngine;
using System.Collections;

public class TalkState : AbstractState {
	NPC _toTalkWith;
	
	public TalkState(Character toControl, NPC toTalkWith) : base(toControl){
		_toTalkWith = toTalkWith;
	}
	
	public override void Update(){
	}
	
	public override void OnEnter(){
		Debug.Log(character.name + ": TalkState Enter");
		_toTalkWith.OpenChat();
	}
	
	public override void OnExit(){
		Debug.Log(character.name + ": TalkState Exit");
		_toTalkWith.CloseChat();
	}
}