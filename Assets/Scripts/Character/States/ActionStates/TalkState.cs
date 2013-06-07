using UnityEngine;
using System.Collections;

public class TalkState : AbstractState {
	NPC _toTalkWith;
	private static readonly float NEARPLAYERDISTANCE = 1.5f;
	
	public TalkState(Character toControl, NPC toTalkWith) : base(toControl){
		_toTalkWith = toTalkWith;
	}
	
	public override void Update(){
	}
	
	public override void OnEnter(){
		DebugManager.instance.Log("Player talk enter", "Player", "State");
		if (!_toTalkWith.IsInteracting() 
				&& _toTalkWith.CanTalk() 
				&& Utils.InDistance(character.gameObject, _toTalkWith.gameObject, NEARPLAYERDISTANCE)){
			character.PlayAnimation(Strings.animation_stand);
			
			if (((Player) character).npcTalkingWith != null){
				((Player) character).npcTalkingWith.LeaveInteraction();
			}
			_toTalkWith.StarTalkingWithPlayer();
			((Player) character).npcTalkingWith = _toTalkWith;
			MakePlayerLookAtNPC();
			_toTalkWith.LookAtPlayer();
			GUIManager.Instance.InitiateInteraction(_toTalkWith);
		} else {
			character.EnterState(new IdleState(character));
		}
	}
	
	private void MakePlayerLookAtNPC(){
		if (Utils.CalcDifference(character.gameObject.transform.position.x, _toTalkWith.gameObject.transform.position.x) < 0){ // if player is on left
			character.LookRight();
		} else {
			character.LookLeft();
		}
	}
	
	public override void OnExit(){
	}
}