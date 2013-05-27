using UnityEngine;
using System.Collections;


public class CastleManToCarpenterSecondConvo : NPCConversation {
	// 1 = The NPC to start the conversation and 2 = The NPC talking back
	protected override void DialogueScript() {
		Add (1, "Uh...how's fishing?");
		Add (2, "I'm still busy you know...");
		Add (1, "Yeah but uh fishing is cool and fun and hip!");
		Add (2, "You are weird...");
		Add (1, "I...uh...yeah...");
	}
}