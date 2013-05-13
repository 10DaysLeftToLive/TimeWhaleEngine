using UnityEngine;
using System.Collections;

public class YoungFarmerMotherToLighthouseGirlToldOn : NPCConversation {
	// 1 = The NPC to start the conversation and 2 = The NPC talking back
	protected override void DialogueScript() {
		Add (1, "Child git over here!");
		Add (2, "Hey! Why did you have to go tell her!");
		Add (1, "Honestly I expected much more integrity out of ya! You should do your work, not git other people ta do it fer ya!");
		Add (2, "I'm a warrior! Warriors don't cook!");
		Add (1, " Yer a farmer! Enough of this! Yer not gettin anymore stories, yer dad and I will make sure of that!");
		Add (1, "Hmmpph...");
	}
}
