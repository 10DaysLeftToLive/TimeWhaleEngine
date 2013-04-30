using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class NPCConversation {
	public List<Dialogue> dialogueList;
	
	public NPCConversation() {
		DialogueScript();
	}
	
	public void Add(int npc, string textToSay) {
		dialogueList.Add (new Dialogue(npc, textToSay));
	}
	
	protected virtual void DialogueScript(){}
}
