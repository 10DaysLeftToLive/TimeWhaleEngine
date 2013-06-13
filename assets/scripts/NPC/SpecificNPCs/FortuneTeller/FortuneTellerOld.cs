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
		speakWithSiblingChatPartFive.AddChat("I see...",1.5f);
		speakWithSiblingChatPartFive.AddChat("You are off your path.",3f);
		speakWithSiblingChatPartFive.AddChat("You should visit the sacred tree.", 3f);
		speakWithSiblingChatPartFive.AddChat("That is your fortune.", 1.5f);
		speakWithSiblingReactionPartFive.AddAction(speakWithSiblingChatPartFive);
		flagReactions.Add(FlagStrings.FortunetellerTalkToSiblingOldPartFive, speakWithSiblingReactionPartFive);
		
		Reaction speakWithSiblingReactionPartSix = new Reaction();
		ShowMultipartChatAction speakWithSiblingChatPartSix = new ShowMultipartChatAction(this);
		speakWithSiblingChatPartSix.AddChat("Have a nice day.", 3f);
		speakWithSiblingReactionPartSix.AddAction(speakWithSiblingChatPartSix);
		flagReactions.Add(FlagStrings.FortunetellerTalkToSiblingOldPartSix, speakWithSiblingReactionPartSix);
		
		Reaction speakWithSiblingReactionPartSeven = new Reaction();
		ShowMultipartChatAction speakWithSiblingChatPartSeven = new ShowMultipartChatAction(this);
		speakWithSiblingChatPartSeven.AddChat("Such a loud one she is...", 3f);
		speakWithSiblingChatPartSeven.AddChat("Ohh.", 1.25f);
		speakWithSiblingChatPartSeven.AddChat("Hello there quiet one.", 2f);
		speakWithSiblingChatPartSeven.AddChat("Care for your fortune?", 1.25f);
		speakWithSiblingReactionPartSeven.AddAction(speakWithSiblingChatPartSeven);
		speakWithSiblingReactionPartSeven.AddAction(new NPCEmotionUpdateAction(this, new EnterFortuneState(this, "Decided to learn your fate?")));
		speakWithSiblingReactionPartSeven.AddAction(new NPCAddScheduleAction(this, fortunetellerFortuneWithPlayer));
		flagReactions.Add(FlagStrings.FortunetellerTalkToSiblingOldPartSeven, speakWithSiblingReactionPartSeven);
		
		//Reaction changeScheduleAfterSiblingFortune = new Reaction();
		//changeScheduleAfterSiblingFortune.AddAction(speakWithSiblingChatPartSeven);
		//changeScheduleAfterSiblingFortune.AddAction(new NPCAddScheduleAction(this, fortunetellerFortuneWithPlayer));
		//changeScheduleAfterSiblingFortune.AddAction(new NPCEmotionUpdateAction(this, new InitialEmotionState(this, "Line 72")));
		//flagReactions.Add(FlagStrings.FortunetellerOldChangeSchedule, changeScheduleAfterSiblingFortune);
	}
	
	protected override EmotionState GetInitEmotionState(){
		//return (new InitialEmotionState(this, "Have the choices in your past affected your present and influenced your future?"));
		return (new EnterFortuneState(this, "Decided to learn your fate?"));
	}
	
	private Schedule fortunetellerFortuneWithPlayer;
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}
	
	protected override void SetUpSchedules(){
		fortunetellerFortuneWithPlayer = (new FortuneToPlayerSchedule(this));
	}
	
	#region EmotionStates
		#region Initial Emotion State
		private class InitialEmotionState : EmotionState{
			public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
			}
		
			public override void UpdateEmotionState() {
			
			}
		}
		#endregion
	#endregion
	
	private class EnterFortuneState : EmotionState {
		
		Reaction stallFortuneReactionA = new Reaction();
		Reaction stallFortuneReactionB = new Reaction();
		Reaction stallFortuneReactionC = new Reaction();
		
		Reaction secretFortuneReaction = new Reaction();
		Reaction secretFortuneReactionA = new Reaction();
		Reaction secretFortuneReactionB = new Reaction();
		Reaction secretFortuneReactionC = new Reaction();
		Reaction secretFortuneReactionD = new Reaction();
		Reaction secretFortuneReactionE = new Reaction();
		Reaction secretFortuneReactionF = new Reaction();
		
		#region pathA - 
		Reaction Path_A1 = new Reaction();
		Reaction Path_A2 = new Reaction();
		Reaction Path_A3 = new Reaction();
		Reaction Path_A4 = new Reaction();
		Reaction Path_A5 = new Reaction();
		Reaction Path_A6 = new Reaction();
		Reaction Path_A7 = new Reaction();
		Reaction Path_A8 = new Reaction();
		Reaction Path_A9 = new Reaction();
		Reaction Path_A10 = new Reaction();
		#endregion
		#region pathB - 
		Reaction Path_B1 = new Reaction();
		Reaction Path_B2 = new Reaction();
		Reaction Path_B3 = new Reaction();
		Reaction Path_B4 = new Reaction();
		Reaction Path_B5 = new Reaction();
		Reaction Path_B6 = new Reaction();
		Reaction Path_B7 = new Reaction();
		Reaction Path_B8 = new Reaction();
		Reaction Path_B9 = new Reaction();
		Reaction Path_B10 = new Reaction();
		#endregion
		#region pathC - 
		Reaction Path_C1 = new Reaction();
		Reaction Path_C2 = new Reaction();
		Reaction Path_C3 = new Reaction();
		Reaction Path_C4 = new Reaction();
		Reaction Path_C5 = new Reaction();
		Reaction Path_C6 = new Reaction();
		Reaction Path_C7 = new Reaction();
		Reaction Path_C8 = new Reaction();
		Reaction Path_C9 = new Reaction();
		Reaction Path_C10 = new Reaction();
		#endregion
		#region pathD - 
		Reaction Path_D1 = new Reaction();
		Reaction Path_D2 = new Reaction();
		Reaction Path_D3 = new Reaction();
		Reaction Path_D4 = new Reaction();
		Reaction Path_D5 = new Reaction();
		Reaction Path_D6 = new Reaction();
		Reaction Path_D7 = new Reaction();
		Reaction Path_D8 = new Reaction();
		Reaction Path_D9 = new Reaction();
		Reaction Path_D10 = new Reaction();
		#endregion
		#region pathE - 
		Reaction Path_E1 = new Reaction();
		Reaction Path_E2 = new Reaction();
		Reaction Path_E3 = new Reaction();
		Reaction Path_E4 = new Reaction();
		Reaction Path_E5 = new Reaction();
		Reaction Path_E6 = new Reaction();
		Reaction Path_E7 = new Reaction();
		Reaction Path_E8 = new Reaction();
		Reaction Path_E9 = new Reaction();
		Reaction Path_E10 = new Reaction();
		#endregion
		#region pathF - 
		Reaction Path_F1 = new Reaction();
		Reaction Path_F2 = new Reaction();
		Reaction Path_F3 = new Reaction();
		Reaction Path_F4 = new Reaction();
		Reaction Path_F5 = new Reaction();
		Reaction Path_F6 = new Reaction();
		Reaction Path_F7 = new Reaction();
		Reaction Path_F8 = new Reaction();
		Reaction Path_F9 = new Reaction();
		Reaction Path_F10 = new Reaction();
		#endregion
		
		Choice beginFortuneChoice = new Choice("I Have", "Then Let us Begin" + "\n\n" + "What is it that you wish for?");
		Choice stallFortuneChoice = new Choice("No", "Patience is perhaps the path we all should take. Take your time, but remember that your time is limited in this world.");
		Choice stallFortuneChoiceA = new Choice("Not Now", "Time is ticking and the sun is slowly setting. Come back soon or it may be too late.");
		Choice stallFortuneChoiceB = new Choice("I'll Pass", "Good bye till we meet again.");
		Choice secretFortuneChoice = new Choice(".", "..");
		Choice secretFortuneChoiceA = new Choice("..", "...");
		Choice secretFortuneChoiceB = new Choice("...", "... .");
		Choice secretFortuneChoiceC = new Choice("... .", "... ..");
		Choice secretFortuneChoiceD = new Choice("... ..", "... ...");
		Choice secretFortuneChoiceE = new Choice("... ...", "Take this. Fairwell.");
		
		#region Choice_A
		Choice Choice_A1a = new Choice("", ""); Choice Choice_A2a = new Choice("", "");
		Choice Choice_A1b = new Choice("", ""); Choice Choice_A2b = new Choice("", "");
		
		Choice Choice_A3a = new Choice("", ""); Choice Choice_A4a = new Choice("", "");
		Choice Choice_A3b = new Choice("", ""); Choice Choice_A4b = new Choice("", "");
		
		Choice Choice_A5a = new Choice("", ""); Choice Choice_A6a = new Choice("", "");
		Choice Choice_A5b = new Choice("", ""); Choice Choice_A6b = new Choice("", "");
		
		Choice Choice_A7a = new Choice("", ""); 
		Choice Choice_A7b = new Choice("", "");
		#endregion
		#region Choice_B 
		Choice Choice_B1a = new Choice("", ""); Choice Choice_B2a = new Choice("", "");
		Choice Choice_B1b = new Choice("", ""); Choice Choice_B2b = new Choice("", "");
		
		Choice Choice_B3a = new Choice("", ""); Choice Choice_B4a = new Choice("", "");
		Choice Choice_B3b = new Choice("", ""); Choice Choice_B4b = new Choice("", "");
	
		Choice Choice_B5a = new Choice("", ""); Choice Choice_B6a = new Choice("", "");
		Choice Choice_B5b = new Choice("", ""); Choice Choice_B6b = new Choice("", "");
		
		Choice Choice_B7a = new Choice("", "");
		Choice Choice_B7b = new Choice("", "");
		#endregion	
		#region Choice_C
		Choice Choice_C1b = new Choice("", ""); Choice Choice_C2a = new Choice("", "");
		Choice Choice_C1a = new Choice("", ""); Choice Choice_C2b = new Choice("", "");
		
		Choice Choice_C3a = new Choice("", ""); Choice Choice_C4a = new Choice("", "");
		Choice Choice_C3b = new Choice("", ""); Choice Choice_C4b = new Choice("", "");
		
		Choice Choice_C5a = new Choice("", ""); Choice Choice_C6a = new Choice("", "");
		Choice Choice_C5b = new Choice("", ""); Choice Choice_C6b = new Choice("", "");
		
		Choice Choice_C7a = new Choice("", ""); 
		Choice Choice_C7b = new Choice("", "");
		#endregion
		#region Choice_D
		Choice Choice_D1a = new Choice("", ""); Choice Choice_D2a = new Choice("", "");
		Choice Choice_D1b = new Choice("", ""); Choice Choice_D2b = new Choice("", "");
		
		Choice Choice_D3a = new Choice("", ""); Choice Choice_D4a = new Choice("", "");
		Choice Choice_D3b = new Choice("", ""); Choice Choice_D4b = new Choice("", "");
		  
		Choice Choice_D5a = new Choice("", ""); Choice Choice_D6a = new Choice("", ""); 
		Choice Choice_D5b = new Choice("", ""); Choice Choice_D6b = new Choice("", ""); 
		 
		Choice Choice_D7a = new Choice("", ""); Choice Choice_D7b = new Choice("", ""); 
		#endregion
		#region Choice_E
		Choice Choice_E1a = new Choice("", ""); Choice Choice_E2a = new Choice("", "");
		Choice Choice_E1b = new Choice("", ""); Choice Choice_E2b = new Choice("", "");
		
		Choice Choice_E3a = new Choice("", ""); Choice Choice_E4a = new Choice("", "");
		Choice Choice_E3b = new Choice("", ""); Choice Choice_E5a = new Choice("", "");
		
		Choice Choice_E4b = new Choice("", ""); Choice Choice_E6a = new Choice("", ""); 
		Choice Choice_E5b = new Choice("", ""); Choice Choice_E6b = new Choice("", "");
		
		Choice Choice_E7a = new Choice("", ""); 
		Choice Choice_E7b = new Choice("", ""); 
		#endregion
		#region Choice_F
		Choice Choice_F1a = new Choice("", ""); Choice Choice_F2a = new Choice("", "");
		Choice Choice_F1b = new Choice("", ""); Choice Choice_F2b = new Choice("", "");	
		
		Choice Choice_F3a = new Choice("", ""); Choice Choice_F4a = new Choice("", "");
		Choice Choice_F3b = new Choice("", ""); Choice Choice_F4b = new Choice("", "");
		 
		Choice Choice_F5a = new Choice("", ""); Choice Choice_F6a = new Choice("", ""); 
		Choice Choice_F5b = new Choice("", ""); Choice Choice_F6b = new Choice("", ""); 
		
		Choice Choice_F7a = new Choice("", "");
		Choice Choice_F7b = new Choice("", ""); 
		#endregion
		
		public EnterFortuneState (NPC toControl, string currentDialogue) : base(toControl, currentDialogue) {
			
			_allChoiceReactions.Add(beginFortuneChoice, new  DispositionDependentReaction(Path_A1));
			_allChoiceReactions.Add(stallFortuneChoice, new  DispositionDependentReaction(stallFortuneReactionA));	
			
			//stallFortuneReaction.AddAction(new NPCRemoveChoiceAction(toControl, stallFortuneChoice,  new DispositionDependentReaction(stallFortuneReactionA)));
			//stallFortuneReaction.AddAction(new NPCAddChoiceAction(toControl, stallFortuneChoiceA, new DispositionDependentReaction(stallFortuneReactionA)));
			#region Stall Fortune Reaction
			stallFortuneReactionA.AddAction(new NPCCallbackOnNPCAction(OnOpenWindowOne, toControl));
			stallFortuneReactionB.AddAction(new NPCCallbackOnNPCAction(OnOpenWindowTwo, toControl));
			stallFortuneReactionC.AddAction(new NPCCallbackAction(ClearChoiceReactions));
			#endregion
			#region Secret Fortune Reaction			
			secretFortuneReaction.AddAction(new NPCRemoveChoiceAction(toControl, beginFortuneChoice));
			secretFortuneReaction.AddAction(new NPCRemoveChoiceAction(toControl, stallFortuneChoiceB));
			secretFortuneReaction.AddAction(new NPCAddChoiceAction(toControl, secretFortuneChoiceA, new DispositionDependentReaction(secretFortuneReactionA)));
			
			secretFortuneReactionA.AddAction(new NPCRemoveChoiceAction(toControl, secretFortuneChoiceA));
			secretFortuneReactionA.AddAction(new NPCAddChoiceAction(toControl, secretFortuneChoiceB, new DispositionDependentReaction(secretFortuneReactionB)));
			
			secretFortuneReactionB.AddAction(new NPCRemoveChoiceAction(toControl, secretFortuneChoiceB));
			secretFortuneReactionB.AddAction(new NPCAddChoiceAction(toControl, secretFortuneChoiceC, new DispositionDependentReaction(secretFortuneReactionC)));
			
			secretFortuneReactionC.AddAction(new NPCRemoveChoiceAction(toControl, secretFortuneChoiceC));
			secretFortuneReactionC.AddAction(new NPCAddChoiceAction(toControl, secretFortuneChoiceD, new DispositionDependentReaction(secretFortuneReactionD)));
			
			secretFortuneReactionD.AddAction(new NPCRemoveChoiceAction(toControl, secretFortuneChoiceD));
			secretFortuneReactionD.AddAction(new NPCAddChoiceAction(toControl, secretFortuneChoiceE, new DispositionDependentReaction(secretFortuneReactionE)));
			
			secretFortuneReactionE.AddAction(new NPCRemoveChoiceAction(toControl, secretFortuneChoiceE));
			secretFortuneReactionE.AddAction(new NPCGiveItemAction(toControl, StringsItem.TimeWhale));
	//EDIT THIS
			secretFortuneReactionE.AddAction(new NPCEmotionUpdateAction(toControl, new InitialEmotionState(toControl, "")));
			#endregion
			
			#region Path_A
			Path_A1.AddAction(new NPCAddChoiceAction(toControl, Choice_A1a, new DispositionDependentReaction(Path_B2)));
			Path_A1.AddAction(new NPCAddChoiceAction(toControl, Choice_A1b, new DispositionDependentReaction(Path_C2)));
			
			Path_A2.AddAction(new NPCAddChoiceAction(toControl, Choice_A2a, new DispositionDependentReaction(Path_B3)));
			Path_A2.AddAction(new NPCAddChoiceAction(toControl, Choice_A2b, new DispositionDependentReaction(Path_C3)));
			
			Path_A3.AddAction(new NPCAddChoiceAction(toControl, Choice_A3a, new DispositionDependentReaction(Path_B4)));
			Path_A3.AddAction(new NPCAddChoiceAction(toControl, Choice_A3b, new DispositionDependentReaction(Path_C4)));
			
			Path_A4.AddAction(new NPCAddChoiceAction(toControl, Choice_A4a, new DispositionDependentReaction(Path_B5)));
			Path_A4.AddAction(new NPCAddChoiceAction(toControl, Choice_A4b, new DispositionDependentReaction(Path_C5)));
			
			Path_A5.AddAction(new NPCAddChoiceAction(toControl, Choice_A5a, new DispositionDependentReaction(Path_B6)));
			Path_A5.AddAction(new NPCAddChoiceAction(toControl, Choice_A5b, new DispositionDependentReaction(Path_C6)));
			
			Path_A6.AddAction(new NPCAddChoiceAction(toControl, Choice_A6a, new DispositionDependentReaction(Path_B7)));
			Path_A6.AddAction(new NPCAddChoiceAction(toControl, Choice_A6b, new DispositionDependentReaction(Path_C7)));
			#endregion	
			#region Path_B
			Path_B1.AddAction(new NPCAddChoiceAction(toControl, Choice_B1a, new DispositionDependentReaction(Path_C2)));
			Path_B1.AddAction(new NPCAddChoiceAction(toControl, Choice_B1b, new DispositionDependentReaction(Path_D2)));
			
			Path_B2.AddAction(new NPCAddChoiceAction(toControl, Choice_B2a, new DispositionDependentReaction(Path_C3)));
			Path_B2.AddAction(new NPCAddChoiceAction(toControl, Choice_B2b, new DispositionDependentReaction(Path_D3)));
			
			Path_B3.AddAction(new NPCAddChoiceAction(toControl, Choice_B3a, new DispositionDependentReaction(Path_C4)));
			Path_B3.AddAction(new NPCAddChoiceAction(toControl, Choice_B3b, new DispositionDependentReaction(Path_D4)));
			
			Path_B4.AddAction(new NPCAddChoiceAction(toControl, Choice_B4a, new DispositionDependentReaction(Path_C5)));
			Path_B4.AddAction(new NPCAddChoiceAction(toControl, Choice_B4b, new DispositionDependentReaction(Path_D5)));
			
			Path_B5.AddAction(new NPCAddChoiceAction(toControl, Choice_B5a, new DispositionDependentReaction(Path_C6)));
			Path_B5.AddAction(new NPCAddChoiceAction(toControl, Choice_B5b, new DispositionDependentReaction(Path_D6)));
			
			Path_B6.AddAction(new NPCAddChoiceAction(toControl, Choice_B6a, new DispositionDependentReaction(Path_C7)));
			Path_B6.AddAction(new NPCAddChoiceAction(toControl, Choice_B6b, new DispositionDependentReaction(Path_D7)));
			#endregion	
			#region Path_C
			Path_C1.AddAction(new NPCAddChoiceAction(toControl, Choice_C1a, new DispositionDependentReaction(Path_D2)));
			Path_C1.AddAction(new NPCAddChoiceAction(toControl, Choice_C1b, new DispositionDependentReaction(Path_E2)));
			
			Path_C2.AddAction(new NPCAddChoiceAction(toControl, Choice_C2a, new DispositionDependentReaction(Path_D3)));
			Path_C2.AddAction(new NPCAddChoiceAction(toControl, Choice_C2b, new DispositionDependentReaction(Path_E3)));
			
			Path_C3.AddAction(new NPCAddChoiceAction(toControl, Choice_C3a, new DispositionDependentReaction(Path_D4)));
			Path_C3.AddAction(new NPCAddChoiceAction(toControl, Choice_C3b, new DispositionDependentReaction(Path_E4)));
			
			Path_C4.AddAction(new NPCAddChoiceAction(toControl, Choice_C4a, new DispositionDependentReaction(Path_D5)));
			Path_C4.AddAction(new NPCAddChoiceAction(toControl, Choice_C4b, new DispositionDependentReaction(Path_E5)));
			
			Path_C5.AddAction(new NPCAddChoiceAction(toControl, Choice_C5a, new DispositionDependentReaction(Path_D6)));
			Path_C5.AddAction(new NPCAddChoiceAction(toControl, Choice_C5b, new DispositionDependentReaction(Path_E6)));
			
			Path_C6.AddAction(new NPCAddChoiceAction(toControl, Choice_C6a, new DispositionDependentReaction(Path_D7)));
			Path_C6.AddAction(new NPCAddChoiceAction(toControl, Choice_C6b, new DispositionDependentReaction(Path_E7)));
			#endregion
			#region Path_D
			Path_D1.AddAction(new NPCAddChoiceAction(toControl, Choice_D1a, new DispositionDependentReaction(Path_E2)));
			Path_D1.AddAction(new NPCAddChoiceAction(toControl, Choice_D1b, new DispositionDependentReaction(Path_F2)));
			
			Path_D2.AddAction(new NPCAddChoiceAction(toControl, Choice_D2a, new DispositionDependentReaction(Path_E3)));
			Path_D2.AddAction(new NPCAddChoiceAction(toControl, Choice_D2b, new DispositionDependentReaction(Path_F3)));
			
			Path_D3.AddAction(new NPCAddChoiceAction(toControl, Choice_D3a, new DispositionDependentReaction(Path_E4)));
			Path_D3.AddAction(new NPCAddChoiceAction(toControl, Choice_D3b, new DispositionDependentReaction(Path_F4)));
			
			Path_D4.AddAction(new NPCAddChoiceAction(toControl, Choice_D4a, new DispositionDependentReaction(Path_E5)));
			Path_D4.AddAction(new NPCAddChoiceAction(toControl, Choice_D4b, new DispositionDependentReaction(Path_F5)));
			
			Path_D5.AddAction(new NPCAddChoiceAction(toControl, Choice_D5a, new DispositionDependentReaction(Path_E6)));
			Path_D5.AddAction(new NPCAddChoiceAction(toControl, Choice_D5b, new DispositionDependentReaction(Path_F6)));
			
			Path_D6.AddAction(new NPCAddChoiceAction(toControl, Choice_D6a, new DispositionDependentReaction(Path_E7)));
			Path_D6.AddAction(new NPCAddChoiceAction(toControl, Choice_D6b, new DispositionDependentReaction(Path_F7)));
			#endregion
			#region Path_E
			Path_E1.AddAction(new NPCAddChoiceAction(toControl, Choice_E1a, new DispositionDependentReaction(Path_F2)));
			Path_E1.AddAction(new NPCAddChoiceAction(toControl, Choice_E1b, new DispositionDependentReaction(Path_A2)));
			
			Path_E2.AddAction(new NPCAddChoiceAction(toControl, Choice_E2a, new DispositionDependentReaction(Path_F3)));
			Path_E2.AddAction(new NPCAddChoiceAction(toControl, Choice_E2b, new DispositionDependentReaction(Path_A3)));
			
			Path_E3.AddAction(new NPCAddChoiceAction(toControl, Choice_E3a, new DispositionDependentReaction(Path_F4)));
			Path_E3.AddAction(new NPCAddChoiceAction(toControl, Choice_E3b, new DispositionDependentReaction(Path_A4)));
			
			Path_E4.AddAction(new NPCAddChoiceAction(toControl, Choice_E4a, new DispositionDependentReaction(Path_F5)));
			Path_E4.AddAction(new NPCAddChoiceAction(toControl, Choice_E4b, new DispositionDependentReaction(Path_A5)));
			
			Path_E5.AddAction(new NPCAddChoiceAction(toControl, Choice_E5a, new DispositionDependentReaction(Path_F6)));
			Path_E5.AddAction(new NPCAddChoiceAction(toControl, Choice_E5b, new DispositionDependentReaction(Path_A6)));
			
			Path_E6.AddAction(new NPCAddChoiceAction(toControl, Choice_E6a, new DispositionDependentReaction(Path_F7)));
			Path_E6.AddAction(new NPCAddChoiceAction(toControl, Choice_E6b, new DispositionDependentReaction(Path_A7)));
			#endregion
			#region Path_F
			Path_F1.AddAction(new NPCAddChoiceAction(toControl, Choice_F1a, new DispositionDependentReaction(Path_A2)));
			Path_F1.AddAction(new NPCAddChoiceAction(toControl, Choice_F1b, new DispositionDependentReaction(Path_B2)));
			
			Path_F2.AddAction(new NPCAddChoiceAction(toControl, Choice_F2a, new DispositionDependentReaction(Path_A3)));
			Path_F2.AddAction(new NPCAddChoiceAction(toControl, Choice_F2b, new DispositionDependentReaction(Path_B3)));
			
			Path_F3.AddAction(new NPCAddChoiceAction(toControl, Choice_F3a, new DispositionDependentReaction(Path_A4)));
			Path_F3.AddAction(new NPCAddChoiceAction(toControl, Choice_F3b, new DispositionDependentReaction(Path_B4)));
			
			Path_F4.AddAction(new NPCAddChoiceAction(toControl, Choice_F4a, new DispositionDependentReaction(Path_A5)));
			Path_F4.AddAction(new NPCAddChoiceAction(toControl, Choice_F4b, new DispositionDependentReaction(Path_B5)));
			
			Path_F5.AddAction(new NPCAddChoiceAction(toControl, Choice_F5a, new DispositionDependentReaction(Path_A6)));
			Path_F5.AddAction(new NPCAddChoiceAction(toControl, Choice_F5b, new DispositionDependentReaction(Path_B6)));
			
			Path_F6.AddAction(new NPCAddChoiceAction(toControl, Choice_F6a, new DispositionDependentReaction(Path_A7)));
			Path_F6.AddAction(new NPCAddChoiceAction(toControl, Choice_B6b, new DispositionDependentReaction(Path_B7)));
			#endregion	
		
		}

		public void UpdateText(NPC toControl, string updatedMessage) {
			SetDefaultText(updatedMessage);
			GUIManager.Instance.RefreshInteraction();
		}

		#region On Open Window Interactions
		public void OnOpenWindowOne(NPC toControl) {
			ClearChoiceReactions();
			UpdateText(toControl, "The fortune speaks. Have you accepted my guidance?");
			//GUIManager.Instance.RefreshInteraction();
			SetOnOpenInteractionReaction(new DispositionDependentReaction(stallFortuneReactionB));
			stallFortuneReactionB.AddAction(new NPCAddChoiceAction(toControl, beginFortuneChoice,  new DispositionDependentReaction(Path_A1)));
			stallFortuneReactionB.AddAction(new NPCAddChoiceAction(toControl, secretFortuneChoice,  new DispositionDependentReaction(secretFortuneReaction)));
			stallFortuneReactionB.AddAction(new NPCAddChoiceAction(toControl, stallFortuneChoiceB, new DispositionDependentReaction(stallFortuneReactionC)));
		}
		
		public void OnOpenWindowTwo(NPC toControl) {
			SetOnOpenInteractionReaction(new DispositionDependentReaction(stallFortuneReactionC));
			stallFortuneReactionC.AddAction(new NPCCallbackAction(ClearChoiceReactions));
			stallFortuneReactionC.AddAction(new NPCEmotionUpdateAction(toControl, new InitialEmotionState(toControl,"The spirits will guide me~")));
		}
		
		#endregion
		public void CloseWindow() {
			GUIManager.Instance.CloseInteractionMenu();
		}
		
		public void ClearChoiceReactions() {
			_allChoiceReactions.Clear();
		}

	}
}
