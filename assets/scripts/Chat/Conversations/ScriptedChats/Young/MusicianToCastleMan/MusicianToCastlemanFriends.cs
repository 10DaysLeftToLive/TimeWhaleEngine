using UnityEngine;
using System.Collections;


public class MusicianToCastlemanFriends : NPCConversation {
	// 1 = The NPC to start the conversation and 2 = The NPC talking back
	protected override void DialogueScript() {
		Add (1, "Hey why don't you introduce my son to some of your friends around town?");
		Add (2, "But I don't want to...I want to read!");
		Add (1, "Heh.  You can do that later.  Now go and have some fun okay?");
		Add (2, "*Sigh*  All right mom.");
		Add (1, "Have fun!");
	}
}