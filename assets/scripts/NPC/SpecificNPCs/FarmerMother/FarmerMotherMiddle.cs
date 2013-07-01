using UnityEngine;
using System.Collections;

/// <summary>
/// FarmerMotherMiddle specific scripting values
/// </summary>
public class FarmerMotherMiddle : NPC {	
	InitialEmotionState initialState;
	Vector3 startingPosition;
	int disposition = 50;
	bool flagSet = false;
	bool husbandReady = false;
	bool carpenterDate = true;
	bool farmersOnBoard = false;
	
	protected void setAngry(){
		this.SetCharacterPortrait(StringsNPC.Angry);
	}
	protected void setSad(){
		this.SetCharacterPortrait(StringsNPC.Sad);
	}
	protected void setDefault(){
		this.SetCharacterPortrait(StringsNPC.Default);
	}
	protected void setHappy(){
		this.SetCharacterPortrait(StringsNPC.Happy);	
	}
	
	#region ReactionInstantiate
	Reaction moveAway = new Reaction();
	Reaction notSilly = new Reaction();
	Reaction tellOn = new Reaction();
	Reaction workandRead = new Reaction();
	Reaction sillyStories = new Reaction();
	Reaction yourRight = new Reaction();
	Reaction apple = new Reaction();
	Reaction applePie = new Reaction();
	Reaction shovel = new Reaction();
	Reaction rope = new Reaction();
	Reaction husbandOnBoard = new Reaction();
	Reaction postDateCastle = new Reaction();
	Reaction postDateCarpenter = new Reaction();
	Reaction castleDate = new Reaction();
	Reaction stoodUp = new Reaction();
	
	//chat
	Reaction introConvoStart = new Reaction();
	Reaction introConvoOne = new Reaction();
	Reaction introConvoTwo = new Reaction();
	Reaction introConvoThree = new Reaction();
	Reaction introConvoFour = new Reaction();
	
	Reaction castleMarriageStart = new Reaction();
	Reaction castleMarriageOne = new Reaction();
	Reaction castleMarriageTwo = new Reaction();
	Reaction castleMarriageThree = new Reaction();
	Reaction castleMarriageFour = new Reaction();
	Reaction castleMarriageFive = new Reaction();
	Reaction castleMarriageSix = new Reaction();
	
	Reaction girlEndStart = new Reaction();
	Reaction girlEndOne = new Reaction();
	Reaction girlEndTwo = new Reaction();
	Reaction girlEndThree = new Reaction();
	Reaction girlEndFour = new Reaction();
	Reaction girlEndFive = new Reaction();
	Reaction girlEndSix = new Reaction();
	#endregion
	
	protected override void SetFlagReactions(){
		SetupReactions();
		flagReactions.Add(FlagStrings.FarmAlive, moveAway);
		flagReactions.Add(FlagStrings.NotSilly, notSilly);
		flagReactions.Add(FlagStrings.TellOnDaughter, tellOn);
		flagReactions.Add(FlagStrings.StoriesAreSilly, sillyStories);
		flagReactions.Add(FlagStrings.YourRight, yourRight);
		flagReactions.Add(FlagStrings.WorkAndStories, workandRead);
		flagReactions.Add(FlagStrings.StoodUp, stoodUp);
		
		flagReactions.Add(FlagStrings.GiveAppleToFarmer, apple);
		flagReactions.Add(FlagStrings.GiveApplePieToFarmer, applePie);
		flagReactions.Add(FlagStrings.GiveShovelToFarmer, shovel);
		flagReactions.Add(FlagStrings.GiveRopeToFarmer, rope);
		
		flagReactions.Add(FlagStrings.HusbandOnBoard, husbandOnBoard);
		
		introConvoStart.AddAction(new NPCAddScheduleAction(this, introConvo)); // turn around
		flagReactions.Add(FarmerFamilyFlagStrings.IntroConvoStart, introConvoStart);
		flagReactions.Add(FarmerFamilyFlagStrings.IntroConvoTwo, introConvoOne);
		flagReactions.Add(FarmerFamilyFlagStrings.IntroConvoFour, introConvoTwo);
		flagReactions.Add(FarmerFamilyFlagStrings.IntroConvoSix, introConvoThree);
		
		
		flagReactions.Add(FarmerFamilyFlagStrings.GirlCastleMarriageTwo, castleMarriageOne);
		flagReactions.Add(FarmerFamilyFlagStrings.GirlCastleMarriageFour, castleMarriageTwo);
		flagReactions.Add(FarmerFamilyFlagStrings.GirlCastleMarriageSix, castleMarriageThree);
		flagReactions.Add(FarmerFamilyFlagStrings.GirlCastleMarriageEight, castleMarriageFour);
		
		
		girlEndStart.AddAction(new NPCAddScheduleAction(this, girlEnd));
		flagReactions.Add(FarmerFamilyFlagStrings.GirlPathEndStart, girlEndStart);
		flagReactions.Add(FarmerFamilyFlagStrings.GirlPathEndThree, girlEndOne);
		flagReactions.Add(FarmerFamilyFlagStrings.GirlPathEndFive, girlEndTwo);
		flagReactions.Add(FarmerFamilyFlagStrings.GirlPathEndSeven, girlEndThree);
		flagReactions.Add(FarmerFamilyFlagStrings.GirlPathEndNine, girlEndFour);
		flagReactions.Add(FarmerFamilyFlagStrings.GirlPathEndEleven, girlEndFive);
		flagReactions.Add(FarmerFamilyFlagStrings.GirlPathEndThirteen, girlEndSix);
	}
	
	protected override EmotionState GetInitEmotionState(){
		initialState = new InitialEmotionState(this,"What do you want? I'm busy." );
		startingPosition = transform.position;
		startingPosition.y += LevelManager.levelYOffSetFromCenter;
		//this.transform.position = new Vector3(200,0,0);
		return (initialState);
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}
	
	Schedule marriageConvo, girlEnd, introConvo;
	protected override void SetUpSchedules(){
		
		marriageConvo = new Schedule(this, Schedule.priorityEnum.High);
		marriageConvo.Add(new TimeTask(40f, new IdleState(this)));
		
		introConvo = new Schedule(this, Schedule.priorityEnum.DoNow);
		introConvo.Add(new Task (new MoveThenDoState(this, new Vector3(startingPosition.x+.01f, startingPosition.y, startingPosition.z), new MarkTaskDone(this))));
		introConvo.Add(new TimeTask(27.5f, new IdleState(this)));
		introConvo.SetCanInteract(false);
		
		girlEnd = new Schedule(this, Schedule.priorityEnum.DoNow);
		girlEnd.Add(new Task (new MoveThenDoState(this, new Vector3(startingPosition.x-.05f, startingPosition.y, startingPosition.z), new MarkTaskDone(this)))); //turn towards farmer
		girlEnd.Add(new TimeTask(19.6f, new IdleState(this)));
		girlEnd.Add(new Task (new MoveThenDoState(this, new Vector3(startingPosition.x+.05f, startingPosition.y, startingPosition.z), new MarkTaskDone(this)))); //turn towards farmer
		girlEnd.Add(new TimeTask(31.4f, new IdleState(this)));
		girlEnd.Add(new Task (new MoveThenDoState(this, new Vector3(startingPosition.x-.05f, startingPosition.y, startingPosition.z), new MarkTaskDone(this)))); //turn towards farmer
		girlEnd.Add(new TimeTask(18.3f, new IdleState(this)));
		girlEnd.SetCanInteract(false);
	}
	
	protected void ResetPosition(){
		this.transform.position = startingPosition;	
	}
	
	protected void ChangeDisposition(NPC npc, string disp){
		disposition += int.Parse(disp);
		if (disposition > 70){
			setHappy();
			initialState.PassStringToEmotionState("happy");
		}else if (disposition < 50){
			setAngry();
		}
	}
	
	protected void ChangeHusbandStatus(){
		husbandReady = true;
		if (disposition > 70 && !flagSet){
			initialState.PassStringToEmotionState("husband");
			farmersOnBoard = true;
		}
	}
	
	protected void FlagToNPC(NPC npc, string text){
		if (text == "castle"){
			carpenterDate = false;	
		}
		if (text == "carpenterSuccess" && farmersOnBoard){
			initialState.PassStringToEmotionState("carpenterSuccess");
		}
		if (text == "carpenterSuccess" && !farmersOnBoard){
			initialState.PassStringToEmotionState("daughterOnBoard");
		}
		if (text == "stoodUp" && farmersOnBoard){
			initialState.PassStringToEmotionState("stoodUp");
			disposition -= 20;
		}
	}
	
	protected void SetupReactions(){
		moveAway.AddAction(new NPCCallbackAction(ResetPosition));
		moveAway.AddAction(new NPCEmotionUpdateAction(this, initialState));
		
		notSilly.AddAction(new NPCCallbackSetStringAction(ChangeDisposition, this, "-15"));
		workandRead.AddAction(new NPCCallbackSetStringAction(ChangeDisposition, this, "-5"));
		
		tellOn.AddAction(new NPCCallbackSetStringAction(ChangeDisposition, this, "10"));
		sillyStories.AddAction(new NPCCallbackSetStringAction(ChangeDisposition, this, "5"));
		yourRight.AddAction(new NPCCallbackSetStringAction(ChangeDisposition, this, "10"));
		
		apple.AddAction(new NPCCallbackSetStringAction(ChangeDisposition, this, "5"));
		applePie.AddAction(new NPCCallbackSetStringAction(ChangeDisposition, this, "10"));
		shovel.AddAction(new NPCCallbackSetStringAction(ChangeDisposition, this, "15"));
		rope.AddAction(new NPCCallbackSetStringAction(ChangeDisposition, this, "10"));
		
		husbandOnBoard.AddAction(new NPCCallbackAction(ChangeHusbandStatus));
		
		postDateCastle.AddAction(new NPCEmotionUpdateAction(this, new HateEmotionState(this, "")));
		postDateCastle.AddAction(new NPCCallbackSetStringAction(ChangeDisposition, this, "-100"));
		
		postDateCarpenter.AddAction(new NPCCallbackSetStringAction(FlagToNPC, this, "carpenterSuccess"));
		
		castleDate.AddAction(new NPCCallbackSetStringAction(FlagToNPC, this, "castle"));
		
		stoodUp.AddAction(new NPCCallbackSetStringAction(FlagToNPC, this, "stoodUp"));
		
		//chat
		ShowMultipartChatAction introConvoOneDialogue = new ShowMultipartChatAction(this);
		introConvoOneDialogue.AddChat("And the last time ah let ya get you way, yah ran from home with that tramp.", 5f);
		introConvoOne.AddAction(introConvoOneDialogue);
		
		ShowMultipartChatAction introConvoTwoDialogue = new ShowMultipartChatAction(this);
		introConvoTwoDialogue.AddChat("What ya need is someone that can help ya settle down and tha carpenter's son will do that very nicely!", 6f);
		introConvoTwo.AddAction(introConvoTwoDialogue);
		
		ShowMultipartChatAction introConvoThreeDialogue = new ShowMultipartChatAction(this);
		introConvoThreeDialogue.AddChat("ENOUGH! What's done is done. Go back and take care of tha lighthouse, ya need ta work!", 5f);
		introConvoThree.AddAction(introConvoThreeDialogue);
		
		
		ShowMultipartChatAction castleMarriageOneDialogue = new ShowMultipartChatAction(this);
		castleMarriageOneDialogue.AddChat("...", 1f);
		castleMarriageOneDialogue.AddChat("I've just been talking to the Carpenter and he says that his tools have gone missing. Do you know anything about that?", 6f);
		castleMarriageOne.AddAction(castleMarriageOneDialogue);
		
		ShowMultipartChatAction castleMarriageTwoDialogue = new ShowMultipartChatAction(this);
		castleMarriageTwoDialogue.AddChat("I am fed up with your lies and attempts to sabotage this marriange. IT. WILL. BE. GOOD. FOR. YOU.", 5f);
		castleMarriageTwo.AddAction(castleMarriageTwoDialogue);
		
		ShowMultipartChatAction castleMarriageThreeDialogue = new ShowMultipartChatAction(this);
		castleMarriageThreeDialogue.AddChat("Enough of your childish behavior! Get back to working at the lighthouse and I fully expect an apology tomorrow morning.", 6f);
		castleMarriageThree.AddAction(castleMarriageThreeDialogue);
		
		ShowMultipartChatAction castleMarriageFourDialogue = new ShowMultipartChatAction(this);
		castleMarriageFourDialogue.AddChat("NOW!", 1f);
		castleMarriageFour.AddAction(castleMarriageFourDialogue);
		
		
		//girl end path
		ShowMultipartChatAction girlEndOneDialogue = new ShowMultipartChatAction(this);
		girlEndOneDialogue.AddChat("Really? you let her influence your thoughts? We've talked about this stuff already. Our daughter needs someone who can guide her!", 8f);
		girlEndOne.AddAction(girlEndOneDialogue);
		
		ShowMultipartChatAction girlEndTwoDialogue = new ShowMultipartChatAction(this);
		girlEndTwoDialogue.AddChat("Oh please! You are too immature to know anything about love!", 5f);
		girlEndTwo.AddAction(girlEndTwoDialogue);
		
		ShowMultipartChatAction girlEndThreeDialogue = new ShowMultipartChatAction(this);
		girlEndThreeDialogue.AddChat("And that's exactly how I know that it will provide the stability you need!", 6f);
		girlEndThreeDialogue.AddChat("Now enough of this nonsense!", 4f);
		girlEndThreeDialogue.AddChat("You girl have crossed the line far too many times. You do it once more and the punishment will be severe.", 7f);
		girlEndThreeDialogue.AddChat("And husband, I would have expected you above this nonsense!", 6f);
		girlEndThree.AddAction(girlEndThreeDialogue);
		
		ShowMultipartChatAction girlEndFourDialogue = new ShowMultipartChatAction(this);
		girlEndFourDialogue.AddChat("Oh don't give me those puppy dog eyes! We need to stick to what we came up with!", 4f);
		girlEndFour.AddAction(girlEndFourDialogue);
		
		ShowMultipartChatAction girlEndFiveDialogue = new ShowMultipartChatAction(this);
		girlEndFiveDialogue.AddChat("...", 1f);
		girlEndFive.AddAction(girlEndFiveDialogue);
		
		ShowMultipartChatAction girlEndSixDialogue = new ShowMultipartChatAction(this);
		girlEndSixDialogue.AddChat("Fine...have it your way but this talk isn't over.", 3f);
		girlEndSix.AddAction(girlEndSixDialogue);
	}
	
	
	#region EmotionStates
	#region Initial Emotion State
	private class InitialEmotionState : EmotionState{
		bool farmerOnBoard = false;
		bool WhyNotFlag = false;
		bool daughterOnBoard = false;
		Choice MarriageChoice = new Choice("What's this about marriage?", "That silly girl needs to settle herself down.");
		Choice WhyNotChoice = new Choice("Why are stories silly?", "Hmmph. In my days you did what your parents told ja, worked however long they wanted and didn't stick your heads in the clouds. Ya got to know and love your parents through their work!");
		Choice HowsFarmingChoice = new Choice ("How's farming?", "Poor.  That fool husband of mine can't sell anythin right. Always undercharges. We're lucky we still have a house. I dun know what my girl will do when she grows up.");
		Choice MarriageIdeaChoice = new Choice("I have an idea about the marriage.", "Go on");
		Choice ConvinceDaughterChoice = new Choice("Convince daughter to marry", "That's a waste of time, stop bothering me!");
		
		
		Reaction MarriageReaction = new Reaction();
		Reaction WhyNotReaction = new Reaction();
		Reaction HowsFarmingReaction = new Reaction();
		Reaction MarriageIdeaReaction = new Reaction();
		Reaction ConvinceDaughterReaction = new Reaction();
		
		Reaction NoteReaction = new Reaction();
		Reaction AppleReaction = new Reaction();
		Reaction ApplePieReaction = new Reaction();
		Reaction ShovelReaction = new Reaction();
		Reaction CaptainsLogReaction = new Reaction();
		
		NPC control;
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, "What brings ya here?  I'm busy."){
			control = toControl;
			_allChoiceReactions.Add(MarriageChoice, new DispositionDependentReaction(MarriageReaction));
			_allChoiceReactions.Add(HowsFarmingChoice, new DispositionDependentReaction(HowsFarmingReaction));
		
		}
		
		public void SetupReactions(){
			NoteReaction.AddAction(new NPCTakeItemAction(control));
			NoteReaction.AddAction(new UpdateCurrentTextAction(control, "Hmm... thanks for givin this to me. If my fool girl had seen this there's no tellin what she would have done."));
			AppleReaction.AddAction(new NPCTakeItemAction(control));
			AppleReaction.AddAction(new UpdateCurrentTextAction(control, "Mmm... that there was delicious!"));
			ApplePieReaction.AddAction(new NPCTakeItemAction(control));
			ApplePieReaction.AddAction(new UpdateCurrentTextAction(control, "Mmm... that there was delicious!"));
			ShovelReaction.AddAction(new NPCTakeItemAction(control));
			ShovelReaction.AddAction(new UpdateCurrentTextAction(control, "I been needing a shovel. Thanks!"));
			CaptainsLogReaction.AddAction(new NPCCallbackAction(UpdateCaptainsLog));
			CaptainsLogReaction.AddAction(new UpdateCurrentTextAction(control, "No thanks! I dun need your silly stories!"));
			
			MarriageReaction.AddAction(new NPCCallbackAction(UpdateMarriage));
			WhyNotReaction.AddAction(new NPCCallbackAction(UpdateWhysNot));
			MarriageIdeaReaction.AddAction(new NPCCallbackAction(MarriageIdeaResponse));
			ConvinceDaughterReaction.AddAction(new NPCCallbackAction(ConvinceDaughterResponse));
		}
		
		public void ConvinceDaughterResponse(){
			if (farmerOnBoard){
				_npcInState.SetCharacterPortrait(StringsNPC.Default);
_npcInState.ChangeFacialExpression(StringsNPC.Default);
				FlagManager.instance.SetFlag(FlagStrings.FarmerOnBoard);
				_allChoiceReactions.Remove(ConvinceDaughterChoice);
			}else {
				_npcInState.SetCharacterPortrait(StringsNPC.Angry);
_npcInState.ChangeFacialExpression(StringsNPC.Angry);
				_allChoiceReactions.Remove(ConvinceDaughterChoice);
				_allChoiceReactions.Add(MarriageIdeaChoice, new DispositionDependentReaction(MarriageIdeaReaction));
			}
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void MarriageIdeaResponse(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
_npcInState.ChangeFacialExpression(StringsNPC.Default);
			_allChoiceReactions.Remove(MarriageIdeaChoice);
			_allChoiceReactions.Add(ConvinceDaughterChoice, new DispositionDependentReaction(ConvinceDaughterReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void UpdateMarriage(){
			
		}
		public void UpdateWhysNot(){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
_npcInState.ChangeFacialExpression(StringsNPC.Default);
			_allChoiceReactions.Remove(WhyNotChoice);
			GUIManager.Instance.RefreshInteraction();
			FlagManager.instance.SetFlag(FlagStrings.ConversationInMiddleFarmerMother);
		}
		public void UpdateCaptainsLog(){
			if(WhyNotFlag == false){
				_allChoiceReactions.Add(WhyNotChoice, new DispositionDependentReaction(WhyNotReaction));
				GUIManager.Instance.RefreshInteraction();
				
			}
			WhyNotFlag = true;
		}
		
		public override void PassStringToEmotionState(string text){
			if (text == "happy"){
				_allChoiceReactions.Add(MarriageIdeaChoice, new DispositionDependentReaction(MarriageIdeaReaction));
			}
			if (text == "husband"){
				farmerOnBoard = true;
				MarriageIdeaChoice = new Choice("Convince daughter to marry.", "Well... my husband has convinced me in to allowing you to give it a shot.");
			}
			if (text == "carpenterSuccess"){
				SetDefaultText("I've heard what you did for my daughter. Bout time she listens to me.");
			}
			if (text == "daughterOnBoard"){ //successful date with carpenter, farmers unaware
				SetDefaultText("Hello there! How's it going?");
				daughterOnBoard = true;
				MarriageChoice = new Choice("What's this about marriage?", "My daughter finally got her head on straight and is on board for the marriage");
			}
			if (text == "stoodUp"){
				SetDefaultText("Went through all that trouble...for nothing.");
			}
		}
	
	}
	#endregion
	
	private class GoneEmotionState : EmotionState{
		public GoneEmotionState(NPC toControl, string currentDialogue) : base(toControl, ""){
		}
	}
	
	private class HateEmotionState : EmotionState{
		public HateEmotionState(NPC toControl, string currentDialogue) : base(toControl, "Do not talk to me."){
		}
		
	}
	#endregion
}
