using UnityEngine;
using System.Collections;

public class MiddleSpecialConvos : NPCConvoList {
	protected override void BuildList() {
		
		#region Conversation One
		_convo = new NPCConvoChance(5);
		
		Add (1, "Hello");
		Add (2, "Hi");
		Add (1, "Nice weather we are having");
		Add (2, "Indeed");
		Add (1, "Why are we having a lame conversation about nothing like mundane people?");
		Add (2, "BECAUSE THIS IS AN EXAMPLE!");
		Add (2, "GOT IT?!");
		Add (1, "Yes'm");
		Add (2, "Good!");
		
		AddConvo(_convo);
		#endregion
		
		#region Conversation Two
		_convo = new NPCConvoChance(7);
		
		Add (1, "Hello convo 2");
		Add (2, "Goodbye convo 2");
		
		AddConvo(_convo);
		#endregion
	}
}
