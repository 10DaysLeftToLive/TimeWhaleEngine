using UnityEngine;
using System.Collections;

public class MotherToMusicianYoung : NPCConversation {
	// 1 = The NPC to start the conversation and 2 = The NPC talking back
	protected override void DialogueScript() {
		Add (1, "Hello there!  I'm your neighbor from below the cliff.");
		Add (1, "We wanted to welcome you to the town so I baked some apple pie for all of us!");
		Add (1, "Why don't you all take a piece!");
		Add (2, "Thank you!  This pie looks lovely!");
		Add (1, "Hey, why don't the two of you kids have fun with each other.");
		Add (1, "That way the musician and I can spend some time getting to know each other.");
		Add (2, "That sounds great.  Have fun you two!");
	}
}