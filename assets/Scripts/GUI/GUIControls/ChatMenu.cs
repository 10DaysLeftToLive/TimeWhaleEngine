using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * ChatMenu.cs
 * 	responsible for displaying the given npc chats above the head of each npc
 */
public class ChatMenu : GUIControl {
	public int DISTANCE_TO_CHAT = 4;
	public int DISTANCE_NEAR_PLAYER = 9;
	private List<NPCChat> _npcsChats = new List<NPCChat>();
	private List<ChatInfo> _currentChats = new List<ChatInfo>();
	private Player player;
	
	public float CHATWIDTH = .45f;
	
	public GUIStyle chatBoxStyle;
	private static float FONTRATIO = 20; // kinda arbitrary
	
	
	public override void Init(){
		player = FindObjectOfType(typeof(Player)) as Player;
        chatBoxStyle.fontSize = (Mathf.RoundToInt(Mathf.Min(ScreenSetup.screenWidth, ScreenSetup.screenHeight) / FONTRATIO));
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
				MakeBox(chatInfo, chatBoxStyle);	
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
		MakeBox(chatInfo, chatBoxStyle);	
	}
	
	string text;
	GUIContent boxContent;
	Vector2 boxSize;
	float boxWidth;
	Vector2 topLeftPos;
	private void MakeBox(ChatInfo infoToDisplay, GUIStyle boxStyle){
		text = infoToDisplay.text;
		boxContent = new GUIContent(text);
		boxSize.x = CHATWIDTH * ScreenSetup.screenWidth; // convert into screen space for gui checking
		boxStyle.wordWrap = false; // need to check if length of the line is less that that allocated so we can make it smaller
		boxWidth = boxStyle.CalcSize(boxContent).x;
		if (boxWidth < boxSize.x){
			boxSize.x = boxWidth;
		}
		boxStyle.wordWrap = true;
		
		boxSize.y = boxStyle.CalcHeight(boxContent, boxSize.x);//need to use calc height to account for word wrapping
		boxSize.x = boxSize.x/ScreenSetup.screenWidth;
		boxSize.y = boxSize.y/ScreenSetup.screenHeight;
		topLeftPos = GetRectTopLeftPoint(infoToDisplay.npcTalking.transform, boxSize);
		GUI.Box(ScreenRectangle.NewRect(topLeftPos.x, topLeftPos.y, boxSize.x, boxSize.y), boxContent, boxStyle);
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
	private Vector2 GetRectTopLeftPoint(Transform npcTransform, Vector2 rectSize){
		pos = npcTransform.position;
		pos.y = npcTransform.collider.bounds.max.y;
		screenPos = Camera.mainCamera.WorldToScreenPoint(pos);
		screenPos.x -= ScreenSetup.verticalBarWidth;
		screenPos.y = ScreenSetup.screenHeight - screenPos.y; // flip it because of differences in screen and rect coords
		screenPos.y -= ScreenSetup.horizontalBarHeight;
		percentageConvertedPos.x = screenPos.x/ScreenSetup.screenWidth;
		percentageConvertedPos.y = screenPos.y/ScreenSetup.screenHeight;
		percentageConvertedPos.x -= rectSize.x/2;
		percentageConvertedPos.y -= rectSize.y;

		return (percentageConvertedPos);
	}
}