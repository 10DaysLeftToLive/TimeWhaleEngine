using UnityEngine;
using System.Collections;

public class MiddleCastleManToLighthouseGirl : NPCConversation {
	// 1 = The NPC to start the conversation and 2 = The NPC talking back
	protected override void DialogueScript() {
		Add (1, "*Out of Breath* At long last I get to try and court my fair lady?");
		Add (2, "Fair Lady? Come now pretending to be like the stories my dad read to me won't make you endearing.");
		Add (1, "Endeading? My dear woman, I always speak this way!");
		Add (2, "Wait...I remember you! you were the one I used to play with on the beach and build sand castles!");
		Add (1, "You remember me!");
		Add (2, "Of course I do! You were really sweet to me!");
		Add (1, "I CAN'T DO ROMANTIC DIALOGUE!!!!!"); // this should stay!
	}
}

