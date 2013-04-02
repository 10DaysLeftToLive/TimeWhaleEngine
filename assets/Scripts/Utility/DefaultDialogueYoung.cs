using System.Collections.Generic;

// Dictionary of non-specific NPC dialogue
// POSSIBILITY - Change to value to an array to provide multiple dialogue options
static class DefaultDialogueYoung {
	private static Dictionary<string, string> dialogue = new Dictionary<string, string>() {
		// Disposition low dialogue
		{"Angry", "I'm not telling you anything"},
		
		// No information dialogue
		{"Ambivalent", "Sorry, I do not want that."},
		
		// Items
		{"Apple", "I heard your Mother wanted an apple."},
		{"Apple[Carpenter]", "I heard Mother wanted an apple."},
		{"Tools", "I heard the Carpenters were looking for tools."},
		{"FishingRod", "I heard the Carpenter's son wanted to go fishing."}
	};
	
	// Return the dialogue depending on the string choice provided
	// POSSIBILITY - If dictionary has multiple options for dialogue choice, then randomly pick one
	public static string getDialogue(string dialogueChoice) {
		return dialogue[dialogueChoice];
	}
}
