using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * NPCChat.cs
 * 	Responsible for performing a single chat instance between npcs.
 *  When ever an npc's chat ends it will notify via delegate the new chat info to display
 *  Used with UpdateChat until the chat has been conpleted and it will return true
 */
public class NPCChat {
	public delegate void NPCChatChange(ChatInfo oldChat, ChatInfo newChat); // update the chat dialog when the current chat changes
	
	private List<ChatInfo> _chatTexts;
	private int _chatIndex;
	private NPCChatChange _npcChatChange;
	
	public ChatInfo GetCurrentInfo(){
		return (_chatTexts[_chatIndex]);
	}
	
	public ChatInfo GetLastChat(){
		return (_chatTexts[_chatTexts.Count-1]);	
	}
	
	public NPCChat(List<ChatInfo> chatTexts){
		_chatTexts = chatTexts;	
		_chatIndex = 0;
	}
	
	public void SetCallback(NPCChatChange npcChatChange){
		_npcChatChange = npcChatChange;
	}
	
	public bool UpdateChat(float timeDelta){
		if (GetCurrentInfo().DecrementTime(timeDelta)){
			if (_chatIndex+1 >= _chatTexts.Count) {
				return true; // if we are at the end
			}
			_npcChatChange(GetCurrentInfo(),_chatTexts[++_chatIndex]);
		}
		return (false);
	}
}