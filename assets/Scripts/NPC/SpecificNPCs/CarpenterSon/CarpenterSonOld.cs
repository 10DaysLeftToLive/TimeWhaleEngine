using UnityEngine;
using System.Collections;

/// <summary>
/// Carpenter son old specific scripting values
/// </summary>
public class CarpenterSonOld : NPC {
	protected override void Init() {
		id = NPCIDs.CARPENTER_SON;
		//AudioListener.volume = 0;
		base.Init();
	}
	internal static bool ateBool = false;
	protected override void SetFlagReactions() {

		
		#region Greet Old Sibling
		Reaction introductionToSiblingOld = new Reaction();
		introductionToSiblingOld.AddAction(new UpdateDefaultTextAction(this, "Best wind in years today, and with my father with me I'm sure I'll get my best catch yet."));
		introductionToSiblingOld.AddAction(new NPCAddScheduleAction(this, greetSiblingOldSchedule));
		flagReactions.Add(FlagStrings.siblingOldReachedCarpenterSonFlag, introductionToSiblingOld);
		
		Reaction greetSiblingPartOne = new Reaction();
		ShowMultipartChatAction greetSiblingPartOneChat = new ShowMultipartChatAction(this);
		greetSiblingPartOneChat.AddChat("I'm fine..", 2f);
		greetSiblingPartOneChat.AddChat("Work as usual.", 2f);
		greetSiblingPartOneChat.AddChat("Brought an apple you say?", 2f);
		greetSiblingPartOne.AddAction(greetSiblingPartOneChat);
		greetSiblingPartOne.AddAction(new NPCEmotionUpdateAction(this, new WantAppleState(this,"Got an apple you say?")));
		flagReactions.Add(FlagStrings.oldCarpenterGreetSiblingPartOneFlag, greetSiblingPartOne);
		#endregion
		
		#region Go To Beach
			#region Give Apple
			Reaction GoToBeachIntroHappy = new Reaction();
			ShowMultipartChatAction goToBeachChatIntroHappy = new ShowMultipartChatAction(this); // 4 seconds
			goToBeachChatIntroHappy.AddChat("Thanks!", 2f);
			goToBeachChatIntroHappy.AddChat("Well, I'm about to take a break on the beach.", 2f);
			goToBeachChatIntroHappy.AddChat("You two care to join me?", 2f);
			GoToBeachIntroHappy.AddAction(goToBeachChatIntroHappy);
			GoToBeachIntroHappy.AddAction(new NPCAddScheduleAction(this, goToBeachSchedule));
			flagReactions.Add(FlagStrings.oldCarpenterActivateGoToBeachHappyFlag, GoToBeachIntroHappy);
			#endregion
			#region Didn'tGiveApple
		Reaction GoToBeachIntroUpset = new Reaction();
		ShowMultipartChatAction goToBeachChatIntroUpset = new ShowMultipartChatAction(this); // 4 seconds
		goToBeachChatIntroUpset.AddChat("Ohh.. Ok.", 2f);
		goToBeachChatIntroUpset.AddChat("Well, I'm about to take a break on the beach.", 2f);
		goToBeachChatIntroUpset.AddChat("You two care to join me?", 2f);
		GoToBeachIntroUpset.AddAction(goToBeachChatIntroUpset);
		GoToBeachIntroUpset.AddAction(new NPCAddScheduleAction(this, goToBeachSchedule));
		flagReactions.Add(FlagStrings.oldCarpenterActivateGoToBeachUpsetFlag, GoToBeachIntroUpset);
			#endregion
		Reaction GoToBeachPartOne = new Reaction();
		ShowMultipartChatAction goToBeachChatPartOne = new ShowMultipartChatAction(this);
		goToBeachChatPartOne.AddChat("Ok.", 2f);
		goToBeachChatPartOne.AddChat("If you change your mind, I'll be at the beach.", 2f);
		GoToBeachPartOne.AddAction(goToBeachChatPartOne);
		GoToBeachPartOne.AddAction(new NPCAddScheduleAction(this, goToBeachSchedule));
		flagReactions.Add(FlagStrings.oldCarpenterGoToBeachPartOneFlag, GoToBeachPartOne);
		
		Reaction GoToBeachPartTwo = new Reaction();
		ShowMultipartChatAction goToBeachChatPartTwo = new ShowMultipartChatAction(this);
		goToBeachChatPartTwo.AddChat("Hey.", 2f);
		goToBeachChatPartTwo.AddChat("Glad you could join me.", 2f);
		goToBeachChatPartTwo.AddChat("It means a lot to have a friend like you here.", 2f);
		GoToBeachPartTwo.AddAction(goToBeachChatPartTwo);
		flagReactions.Add(FlagStrings.oldCarpenterGoToBeachPartTwoFlag, GoToBeachPartTwo);
		
//Have discussions about his life - I'm Glad too., Everything ok?, ???		
		
		Reaction GoToBeachPartThree = new Reaction();
		ShowMultipartChatAction goToBeachChatPartThree = new ShowMultipartChatAction(this);
		goToBeachChatPartThree.AddChat("Have you ever just looked out into the ocean", .3f);
		goToBeachChatPartThree.AddChat("Have you ever just looked out into the ocean.", .3f);
		goToBeachChatPartThree.AddChat("Have you ever just looked out into the ocean..", .3f);
		goToBeachChatPartThree.AddChat("Have you ever just looked out into the ocean...", 1.5f);
		/*
		goToBeachChatPartThree.AddChat("and ", .2f);
		goToBeachChatPartThree.AddChat("and t", .2f);
		goToBeachChatPartThree.AddChat("and th", .2f);
		goToBeachChatPartThree.AddChat("and tho", .2f);
		goToBeachChatPartThree.AddChat("and thou", .2f);
		goToBeachChatPartThree.AddChat("and thoug", .2f);
		goToBeachChatPartThree.AddChat("and though", .2f);
		goToBeachChatPartThree.AddChat("and thought", .2f);
*/		goToBeachChatPartThree.AddChat("Have you ever just looked out into the ocean..." + "\n" + " and wondered.", .5f);
		goToBeachChatPartThree.AddChat("Have you ever just looked out into the ocean..." + "\n" + " and wondered..", .5f);
		goToBeachChatPartThree.AddChat("Have you ever just looked out into the ocean..." + "\n" + " and wondered...", 2f);
		goToBeachChatPartThree.AddChat("Am I following my dreams?", 2f);
		//goToBeachChatPartThree.AddChat("Did I make a mistake?", 2f);
		GoToBeachPartThree.AddAction(goToBeachChatPartThree);
// Wait for 8 seconds or below	
		GoToBeachPartThree.AddAction(new NPCEmotionUpdateAction(this, new AnswerLivedAGoodLifeState(this, "Have you ever just wondered, "+ "\n" +"am I following my dreams?")));
		flagReactions.Add(FlagStrings.oldCarpenterGoToBeachPartThreeFlag, GoToBeachPartThree);

/*
		Reaction GoToBeachPartFour = new Reaction();
		ShowMultipartChatAction goToBeachChatPartFour = new ShowMultipartChatAction(this);
		goToBeachChatPartFour.AddChat("I always wanted to be a fisherman.", 2f);
		goToBeachChatPartFour.AddChat("I'm knew my father never would have allowed it anyways.", 2f);
		goToBeachChatPartFour.AddChat("Sometimes I wonder what my life would have been like if I had followed my dreams.", 3f);
		GoToBeachPartFour.AddAction(goToBeachChatPartFour);
		flagReactions.Add(FlagStrings.oldCarpenterGoToBeachPartFourFlag, GoToBeachPartFour);
		
		Reaction GoToBeachPartFive = new Reaction();
		ShowMultipartChatAction goToBeachChatPartFive = new ShowMultipartChatAction(this);
		goToBeachChatPartFive.AddChat("Why did I ever listen to him. ", .3f);
		goToBeachChatPartFive.AddChat("Why did I ever listen to him.. ", .3f);
		goToBeachChatPartFive.AddChat("Why did I ever listen to him... ", 2f);
		goToBeachChatPartFive.AddChat("Why didn't I believe in myself?", 2f);
		goToBeachChatPartFive.AddChat("If only I had a chance back when I was a kid.", 2f);
		goToBeachChatPartFive.AddChat("If only I had a chance back when I was a kid." +"\n" +"I", 1.5f);
		
		goToBeachChatPartFive.AddChat("If only I had a chance back when I was a kid." +"\n" +"I C", .2f);
		goToBeachChatPartFive.AddChat("If only I had a chance back when I was a kid." +"\n" +"I CO", .2f);
		goToBeachChatPartFive.AddChat("If only I had a chance back when I was a kid." +"\n" +"I COU", .2f);
		goToBeachChatPartFive.AddChat("If only I had a chance back when I was a kid." +"\n" +"I COUL", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD H", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD HA", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD HAV", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD HAVE", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD HAVE S", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD HAVE SH", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD HAVE SHO", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD HAVE SHOW", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD HAVE SHOWN", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD HAVE SHOWN H", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD HAVE SHOWN HI", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD HAVE SHOWN HIM", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD HAVE SHOWN HIM!", 2f);
		/*
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I Could", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COuld", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COUld", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULd", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD Have", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD HAve", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD HAVe", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD HAVE", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD HAVE Shown", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD HAVE SHown", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD HAVE SHOwn", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD HAVE SHOWn", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD HAVE SHOWN", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD HAVE SHOWN Him", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD HAVE SHOWN HIm", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD HAVE SHOWN HIM", .2f);
		goToBeachChatPartFive.AddChat("If only I had a pole back when I was a kid." +"\n" +"I COULD HAVE SHOWN HIM!", 2f);
		
		GoToBeachPartFive.AddAction(goToBeachChatPartFive);
		flagReactions.Add(FlagStrings.oldCarpenterGoToBeachPartFiveFlag, GoToBeachPartFive);
		
		Reaction GoToBeachPartSix = new Reaction();
		ShowMultipartChatAction goToBeachChatPartSix = new ShowMultipartChatAction(this);
		goToBeachChatPartSix.AddChat("", 2f);
		goToBeachChatPartSix.AddChat("Work as usual..", 2f);
		goToBeachChatPartSix.AddChat("Brought an apple you say?", 2f);
		GoToBeachPartSix.AddAction(goToBeachChatPartSix);
		//GoToBeachPartSix.AddAction(new NPCAddScheduleAction(this, goToBeachSchedule));
		flagReactions.Add(FlagStrings.oldCarpenterGoToBeachPartSixFlag, GoToBeachPartSix);
	*/	
		#endregion
		
		Reaction reconcileReaction = new Reaction();
		reconcileReaction.AddAction(new NPCTeleportToAction(this, MapLocations.BaseOfPierOld));
		reconcileReaction.AddAction(new UpdateDefaultTextAction(this, "Best wind in years today, and with my father with me I'm sure I'll get my best catch yet."));
		flagReactions.Add(FlagStrings.carpenterSonReconcile, reconcileReaction);	
	}
	 	
	protected override EmotionState GetInitEmotionState(){
		//return (new InitialEmotionState(this, "My back aches, my arms are tired. I wish I never got into this lousy carpentry business."));
		return (new InitialEmotionState(this, "Hey. I'm busy at work." + "\n" + "Got anything you need from me?"));
	}
	
	protected override Schedule GetSchedule() {
		//Schedule schedule = new DefaultSchedule(this);
		//return (schedule);
		Schedule schedule = new Schedule(this, Schedule.priorityEnum.Low); 
		schedule.Add(new TimeTask(120f, new IdleState(this)));
		return (schedule);
	}

	#region Carpenter Old Schedules
	private Schedule greetSiblingOldSchedule;
	private Schedule goToBeachSchedule;
	#endregion
	protected override void SetUpSchedules() {
		greetSiblingOldSchedule = (new CarpenterSonOldGreetSibllingSchedule(this));
		goToBeachSchedule = (new CarpenterSonOldToBeachScript(this));
		
	}
	
	#region EmotionStates
	#region Initial Emotion State
	private class InitialEmotionState : EmotionState{
		//return (new InitialEmotionState(this, "Hey. I'm busy at work." + "\n" + "Got anything you need from me?"));
		Reaction aReaction = new Reaction();
		Reaction bReaction = new Reaction();
		Reaction cReaction = new Reaction();
		Reaction dReaction = new Reaction();
		
		Choice aChoice = new Choice("a", "hey");
		Choice bChoice = new Choice("b", "hey");
		Choice cChoice = new Choice("c", "hey");
		Choice dChoice = new Choice("d", "hey");
		
		
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){		
			//aReaction.AddAction();
			//bReaction.AddAction();
			
			
			_allChoiceReactions.Add(aChoice, new DispositionDependentReaction(aReaction));
			_allChoiceReactions.Add(bChoice, new DispositionDependentReaction(bReaction));
		}
		
		public override void UpdateEmotionState(){
			
		}
	}
	#endregion
//left is top
//middle is middle
//right is bottom
	private class AnswerLivedAGoodLifeState : EmotionState {
		Reaction choiceLeftReaction = new Reaction();
		Reaction choiceCenterReaction = new Reaction();
		Reaction choiceRightReaction = new Reaction();
		
		//CarpenterSon.SetCharacterPortrait(StringsNPC.Fish + StringsNPC.Angry);
		//NPC.SetCharacterPortrait([string]);
		
		public void RefreshOptions() {
			_allChoiceReactions.Clear();
			choiceLeftReaction = new Reaction();
			choiceRightReaction = new Reaction();	
			choiceCenterReaction = new Reaction();	
		}
		
		public AnswerLivedAGoodLifeState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue) {	
			toControl.SetCharacterPortrait(StringsNPC.CarpenterSonOld + StringsNPC.Sad);
			choiceLeftReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatPartOne,toControl));
			choiceLeftReaction.AddAction(new ShowOneOffChatAction(toControl, "Then you understand." + "\n" + "I feel like I lived my father's dream and not my own.",2f));
			choiceLeftReaction.AddAction(new NPCCallbackOnNPCAction(setPortraitSad, toControl));
			choiceRightReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatPartOne,toControl));
			choiceRightReaction.AddAction(new ShowOneOffChatAction(toControl,"Well I definitely have." + "\n" + "I feel like I lived my father's dream and not my own.",2f));
			choiceRightReaction.AddAction(new NPCCallbackOnNPCAction(setPortraitSad,toControl));
			_allChoiceReactions.Add(new Choice(" I have before.","Then you understand." + "\n" + "I feel like I lived my father's dream and not my own."), new DispositionDependentReaction(choiceLeftReaction));
			_allChoiceReactions.Add(new Choice("I haven't before.","Well I definitely have." + "\n" + "I feel like I lived my father's dream and not my own."), new DispositionDependentReaction(choiceRightReaction));
		}
		
		public void setPortraitAngry(NPC toControl) {
			toControl.SetCharacterPortrait(StringsNPC.CarpenterSonOld + StringsNPC.Angry);	
		}
		
		public void setPortraitHappy(NPC toControl) {
			toControl.SetCharacterPortrait(StringsNPC.CarpenterSonOld + StringsNPC.Happy);	
		}
		
		public void setPortraitNormal(NPC toControl) {
			toControl.SetCharacterPortrait(StringsNPC.CarpenterSonOld);	
		}
		
		public void setPortraitSad(NPC toControl) {
			toControl.SetCharacterPortrait(StringsNPC.CarpenterSonOld + StringsNPC.Sad);	
		}
		
		public void setPortraitSmile(NPC toControl) {
			toControl.SetCharacterPortrait(StringsNPC.CarpenterSonOld + StringsNPC.Smile);	
		}
		
		public void setPortraitYelling(NPC toControl) {
			toControl.SetCharacterPortrait(StringsNPC.CarpenterSonOld + StringsNPC.Yelling);	
		}
		
		public void ContinueCarpenterSonChatPartOne(NPC toControl) {
			RefreshOptions();
			choiceLeftReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatPartTwoNeutral, toControl));
			choiceLeftReaction.AddAction(new ShowOneOffChatAction(toControl, "I never really wanted to be a Carpenter",2f));
			choiceCenterReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatPartTwoAngry, toControl));
			choiceCenterReaction.AddAction(new ShowOneOffChatAction(toControl, "No! I NEVER wanted to be a Carpenter.",2f));
			choiceRightReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatPartTwoApologetic,toControl));
			choiceRightReaction.AddAction(new ShowOneOffChatAction(toControl, "It's ok."+ "\n" + "Sometimes we don't think about it till it's too late.",2f));
			_allChoiceReactions.Add(new Choice("Why do you say that?","I never really wanted to be a Carpenter."), new DispositionDependentReaction(choiceLeftReaction));
			_allChoiceReactions.Add(new Choice("Maybe you did?","No! I NEVER wanted to be a Carpenter."), new DispositionDependentReaction(choiceCenterReaction));
			_allChoiceReactions.Add(new Choice("I'm sorry.","It's ok."+ "\n" + "Sometimes we don't think about it till it's too late."), new DispositionDependentReaction(choiceRightReaction));
		
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void ContinueCarpenterSonChatPartTwoNeutral(NPC toControl) {
			RefreshOptions();
			choiceLeftReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatPartThreeFisherman, toControl));
			choiceLeftReaction.AddAction(new ShowOneOffChatAction(toControl, "I wanted to be a fisherman."+ "\n" + "I have the passion for it.",2f));
			choiceLeftReaction.AddAction(new NPCCallbackOnNPCAction(setPortraitSmile,toControl));
			choiceRightReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatPartThreeDream, toControl));
			choiceRightReaction.AddAction(new NPCCallbackOnNPCAction(setPortraitSad,toControl));
			choiceRightReaction.AddAction(new ShowOneOffChatAction(toControl, "The family tradition is a big burden and it just wasn't my dream.",2f));
			_allChoiceReactions.Add(new Choice("What did you want to be?","I wanted to be a fisherman."+ "\n" + "I have the passion for it."), new DispositionDependentReaction(choiceLeftReaction));
			_allChoiceReactions.Add(new Choice("Why not?","The family legacy is a big burden and it just wasn't my dream."), new DispositionDependentReaction(choiceRightReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void ContinueCarpenterSonChatPartTwoAngry(NPC toControl) {
			RefreshOptions();
			choiceLeftReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatPartThreeRegret, toControl));
			choiceLeftReaction.AddAction(new ShowOneOffChatAction(toControl,"Sorry, I didn't mean to yell."+ "\n" + "I just regret that I never did anything to stop him.",2f));
			choiceLeftReaction.AddAction(new NPCCallbackOnNPCAction(setPortraitNormal,toControl));
			choiceCenterReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatPartThreeUpset, toControl));
			choiceCenterReaction.AddAction(new ShowOneOffChatAction(toControl,"Don't remind me." + "\n" +"I've been sorry for myself enough over the years.",2f));
			choiceCenterReaction.AddAction(new NPCCallbackOnNPCAction(setPortraitAngry,toControl));
			choiceRightReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatPartThreeRegret, toControl));
			choiceRightReaction.AddAction(new ShowOneOffChatAction(toControl, "I'm sorry for getting worked up." + "\n" + "It's just difficult thinking you've lived a life that wasn't yours.",3f));
			choiceRightReaction.AddAction(new NPCCallbackOnNPCAction(setPortraitSad,toControl));
			
			_allChoiceReactions.Add(new Choice("Don't get so defensive.","Sorry, I didn't mean to yell."+ "\n" + "I just regret that I never did anything to stop him."), new DispositionDependentReaction(choiceLeftReaction));
			_allChoiceReactions.Add(new Choice("My apologies.","Don't remind me." + "\n" +"I've been sorry for myself enough over the years."), new DispositionDependentReaction(choiceCenterReaction));
			_allChoiceReactions.Add(new Choice("...","I'm sorry for getting worked up." + "\n" + "It's just difficult thinking you've lived a life that wasn't yours."), new DispositionDependentReaction(choiceRightReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void ContinueCarpenterSonChatPartTwoApologetic(NPC toControl) {
			RefreshOptions();
			choiceLeftReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatPartThreeFisherman, toControl));
			choiceLeftReaction.AddAction(new ShowOneOffChatAction(toControl, "I wanted to be a fisherman." + "\n" + "I love fish and the sea.",2f));
			choiceRightReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatPartThreeRegret, toControl));
			choiceRightReaction.AddAction(new ShowOneOffChatAction(toControl, "Ya, I'm ok." + "\n" + "I just regret that I never took a chance.",2f));
			_allChoiceReactions.Add(new Choice("What did you want to be?","I wanted to be a fisherman." + "\n" + "I love fish and the sea."), new DispositionDependentReaction(choiceLeftReaction));
			_allChoiceReactions.Add(new Choice("Are you ok?","Ya, I'm ok." + "\n" + "I just regret that I never took a chance."), new DispositionDependentReaction(choiceRightReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void ContinueCarpenterSonChatPartThreeDream(NPC toControl) {
			RefreshOptions();
			choiceLeftReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatPartThreeFisherman, toControl));
			choiceLeftReaction.AddAction(new ShowOneOffChatAction(toControl, "I dreamed of being be a fisherman. A legendary Angler." + "\n" + "I wanted to be known all across the sea.",3f));
			choiceRightReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatPartFourFamily, toControl));
			choiceRightReaction.AddAction(new ShowOneOffChatAction(toControl, "I know family is important, but..",2f));
			_allChoiceReactions.Add(new Choice("What was your dream?","I dreamed of being be a fisherman. A legendary Angler." + "\n" + "I wanted to be known all across the sea."), new DispositionDependentReaction(choiceLeftReaction));
			_allChoiceReactions.Add(new Choice("What about the legacy?","I know family is important, but.." + "\n" + "Sometimes you just know it's not for you."), new DispositionDependentReaction(choiceRightReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void ContinueCarpenterSonChatPartThreeUpset(NPC toControl) {
			RefreshOptions();
			choiceLeftReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatPartFourAngryRegret, toControl));
			choiceLeftReaction.AddAction(new ShowOneOffChatAction(toControl, "Are you just saying that to comfort me?" + "I'm living a life I regret.",2f));
			choiceRightReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatPartThreeRegret, toControl));
			choiceRightReaction.AddAction(new ShowOneOffChatAction(toControl, "I'm sorry." + "\n" + "I just wish I faced my fears and followed me dream.",2f));
			_allChoiceReactions.Add(new Choice("But you are good enough.","Are you just saying that to comfort me?" + "I'm living a life I regret."), new DispositionDependentReaction(choiceLeftReaction));
			_allChoiceReactions.Add(new Choice("I didn't mean it like that.","I'm sorry." + "\n" + "I just wish I faced my fears and followed me dream."), new DispositionDependentReaction(choiceRightReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void ContinueCarpenterSonChatPartThreeRegret(NPC toControl) {
			RefreshOptions();
			choiceLeftReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatPartFourChance, toControl));
			choiceLeftReaction.AddAction(new ShowOneOffChatAction(toControl, "Thanks." + "\n" + "But I think it's too late. I'm too old now.",2f));
			choiceRightReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatPartFourLearn, toControl));
			choiceRightReaction.AddAction(new ShowOneOffChatAction(toControl, "I did learn carpentry, but it wasn't satisfying.",2f));
			_allChoiceReactions.Add(new Choice("It's not too late!","Thanks." + "\n" + "But I think it's too late. I'm too old now."), new DispositionDependentReaction(choiceLeftReaction));
			_allChoiceReactions.Add(new Choice("Did you learn though?","Not really." +"\n" + "Most of what I did was a waste of time."), new DispositionDependentReaction(choiceRightReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void ContinueCarpenterSonChatPartThreeFisherman(NPC toControl) {
			RefreshOptions();
			choiceLeftReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatPartFourDream, toControl));
			choiceLeftReaction.AddAction(new ShowOneOffChatAction(toControl, "Thanks." + "\n" + "I just wish I could have lived my dream.",2f));
			choiceCenterReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatPartFourChance, toControl));
			choiceCenterReaction.AddAction(new ShowOneOffChatAction(toControl, "It's a bit late now, don't you think?",2f));
			choiceRightReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatPartFourInsult, toControl));
			choiceRightReaction.AddAction(new ShowOneOffChatAction(toControl, "You really think so?" + "\n" + "I thought you of all people would be a supporter..",2f));
			_allChoiceReactions.Add(new Choice("That's a cool dream.","Thanks." + "\n" + "I just wish I could have lived my dream."), new DispositionDependentReaction(choiceLeftReaction));
			_allChoiceReactions.Add(new Choice("Why don't you start now?","It's a bit late now, don't you think?"), new DispositionDependentReaction(choiceCenterReaction));
			_allChoiceReactions.Add(new Choice("Fishing is boring.","You really think so?" + "\n" + "I thought you of all people would be a supporter.."), new DispositionDependentReaction(choiceRightReaction));
			GUIManager.Instance.RefreshInteraction();
		}

		public void ContinueCarpenterSonChatPartFourFamily(NPC toControl) {
			RefreshOptions();
			choiceLeftReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatPartFiveFather, toControl));
			choiceLeftReaction.AddAction(new ShowOneOffChatAction(toControl, "He's not worth the effort." + "\n" + "He never believed in me.",2f));
			choiceRightReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatCompleteSupported, toControl));
			choiceRightReaction.AddAction(new ShowOneOffChatAction(toControl, "Thanks for listening." +"\n" + "I know I can be tough to listen to.",2f));
			_allChoiceReactions.Add(new Choice("What about your father?","He's not worth the effort." + "\n" + "He never believed in me."), new DispositionDependentReaction(choiceLeftReaction));
			_allChoiceReactions.Add(new Choice("I understand.","Thanks for listening." +"\n" + "I know I can be tough to listen to."), new DispositionDependentReaction(choiceRightReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void ContinueCarpenterSonChatPartFourAngryRegret(NPC toControl) {
			RefreshOptions();
			choiceLeftReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatPartFiveUnderstanding, toControl));
			choiceLeftReaction.AddAction(new ShowOneOffChatAction(toControl, "Are you just saying that to comfort me?" + "I'm living a life I regret.",2f));
			choiceRightReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatCompleteRegret, toControl));
			choiceRightReaction.AddAction(new ShowOneOffChatAction(toControl, "I'm sorry, I thought you were mocking me." + "\n" + "If only I could go back..",2f));
			_allChoiceReactions.Add(new Choice("I want to help is all.","I appreciate your concerns, but I don't know if you understand."), new DispositionDependentReaction(choiceLeftReaction));
			_allChoiceReactions.Add(new Choice("No I really mean it!","I'm sorry, I thought you were mocking me." + "\n" + "If only I could go back.."), new DispositionDependentReaction(choiceRightReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void ContinueCarpenterSonChatPartFourLearn(NPC toControl) {
			RefreshOptions();
			choiceLeftReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatPartFourChance, toControl));
			choiceLeftReaction.AddAction(new ShowOneOffChatAction(toControl, "Thanks." + "\n" + "But I think it's too late. I'm too old now.",2f));
			choiceCenterReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatCompleteWasted, toControl));
			choiceCenterReaction.AddAction(new ShowOneOffChatAction(toControl, "Yes. It was a life wasted." +"\n" + "But hey, thanks for listening.",2f));
			choiceRightReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatPartFiveFather, toControl));
			choiceRightReaction.AddAction(new ShowOneOffChatAction(toControl, "I did learn carpentry, but it wasn't satisfying.",2f));
			_allChoiceReactions.Add(new Choice("What about Carpentry?","Thanks." + "\n" + "But I think it's too late. I'm too old now."), new DispositionDependentReaction(choiceLeftReaction));
			_allChoiceReactions.Add(new Choice("Was it really?","Yes. It was a life wasted." +"\n" + "But hey, thanks for listening."), new DispositionDependentReaction(choiceCenterReaction));
			_allChoiceReactions.Add(new Choice("What about from your father?","No, we had nothing in common."), new DispositionDependentReaction(choiceRightReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void ContinueCarpenterSonChatPartFourDream(NPC toControl) {
			RefreshOptions();
			choiceLeftReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatCompleteSupported, toControl));
			choiceLeftReaction.AddAction(new ShowOneOffChatAction(toControl,"A pole would be nice!"+ "\n" + "Haha, I'm kidding. Thanks though.",2f));
			choiceRightReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatCompleteSupported, toControl));
			choiceRightReaction.AddAction(new ShowOneOffChatAction(toControl, "Haha! I might be too old to reel one in anyways." + "\n" +"Thanks for the support though!",2f));
			_allChoiceReactions.Add(new Choice("Can I help?","A pole would be nice!"+ "\n" + "Haha, I'm kidding. Thanks though."), new DispositionDependentReaction(choiceLeftReaction));
			_allChoiceReactions.Add(new Choice("You can!","Haha! I might be too old to reel one in anyways." + "/n" + "Thanks for the support though!"), new DispositionDependentReaction(choiceRightReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void ContinueCarpenterSonChatPartFourChance(NPC toControl) {
			RefreshOptions();
			choiceLeftReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatCompleteRegret, toControl));
			choiceLeftReaction.AddAction(new ShowOneOffChatAction(toControl, "Oh well.." + "\n" + "Maybe in my next life.",2f));
			choiceRightReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatCompleteSupported, toControl));
			choiceRightReaction.AddAction(new ShowOneOffChatAction(toControl, "Hmm. maybe you're right." +"\n" + "Thanks for chatting with me.",2f));
			_allChoiceReactions.Add(new Choice("Maybe you're right.","Oh well.." + "\n" + "Maybe in my next life."), new DispositionDependentReaction(choiceLeftReaction));
			_allChoiceReactions.Add(new Choice("It's never too late!","Hmm. maybe you're right." +"\n" + "Thanks for chatting with me."), new DispositionDependentReaction(choiceRightReaction));
			GUIManager.Instance.RefreshInteraction();
		}
				
		public void ContinueCarpenterSonChatPartFourInsult(NPC toControl) {
			RefreshOptions();
			choiceLeftReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatCompleteSecretDefeated, toControl));
			choiceLeftReaction.AddAction(new ShowOneOffChatAction(toControl, "Maybe you're right." + "\n" + "Guess I won't need this anymore.",2f));
			choiceRightReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatCompleteSupported, toControl));
			choiceRightReaction.AddAction(new ShowOneOffChatAction(toControl, "As much as I don't agree, I appreciate your honesty." + "\n" + "thanks.",2f));
			_allChoiceReactions.Add(new Choice("Fishing's not cool.","Maybe you're right." + "\n" + "Guess I won't need this anymore."), new DispositionDependentReaction(choiceLeftReaction));
			_allChoiceReactions.Add(new Choice("I'm just being honest.","As much as I don't agree, I appreciate your honesty." + "\n" + "thanks."), new DispositionDependentReaction(choiceRightReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void ContinueCarpenterSonChatPartFiveFather(NPC toControl) {
			RefreshOptions();
			choiceLeftReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatCompleteRegret, toControl));
			choiceCenterReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatCompleteAngry, toControl));
			choiceRightReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatCompleteSupported, toControl));
			_allChoiceReactions.Add(new Choice("Are you sure?","Yes. He never gave me much support" + "\n" + "Life moves on.."), new DispositionDependentReaction(choiceLeftReaction));
			_allChoiceReactions.Add(new Choice("He loved you.","How would you know? He wasn't your father."), new DispositionDependentReaction(choiceCenterReaction));
			_allChoiceReactions.Add(new Choice("Sounds tough.","Yes. It truly was." + "\n " + "Thanks for understanding."), new DispositionDependentReaction(choiceRightReaction));	
			GUIManager.Instance.RefreshInteraction();
		}				
		
		public void ContinueCarpenterSonChatPartFiveUnderstanding(NPC toControl) {
			RefreshOptions();
			choiceLeftReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatCompleteSupported, toControl));
			choiceLeftReaction.AddAction(new ShowOneOffChatAction(toControl, "Are you just saying that to comfort me?" + "I'm living a life I regret.",2f));
			choiceRightReaction.AddAction(new NPCCallbackOnNPCAction(ContinueCarpenterSonChatCompleteRegret, toControl));
			choiceRightReaction.AddAction(new ShowOneOffChatAction(toControl, "It's ok, this is my own battle." + "I'll figure it out one day..",2f));
			_allChoiceReactions.Add(new Choice("I truly understand.","I'm glad. It's nice to have someone to relate to. " + "\n" + "thanks for listening."), new DispositionDependentReaction(choiceLeftReaction));
			_allChoiceReactions.Add(new Choice("I'm not sure.","It's ok, this is my own battle." + "I'll figure it out one day.."), new DispositionDependentReaction(choiceRightReaction));
			GUIManager.Instance.RefreshInteraction();
		}
				
		public void ContinueCarpenterSonChatCompleteSupported(NPC toControl) {
			RefreshOptions();
			SetDefaultText("Supported =]");
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void ContinueCarpenterSonChatCompleteWasted(NPC toControl) {
			RefreshOptions();	
			SetDefaultText("Wasted =/");
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void ContinueCarpenterSonChatCompleteAngry(NPC toControl) {
			RefreshOptions();	
			SetDefaultText("Angry >=[");
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void ContinueCarpenterSonChatCompleteRegret(NPC toControl) {
			RefreshOptions();	
			SetDefaultText("Regret =[");
			GUIManager.Instance.RefreshInteraction();
		}
	
		public void ContinueCarpenterSonChatCompleteSecretDefeated(NPC toControl) {
			RefreshOptions();	
			SetDefaultText("Secret Defeated =)");
			GUIManager.Instance.RefreshInteraction();
		}

	
	}
		
	private class WantAppleState : EmotionState {
		Reaction didntGiveApple = new Reaction();
		Reaction giveApple = new Reaction();
		public WantAppleState(NPC toControl, string currentDialogue) : base (toControl, currentDialogue) {
			didntGiveApple.AddAction(new NPCCallbackAction(DidntGiveApple));
			didntGiveApple.AddAction(new NPCEmotionUpdateAction(toControl, new GaveAppleState(toControl, "hey!")));
			
			giveApple.AddAction(new NPCCallbackAction(GiveApple));
			giveApple.AddAction(new NPCTakeItemAction(toControl));
			giveApple.AddAction(new NPCEmotionUpdateAction(toControl, new GaveAppleState(toControl, "hey!")));
			
			_allItemReactions.Add(StringsItem.Apple, new DispositionDependentReaction(giveApple));
			_allChoiceReactions.Add(new Choice ("I don't", "Ohh ok.."), new DispositionDependentReaction(didntGiveApple));
		}
		public void GiveApple() {
			SetDefaultText("Hey, thanks!");
			GUIManager.Instance.RefreshInteraction();
			GUIManager.Instance.CloseInteractionMenu();
			FlagManager.instance.SetFlag(FlagStrings.oldCarpenterActivateGoToBeachHappyFlag);
		}
		
		public void DidntGiveApple() {
			SetDefaultText("Ohh.. Ok.");
			GUIManager.Instance.CloseInteractionMenu();
			FlagManager.instance.SetFlag(FlagStrings.oldCarpenterActivateGoToBeachUpsetFlag);
		}
	}
		
	private class GaveAppleState : EmotionState {
		public GaveAppleState(NPC toControl, string currentDialogue) : base (toControl, currentDialogue) {
			//_allItemReactions.Add("apple", 
			
		}
	}					
	#endregion
}
