using UnityEngine;
using System.Collections;

public class ShowOneOffChatAction : Action {
	private ChatInfo chatInfo;
	
	public ShowOneOffChatAction(){}
	
	public ShowOneOffChatAction(NPC _npcToChat, string _textToShow){
		chatInfo = new ChatInfo(_npcToChat, _textToShow);
	}
	
	public ShowOneOffChatAction(NPC _npcToChat, string _textToShow, float _timeToShow){
		chatInfo = new ChatInfo(_npcToChat, _textToShow, _timeToShow);
	}
	
	public override void Perform(){
		GUIManager.Instance.AddNPCChat(new NPCOneOffChat(chatInfo));
	}
}