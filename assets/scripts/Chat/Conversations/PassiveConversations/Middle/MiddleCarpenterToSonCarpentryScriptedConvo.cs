using UnityEngine;
using System.Collections;

public class MiddleCarpenterToSonCarpentryScriptedConvo : NPCConversation{
    // 1 = The NPC to start the conversation and 2 = The NPC talking back
    protected override void DialogueScript() {
        Add(1, "We'll finally get the windmill done today!");
        Add(2, "Yeah, but I need to do something first");
        Add(1, "Oh? Well make sure you get back in time to help your old man out");
    }
}
