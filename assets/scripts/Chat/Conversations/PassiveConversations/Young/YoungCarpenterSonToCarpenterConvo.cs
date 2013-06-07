using UnityEngine;
using System.Collections;

public class YoungCarpenterSonToCarpenterConvo : NPCConversation {
    // 1 = The NPC to start the conversation and 2 = The NPC talking back
    protected override void DialogueScript() {
        Add(2, "Where are your tools?");
        Add(1, "I don't know, I've looked everywhere for them");
        Add(2, "Keep searching, it's not like Gnomes came in the night and stole them");
    }
} 

