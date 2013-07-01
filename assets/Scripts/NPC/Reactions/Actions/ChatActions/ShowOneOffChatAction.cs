using UnityEngine;
using System.Collections;

/// <summary>
/// Show one off chat action.
/// Will display the given chat information and will close the interaction menu if that npc is interacting
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
		if (chatInfo.npcTalking.IsInteracting()){ // if we want to disaplay a chat we should close the interaction menu
			GUIManager.Instance.CloseInteractionMenu();
		}
		GUIManager.Instance.AddNPCChat(new NPCOneOffChat(chatInfo));
	}
}