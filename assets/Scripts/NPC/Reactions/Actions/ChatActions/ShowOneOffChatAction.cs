using UnityEngine;
using System.Collections;

public class ShowOneOffChatAction : Action {
	private ChatInfo chatInfo;
	
	public ShowOneOffChatAction(){}
	
	public ShowOneOffChatAction(NPC _npcToChat, string _textToShow){
		chatInfo = new ChatInfo(_npcToChat, _textToShow);
	}
	
	public override void Perform(){
		GUIManager.Instance.AddNPCChat(new NPCOneOffChat(chatInfo));
	}
}