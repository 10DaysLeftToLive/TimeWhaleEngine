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
	
	public float distAboveHead = 34f;
	public float distToRightOfHead = -3.7f;
	public GUIStyle chatBoxStyle;
	
	public override void Init(){
		_npcsChats = new List<NPCChat>();
		_currentChats = new List<ChatInfo>();
	}
	
	private List<NPCChat> toRemove = new List<NPCChat>();
	public override void UpdateControl(){
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
		_currentChats.Remove(oldChat);
		_currentChats.Add(newChat);
	}
	
	private bool hasSetStyle = false;
	public override void Render(){
		if (hasSetStyle){
			hasSetStyle = !hasSetStyle;
		}
		ShowChats();
	}
	
	private void ShowChats(){
		foreach (ChatInfo chatInfo in _currentChats){
			GUI.Label(GetRectOverNPC(chatInfo.npcTalking), chatInfo.text, chatBoxStyle);	
		}
	}
	
	private Rect GetRectOverNPC(NPC npc){
		Vector3 pos = npc.transform.position;
		pos.y = npc.transform.collider.bounds.max.y;
		pos.x += npc.transform.collider.bounds.size.x * distToRightOfHead;
		Vector2 screenPos = Camera.mainCamera.WorldToScreenPoint(pos);
		screenPos.y += distAboveHead;
		// invert the y and put it above the npc, kinda arbitrary values
		Vector2 percentageConvertedPos = new Vector2(screenPos.x/ScreenSetup.screenWidth, 1 - (screenPos.y/ScreenSetup.screenHeight));
		return (ScreenRectangle.NewRect(percentageConvertedPos.x, percentageConvertedPos.y, CHATWIDTH, CHATHEIGHT));
	}
}