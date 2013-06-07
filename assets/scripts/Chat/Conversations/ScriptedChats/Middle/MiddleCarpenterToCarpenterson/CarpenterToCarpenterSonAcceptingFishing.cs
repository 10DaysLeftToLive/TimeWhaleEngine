using UnityEngine;
using System.Collections;

public class MiddleCarpenterToCarpenterSonAcceptingFishing : NPCConversation{
    // 1 = The NPC to start the conversation and 2 = The NPC talking back
    protected override void DialogueScript() {
        Add(1, "Wha'ts that! A fishing rod!?");
        Add(1, "What is wrong with you boy, have you no respect for our family's traditions?");
        Add(2, "I wanted to fish today father. Is that a crime?");
        Add(1, "For you it is. I promised we'd take care of the Farmer's roof today.");
        Add(1, "I'll not be having my son off fishing for the day instead of doing real work!");
        Add(2, "What!? You never told me anything about that!");
        Add(2, "If you're not going to tell me when I'm due for forced labor I'll have no part in it.");
        Add(2, "I'm going, and I don't care how much you yell at me about it.");
        Add(1, "What!?");
        Add(1, "...");
        Add(1, "Worthless, good for nothing");
    }
}
