using UnityEngine;
using System.Collections;

/// <summary>
/// Show one off chat action will display a chatinfo to the screen for a set time or a time based on its' text size
/// </summary>
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