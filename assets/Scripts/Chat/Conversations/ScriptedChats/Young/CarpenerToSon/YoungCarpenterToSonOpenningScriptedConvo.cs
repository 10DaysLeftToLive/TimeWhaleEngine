using UnityEngine;
using System.Collections;

public class YoungCarpenterToSonOpenningScriptedConvo : NPCConversation {
	// 1 = The NPC to start the conversation and 2 = The NPC talking back
	protected override void DialogueScript() {
		Add (1, "Have you found my old tools yet?");
		Add (2, "But, but...I don't know where they are...");
		Add (1, "Honestly how can you measure up our great tradition if you can't even find the tools.  Stop being lazy.");
	}
}
