using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * ChatMenu.cs
 * 	responsible for displaying the given npc chats above the head of each npc
 */
public class ChatMenu : GUIControl {
	private List<NPCChat> _npcsChats;
	private List<ChatInfo> _currentChats;
	
	private static float CHATWIDTH = .2f;
	private static float CHATHEIGHT = .2f;
	
	public override void Init(){
		_npcsChats = new List<NPCChat>();
		_currentChats = new List<ChatInfo>();
	}
	
	private List<NPCChat> toRemove = new List<NPCChat>();
	public override void Update(){
		toRemove.Clear();
		foreach (NPCChat npcChat in _npcsChats){
			if (npcChat.UpdateChat(Time.deltaTime)){
				toRemove.Add(npcChat);
			}
		}
		foreach (NPCChat chatToRemove in toRemove){
			_npcsChats.Remove(chatToRemove);
			_currentChats.Remove(chatToRemove.GetLastChat()); // remove the chat from the list
		}
	}
	
	public void AddChat(NPCChat npcChatToAdd){
		_npcsChats.Add(npcChatToAdd);
		npcChatToAdd.SetCallback(UpdateChat);
		_currentChats.Add(npcChatToAdd.GetCurrentInfo());
	}
			
	public void UpdateChat(ChatInfo oldChat, ChatInfo newChat){
		Debug.Log("Updating chat (" + oldChat.text + ", " + newChat.text + ")");
		_currentChats.Remove(oldChat);
		_currentChats.Add(newChat);
	}
	
	public override void Render(){
		ShowChats();
	}
	
	private void ShowChats(){
		foreach (ChatInfo chatInfo in _currentChats){
			GUI.Label(GetRectOverNPC(chatInfo.npcTalking), chatInfo.text);	
		}
	}
	
	private Rect GetRectOverNPC(NPC npc){
		Vector3 pos = npc.transform.position;
		Vector3 screenPos = Camera.mainCamera.WorldToViewportPoint(pos);
		screenPos.y += .1f;
		Debug.Log("GetRectOverNPC = " + ScreenRectangle.NewRect(screenPos.x, screenPos.y, CHATWIDTH, CHATHEIGHT));
		return (ScreenRectangle.NewRect(screenPos.x, screenPos.y, CHATWIDTH, CHATHEIGHT));
	}
}
