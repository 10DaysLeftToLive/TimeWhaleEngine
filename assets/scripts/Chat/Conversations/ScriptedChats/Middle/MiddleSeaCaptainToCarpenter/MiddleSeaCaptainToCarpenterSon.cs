using UnityEngine;
using System.Collections;

public class MiddleSeaCaptainToCarpenterSon : NPCConversation{
    // 1 = The NPC to start the conversation and 2 = The NPC talking back
    protected override void DialogueScript() {
        Add(1, "Hello there, I hear you're a carpenter!");
        Add(2, "Well, I really want to become a fisherman.");
        Add(1, "Even better! You see, I lost my ship to the sea and washed up here not too long ago...");
        Add(2, "Everyone knows that.");
        Add(1, "Well, yes, but I was thinking of making a new ship! I don't have the skills to make a ship myself though, which is why I was wondering if you could give me a hand.");
        Add(2, "Ahh, hmm, that sounds like an interesting idea.");
        Add(1, "Well, there's some wood I've gathered if you want to get started.");
        Add(2, "Sure, I've got nothing better to do...");
    }
}
