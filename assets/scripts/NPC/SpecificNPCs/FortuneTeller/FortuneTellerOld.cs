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
		speakWithSiblingChatPartThree.AddChat(".. F", .33f);
		speakWithSiblingChatPartThree.AddChat(".. Fi", .5f);
		speakWithSiblingChatPartThree.AddChat(".. Fin", .5f);
		speakWithSiblingChatPartThree.AddChat(".. Fine", 1f);
		speakWithSiblingChatPartThree.AddChat(".. Fine.", 1f);
		speakWithSiblingChatPartThree.AddChat("Let's get this over with.", 2f);
		speakWithSiblingChatPartThree.AddChat("What is it that you seek?", 2f);
		speakWithSiblingReactionPartThree.AddAction(speakWithSiblingChatPartThree);
		flagReactions.Add(FlagStrings.FortunetellerTalkToSiblingOldPartThree, speakWithSiblingReactionPartThree);
		
		Reaction speakWithSiblingReactionPartFour = new Reaction();
		ShowMultipartChatAction speakWithSiblingChatPartFour = new ShowMultipartChatAction(this);
		speakWithSiblingChatPartFour.AddChat(".",.2f);
		speakWithSiblingChatPartFour.AddChat("..",.2f);
		speakWithSiblingChatPartFour.AddChat("...", 2f);
		speakWithSiblingChatPartFour.AddChat("Seeking a fortune with no goal in mind?", 1.5f);
		speakWithSiblingReactionPartFour.AddAction(speakWithSiblingChatPartFour);
		flagReactions.Add(FlagStrings.FortunetellerTalkToSiblingOldPartFour, speakWithSiblingReactionPartFour);
		
		Reaction speakWithSiblingReactionPartFive = new Reaction();
		ShowMultipartChatAction speakWithSiblingChatPartFive = new ShowMultipartChatAction(this);
		speakWithSiblingChatPartFive.AddChat(".",.5f);
		speakWithSiblingChatPartFive.AddChat("..", .6f);
		speakWithSiblingChatPartFive.AddChat("...", .7f);
		speakWithSiblingChatPartFive.AddChat("You...", .5f);
		speakWithSiblingChatPartFive.AddChat("You..", .6f);
		speakWithSiblingChatPartFive.AddChat("You.", .7f);
		speakWithSiblingChatPartFive.AddChat("You should visit the sacred tree.", 2f);
		speakWithSiblingChatPartFive.AddChat("That is your fortune.", 1.5f);
		speakWithSiblingReactionPartFive.AddAction(speakWithSiblingChatPartFive);
		flagReactions.Add(FlagStrings.FortunetellerTalkToSiblingOldPartFive, speakWithSiblingReactionPartFive);
		
		Reaction speakWithSiblingReactionPartSix = new Reaction();
		ShowMultipartChatAction speakWithSiblingChatPartSix = new ShowMultipartChatAction(this);
		speakWithSiblingChatPartSix.AddChat("Have a nice day.",1.5f);
		speakWithSiblingReactionPartSix.AddAction(speakWithSiblingChatPartSix);
		flagReactions.Add(FlagStrings.FortunetellerTalkToSiblingOldPartSix, speakWithSiblingReactionPartSix);
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
