using UnityEngine;
using System.Collections;

public class MiddleCarpenterToSonDefaultScriptedConvo : NPCConversation {
    // 1 = The NPC to start the conversation and 2 = The NPC talking back
    protected override void DialogueScript() {
        Add(1, "Where are your tools?");
        Add(2, "I don't know, I've looked everywhere for them");
        Add(1, "Kepp searching, it's not like Gnomes came in the night and stole them");
    }
}

