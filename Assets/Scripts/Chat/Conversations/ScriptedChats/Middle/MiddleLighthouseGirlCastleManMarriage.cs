using UnityEngine;
using System.Collections;

public class MiddleLighthouseGirlCastleManMarriage : NPCConversation {
	// 1 = The NPC to start the conversation and 2 = The NPC talking back
	protected override void DialogueScript() {
		Add (1, "I'm marrying the CastleMan and there's nother you can do about it!");
		Add (2, "....");
		Add (2, "I've just been talking to the Carpenter and he says that his tools have gone missing. Do you know anything about that?");
		Add (1, "What...uh...yes...I mean NO!");
		Add (2, "I am fed up with your lies and attempts to sabotage this marriage. IT. WILL. BE. GOOD. FOR. YOU.");
		Add (1, "*Hmmmph* I.DON'T. LIKE. THE. CARPENTER'S. SON.");
		Add (2, "Enough of you childish behavior! Get back to working at the lighthouse and I fully expect an apology tomorrow morning.");
		Add (1, "...and you wonder why I ran away...");
		Add (2, "NOW!");
	}
}
