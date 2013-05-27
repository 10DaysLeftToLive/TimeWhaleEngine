using UnityEngine;
using System.Collections;


public class CastleManToCarpenterFirstConvo : NPCConversation {
	// 1 = The NPC to start the conversation and 2 = The NPC talking back
	protected override void DialogueScript() {
		Add (1, "I...uh...I'm the new kid...");
		Add (2, "Oh hey, that's cool, nice to meet you.  You got a name?");
		Add (1, "I...uh...I'm the musicician's son!");
		Add (2, "That's your name?  Never mind, I'm a bit busy, why don't we talk later?");
		Add (1, "I...uh...sure?");
		Add (1, "...See!  He hates me!");
	}
}