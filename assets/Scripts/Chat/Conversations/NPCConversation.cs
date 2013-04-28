using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class NPCConversation {
	public List<Dialogue> convoList;
	protected NPC _npcOne;
	protected NPC _npcTwo;
	
	public NPCConversation(NPC npcOne, NPC npcTwo) {
		_npcOne = npcOne;
		_npcTwo = npcTwo;
		DialogueScript();
	}
	
	protected void Add(NPC npc, string line) {
		convoList.Add (new Dialogue(npc, line));
	}
	
	protected abstract void DialogueScript();
}
