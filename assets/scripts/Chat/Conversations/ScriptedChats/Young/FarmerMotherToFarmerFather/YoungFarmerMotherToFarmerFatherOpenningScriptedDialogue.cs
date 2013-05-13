using UnityEngine;
using System.Collections;

public class YoungFarmerMotherToFarmerFatherOpenningScriptedDialogue : NPCConversation {
	// 1 = The NPC to start the conversation and 2 = The NPC talking back
	protected override void DialogueScript() {
		Add (1, "....ya just can't let her keep 'er head in the clouds. She needs ta learn practical skills!");
		Add (2, "Look, a little bit of storytelling doesn't do any harm...");
		Add (1, "Look at 'er! She's going around thinkin' she's gonna grow up ta be a warrior!");
		Add (2, "You're right...");
		Add (1, "So no more storytelling");
	}
}
