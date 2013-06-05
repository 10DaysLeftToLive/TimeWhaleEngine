using UnityEngine;
using System.Collections;



/// <summary>
/// SiblingOld specific scripting values
/// </summary>
public class SiblingOld : NPC {
	private static readonly float Y_COORDINATE = -1.735313f + (LevelManager.levelYOffSetFromCenter*2);
	
	protected override void Init() {
		id = NPCIDs.SIBLING;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		#region FirstPassiveChat
		Reaction gameStartPassiveChat = new Reaction();
//Emotion State shouldn't say the text: Care to talk with the Carpenter's Son?" + "\n\n" + "Let me know when you're ready to continue.
		gameStartPassiveChat.AddAction(new NPCEmotionUpdateAction(this, new InitialEmotionState(this, "Care to talk with the Carpenter's Son?" + "\n\n" + "Let me know when you're ready to continue.")));
		#region reactivate after debugging <&^&>
		/*
		ShowMultipartChatAction introMultChatPartOne = new ShowMultipartChatAction(this);
		introMultChatPartOne.AddChat("Hey!", 2f);
		introMultChatPartOne.AddChat("Feeling well today?", 2f);
		introMultChatPartOne.AddChat("You up for a race like old times?", 2.5f);
		gameStartPassiveChat.AddAction(introMultChatPartOne);
		*/
		#endregion
		gameStartPassiveChat.AddAction(new NPCTeleportToAction(this, new Vector3(30f, -1.6f + (LevelManager.levelYOffSetFromCenter * 2),0)));
		//gameStartPassiveChat.AddAction(new NPCAddScheduleAction(this, oldSiblingIntroductionSchedule));
		gameStartPassiveChat.AddAction(new NPCAddScheduleAction(this, walkToCarpenterSchedule));
		flagReactions.Add(FlagStrings.oldSiblingIntroChatFlag, gameStartPassiveChat);
		#endregion
		#region Race To Forest
		Reaction RaceToForestPartOne = new Reaction();
		ShowMultipartChatAction beginRaceToForestPartOne = new ShowMultipartChatAction(this);
		beginRaceToForestPartOne.AddChat("Ready?", 2f);
		beginRaceToForestPartOne.AddChat("Set!", 1.5f);
		RaceToForestPartOne.AddAction(beginRaceToForestPartOne);
		flagReactions.Add(FlagStrings.siblingOldIntroRaceChatPartOneFlag, RaceToForestPartOne);
		
		Reaction RaceToForestPartTwo = new Reaction();
		ShowMultipartChatAction beginRaceToForestPartTwo = new ShowMultipartChatAction(this);
		beginRaceToForestPartTwo.AddChat("Go!!", 3f);
		beginRaceToForestPartTwo.AddChat("Hurry up slowpoke!", 2f);
		beginRaceToForestPartTwo.AddChat("Hahahaha!", 2f);
		RaceToForestPartTwo.AddAction(beginRaceToForestPartTwo);
		flagReactions.Add(FlagStrings.siblingOldIntroRaceChatPartTwoFlag, RaceToForestPartTwo);
		
		Reaction RaceToForestPartThree = new Reaction();
		ShowMultipartChatAction beginRaceToForestPartThree = new ShowMultipartChatAction(this);
		beginRaceToForestPartThree.AddChat("Whew!", 2f);
		beginRaceToForestPartThree.AddChat("Good race!", 3f);
		beginRaceToForestPartThree.AddChat("It's been a while, hasn't it?", 3f);
//Mom's garden?		//beginRaceToForestPartThree.AddChat("Aww, we never got to plant mom's garden.,2f);
		RaceToForestPartThree.AddAction(beginRaceToForestPartThree);
		flagReactions.Add(FlagStrings.siblingOldIntroRaceChatPartThreeFlag, RaceToForestPartThree);
		
		Reaction RaceToForestPartFour = new Reaction();
		ShowMultipartChatAction beginRaceToForestPartFour = new ShowMultipartChatAction(this);
		beginRaceToForestPartFour.AddChat("Mom's garden isn't looking so good.", 3f);
		beginRaceToForestPartFour.AddChat("Too bad we weren't able to help before she passed.", 3f);
		beginRaceToForestPartFour.AddChat("Oh well...", 2.5f);
		beginRaceToForestPartFour.AddChat("If only we could go back, right?", 3f);
		RaceToForestPartFour.AddAction(beginRaceToForestPartFour);
		flagReactions.Add(FlagStrings.siblingOldIntroRaceChatPartFourFlag, RaceToForestPartFour);
		
		Reaction RaceToForestPartFive = new Reaction();
		ShowMultipartChatAction beginRaceToForestPartFive = new ShowMultipartChatAction(this);
		beginRaceToForestPartFive.AddChat("Hey.", 2f);
		beginRaceToForestPartFive.AddChat("Want to go visit ole Carp?", 3f);
		beginRaceToForestPartFive.AddChat("I'm sure he'd be happy to have visitors.", 3f);
		beginRaceToForestPartFive.AddChat("Race you back!", 3.5f);
		beginRaceToForestPartFive.AddChat("Hahahaha!", 2.5f);
		RaceToForestPartFive.AddAction(beginRaceToForestPartFive);
		flagReactions.Add(FlagStrings.siblingOldIntroRaceChatPartFiveFlag, RaceToForestPartFive);
		
		Reaction RaceToForestPartSix = new Reaction();
		ShowMultipartChatAction beginRaceToForestPartSix = new ShowMultipartChatAction(this);
		beginRaceToForestPartSix.AddChat("Let's go to the bridge!", 1.5f);
		RaceToForestPartSix.AddAction(beginRaceToForestPartSix);
		RaceToForestPartSix.AddAction(new NPCAddScheduleAction(this, walkToCarpenterSchedule));
		flagReactions.Add(FlagStrings.siblingOldIntroRaceChatPartSixFlag, RaceToForestPartSix);
	
		#endregion
		#region To Carpenter House	
		
		Reaction IntroStoryPartOne = new Reaction();
		ShowMultipartChatAction walkToCarpenterPartOne = new ShowMultipartChatAction(this);
		walkToCarpenterPartOne.AddChat("Hey look! An apple!", 1.5f);
		walkToCarpenterPartOne.AddChat("Let's take it to Carp.", 2f);
		walkToCarpenterPartOne.AddChat("He loves apples!", 1.5f);
		IntroStoryPartOne.AddAction(walkToCarpenterPartOne);
		flagReactions.Add(FlagStrings.oldSiblingIntroStoryOnePartOneFlag, IntroStoryPartOne);
		
		Reaction IntroStoryPartTwo = new Reaction();
		//IntroStoryPartTwo.AddAction(new ShowOneOffChatAction(this, "Remember the apple trees that were once here?", 5f));
		IntroStoryPartTwo.AddAction(new ShowOneOffChatAction(this, "Remember the apple trees that once were here?", 5f));
		flagReactions.Add(FlagStrings.oldSiblingIntroStoryOnePartTwoFlag, IntroStoryPartTwo);
		
		Reaction ReachCarpenterHouse = new Reaction();
		ShowMultipartChatAction reachedCarpenterDialogue = new ShowMultipartChatAction(this);
		reachedCarpenterDialogue.AddChat("Hey Carp!", 2f);
		reachedCarpenterDialogue.AddChat("Doing well today?", 2f);
		reachedCarpenterDialogue.AddChat("We brought you an apple!", 2f);
		ReachCarpenterHouse.AddAction(reachedCarpenterDialogue);
		flagReactions.Add(FlagStrings.siblingOldReachedCarpenterSonFlag, ReachCarpenterHouse);
		
		Reaction GreetCarpenterPartOne = new Reaction();
		ShowMultipartChatAction greetCarpenterPartOneChat = new ShowMultipartChatAction(this);
		greetCarpenterPartOneChat.AddChat("Ya!", 1f);
		greetCarpenterPartOneChat.AddChat("Hey, give it to him!", 2f);
		GreetCarpenterPartOne.AddAction(greetCarpenterPartOneChat);
		flagReactions.Add(FlagStrings.siblingOldGreetCarpenterSonOldPartOneFlag, GreetCarpenterPartOne);
		
		Reaction appleGivenReaction = new Reaction();
		appleGivenReaction.AddAction(new NPCAddScheduleAction(this, oldSiblingtoFortuneTellerSchedule));	
		flagReactions.Add(FlagStrings.oldCarpenterActivateGoToBeachHappyFlag, appleGivenReaction);
		
		Reaction appleNotGivenReaction = new Reaction();
		appleNotGivenReaction.AddAction(new NPCAddScheduleAction(this, oldSiblingtoFortuneTellerSchedule));	
		flagReactions.Add(FlagStrings.oldCarpenterActivateGoToBeachUpsetFlag, appleNotGivenReaction);
		
		#endregion
		
		#region
		Reaction goToFortuneTellerIntroReaction = new Reaction();
		ShowMultipartChatAction goToFortuneTellerIntroChat = new ShowMultipartChatAction(this); // 9 seconds
		goToFortuneTellerIntroChat.AddChat("Um..", 1f);
		goToFortuneTellerIntroChat.AddChat("We're going to pass.", 2f);
		goToFortuneTellerIntroChat.AddChat("We're getting our fortunes upstairs.", 1.5f);
//Add context to the going upstairs, make the choice more meaningful (can't see the conversation from both sides.
//Pause
		goToFortuneTellerIntroChat.AddChat("Have fun though!", 2f);
		goToFortuneTellerIntroChat.AddChat("See you later!", 3f);
		goToFortuneTellerIntroReaction.AddAction(goToFortuneTellerIntroChat);
		flagReactions.Add(FlagStrings.siblingOldGoToFortuneTellerIntro, goToFortuneTellerIntroReaction);
		#endregion
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Do you remember the old times and all our memories together? I feel like we were just kids not too long ago..."));
	}
	
	protected override Schedule GetSchedule(){	
		#region reactivate after debugging <&^&>
		/*
		Schedule schedule = new Schedule(this, Schedule.priorityEnum.DoNow); 
		schedule.Add(new TimeTask(1.25f, new IdleState(this)));
		Task oldSiblingMoveToBridge = new Task(new MoveThenDoState(this, new Vector3(2f,Y_COORDINATE, .3f), new MarkTaskDone(this)));
		oldSiblingMoveToBridge.AddFlagToSet(FlagStrings.oldSiblingIntroChatFlag);
		schedule.Add(oldSiblingMoveToBridge);
		schedule.Add(new TimeTask(5.5f, new IdleState(this)));
		*/
		#endregion
		Schedule schedule = new Schedule(this, Schedule.priorityEnum.DoNow); 
		schedule.Add(new TimeTask(1.25f, new IdleState(this)));
		Task oldSiblingMoveToBridge = new Task(new MoveThenDoState(this, new Vector3(2f,Y_COORDINATE, .3f), new MarkTaskDone(this)));
		oldSiblingMoveToBridge.AddFlagToSet(FlagStrings.oldSiblingIntroChatFlag);
		schedule.Add(oldSiblingMoveToBridge);
		schedule.Add(new TimeTask(.25f, new IdleState(this)));
		return (schedule);
	}
	
	#region Schedule List
	private Schedule oldSiblingIntroductionSchedule;
	private Schedule oldSiblingtoFortuneTellerSchedule;
	private Schedule walkToCarpenterSchedule;
	#endregion
	
	protected override void SetUpSchedules() {
		oldSiblingIntroductionSchedule = (new SiblingOldRaceToForestSchedule(this));
		walkToCarpenterSchedule = (new SiblingOldWalkToCarpenterSchedule(this));
		oldSiblingtoFortuneTellerSchedule = (new SiblingOldGreetCarpenterSonSchedule(this));
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
