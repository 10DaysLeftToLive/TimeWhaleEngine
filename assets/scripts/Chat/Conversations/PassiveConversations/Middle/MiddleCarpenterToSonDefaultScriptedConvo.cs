using UnityEngine;
using System.Collections;

public class MiddleCarpenterToSonDefaultScriptedConvo : NPCConversation {
    // 1 = The NPC to start the conversation and 2 = The NPC talking back
    protected override void DialogueScript() {
        Add(1, "What have you gotten done?");
        Add(2, "Ah... well...");
        Add(1, "You've done nothing? What kind of worthless son are you!?");
        Add(1, "It's like this every time.  Do you have no drive for success?");
        Add(1, "How do you plan on honoring our family name if you do nothing to improve your work?");
        Add(1, "You're a no good, no use, miserable excuse for a son!");
    }
}
