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
	
	protected List<ChatInfo> _chatTexts;
	protected int _chatIndex;
	private NPCChatChange _npcChatChange;
	
	public ChatInfo GetCurrentInfo(){
		return (_chatTexts[_chatIndex]);
	}
	
	public ChatInfo GetLastChat(){
		return (_chatTexts[_chatTexts.Count-1]);	
	}
	
	public NPCChat(){
		_chatTexts = new List<ChatInfo>();
		_chatIndex = 0;
	}
	
	public NPCChat(List<ChatInfo> chatTexts) : base(){
		_chatTexts = chatTexts;	
	}
	
	public void AddChatInfo(ChatInfo chatToAdd){
		_chatTexts.Add(chatToAdd);
	}
	
	public float AddUpTimeToChat(){
		float timeTotal = 0;
		foreach (ChatInfo chatInfo in _chatTexts){
			timeTotal += chatInfo.GetTime();
		}
		return (timeTotal);
	}
	
	/// <summary>
	/// Sets the callback for getting the new chat data at the right time. This will only be used by the chat menu and will be done 
	/// 	automatically
	/// </summary>
	/// <param name='npcChatChange'>
	/// Npc chat change.
	/// </param>
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