using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PassiveChatToPlayer : ManagerSingleton<PassiveChatToPlayer> {
	private static List<ChatToPlayer> whatToSayQueue;
	private ChatToPlayer chatToSay;
	private string textToSay;
	
	public override void Init() {
		whatToSayQueue = new List<ChatToPlayer>();
	}
	
	public string GetTextToSay(NPC npc) {
		if (whatToSayQueue.Count > 0) {
			if (whatToSayQueue[whatToSayQueue.Count - 1]._npc != npc) {
				chatToSay = whatToSayQueue[whatToSayQueue.Count - 1];
				textToSay = chatToSay._textToSay;
				whatToSayQueue.Remove(chatToSay);
				return (textToSay);
			}
		}
		
		return ChooseGenericText();
	}
	
	public void AddTextToSay(string toSay) {
		whatToSayQueue.Add(new ChatToPlayer(toSay));
	}
	
	public void AddTextToSayAboutNPC(NPC npc, string toSay) {
		whatToSayQueue.Add(new ChatToPlayer(npc, toSay));
	}
	
	public void RemoveNPCChat(NPC npc) {
		foreach (ChatToPlayer chat in whatToSayQueue) {
			if (chat._npc == npc) {
				whatToSayQueue.Remove(chat);
			}
		}
	}
	
	private string ChooseGenericText() {
		switch(Random.Range(1, 3)) {
			case 1:
				return "How's it going?";
				break;
			
			case 2:
				return "Hello";
				break;
			
			case 3:
				return "Salutations";
				break;
			
			default:
				return "Hi";
				break;
		}
	}
		
		
}

public struct ChatToPlayer {
	public NPC _npc;
	public string _textToSay;
	
	public ChatToPlayer(string textToSay) {
		_npc = null;
		_textToSay = textToSay;
	}
	
	public ChatToPlayer(NPC npc, string textToSay) {
		_npc = npc;
		_textToSay = textToSay;
	}
}