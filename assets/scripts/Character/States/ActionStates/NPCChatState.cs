using UnityEngine;
using System.Collections;

public class NPCChatState : AbstractState {
	Character _player;
	float timeToChat;
	NPCChat _chatToPerform;
	
	public NPCChatState(Character toControl, Character player, NPCChat chatToPerform) : base(toControl){
		_player = player;
		_chatToPerform = chatToPerform;
	}
	
	public override void Update(){
		FacePlayer();
		timeToChat -= Time.deltaTime;
		
		if (timeToChat <= 0) {
			if (character is NPC) {
				((NPC)character).NextTask();
			} 
		}
	}
	
	public void FacePlayer() {
		if (character.transform.position.x > _player.transform.position.x) {
			character.LookLeft();
		} else {
			character.LookRight();
		}
	}
	
	public override void OnEnter(){
		timeToChat = _chatToPerform.AddUpTimeToChat();
		GUIManager.Instance.AddNPCChat(_chatToPerform);
		
		character.PlayAnimation(Strings.animation_stand); // Should be a talk animation
		_player.PlayAnimation(Strings.animation_stand);
	}
	
	public override void OnExit(){
		
	}
}
