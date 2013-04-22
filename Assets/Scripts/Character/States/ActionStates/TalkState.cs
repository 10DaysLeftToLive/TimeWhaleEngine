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
		character.PlayAnimation(Strings.animation_stand);
		_toTalkWith.StarTalkingWithPlayer();
		GUIManager.Instance.InitiateInteraction(_toTalkWith);
	}
	
	public override void OnExit(){
		Debug.Log(character.name + ": TalkState Exit");
	}
}