using UnityEngine;
using System.Collections;

public class LighthouseToFarmerMother : NPCConversation {
	// 1 = The NPC to start the conversation and 2 = The NPC talking back
	protected override void DialogueScript() {
		Add (1, "I finished the pie!");
		Add (2, "Well then, ya did a good job!");
		Add (1, "Can I go and play on the beach?");
		Add (2, "I dunno, ya still got a bunch ta do!");
		Add (1, "Please! Please! Please!");
		Add (2, "All right.  Have fun!");
		Add (1, "Yay!");
	}
}
