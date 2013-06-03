using UnityEngine;
using System.Collections;

public class MusicianToCastlemanNotFriends : NPCConversation {
	// 1 = The NPC to start the conversation and 2 = The NPC talking back
	protected override void DialogueScript() {
		Add (1, "Hey why don't you introduce my son to some of your friends around town?..");
		Add (2, "But I don't want to!  Everyone here is mean!");
		Add (1, "That's no way to make friends!");
		Add (2, "But I don't want to make friends I want to read!");
		Add (1, "You need to get over your shyness.  Come on, both of you go run around town now!");
		Add (2, "...Fine!");
		Add (1, "Have fun!");
	}
}