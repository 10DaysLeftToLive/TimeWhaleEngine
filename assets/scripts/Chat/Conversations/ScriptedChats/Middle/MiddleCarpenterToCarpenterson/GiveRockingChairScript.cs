using UnityEngine;
using System.Collections;

public class GiveRockingChairScript : NPCConversation{
    // 1 = The NPC to start the conversation and 2 = The NPC talking back
    protected override void DialogueScript() {
        Add(1, "What have you been doing all day?");
        Add(2, "Making you a rocking chair!");
        Add(1, "Oh! You are the best son a man could ask for");
    }
}
