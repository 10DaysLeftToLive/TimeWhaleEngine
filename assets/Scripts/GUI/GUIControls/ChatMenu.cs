using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * ChatMenu.cs
 * 	responsible for displaying the given npc chats above the head of each npc
 */
public class ChatMenu : GUIControl {
	private static int DISTANCE_TO_CHAT = 4;
	private static int DISTANCE_NEAR_PLAYER = 9;
	private List<NPCChat> _npcsChats = new List<NPCChat>();
	private List<ChatInfo> _currentChats = new List<ChatInfo>();
	private Player player;
	
	private static float CHATWIDTH = .2f;
	private static float CHATHEIGHT = .1f;
	
	public GUIStyle chatBoxStyle;
	
	public override void Init(){
		player = FindObjectOfType(typeof(Player)) as Player;		
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
	
	public override void Render(){
		ShowChats();
	}
	
	private void ShowChats(){
		foreach (ChatInfo chatInfo in _currentChats){
			if (IsVisibleToPlayer(chatInfo.npcTalking)){
				GUI.color = new Color(1,1,1, 1);
				GUI.Box(GetRectOverNPC(chatInfo.npcTalking), chatInfo.text, chatBoxStyle);	
			} else if (IsNearPlayer(chatInfo.npcTalking)) {
				FadeChatBox(chatInfo);
			}
		}
	}
	
	float distance;
	float distancePercent;
	private void FadeChatBox(ChatInfo chatInfo){
		distance = Utils.GetDistance(chatInfo.npcTalking.gameObject, player.gameObject);
		distancePercent = 1 - distance/DISTANCE_NEAR_PLAYER;
		GUI.color = new Color(1,1,1,distancePercent);
		GUI.Box(GetRectOverNPC(chatInfo.npcTalking), chatInfo.text, chatBoxStyle);	
	}
	
	private bool IsVisibleToPlayer(NPC npc){
		return (Utils.InDistance(npc.gameObject, player.gameObject, DISTANCE_TO_CHAT));
	}
		
	private bool IsNearPlayer(NPC npc){
		return (Utils.InDistance(npc.gameObject, player.gameObject, DISTANCE_NEAR_PLAYER));
	}
	
	private static Vector3 pos;
	private static Vector3 screenPos;
	private static Vector2 percentageConvertedPos = new Vector2();
	private Rect GetRectOverNPC(NPC npc){
		pos = npc.transform.position;
		pos.y = npc.transform.collider.bounds.max.y;
		screenPos = Camera.mainCamera.WorldToScreenPoint(pos);
		screenPos.x -= ScreenSetup.verticalBarWidth;
		screenPos.y = ScreenSetup.screenHeight - screenPos.y; // flip it because of differences in screen and rect coords
		screenPos.y -= ScreenSetup.horizontalBarHeight;
		percentageConvertedPos.x = screenPos.x/ScreenSetup.screenWidth;
		percentageConvertedPos.y = screenPos.y/ScreenSetup.screenHeight;
		percentageConvertedPos.y -= CHATHEIGHT;

		return (ScreenRectangle.NewRect(percentageConvertedPos.x, percentageConvertedPos.y, CHATWIDTH, CHATHEIGHT));
	}
}