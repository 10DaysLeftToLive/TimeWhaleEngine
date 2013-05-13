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
		//StartCoroutine(TestFlagManager());
		StartCoroutine(TestOneOffChat());
	}
	
	private IEnumerator TestFlagManager(){
		yield return new WaitForSeconds(.1f);
		
		FlagManager.instance.SetFlag("Eat pie");	
		//FlagManager.instance.SetFlag("Eat pies");	
		//FlagManager.instance.SetFlag("Say hi");	
	}
	
	private IEnumerator TestInteractions(){
		yield return new WaitForSeconds(1f);
		
		manager.InitiateInteraction(npc1);
	}
	
	private IEnumerator TestOneOffChat(){
		yield return new WaitForSeconds(.1f);
		Debug.Log("Testing one off chat");
		npc1 = GameObject.Find("MotherYoung").GetComponent<NPC>();
		npc2 = GameObject.Find("SiblingYoung").GetComponent<NPC>();
		
		
		List<ChatInfo> chats = new List<ChatInfo>();
		chats.Add(new ChatInfo(npc1, "Chat 1", 10f));
		NPCChat chat = new NPCChat(chats);
		GUIManager.Instance.AddNPCChat(new NPCOneOffChat(new ChatInfo(npc1, "This is but a test, if it were real you would be dead already", 100f)));
		GUIManager.Instance.AddNPCChat(new NPCOneOffChat(new ChatInfo(npc2, "This is but a test", 100f)));
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
