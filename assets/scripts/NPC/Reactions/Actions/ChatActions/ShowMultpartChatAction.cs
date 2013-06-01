using UnityEngine;
using System.Collections;

public class ShowMultipartChatAction : Action {
	private NPCChat npcChat;
	private NPC npcToChat;
	
	public ShowMultipartChatAction(){}
	
	public ShowMultipartChatAction(NPC _npcToChat){
		npcToChat = _npcToChat;
		npcChat = new NPCChat();
	}
	
	public void AddChat(ChatInfo chatToAdd){
		npcChat.AddChatInfo(chatToAdd);
	}
	
	public void AddChat(string text){
		npcChat.AddChatInfo(new ChatInfo(npcToChat, text));
	}
	
	public void AddChat(string text, float timeToShow){
		npcChat.AddChatInfo(new ChatInfo(npcToChat, text, timeToShow));
	}
	
	public override void Perform(){
		GUIManager.Instance.AddNPCChat(npcChat);
	}
}