using UnityEngine;
using System.Collections;

/// <summary>
/// LighthouseGirl Middle specific scripting values
/// </summary>
public class LighthouseGirlMiddle : NPC {
	InitialEmotionState initialState;
	ProMarriageEmotionState marriageState;
	AntiMarriageEmotionState noMarriageState;
	StoodUpEmotionState stoodUpState;
	Vector3 startingPosition;
	bool successfulDate= false;
	bool waitingOnDateTimer = false;
	bool dateOver = false;
	float time;
	protected override void Init() {
		id = NPCIDs.LIGHTHOUSE_GIRL;
		base.Init();
	}
	
	protected override void CharacterUpdate ()
	{
		if (waitingOnDateTimer && !dateOver){
			time -= Time.deltaTime;
			Debug.Log(time);
			if (time < 0 && !dateOver){
				FlagManager.instance.SetFlag(FlagStrings.EndOfDate);
				dateOver = true;
				this.RemoveScheduleWithFlag("a");
			}
		}
		
		base.CharacterUpdate ();
	}
	
	protected override void SetFlagReactions(){
		Reaction moveAway = new Reaction();
		moveAway.AddAction(new NPCCallbackAction(ResetPosition));
		moveAway.AddAction(new NPCEmotionUpdateAction(this, initialState));
		moveAway.AddAction(new NPCAddScheduleAction(this, openningWaitingSchedule));
		moveAway.AddAction(new NPCAddScheduleAction(this, postOpenningSchedule));
		flagReactions.Add(FlagStrings.FarmAlive, moveAway);
		
		Reaction antiMarriagePlanInAction = new Reaction();
		antiMarriagePlanInAction.AddAction(new NPCAddScheduleAction(this, noMarriageSchedule));
		antiMarriagePlanInAction.AddAction(new NPCEmotionUpdateAction(this, noMarriageState));
		flagReactions.Add(FlagStrings.ToolsToGirl, antiMarriagePlanInAction);
		
		Reaction marriageToCastleMan = new Reaction();
		antiMarriagePlanInAction.AddAction(new NPCEmotionUpdateAction(this, noMarriageState));
		marriageToCastleMan.AddAction(new NPCAddScheduleAction(this, marriageToCastleManSchedule));
		flagReactions.Add(FlagStrings.ToolsForMarriage, marriageToCastleMan);
		
		Reaction castleboyNotInsane = new Reaction();
		castleboyNotInsane.AddAction(new NPCCallbackAction(SendNotInsaneToState));
		flagReactions.Add(FlagStrings.NotInsane, castleboyNotInsane);
		
		#region date
		Reaction dateCarpenterNotified = new Reaction();
		dateCarpenterNotified.AddAction(new NPCCallbackAction(DateSuccess));
		flagReactions.Add(FlagStrings.CarpenterDating, dateCarpenterNotified);
		
		Reaction dateCastleManNotified = new Reaction();
		dateCastleManNotified.AddAction(new NPCCallbackAction(DateSuccess));
		flagReactions.Add(FlagStrings.CastleManDating, dateCastleManNotified);
		
		Reaction waitForPlayer = new Reaction();
		waitForPlayer.AddAction(new NPCAddScheduleAction(this, ropeDownSchedule));
		flagReactions.Add(FlagStrings.WaitForPlayerBeforeRope, waitForPlayer);
		
		Reaction waitingForDate = new Reaction();
		waitingForDate.AddAction(new NPCCallbackAction(MoveToBeach)); // teleport to beach
		waitingForDate.AddAction(new NPCCallbackAction(WaitingOnDate));
		//waitingForDate.AddAction(new NPCAddScheduleAction(this, waitingOnDate));
		flagReactions.Add(FlagStrings.WaitingForDate, waitingForDate);
		
		Reaction endOfDate = new Reaction();
		endOfDate.AddAction(new NPCAddScheduleAction(this, backToFarmSchedule)); // move back to farm
		endOfDate.AddAction(new NPCEmotionUpdateAction(this, initialState));
		endOfDate.AddAction(new NPCCallbackAction(SendDateOverToState));
		flagReactions.Add(FlagStrings.EndOfDate, endOfDate);
		
		Reaction stoodUp = new Reaction();
		stoodUp.AddAction(new NPCEmotionUpdateAction(this, stoodUpState));
		flagReactions.Add(FlagStrings.StoodUp, stoodUp);
		
		
		Reaction farmerOnBoard = new Reaction();
		farmerOnBoard.AddAction(new NPCCallbackAction(SendFarmerOnBoard));
		flagReactions.Add(FlagStrings.FarmerOnBoard, farmerOnBoard);
		#endregion
	}
	
	protected override EmotionState GetInitEmotionState(){
		initialState = new InitialEmotionState(this, "Hi!  Would you mind helping me out?  I need to get out of my arranged marriage, but need help with distracting my mom to make it work!");
		marriageState = new ProMarriageEmotionState(this, "");
		noMarriageState = new AntiMarriageEmotionState(this, "");
		stoodUpState = new StoodUpEmotionState(this, "");
		startingPosition = transform.position;
		startingPosition.y += LevelManager.levelYOffSetFromCenter;
		this.transform.position = new Vector3(200,0,0);
		return (new GoneEmotionState(this, ""));
	}
	
	Schedule openningWaitingSchedule, waitingOnDate, backToFarmSchedule, ropeDownSchedule;
	NPCConvoSchedule postOpenningSchedule;
	NPCConvoSchedule noMarriageSchedule, marriageToCastleManSchedule;
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}

	protected override void SetUpSchedules(){
		
		float dateTime = 50;
		
		openningWaitingSchedule = new Schedule(this, Schedule.priorityEnum.DoNow);
		openningWaitingSchedule.Add(new TimeTask(30, new WaitTillPlayerCloseState(this, player)));	
		
		backToFarmSchedule = new Schedule(this, Schedule.priorityEnum.High);
		//backToFarmSchedule.Add(new TimeTask(8f, new IdleState(this)));
		backToFarmSchedule.Add(new Task(new MoveThenDoState(this, startingPosition, new MarkTaskDone(this))));
		
		ropeDownSchedule = new Schedule(this, Schedule.priorityEnum.High);
		ropeDownSchedule.Add(new TimeTask(30, new WaitTillPlayerGoneState(this, player)));
		Task setFlag = new Task( new MoveThenDoState(this, startingPosition, new MarkTaskDone(this)));
		setFlag.AddFlagToSet(FlagStrings.WaitingForDate);
		ropeDownSchedule.Add(setFlag);
		ropeDownSchedule.AddFlagGroup("a");
		
		postOpenningSchedule =  new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.FarmerMotherMiddle),
			new MiddleFarmerMotherToLighthouseGirl(), Schedule.priorityEnum.DoConvo); 
		//postOpenningSchedule.SetCanNotInteractWithPlayer();
		
		noMarriageSchedule =  new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.FarmerMotherMiddle),
			new MiddleLighthouseGirlNoMarriage(), Schedule.priorityEnum.DoConvo); 
		//noMarriageSchedule.SetCanNotInteractWithPlayer();
		
		marriageToCastleManSchedule =  new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.FarmerMotherMiddle),
			new MiddleLighthouseGirlCastleManMarriage(), Schedule.priorityEnum.DoConvo); 
		//marriageToCastleManSchedule.SetCanNotInteractWithPlayer();
		
	}
	
	protected void WaitingOnDate(){
		waitingOnDateTimer = true;
		time = 40;
	}
	
	protected void DateSuccess(){
		successfulDate = true;	
	}
	
	protected void ResetPosition(){
		this.transform.position = startingPosition;	
	}
	
	protected void MoveToBeach(){
		this.transform.position = MapLocations.MiddleOfBeachMiddle;	
	}
	
	protected void SendNotInsaneToState(){
		initialState.PassStringToEmotionState(FlagStrings.NotInsane);
	}
	
	protected void SendDateOverToState(){
		Debug.Log("date over");
		if (initialState.carpenterPath && successfulDate){ //date success with carpenter
			FlagManager.instance.SetFlag(FlagStrings.PostDatingCarpenter);
			marriageState.PassStringToEmotionState("carpenter");
		}else if (successfulDate){ //date success with castleboy
			FlagManager.instance.SetFlag(FlagStrings.PostCastleDate);
			marriageState.PassStringToEmotionState("castleboy");
		}else {
			Debug.Log("lighthouse girl stoodup!");
			FlagManager.instance.SetFlag(FlagStrings.StoodUp);
		}

	}
	
	protected void SendFarmerOnBoard(){
		initialState.PassStringToEmotionState(FlagStrings.FarmerOnBoard);
	}
	
	#region EmotionStates
	private class StartingEmotionState : EmotionState{
		public StartingEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
		}
	}
	
	#region Initial Emotion State
	private class InitialEmotionState : EmotionState{
		Choice GoOnChoice = new Choice("Go on", "");
		Choice OkChoice = new Choice("Ok", "I want to try and find ways to get the carpenter and my mom upset with each other.");
		Choice ContinueChoice = new Choice("Continue", "Then hopefully my mom will call things off...You on board with this plan?");
		Choice PlanChoice = new Choice("What's the Plan?", "Promise you won't tell anyone?");
		Choice MarriageChoice = new Choice("What marriage?", "My mother wants me to marry the carpenter's son, so that i can 'settle down'. But i would rather die than marry someone I don't love!  Well dying might be a bit extreme...");
		Choice YesChoice = new Choice("Yes", "Alright! So see if you can get him to cut down my mother's favorite tree, or steal his tools and give them to me...");
		Choice AnotherTimeChoice = new Choice("Maybe \nanother time", "Another time...");
		Choice CarpenterChoice = new Choice("Carpenter's Son", "Oh. Him...I'm shocked he could even muster up the ability to write two words, much less this letter.");
		Choice CastleManChoice = new Choice("Castle man", "If you're seeing this...you have entered..the twilight zone!!!");
		Choice NotBadChoice = new Choice("He's not THAT bad...", "Hmmph. Yeah right! He's just a thick headed barbarian, just like Genghis Khan in all of the stories I've read. I'll have nothing to do with him!");
		Choice YourRightChoice = new Choice("You're right", "Such a nice letter.");
		Choice TalkedChoice = new Choice("Have you even talked with him?", "No...But I'm sure that I'm right! My books have never proved me worng! In fact if you help me find a way to sneak out without my parents seeing, I'll prive it to you!");
		
		Reaction GoOnReaction = new Reaction();
		Reaction OkReaction = new Reaction();
		Reaction ContinueReaction = new Reaction();
		Reaction PlanReaction = new Reaction();
		Reaction MarriageReaction = new Reaction();
		Reaction YesReaction = new Reaction();
		Reaction AnotherTimeReaction = new Reaction();
		Reaction CarpenterReaction = new Reaction();
		Reaction CastleManReaction = new Reaction();
		Reaction NotBadReaction = new Reaction();
		Reaction YourRightReaction = new Reaction();
		Reaction TalkedReaction = new Reaction();
		
		Reaction NoteReaction = new Reaction();
		Reaction RopeReaction = new Reaction();
		Reaction ToolsReaction = new Reaction();
		
		bool planStarted = false;
		public bool carpenterPath = false;
		bool gaveTools = false;
		bool waitingDateFlag = false;
		bool castleBoyInsane = true;
		
		NPC control;
		
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			control = toControl;
			SetupInteractions();
			
			_allChoiceReactions.Add(PlanChoice,new DispositionDependentReaction(PlanReaction));
			_allChoiceReactions.Add(MarriageChoice,new DispositionDependentReaction(MarriageReaction));
			
			_allItemReactions.Add(StringsItem.Note, new DispositionDependentReaction(NoteReaction));
			
		}
		
		public void SetupInteractions(){
			OkReaction.AddAction(new NPCCallbackAction(OkResponse));
			ContinueReaction.AddAction(new NPCCallbackAction(ContinueResponse));
			PlanReaction.AddAction(new NPCCallbackAction(PlanResponse));
			MarriageReaction.AddAction(new NPCCallbackAction(MarriageResponse));
			YesReaction.AddAction(new NPCCallbackAction(YesResponse));
			AnotherTimeReaction.AddAction(new NPCCallbackAction(AnotherTimeResponse));
			CarpenterReaction.AddAction(new NPCCallbackAction(CarpenterResponse));
			CastleManReaction.AddAction(new NPCCallbackAction(CastleManResponse));
			NotBadReaction.AddAction(new NPCCallbackAction(NotBadResponse));
			YourRightReaction.AddAction(new NPCCallbackAction(YourRightResponse));
			TalkedReaction.AddAction(new NPCCallbackAction(TalkedResponse));


			
			NoteReaction.AddAction(new NPCCallbackAction(NoteResponse));
			NoteReaction.AddAction(new UpdateCurrentTextAction(control, "A romantic note? Perhaps there is hope! Who wrote this?"));
			NoteReaction.AddAction(new NPCTakeItemAction(control));
			
			RopeReaction.AddAction(new NPCCallbackAction(RopeResponse));
			RopeReaction.AddAction(new UpdateCurrentTextAction(control, "Okay. I'll sneak off the farm using the rope to scale down the cliff. You go tell the Carpenter's son to go meet me on the beach."));
			RopeReaction.AddAction(new NPCTakeItemAction(control));
			
			ToolsReaction.AddAction(new NPCCallbackAction(ToolsResponse));
			ToolsReaction.AddAction(new UpdateCurrentTextAction(control, "Everything's finished! Time to put this plan into effect!"));
			ToolsReaction.AddAction(new NPCTakeItemAction(control));
		}
		
		public override void PassStringToEmotionState(string text){
			if (text == FlagStrings.NotInsane)
				CastleManChoice = new Choice("Castle man", "Aww...how sweet. He's such a nice guy. I wish my parents had promised me for him no the carpenter's son...I'm gonna meet with him! The only castch is I need a way to sneak out. Lets see if we can figure something out...");
				castleBoyInsane = false;
			if (text == FlagStrings.FarmerOnBoard){ //bypass rope requirement
				CastleManChoice = new Choice("Castle man", "Aww...how sweet. He's such a nice guy. I wish my parents had promised me for him no the carpenter's son...I'm gonna meet with him! Go tell the Castle Man to go meet me on the beach.");
				CastleManReaction.Clear();
				CastleManReaction.AddAction(new NPCCallbackAction(RopeResponse));
				Choice TalkedChoice = new Choice("Have you even talked with him?", "No...But I'm sure that I'm right! My books have never proved me worng! I'll prove it to you! Go tell the Carpenter's son to go meet me on the beach.");
				TalkedReaction.Clear();
				TalkedReaction.AddAction(new NPCCallbackAction(RopeResponse));
			}
		}
		
		public void TalkedResponse(){
			_allChoiceReactions.Clear();
			_allItemReactions.Add(StringsItem.Rope, new DispositionDependentReaction(RopeReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I need a way to sneak out without my parents seeing.");
		}
		public void ToolsResponse(){
			_allChoiceReactions.Clear();
			
			if (!gaveTools){
				gaveTools = true;
				FlagManager.instance.SetFlag(FlagStrings.ToolsToGirl);
			}
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("ARRGGHH. I can't believe that didn't work. I've tried everything to get out of this! Why can't it just be like in the stories where the hero always wins!");
		}
		public void RopeResponse(){
			if (carpenterPath){
				SetDefaultText("Tell the carpenter's son to meet me at the beach!");
				FlagManager.instance.SetFlag(FlagStrings.CarpenterDate);
			}else{
				SetDefaultText("Tell the castleboy to meet me at the beach!");
				FlagManager.instance.SetFlag(FlagStrings.CastleDate);
			}
			if (!waitingDateFlag)
				FlagManager.instance.SetFlag(FlagStrings.WaitForPlayerBeforeRope);
				//FlagManager.instance.SetFlag(FlagStrings.WaitingForDate);
		}
		public void NotBadResponse(){
			_allChoiceReactions.Clear();
			_allItemReactions.Clear();
			
			_allChoiceReactions.Add(TalkedChoice,new DispositionDependentReaction(TalkedReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("The carpenter is a thick headed barbarian. I'll have nothing to do with him!");
		}
		public void YourRightResponse(){
			_allChoiceReactions.Clear();
			if (planStarted){
				SetDefaultText("Exactly! There's no time worth wasting. Now to get back to our plan and ruin this marriage!");
				
				_allItemReactions.Add(StringsItem.Toolbox, new DispositionDependentReaction(ToolsReaction));
				GUIManager.Instance.RefreshInteraction();
				SetDefaultText("Alright! So see if you can get him to cut down my mother's favorite tree?, or steal his tools and give them to me...");
			}else {
				SetDefaultText("So, now that you understand me a bit better, how bout we go back to that plan of mine?");
				_allChoiceReactions.Add(MarriageChoice,new DispositionDependentReaction(MarriageReaction));
				_allChoiceReactions.Add(PlanChoice,new DispositionDependentReaction(PlanReaction));
				GUIManager.Instance.RefreshInteraction();
				SetDefaultText("Hi! Would you mind helping me out? I need to get out of my arranged marriage, but need help with distracting my mom to make it work!");
			}
			
		}
		public void CarpenterResponse(){
			_allChoiceReactions.Clear();
			
			carpenterPath = true;
			_allChoiceReactions.Add(NotBadChoice,new DispositionDependentReaction(NotBadReaction));
			_allChoiceReactions.Add(YourRightChoice,new DispositionDependentReaction(YourRightReaction));
			
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I don't believe the carpenter wrote this letter.");
		}
		public void CastleManResponse(){
			_allChoiceReactions.Clear();
			_allItemReactions.Clear();
			FlagManager.instance.SetFlag(FlagStrings.NotInsane);
			if (castleBoyInsane){
				SetDefaultText("Such a nice letter. Too bad hes crazy.");
				YourRightResponse();
			}else {
				_allItemReactions.Add(StringsItem.Rope, new DispositionDependentReaction(RopeReaction));
				SetDefaultText("Aww...how sweet. He's such a nice guy. I wish my parents had promised me for him not the carpenter's son.. I'm gonna meet with him! The only castch is I need a way to sneak out. Lets see if we can figure something out...");
			}
			GUIManager.Instance.RefreshInteraction();
		}
		#region response func
		public void NoteResponse(){
			_allChoiceReactions.Clear();
			
			_allChoiceReactions.Add(CarpenterChoice,new DispositionDependentReaction(CarpenterReaction));
			_allChoiceReactions.Add(CastleManChoice,new DispositionDependentReaction(CastleManReaction));
			
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("So who wrote the note?");
		}
		
		public void GoOnResponse(){		}
		public void OkResponse(){
			_allChoiceReactions.Remove(OkChoice);

			_allChoiceReactions.Add(ContinueChoice,new DispositionDependentReaction(ContinueReaction));
			
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I want you to try and find ways to get the carpenter and my mom upset with each other");
		}
		public void ContinueResponse(){
			_allChoiceReactions.Remove(ContinueChoice);
			
			_allChoiceReactions.Add(AnotherTimeChoice,new DispositionDependentReaction(AnotherTimeReaction));
			_allChoiceReactions.Add(YesChoice,new DispositionDependentReaction(YesReaction));
			
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("You on board with this plan?");
		}
		public void PlanResponse(){
			_allChoiceReactions.Clear();
			_allItemReactions.Clear();
			
			planStarted = true;
			_allChoiceReactions.Add(OkChoice,new DispositionDependentReaction(OkReaction));
			
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Promise you don't tell anyone about this?");
			
			FlagManager.instance.SetFlag(FlagStrings.CastleDate);	//test
			FlagManager.instance.SetFlag(FlagStrings.WaitForPlayerBeforeRope); //test
		}
		public void MarriageResponse(){	
			_allChoiceReactions.Remove(PlanChoice);
			_allChoiceReactions.Remove(MarriageChoice);
			GUIManager.Instance.RefreshInteraction();
			
			_allChoiceReactions.Add(PlanChoice,new DispositionDependentReaction(PlanReaction));
			_allChoiceReactions.Add(MarriageChoice,new DispositionDependentReaction(MarriageReaction));
		}
		public void YesResponse(){
			_allChoiceReactions.Clear();
			_allItemReactions.Clear();
			
			_allItemReactions.Add(StringsItem.Note, new DispositionDependentReaction(NoteReaction));
			_allItemReactions.Add(StringsItem.Toolbox, new DispositionDependentReaction(ToolsReaction));
			SetDefaultText("Steal the carpenter's tools for me...");
			
			GUIManager.Instance.RefreshInteraction();
		}
		public void AnotherTimeResponse(){
			_allChoiceReactions.Remove(YesChoice);
			_allChoiceReactions.Remove(AnotherTimeChoice);
			GUIManager.Instance.RefreshInteraction();
			_allChoiceReactions.Add(YesChoice, new DispositionDependentReaction(YesReaction));
			_allChoiceReactions.Add(AnotherTimeChoice, new DispositionDependentReaction(AnotherTimeReaction));
		}
		
		public override void UpdateEmotionState(){
			
		}
		#endregion
	
	}
	private class AntiMarriageEmotionState : EmotionState{
		Choice GiveUpChoice = new Choice ("Why give up now?", "I'm not!  It just never works.  I wish there was someone who was willing to help me with this...");
		Choice NotSoBadChoice  = new Choice("Maybe its not so bad?", "Yeah Right!  There is no way  that's happening!  Anyways I'm out of ideas, so I need time to think of more ways to sabotage this marriage.");
		Choice AnyoneNiceChoice = new Choice("Isn't there anyone who is nice to you?","That's it!  My dad!  He's always been kind to me! But he's too afraid to stand up to my mom...");
		Choice NiceToMomChoice = new Choice("Have you tried being nice to your mom", "It wouldn't work.  Once she has an idea in mind, she won't change it!");
		
		Reaction GiveUpReaction = new Reaction();
		Reaction NotSoBadReaction = new Reaction();
		Reaction AnyoneNiceReaction = new Reaction();
		Reaction NiceToMomReaction = new Reaction();
		
		Reaction ToySwordReaction = new Reaction();
		Reaction CaptainsLogReaction = new Reaction();
		
		NPC control;
		public 	AntiMarriageEmotionState(NPC toControl, string currentDialogue): base (toControl, "ARRGGHH.  I can't believe that didn't work.  I've tried everything to get out of this!  Why can't it just be like in the stories where the hero always wins!"){
			control = toControl;
			
			SetupReactions();
			
			_allChoiceReactions.Add(GiveUpChoice,new DispositionDependentReaction(GiveUpReaction));
			_allChoiceReactions.Add(NotSoBadChoice,new DispositionDependentReaction(NotSoBadReaction));
			
			_allItemReactions.Add(StringsItem.ToySword, new DispositionDependentReaction(ToySwordReaction));
			_allItemReactions.Add(StringsItem.CaptainsLog, new DispositionDependentReaction(CaptainsLogReaction));
			
		}
		
		public void SetupReactions(){
			GiveUpReaction.AddAction(new NPCCallbackAction(GiveUpResponse));
			NotSoBadReaction.AddAction(new NPCCallbackAction(NotSoBadResponse));
			AnyoneNiceReaction.AddAction(new NPCCallbackAction(AnyoneNiceResponse));
			NiceToMomReaction.AddAction(new NPCCallbackAction(NiceToMomResponse));
			
			ToySwordReaction.AddAction(new NPCCallbackAction(ToySwordResponse));
			ToySwordReaction.AddAction(new UpdateCurrentTextAction(control, "A toy sword?  You know I wished I could play with a toy sword when I was a kid so that I could be like the heroes in my dad's stories..."));
			ToySwordReaction.AddAction(new NPCTakeItemAction(control));
			
			CaptainsLogReaction.AddAction(new NPCCallbackAction(CaptainsLogResponse));
			CaptainsLogReaction.AddAction(new UpdateCurrentTextAction(control, "A captain's log....and then stranded on an island I managed to become friendly with the natives... You know this reminds me of the stories my dad would tell me as a kid..."));
			CaptainsLogReaction.AddAction(new NPCTakeItemAction(control));
			
		}
		
		public void ToySwordResponse(){}
		public void CaptainsLogResponse(){}
		
		public void GiveUpResponse(){
			 _allChoiceReactions.Clear();
			_allChoiceReactions.Add(AnyoneNiceChoice,new DispositionDependentReaction(AnyoneNiceReaction));
			_allChoiceReactions.Add(NiceToMomChoice,new DispositionDependentReaction(NiceToMomReaction));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I wish there was someone who was willing to help me with this...");
		}
		public void NotSoBadResponse(){
			_allChoiceReactions.Clear();
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("I need time to think of more ways to sabotage this marriage.");
		}
		public void AnyoneNiceResponse(){
			_allChoiceReactions.Clear();
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("My dad is too afraid to stand up to my mom...");
		}
		public void NiceToMomResponse(){
			_allChoiceReactions.Clear();
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("My mom will never back down.");
		}
		
	}
	
	private class ProMarriageEmotionState : EmotionState {
		bool gaveTools = false;
		
		Reaction ToolsReaction = new Reaction();
		public ProMarriageEmotionState( NPC toControl, string currentDialogue): base (toControl, "That was amazing! True love like in the stories! All I have to do is convince my parents that this is a good idea...Hmm...well time to sabotage the marriage!"){
			
			ToolsReaction.AddAction(new NPCCallbackAction(ToolsResponse));
			ToolsReaction.AddAction(new UpdateCurrentTextAction(toControl, "Everything's finished! Time to put this plan into effect!"));
			ToolsReaction.AddAction(new NPCTakeItemAction(toControl));
			
			_allItemReactions.Add(StringsItem.Toolbox, new DispositionDependentReaction(ToolsReaction));
		}
		
		public void ToolsResponse(){
			_allChoiceReactions.Clear();
			_allItemReactions.Clear();
			if (!gaveTools){
				gaveTools = true;
				FlagManager.instance.SetFlag(FlagStrings.ToolsForMarriage);
			}
		}
		
		public override void PassStringToEmotionState(string text){
			if (text == "carpenter"){
				_allItemReactions.Clear();
				SetDefaultText("That was amazing! True love like in the stories!");
			}
		}
		
	}
	
	private class StoodUpEmotionState : EmotionState{
		Reaction ToolsReaction = new Reaction();
		Reaction randomMessage;
		bool gaveTools = false;
		public StoodUpEmotionState(NPC toControl, string currentDialogue) : base(toControl, "He stood me up! I can't believe it! That's what you get for trusting men. I'm definitely not going to marry now!"){
		
			ToolsReaction.AddAction(new NPCCallbackAction(ToolsResponse));
			ToolsReaction.AddAction(new UpdateCurrentTextAction(toControl, "Everything's finished! Time to put this plan into effect!"));
			ToolsReaction.AddAction(new NPCTakeItemAction(toControl));
			
			_allItemReactions.Add(StringsItem.Toolbox, new DispositionDependentReaction(ToolsReaction));
			
			//randomMessage.AddAction(new NPCCallbackAction(RandomMessage));
			//SetOnOpenInteractionReaction(new DispositionDependentReaction(randomMessage));
			
		}
		
		public void RandomMessage(){
			SetDefaultText("Get me some tools so I can sabotage this marriage!");
		}
		
		public void ToolsResponse(){
			_allChoiceReactions.Clear();
			
			if (!gaveTools){
				gaveTools = true;
				FlagManager.instance.SetFlag(FlagStrings.ToolsToGirl);
			}
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("ARRGGHH. I can't believe that didn't work. I've tried everything to get out of this! Why can't it just be like in the stories where the hero always wins!");
		}

	}
	
	private class GoneEmotionState : EmotionState{
		public GoneEmotionState(NPC toControl, string currentDialogue) : base(toControl, ""){
		}

	}
	
	#endregion
	#endregion
}
