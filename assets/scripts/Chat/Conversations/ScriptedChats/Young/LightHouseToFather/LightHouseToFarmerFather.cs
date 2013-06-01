using UnityEngine;
using System.Collections;

public class LightHouseToFarmerFather : NPCConversation {
	// 1 = The NPC to start the conversation and 2 = The NPC talking back
	protected override void DialogueScript() {
		Add (1, "Fine...DAD HELP ME!  I'M BEING BULLIED!");
		Add (2, "Hey, you over there!  Get away from my daughter!");
		Add (2, "I don't like bullies, so I don't want to see you talk to my daughter again!");
		Add (1, "Ha!  Take that!");
		Add (2, "Now now, don't sink to that level.");
		Add (1, "Okay dad...");
	}
}
