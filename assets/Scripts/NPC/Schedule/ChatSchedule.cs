using UnityEngine;
using System.Collections;

public class ChatSchedule : Schedule{
	public ChatSchedule(NPC npcToChat, NPCChat chatToPerform) : base(npcToChat, priorityEnum.DoNow){
		float timeToChat = chatToPerform.AddUpTimeToChat();
		Debug.Log("Time to chat = " + timeToChat);
		canChat = false;
		GUIManager.Instance.AddNPCChat(chatToPerform);
		Add(new TimeTask(timeToChat, new InteractingWithPlayerState(npcToChat)));
	}
}