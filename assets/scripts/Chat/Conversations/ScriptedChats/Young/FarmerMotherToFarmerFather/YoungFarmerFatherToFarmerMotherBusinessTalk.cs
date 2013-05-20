using UnityEngine;
using System.Collections;

public class YoungFarmerFatherToFarmerMotherBusinessTalk : NPCConversation {
	// 1 = The NPC to start the conversation and 2 = The NPC talking back
	protected override void DialogueScript() {
		Add (1, "But I did the best I could...");
		Add (2, "How many times have I toldja, ya need to make harder deals!");
		Add (1, "It doesn't feel right though...");
		Add (2, "I don't care about your feelings!  You need to git out there and sell our produce.  We need tha money to raise our daughter!");
		Add (1, "All right...I'll try...I'll try harder..");
		Add (2, "Ya don't need ta try, ya need ta act!");
	}
}
