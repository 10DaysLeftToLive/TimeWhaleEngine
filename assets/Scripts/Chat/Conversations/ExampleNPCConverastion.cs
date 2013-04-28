using UnityEngine;
using System.Collections;

public class ExampleNPCConverastion : NPCConversation {
	public ExampleNPCConverastion(NPC npcOne, NPC npcTwo) : base(npcOne, npcTwo) {
	}
	
	// 1 = The NPC to start the conversation
	// 2 = The NPC talking back
	protected override void DialogueScript() {
		Add (_npcOne, "Hello");
		Add (_npcTwo, "Hi");
		Add (_npcOne, "Nice weather we are having");
		Add (_npcTwo, "Indeed");
		Add (_npcOne, "Why are we having a lame conversation about nothing like mundane people?");
		Add (_npcTwo, "BECAUSE THIS IS AN EXAMPLE!");
		Add (_npcTwo, "GOT IT?!");
		Add (_npcOne, "Yes'm");
		Add (_npcTwo, "Good!");
	}
}
