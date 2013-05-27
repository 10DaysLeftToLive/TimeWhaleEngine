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
	bool carpenterDateSuccess = false;
	bool dateForMe = false;
	bool successfulDate = false;
	protected override void Init() {
		id = NPCIDs.CASTLE_MAN;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		Reaction notInsane = new Reaction();
		notInsane.AddAction(new NPCEmotionUpdateAction(this, saneState));
		flagReactions.Add(FlagStrings.NotInsane, notInsane);
		
		Reaction waitingForDate = new Reaction();
		waitingForDate.AddAction(new NPCEmotionUpdateAction(this, dateState));
		flagReactions.Add(FlagStrings.WaitingForDate, waitingForDate);
		
		Reaction datingThyEnemy = new Reaction();
		datingThyEnemy.AddAction(new NPCCallbackAction(setFlagCarpenterDateSuccess));
		datingThyEnemy.AddAction(new NPCEmotionUpdateAction(this, marriedCarpenterState));
		flagReactions.Add(FlagStrings.PostDatingCarpenter, datingThyEnemy);
		
		Reaction gotTheGirl = new Reaction();
		gotTheGirl.AddAction(new NPCEmotionUpdateAction(this, dateSuccessState));
		flagReactions.Add(FlagStrings.PostCastleDate, gotTheGirl);
		
		Reaction iBeDating = new Reaction();
		iBeDating.AddAction(new NPCCallbackAction(setFlagDateForMe));
		flagReactions.Add(FlagStrings.CastleDate, iBeDating);
		
		Reaction endOfDate = new Reaction();
		endOfDate.AddAction(new NPCCallbackAction(dateOver));
		flagReactions.Add(FlagStrings.EndOfDate, endOfDate);
		
		Reaction stoodUpLG = new Reaction();
		stoodUpLG.AddAction(new NPCEmotionUpdateAction(this, stoodUpState));
		flagReactions.Add(FlagStrings.CastleManNoShow, stoodUpLG);
		
		Reaction moveToDate = new Reaction();
		moveToDate.AddAction(new NPCAddScheduleAction(this, dateWithLG));
		moveToDate.AddAction(new NPCAddScheduleAction(this, moveToBeach));
		flagReactions.Add(FlagStrings.CastleManDating, moveToDate);
		
	}
	
	protected override EmotionState GetInitEmotionState(){
		initialState = new InitialEmotionState(this, "Are you looking for a Castle too?");
		dateSuccessState = new DateSuccess(this, "");
		marriedCarpenterState = new MarriedCarpenter(this, "");
		carpenterFightState = new CarpenterFight(this, "");
		stoodUpState = new StoodUp(this, "");
		saneState = new SaneState(this, "");
		dateState = new Date(this, "");
		return (initialState);
	}
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}
	Schedule moveToBeach;
	NPCConvoSchedule dateWithLG;
	protected override void SetUpSchedules(){
		
		moveToBeach = new Schedule(this, Schedule.priorityEnum.DoNow);
		moveToBeach.Add(new Task(new MoveThenDoState(this, new Vector3 (60,44.5f,.5f), new MarkTaskDone(this))));
		
		dateWithLG =  new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.LighthouseGirlMiddle),
			new MiddleCastleManToLighthouseGirl(), Schedule.priorityEnum.DoNow); 
		dateWithLG.SetCanNotInteractWithPlayer();
	}
	
	protected void dateOver(){
		if (dateForMe)
			FlagManager.instance.SetFlag(FlagStrings.CastleManNoShow);
	}
	
	protected void setFlagDateForMe(){
		dateForMe = true;
	}
	
	protected void setFlagCarpenterDateSuccess(){
		carpenterDateSuccess = true;
	}
	
	protected void letCastleManKnowOfDate(){
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
				FlagManager.instance.SetFlag(FlagStrings.CastleManDating);
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
		public DateSuccess (NPC toControl, string currentDialogue):base (toControl, currentDialogue){
			
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
