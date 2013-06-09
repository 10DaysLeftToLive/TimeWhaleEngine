using UnityEngine;
using System.Collections;

/// <summary>
/// CastlemanMiddle specific scripting values
/// </summary>
public class CastlemanMiddle : NPC {
	CarpenterFight carpenterFightState;
	DateSuccess dateSuccessState;
	MarriedCarpenter marriedCarpenterState;
	InitialEmotionState initialState;
	SaneState saneState;
	Date dateState;
	StoodUp stoodUpState;
	Vector3 startingPosition;
	bool carpenterDateSuccess = false;
	bool dateForMe = false;
	bool successfulDate = false;
	protected override void Init() {
		id = NPCIDs.CASTLE_MAN;
		base.Init();
	}
	
	#region ReactionInstantiate
	Reaction notInsane = new Reaction();
	Reaction waitingForDate = new Reaction();
	Reaction datingThyEnemy = new Reaction();
	Reaction gotTheGirl = new Reaction();
	Reaction iBeDating = new Reaction();
	Reaction endOfDate = new Reaction();
	Reaction stoodUpLG = new Reaction();
	Reaction moveToDate = new Reaction();
	Reaction girlMarriageApproved = new Reaction();
	
	//chat stuff
	Reaction castleDateOne = new Reaction();
	Reaction castleDateTwo = new Reaction();
	Reaction castleDateThree = new Reaction();
	Reaction castleDateFour = new Reaction();
	Reaction castleDateFive = new Reaction();
	Reaction castleDateSix = new Reaction();
	#endregion
	
	protected override void SetFlagReactions(){
		notInsane.AddAction(new NPCEmotionUpdateAction(this, saneState));
		flagReactions.Add(FlagStrings.NotInsane, notInsane);
		
		waitingForDate.AddAction(new NPCEmotionUpdateAction(this, dateState));
		flagReactions.Add(FlagStrings.WaitingForDate, waitingForDate);
		
		datingThyEnemy.AddAction(new NPCCallbackAction(setFlagCarpenterDateSuccess));
		datingThyEnemy.AddAction(new NPCEmotionUpdateAction(this, marriedCarpenterState));
		flagReactions.Add(FlagStrings.PostDatingCarpenter, datingThyEnemy);
		
		gotTheGirl.AddAction(new NPCEmotionUpdateAction(this, dateSuccessState));
		flagReactions.Add(FlagStrings.PostCastleDate, gotTheGirl);
		
		iBeDating.AddAction(new NPCCallbackAction(setFlagDateForMe));
		flagReactions.Add(FlagStrings.CastleDate, iBeDating);
		
		endOfDate.AddAction(new NPCCallbackAction(dateOver));
		endOfDate.AddAction(new NPCAddScheduleAction(this, moveBack));
		flagReactions.Add(FlagStrings.EndOfDate, endOfDate);
		
		stoodUpLG.AddAction(new NPCEmotionUpdateAction(this, stoodUpState));
		flagReactions.Add(FlagStrings.CastleManNoShow, stoodUpLG);
		
		moveToDate.AddAction(new NPCAddScheduleAction(this, moveToBeach));
		flagReactions.Add(FlagStrings.CastleManDating, moveToDate);
		
		girlMarriageApproved.AddAction(new NPCCallbackSetStringAction(FlagToNPC, this, "marriage"));
		flagReactions.Add(FarmerFamilyFlagStrings.GirlPathEndThirteen, girlMarriageApproved);
		
		flagReactions.Add(FarmerFamilyFlagStrings.GirlCastleDateOne, castleDateOne);
		flagReactions.Add(FarmerFamilyFlagStrings.GirlCastleDateThree, castleDateTwo);
		flagReactions.Add(FarmerFamilyFlagStrings.GirlCastleDateFive, castleDateThree);
		
	}
	
	protected override EmotionState GetInitEmotionState(){
		initialState = new InitialEmotionState(this, "Are you looking for a Castle too?");
		dateSuccessState = new DateSuccess(this, "");
		marriedCarpenterState = new MarriedCarpenter(this, "");
		carpenterFightState = new CarpenterFight(this, "");
		stoodUpState = new StoodUp(this, "");
		saneState = new SaneState(this, "");
		dateState = new Date(this, "");
		
		startingPosition = transform.position;
		startingPosition.y += LevelManager.levelYOffSetFromCenter;
		return (initialState);
	}
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}
	Schedule moveToBeach, moveBack, dateWithLG;
	//NPCConvoSchedule dateWithLG;
	protected override void SetUpSchedules(){
		SetupReactions();
		moveBack = new Schedule(this, Schedule.priorityEnum.High);
		moveBack.Add(new Task(new MoveThenDoState(this, startingPosition, new MarkTaskDone(this))));
		
		moveToBeach = new Schedule(this, Schedule.priorityEnum.DoNow);
		moveToBeach.Add(new Task(new MoveThenDoState(this, new Vector3(MapLocations.MiddleOfBeachMiddle.x+1.5f, MapLocations.MiddleOfBeachMiddle.y, MapLocations.MiddleOfBeachMiddle.z), new MarkTaskDone(this))));
		Task reachedBeach = new TimeTask(.1f,new IdleState(this));
		reachedBeach.AddFlagToSet(FarmerFamilyFlagStrings.GirlCastleDateOne);
		moveToBeach.Add(reachedBeach);
		moveToBeach.Add(new TimeTask(5.3f, new IdleState(this)));
		
		Task reachedBeachTwo = new TimeTask(.1f,new IdleState(this));
		reachedBeachTwo.AddFlagToSet(FarmerFamilyFlagStrings.GirlCastleDateTwo);
		moveToBeach.Add(reachedBeachTwo);
		moveToBeach.Add(new TimeTask(7.3f, new IdleState(this)));
		
		Task reachedBeachThree = new TimeTask(.1f,new IdleState(this));
		reachedBeachThree.AddFlagToSet(FarmerFamilyFlagStrings.GirlCastleDateThree);
		moveToBeach.Add(reachedBeachThree);
		moveToBeach.Add(new TimeTask(3.3f, new IdleState(this)));
		
		
		Task reachedBeachFour = new TimeTask(.1f,new IdleState(this));
		reachedBeachFour.AddFlagToSet(FarmerFamilyFlagStrings.GirlCastleDateFour);
		moveToBeach.Add(reachedBeachFour);
		moveToBeach.Add(new TimeTask(6.3f, new IdleState(this)));
		
		Task reachedBeachFive = new TimeTask(.1f,new IdleState(this));
		reachedBeachFive.AddFlagToSet(FarmerFamilyFlagStrings.GirlCastleDateFive);
		moveToBeach.Add(reachedBeachFive);
		moveToBeach.Add(new TimeTask(2.3f, new IdleState(this)));
		
		Task reachedBeachSix = new TimeTask(.1f,new IdleState(this));
		reachedBeachSix.AddFlagToSet(FarmerFamilyFlagStrings.GirlCastleDateSix);
		moveToBeach.Add(reachedBeachSix);
		moveToBeach.Add(new TimeTask(6f, new IdleState(this)));
		
		Task reachedBeachEnd = new TimeTask(.1f,new IdleState(this));
		reachedBeachEnd.AddFlagToSet(FlagStrings.EndOfDate);
		moveToBeach.Add(reachedBeachEnd);
		moveToBeach.Add(new TimeTask(3f, new IdleState(this)));
		
		
		/*dateWithLG =  new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.LighthouseGirlMiddle),
			new MiddleCastleManToLighthouseGirl(), Schedule.priorityEnum.DoConvo); 
		dateWithLG.SetCanNotInteractWithPlayer();*/
	}
	
	protected void dateOver(){
		if (!dateForMe)
			FlagManager.instance.SetFlag(FlagStrings.CastleManNoShow);
	}
	
	protected void setFlagDateForMe(){
		dateForMe = true;
	}
	
	protected void setFlagCarpenterDateSuccess(){
		carpenterDateSuccess = true;
	}
	
	protected void FlagToNPC(NPC npc, string text){
		if (text == "marriage"){
			dateSuccessState.PassStringToEmotionState(text);	
		}
	}
	
	protected void SetupReactions(){
		ShowMultipartChatAction castleDateOneDialogue = new ShowMultipartChatAction(this);
		castleDateOneDialogue.AddChat("*Out of Breath* At long last I get to try and court you my fair lady!", 5f);
		castleDateOne.AddAction(castleDateOneDialogue);
		
		ShowMultipartChatAction castleDateTwoDialogue = new ShowMultipartChatAction(this);
		castleDateTwoDialogue.AddChat("Endearing? My dear woman, I always speak this way!", 3f);
		castleDateTwo.AddAction(castleDateTwoDialogue);
		
		ShowMultipartChatAction castleDateThreeDialogue = new ShowMultipartChatAction(this);
		castleDateThreeDialogue.AddChat("You remember me!", 2f);
		castleDateThree.AddAction(castleDateThreeDialogue);
	}
	
	
	#region EmotionStates
	#region Initial Emotion State
	private class InitialEmotionState : EmotionState{
		Reaction gaveSeashell;
		Reaction gaveRose;
		Reaction lookLike;
		Reaction lastSeen;
		Reaction randomMessage;
		bool rose = false, seaShell = false;
		//List of random things the insane castleman says.
		string[] stringList = {"Are you looking for a Castle too?",
				"Castle castle castle castle castle...Oh hello there!",
				"In the sky my castle!  No wait that's a bird...",
				"Have you found it! Have you found it!  Have you found it!",
				"I think I saw my castle walking around yesterday!",
				"Look under that leave!  My castle!  Wait...that's an earthworm...",
				"Is my castle in the sky?  Is my castle in a pie?  Is my castle in the rye?",
				"Oh castle, Oh castle, how great thy beauty is!",
				"I wish I was a castle..."};
		int stringCounter = 9; //Counts the number of strings in string list.
		
		
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			Choice seen = new Choice("Last Seen", "Twas at a beach!  When my castle was swallowed by the cavernous waves!");
			Choice look = new Choice("What does it look like?", "It was glowing with great light, and lined with many flags and alabaster walls.  It was the most beautiful thing in the world....Please find it for me.");
			
			//Code for the choices for interacting with the castleman
			lastSeen = new Reaction();
			lookLike = new Reaction();
			//****************************************************************************************************************
			lastSeen.AddAction(new NPCCallbackAction(LastSeenResponse));
			//lastSeen.AddAction(new NPCEmotionUpdateAction(toControl, new CarpenterFight(toControl, "MUHAHAHA")));//Remove this later
			//****************************************************************************************************************
			lastSeen.AddAction(new UpdateCurrentTextAction(toControl, "Twas at a beach!  When my castle was swallowed by the cavernous waves!"));
			lookLike.AddAction(new UpdateCurrentTextAction(toControl, "It was glowing with great light, and lined with many flags and alabaster walls.  It was the most beautiful thing in the world....Please find it for me."));
			
			//Code for giving the seashell
			Reaction gaveShell = new Reaction();
			gaveShell.AddAction(new NPCTakeItemAction(toControl));
			gaveShell.AddAction(new NPCCallbackAction(SetSeaShell));
			gaveShell.AddAction(new UpdateCurrentTextAction(toControl, "A piece of the wall of my castle!  Perhaps there is hope for my sufferings!"));
			_allItemReactions.Add(StringsItem.Seashell,  new DispositionDependentReaction(gaveShell)); // change item to shell
			
			//Code for the giving rose interaction.
			gaveRose = new Reaction();
			gaveRose.AddAction(new NPCTakeItemAction(toControl));
			gaveRose.AddAction(new NPCCallbackAction(SetRose));
			gaveRose.AddAction(new UpdateCurrentTextAction(toControl, "A piece of the jungle of thorns!  If you can brave that perhaps...perhaps you are the one who can truly find my castle."));
			_allItemReactions.Add(StringsItem.Rose,  new DispositionDependentReaction(gaveRose)); // change item to rose
			
			_allChoiceReactions.Add(seen, new DispositionDependentReaction(lastSeen));
			_allChoiceReactions.Add(look, new DispositionDependentReaction(lookLike));
			
			//Sets up the random dialogue for the castleman.
			randomMessage = new Reaction();
			randomMessage.AddAction(new NPCCallbackAction(RandomMessage));
			SetOnOpenInteractionReaction(new DispositionDependentReaction(randomMessage));
		}
		public void RandomMessage(){
			SetDefaultText(stringList[(int)Random.Range(0,stringCounter)]);
		}
		public void SetRose(){
			rose = true;
			if (seaShell == true && rose == true){
				//Set a flag here for the castleman
			}
		}
		public void SetSeaShell(){
			seaShell = true;
			if (seaShell == true && rose == true){
				//Set a flag here for the castleman
			}
		}
		public void LastSeenResponse(){
			FlagManager.instance.SetFlag(FlagStrings.NotInsane);

		}
		public override void UpdateEmotionState(){
			
		}
	
	}
	//This is the basic path for the CastleMan if the LG is still open to marriage.
	private class SaneState: EmotionState{
		Choice SayChoice = new Choice("What does it say?", "It's never right.  I mean look at this: 'Roses are red'?  How pedestrian can you get?  The farmer's daughter will never like me with garbage like that...");
		Choice WhatWritingChoice = new Choice("What are you writing?","It's a love letter for the farmer's daughter...it's just all wrong.  I mean 'Roses are red'?  So simplistic.");
		Choice JudgeItChoice = new Choice("Have you tried letting her judge it?","But what if its not perfect? Hold on.  Maybe you have a point.  Here, you try and deliver it to her.  The farmer never lets me anywhere near her daughter.");
		
		Reaction SayReaction = new Reaction();
		Reaction WhatWritingReaction = new Reaction();
		Reaction JudgeItReaction = new Reaction();		
		
		NPC control;
		public SaneState (NPC toControl, string currentDialogue): base(toControl, "Hello good sir!  Could you read this note for me?  Wait...never mind.  Its never going to be good enough..."){
			control = toControl;
			
			_allChoiceReactions.Add(SayChoice, new DispositionDependentReaction(SayReaction));
			_allChoiceReactions.Add(WhatWritingChoice, new DispositionDependentReaction(WhatWritingReaction));
			
		}
		public void SetupInteractions(){
			SayReaction.AddAction(new NPCCallbackAction(WhatResponse));
			WhatWritingReaction.AddAction(new NPCCallbackAction(WhatResponse));
			JudgeItReaction.AddAction(new NPCCallbackAction(JudgeItResponse));
			JudgeItReaction.AddAction(new NPCGiveItemAction(control,StringsItem.Note));
		}
		
		public void JudgeItResponse(){
			_allChoiceReactions.Clear();
			SetDefaultText("So...What did she think of the letter?");
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void WhatResponse(){
			_allChoiceReactions.Clear();
			_allChoiceReactions.Add(JudgeItChoice, new DispositionDependentReaction(JudgeItReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Sigh...the farmer's daughter will never like me with a letter like this...");
			GUIManager.Instance.RefreshInteraction();
		}
	}
	
	private class Date: EmotionState{
		Choice DateChoice = new Choice("You have a date!", "Really? This...this...this is the most beauteous day of my life! Hurry to the beach. I cannot tarry!");
		
		Reaction DateReaction = new Reaction();
		
		bool flagSet = false;
		public Date (NPC toControl, string currentDialogue):base (toControl, "Have you delievered the letter yet?"){
			_allChoiceReactions.Clear();
			
			DateReaction.AddAction(new NPCCallbackAction(DateResponse));
			_allChoiceReactions.Add(DateChoice, new DispositionDependentReaction(DateReaction));
		}
		
		public void DateResponse(){
			if (!flagSet){
				_allChoiceReactions.Clear();
				_allItemReactions.Clear();
				GUIManager.Instance.CloseInteractionMenu();
				SetDefaultText("Im busy now, go away.");
				FlagManager.instance.SetFlag(FlagStrings.CastleManDating);
				flagSet = true;
			}
		}
		
	}
	
	private class StoodUp: EmotionState{
		public StoodUp (NPC toControl, string currentDialogue):base (toControl, "Why did you not tell me that the lighthouse girl wanted to meet. I would gladly have run there to meet with her at a moment's notice. Now all has been lost!"){
			
		}
	}
	
	//In this state the Castle Man hate you
	private class MarriedCarpenter: EmotionState{
		public MarriedCarpenter (NPC toControl, string currentDialogue):base (toControl, "Blackguard!  You have betrayed me for the last time!  Speak to me no more!"){
			
		}
	}
	
	private class DateSuccess: EmotionState{
		public DateSuccess (NPC toControl, string currentDialogue):base (toControl, "The fair maiden does like me! I only hope that she can overcome her parents mistrust towards me. She told me she would."){
			
		}
		
		public override void PassStringToEmotionState(string text){
			if (text == "marriage"){
				SetDefaultText("I am eternally in your debt! Ask me anything and I shall surely give it to you!");
				FlagManager.instance.SetFlag(FlagStrings.CastleMarriage);
			}
		}
	}
	
	//Emotion state for fighting with the carpenter in old.
	private class CarpenterFight: EmotionState{
		Choice WillDo;
		Reaction WhatWillDo;
		public CarpenterFight (NPC toControl, string currentDialogue):base (toControl, "I've thought very hard about this and I must say, all of this has to be the fault of the Carpenter's Son.  He stole my fair princess from me!"){
			WillDo = new Choice ("What will you do?", "I will get my revenge!");
			WhatWillDo = new Reaction();
			
			WhatWillDo.AddAction(new NPCCallbackAction(Revenge));
				
			_allChoiceReactions.Add(WillDo, new DispositionDependentReaction(WhatWillDo));
		}
		public void Revenge(){
			_allChoiceReactions.Remove(WillDo);
			SetDefaultText("I will get revenge!");
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("REVENGE!!  I WILL HAVE REVENGE!!");
		}
	}
	
	#endregion
	#endregion
}
