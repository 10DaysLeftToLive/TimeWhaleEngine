using UnityEngine;
using System.Collections;

public class CastlemanToLighthouseNotFriends : NPCConversation {
	// 1 = The NPC to start the conversation and 2 = The NPC talking back
	protected override void DialogueScript() {
		Add (1, "Hi...I didn't...I didn't expect to see you...");
		Add (2, "I came down to look for some seashells to build a sandcastle with!");
		Add (1, "Uh...that uh sounds fun...");
		Add (2, "You sound weird.");
		Add (1, "I uh...");
		Add (2, "Cat got your tongue?  Well I'm gonna play on my own!");
		Add (1, "...");
	}
}