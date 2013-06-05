using UnityEngine;
using System.Collections;

public class MiddleSeaCaptainFortuneTellerFirstConvo : NPCConversation {	
	// 1 = The NPC to start the conversation and 2 = The NPC talking back
	protected override void DialogueScript() {
		Add (1, "Hmm, you look like a fortuenteller. Can you tell me my future?");
		Add (2, "Yes");
		Add (1, "Will I be able to get off this island?");
		Add (2, "No");
		Add (1, "You didn't even have to do anything to know that?");
		Add (1, "No drawing of tarot cards? No trinket throwing?");
		Add (2, "The aura that you exude is strong enough that I don't have to do anything to predict your future.");
		Add (1, "I never believed in fortunes...");
	}
}
