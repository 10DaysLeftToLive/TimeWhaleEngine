using UnityEngine;
using System.Collections;

public class CastleManToLighthouseFirstMeeting : NPCConversation {
	// 1 = The NPC to start the conversation and 2 = The NPC talking back
	protected override void DialogueScript() {
		Add (1, "Hi...uh...I'm the new kid!");
		Add (2, "You are?  What's it like off this island?  Are there castles?  I like castles!");
		Add (1, "Well where I used to live we didn't have any castles...but my dad did tell poems about castles!");
		Add (2, "I love poetry!  Especially when it involves epic stories of fighting!");
		Add (1, "Well I don't know if I know too many of those...");
		Add (2, "No?  Well if you know some tell me!  Anyways later, I don't want my mom to get mad.");
		Add (1, "Bye...");
		
	}
}