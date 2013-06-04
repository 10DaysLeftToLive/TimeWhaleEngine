using UnityEngine;
using System.Collections;

public class YoungFarmerMotherToFarmerFatherOpenningScriptedDialogue : NPCConversation {
	// 1 = The NPC to start the conversation and 2 = The NPC talking back
	protected override void DialogueScript() {
		Add (1, "...ya need ta take care of tha farm first!");
		Add (1, "Ya are so obsessed with teachin that kid ta read that ya forgot ta buy the seeds fer next year!");
		Add (1, "Now we need ta figure out what ta do, or we'll starve!");
		Add (2, "But...our daughter is starting to learn how to read and I want to support her!");
		Add (1, "There ain't gonna be nothing to support if we don't have any crops!");
		Add (2, "I...");
		Add (2, "I...I guess I can try to find seeds later this night and stop telling our daughter stories...");
		Add (1, "Hmmpphh!  Too little too late, if ya ask me!");
	}
}
