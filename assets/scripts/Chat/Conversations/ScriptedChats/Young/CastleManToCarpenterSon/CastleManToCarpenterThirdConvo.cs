using UnityEngine;
using System.Collections;

public class CastleManToCarpenterThirdConvo : NPCConversation {
	// 1 = The NPC to start the conversation and 2 = The NPC talking back
	protected override void DialogueScript() {
		Add (1, "So...Sorry for being so weird...I just want to be your friend.");
		Add (2, "Heh. It's okay.  I figured you were just being akward.");
		Add (1, "So yeah...I'm the new kid in town.");
		Add (2, "What do you like doing?");
		Add (1, "I like to read and write poetry!");
		Add (2, "Nice!  That sounds cool!  Well, I am busy, but I hope to see you around some time again!");
		Add (1, "See you later!");
	}
}