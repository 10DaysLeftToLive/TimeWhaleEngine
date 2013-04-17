using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This is just for testing out various things temporarily as more test harnesses are added. Feel free to add your own things in the mean time though.
public class test : MonoBehaviour {
	NPCChat npcChat;
	public GUIManager manager;
	public NPC npc1;
	public NPC npc2;
	
	public bool DOTESTS = false;
	
	void Start () {
		if (!DOTESTS) return;
		StartCoroutine(TestInteractions());
	}
	
	private IEnumerator TestInteractions(){
		yield return new WaitForSeconds(.1f);
		
		manager.InitiateInteraction(npc1);
	}
	
	private void TestChat(){
		List<ChatInfo> chats = new List<ChatInfo>();
		chats.Add(new ChatInfo(npc1, "Chat 1"));
		chats.Add(new ChatInfo(npc2, "Chat 2"));
		chats.Add(new ChatInfo(npc1, "Chat 3"));
		chats.Add(new ChatInfo(npc2, "Chat 4"));
		
		npcChat = new NPCChat(chats);
		
		manager.AddNPCChat(npcChat);
	}

	
	void Update () {
		
	}
}
