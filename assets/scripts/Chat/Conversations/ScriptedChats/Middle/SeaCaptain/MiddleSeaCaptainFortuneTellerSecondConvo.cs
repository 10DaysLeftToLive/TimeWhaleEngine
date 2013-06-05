using UnityEngine;
using System.Collections;

public class MiddleSeaCaptainFortuneTellerSecondConvo : NPCConversation {	
	// 1 = The NPC to start the conversation and 2 = The NPC talking back
	protected override void DialogueScript() {
		Add (2, "Back to ignore more of my fortunes?");
		Add (1, "Aye. I do not see how ye can think I can't get off this island.");
		Add (2, "To start, you didn't drop your anchor and your boat floated away");
		Add (1, "It was an 'onest mistake.");
		Add (2, "Then you don't know how to read a treasure map.");
		Add (1, "Hey! Now yer just instulting me.");
		Add (2, "You came to me. I am only telling you what I see.");
		Add (1, "AAAGH! Yer just bein' mean!");
	}
}