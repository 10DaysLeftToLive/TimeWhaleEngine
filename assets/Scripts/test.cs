using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class test : MonoBehaviour {
	NPCChat npcChat;
	public GUIManager manager;
	public NPC npc1;
	public NPC npc2;
	
	// Use this for initialization
	void Start () {
		List<ChatInfo> chats = new List<ChatInfo>();
		chats.Add(new ChatInfo(npc1, "Chat 1"));
		chats.Add(new ChatInfo(npc2, "Chat 2"));
		chats.Add(new ChatInfo(npc1, "Chat 3"));
		chats.Add(new ChatInfo(npc2, "Chat 4"));
		
		npcChat = new NPCChat(chats);
		
		manager.AddNPCChat(npcChat);
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
