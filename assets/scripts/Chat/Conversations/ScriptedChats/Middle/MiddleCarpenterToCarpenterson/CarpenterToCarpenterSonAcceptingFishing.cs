using UnityEngine;
using System.Collections;

public class MiddleCarpenterToCarpenterSonAcceptingFishing : NPCConversation{
    // 1 = The NPC to start the conversation and 2 = The NPC talking back
    protected override void DialogueScript() {
        Add(1, "What are you doing?");
        Add(2, "I'm building a boat, father");
        Add(1, "I see. I thought you didn't want to be a carpenter.");
        Add(2, "Well..");
        Add(2, "Well you see, I want to be a fisherman, but I can't without the proper tools, which I can make because of my carpentry skills");
        Add(1, "I, I see. Carry on then");
    }
}
