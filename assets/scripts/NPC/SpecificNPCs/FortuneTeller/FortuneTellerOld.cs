using UnityEngine;
using System.Collections;

/// <summary>
/// FortuneTellerOld specific scripting values
/// </summary>
public class FortuneTellerOld : NPC {
	protected override void Init() {
		id = NPCIDs.FORTUNE_TELLER;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		Reaction speakWithSiblingReactionPartOne = new Reaction();
		ShowMultipartChatAction speakWithSiblingChatPartOne = new ShowMultipartChatAction(this);
		speakWithSiblingChatPartOne.AddChat("Patience.", 2f);
		speakWithSiblingReactionPartOne.AddAction(speakWithSiblingChatPartOne);
		flagReactions.Add(FlagStrings.FortunetellerTalkToSiblingOldPartOne, speakWithSiblingReactionPartOne);
		
		Reaction speakWithSiblingReactionPartTwo = new Reaction();
		ShowMultipartChatAction speakWithSiblingChatPartTwo = new ShowMultipartChatAction(this);
		speakWithSiblingChatPartTwo.AddChat("I said patience!!", 2f);		
		speakWithSiblingReactionPartTwo.AddAction(speakWithSiblingChatPartTwo);
		flagReactions.Add(FlagStrings.FortunetellerTalkToSiblingOldPartTwo, speakWithSiblingReactionPartTwo);
		
		Reaction speakWithSiblingReactionPartThree = new Reaction();
		ShowMultipartChatAction speakWithSiblingChatPartThree = new ShowMultipartChatAction(this);
		speakWithSiblingChatPartThree.AddChat(".", .12f);
		speakWithSiblingChatPartThree.AddChat("..", .12f);
		speakWithSiblingChatPartThree.AddChat("...", .12f);
		speakWithSiblingChatPartThree.AddChat("...!", .12f);
		speakWithSiblingChatPartThree.AddChat("...!!", 1.5f);
		speakWithSiblingChatPartThree.AddChat("...!", .75f);
		speakWithSiblingChatPartThree.AddChat("...", .5f);
		speakWithSiblingChatPartThree.AddChat("..", .33f);
		speakWithSiblingChatPartThree.AddChat(".. O", .5f);
		speakWithSiblingChatPartThree.AddChat(".. Ok", .5f);
		speakWithSiblingChatPartThree.AddChat(".. Ok.", 2f);
		speakWithSiblingChatPartThree.AddChat("Let's get this over with.", 2f);
		
		speakWithSiblingReactionPartThree.AddAction(speakWithSiblingChatPartThree);
		flagReactions.Add(FlagStrings.FortunetellerTalkToSiblingOldPartThree, speakWithSiblingReactionPartThree);
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Have the choices in your past affected your present and influenced your future?"));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}
	
	protected override void SetUpSchedules(){
		
	}
	
	
	#region EmotionStates
	#region Initial Emotion State
	private class InitialEmotionState : EmotionState{
	
	
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
		
		}
		
		public override void UpdateEmotionState(){
			
		}
	
	}
	#endregion
	#endregion
}
