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
		if (!_toTalkWith.IsInteracting() && _toTalkWith.CanTalk()){
			if (((Player) character).npcTalkingWith != null){
				((Player) character).npcTalkingWith.LeaveInteraction();
			}
			_toTalkWith.StarTalkingWithPlayer();
			((Player) character).npcTalkingWith = _toTalkWith;
			GUIManager.Instance.InitiateInteraction(_toTalkWith);
		} else {
			Debug.Log("Tried to talk to " + _toTalkWith + " but it didn't want to talk");	
			character.EnterState(new IdleState(character));
		}
		
	}
	
	public override void OnExit(){
		Debug.Log(character.name + ": TalkState Exit");
	}
}