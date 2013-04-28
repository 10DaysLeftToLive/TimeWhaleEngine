using UnityEngine;
using System.Collections;

public class ExampleNPCConverastion : NPCConversation {	
	// 1 = The NPC to start the conversation and 2 = The NPC talking back
	protected override void DialogueScript() {
		Add (1, "Hello");
		Add (2, "Hi");
		Add (1, "Nice weather we are having");
		Add (2, "Indeed");
		Add (1, "Why are we having a lame conversation about nothing like mundane people?");
		Add (2, "BECAUSE THIS IS AN EXAMPLE!");
		Add (2, "GOT IT?!");
		Add (1, "Yes'm");
		Add (2, "Good!");
	}
}
