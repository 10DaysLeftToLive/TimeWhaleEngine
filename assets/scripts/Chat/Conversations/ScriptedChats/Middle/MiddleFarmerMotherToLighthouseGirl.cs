using UnityEngine;
using System.Collections;

public class MiddleFarmerMotherToLighthouseGirl : NPCConversation {
	// 1 = The NPC to start the conversation and 2 = The NPC talking back
	protected override void DialogueScript() {
		Add (1, "...Look mom! I'm not going to marry the Carpenter's Son!");
		Add (2, "And the last time ah let ya get your way, yah ran from home with that tramp.");
		Add (1, "But I've chosen not to marry anyone this time!");
		Add (2, "What ya need is someone that can help ya settle down and the carpenter's son will do that very nicely!");
		Add (1, "Mom, I...");
		Add (2, "ENOUGH! What's done is done. Go back and take care of the lighthouse, ya need ta work!");
		Add (1, "...Fine...this isn't over..");
	}
}
