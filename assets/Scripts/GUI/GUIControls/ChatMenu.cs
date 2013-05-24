using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * ChatMenu.cs
 * 	responsible for displaying the given npc chats above the head of each npc
 */
public class ChatMenu : GUIControl {
	private List<NPCChat> _npcsChats = new List<NPCChat>();
	private List<ChatInfo> _currentChats = new List<ChatInfo>();
	
	private static float CHATWIDTH = .2f;
	private static float CHATHEIGHT = .1f;
	
	public float distAboveHead = 34f;
	public float distToRightOfHead = 60f;
	public GUIStyle chatBoxStyle;
	
	public override void Init(){
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
		ShowChats();
	}
	
	private void ShowChats(){
		foreach (ChatInfo chatInfo in _currentChats){
			GUI.Box(GetRectOverNPC(chatInfo.npcTalking), chatInfo.text, chatBoxStyle);	
		}
	}
	
	private Rect GetRectOverNPC(NPC npc){
		Vector3 pos = npc.transform.position;
		pos.y = npc.transform.collider.bounds.max.y;
		Vector3 screenPos = Camera.mainCamera.WorldToScreenPoint(pos);
		screenPos.x -= ScreenSetup.verticalBarWidth;
		screenPos.y = ScreenSetup.screenHeight - screenPos.y; // flip it because of differences in screen and rect coords
		screenPos.y -= ScreenSetup.horizontalBarHeight;
		Vector2 percentageConvertedPos = new Vector2(screenPos.x/ScreenSetup.screenWidth, screenPos.y/ScreenSetup.screenHeight);
		percentageConvertedPos.y -= CHATHEIGHT;

		return (ScreenRectangle.NewRect(percentageConvertedPos.x, percentageConvertedPos.y, CHATWIDTH, CHATHEIGHT));
	}
}