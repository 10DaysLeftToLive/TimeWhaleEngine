using UnityEngine;
using System.Collections;

public class GiveNothingScript : NPCConversation{
    // 1 = The NPC to start the conversation and 2 = The NPC talking back
    protected override void DialogueScript() {
        Add(1, "What have you been doing all day?");
        Add(2, "Ahh, something?");
        Add(1, "...lets go home son");
    }
}
