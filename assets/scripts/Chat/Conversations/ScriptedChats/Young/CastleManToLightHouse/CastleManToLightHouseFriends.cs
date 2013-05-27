using UnityEngine;
using System.Collections;

public class CastleManToLightHouseFriends : NPCConversation {
	// 1 = The NPC to start the conversation and 2 = The NPC talking back
	protected override void DialogueScript() {
		Add (1, "Hi!  How are you doing?");
		Add (2, "Pretty good.  I'm here to find seashells to build a sand castle!");
		Add (1, "Do you need any help?");
		Add (2, "Heh...sure why not!");
		Add (1, "All right let's do this!");
	}
}