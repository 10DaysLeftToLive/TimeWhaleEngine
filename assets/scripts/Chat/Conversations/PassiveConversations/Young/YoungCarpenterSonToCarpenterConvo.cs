using UnityEngine;
using System.Collections;

public class YoungCarpenterSonToCarpenterConvo : NPCConversation {
    // 1 = The NPC to start the conversation and 2 = The NPC talking back
    protected override void DialogueScript() {

        Add(2, "Today we're going to spend the day practicing some carpentry.");
        Add(2, "The first step in becoming a great carpenter is to practice your craft every day.");
        Add(2, "Do you have your tools and materials?");
        Add(1, "Uh, I don't have my tools.");
        Add(2, "Well, where are your tools?");
        Add(1, "I don't know, I've looked everywhere for them.");
        Add(2, "Keep searching, it's not like Gnomes came in the night and stole them.");
        Add(2, "Once you find them, start crafting products.");
        Add(2, "I'll be whittling on my stump.");
    }
} 

