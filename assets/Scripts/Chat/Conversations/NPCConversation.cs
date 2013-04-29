using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class NPCConversation {
	public List<Dialogue> convoList;
	
	public NPCConversation() {
		DialogueScript();
	}
	
	protected void Add(int npc, string textToSay) {
		convoList.Add (new Dialogue(npc, textToSay));
	}
	
	protected abstract void DialogueScript();
}
