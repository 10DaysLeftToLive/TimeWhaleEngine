using UnityEngine;
using System.Collections;

/// <summary>
/// LighthouseGirl Middle specific scripting values
/// </summary>
public class LighthouseGirlMiddle : NPC {
	InitialEmotionState initialState;
	Vector3 startingPosition;
	protected override void Init() {
		id = NPCIDs.LIGHTHOUSE_GIRL;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		Reaction moveAway = new Reaction();
		moveAway.AddAction(new NPCCallbackAction(ResetPosition));
		moveAway.AddAction(new NPCEmotionUpdateAction(this, initialState));
		flagReactions.Add(FlagStrings.FarmAlive, moveAway);
	}
	
	protected override EmotionState GetInitEmotionState(){
		initialState = new InitialEmotionState(this, "Hi! Would you mind helping me out? I need to get out of my arranged marriage, but need help with distracting my mom to make it work!");
		startingPosition = transform.position;
		startingPosition.y += LevelManager.levelYOffSetFromCenter;
		this.transform.position = new Vector3(200,0,0);
		return (new GoneEmotionState(this, ""));
	}
	
	Schedule openningWaitingSchedule;
	Schedule postOpenningSchedule;
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}

	protected override void SetUpSchedules(){
		
		/*openningWaitingSchedule = new Schedule(this, Schedule.priorityEnum.DoNow);
		openningWaitingSchedule.Add(new TimeTask(30, new WaitTillPlayerCloseState(this, player)));
		scheduleStack.Add(openningWaitingSchedule);*/
		
		/*postOpenningSchedule = new Schedule(this,Schedule.priorityEnum.Medium);
		postOpenningSchedule.Add(new TimeTask(10, new MoveThenDoState(this, new Vector3(transform.position.x - 10, transform.position.y, transform.position.z), new IdleState(this))));
		postOpenningSchedule.Add(new TimeTask(30, new ChangeEmotionState(this, initialState)));
		scheduleStack.Add(postOpenningSchedule);*/
		
		
		//scheduleStack.Add(new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.FarmerFatherMiddle), 
		//	new YoungFarmerMotherToFarmerFatherOpenningScriptedDialogue(),Schedule.priorityEnum.High));
		
	}
	
	protected void ResetPosition(){
		this.transform.position = startingPosition;	
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
		Choice YourRightChoice = new Choice("You're right", "If you're seeing this...you have entered..the twilight zone!!!");
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
		bool carpenterPath = false;
		NPC control;
		
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			control = toControl;
			SetupInteractions();
			
			_allChoiceReactions.Add(PlanChoice,new DispositionDependentReaction(PlanReaction));
			
			_allChoiceReactions.Add(MarriageChoice,new DispositionDependentReaction(MarriageReaction));
			
			_allItemReactions.Add(StringsItem.Note, new DispositionDependentReaction(NoteReaction));
		}
		
		public void SetupInteractions(){
			PlanReaction.AddAction(new NPCCallbackAction(PlanResponse));
			MarriageReaction.AddAction(new NPCCallbackAction(MarriageResponse));
			NotBadReaction.AddAction(new NPCCallbackAction(NotBadResponse));
			YourRightReaction.AddAction(new NPCCallbackAction(YourRightResponse));
			CarpenterReaction.AddAction(new NPCCallbackAction(CarpenterResponse));
			CastleManReaction.AddAction(new NPCCallbackAction(CastleManResponse));
			ContinueReaction.AddAction(new NPCCallbackAction(ContinueResponse));
			AnotherTimeReaction.AddAction(new NPCCallbackAction(AnotherTimeResponse));
			YesReaction.AddAction(new NPCCallbackAction(YesResponse));
			OkReaction.AddAction(new NPCCallbackAction(OkResponse));
			
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
		
		public void TalkedResponse(){}
		public void ToolsResponse(){}
		public void RopeResponse(){}
		public void NotBadResponse(){
			_allChoiceReactions.Clear();
			_allItemReactions.Clear();
			
			_allItemReactions.Add(StringsItem.Toolbox, new DispositionDependentReaction(ToolsReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		public void YourRightResponse(){
			_allChoiceReactions.Clear();
			if (planStarted){
				SetDefaultText("Exactly! There's no time worth wasting. Now to get back to our plan and ruin this marriage!");
				
				_allItemReactions.Add(StringsItem.Toolbox, new DispositionDependentReaction(ToolsReaction));
				GUIManager.Instance.RefreshInteraction();
				SetDefaultText("Alright! So see if you can get him to cut down my mother's favorite tree?, or steal his tools and give them to me...");
			}else {
				SetDefaultText("So, now that  you understand me a bit better, how bout we go back to that plan of mine?");
				_allChoiceReactions.Add(MarriageChoice,new DispositionDependentReaction(MarriageReaction));
				_allChoiceReactions.Add(PlanChoice,new DispositionDependentReaction(PlanReaction));
				GUIManager.Instance.RefreshInteraction();
				SetDefaultText("Hi! Would you mind helping me out? I need to get out of my arranged marriage, but need help with distracting my mom to make it work!");
			}
			
		}
		#region response func
		public void CarpenterResponse(){
			_allChoiceReactions.Clear();
			
			carpenterPath = true;
			_allChoiceReactions.Add(NotBadChoice,new DispositionDependentReaction(NotBadReaction));
			_allChoiceReactions.Add(YourRightChoice,new DispositionDependentReaction(YourRightReaction));
			
			GUIManager.Instance.RefreshInteraction();
			//if castleman is insane go somewhere
		}
		public void CastleManResponse(){
			RopeReaction.AddAction(new UpdateCurrentTextAction(control, "Okay. I'll sneak off the farm using the rope to scale down the cliff. You go tell the Castle Man to go meet me on the beach."));
			//if castleman is insane go somewhere
			
		}
		public void NoteResponse(){
			_allChoiceReactions.Clear();
			
			_allChoiceReactions.Add(CarpenterChoice,new DispositionDependentReaction(CarpenterReaction));
			_allChoiceReactions.Add(CastleManChoice,new DispositionDependentReaction(CastleManReaction));
			
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void GoOnResponse(){		}
		public void OkResponse(){
			_allChoiceReactions.Remove(OkChoice);

			_allChoiceReactions.Add(ContinueChoice,new DispositionDependentReaction(ContinueReaction));
			
			GUIManager.Instance.RefreshInteraction();
		}
		public void ContinueResponse(){
			_allChoiceReactions.Remove(ContinueChoice);
			
			_allChoiceReactions.Add(AnotherTimeChoice,new DispositionDependentReaction(AnotherTimeReaction));
			_allChoiceReactions.Add(YesChoice,new DispositionDependentReaction(YesReaction));
			
			GUIManager.Instance.RefreshInteraction();
		}
		public void PlanResponse(){
			_allChoiceReactions.Clear();
			_allItemReactions.Clear();
			
			planStarted = true;
			_allChoiceReactions.Add(OkChoice,new DispositionDependentReaction(OkReaction));
			
			GUIManager.Instance.RefreshInteraction();
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
			
			GUIManager.Instance.RefreshInteraction();
		}
		public void AnotherTimeResponse(){
			_allChoiceReactions.Remove(YesChoice);
			_allChoiceReactions.Remove(AnotherTimeChoice);
			GUIManager.Instance.RefreshInteraction();
		}
		
		public override void UpdateEmotionState(){
			
		}
		#endregion
	
	}
	private class AntiMarriage : EmotionState{
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
		public 	AntiMarriage(NPC toControl, string currentDialogue): base (toControl, "ARRGGHH.  I can't believe that didn't work.  I've tried everything to get out of this!  Why can't it just be like in the stories where the hero always wins!"){
			control = toControl;
			
			SetupReactions();
			
			_allChoiceReactions.Add(GiveUpChoice,new DispositionDependentReaction(GiveUpReaction));
			_allChoiceReactions.Add(NotSoBadChoice,new DispositionDependentReaction(NotSoBadReaction));
			
			//_allItemReactions.Add(StringsItem.ToySword, new DispositionDependentReaction(AnyoneNiceResponse));
			//_allItemReactions.Add(StringsItem.CaptainsLog, new DispositionDependentReaction(AnyoneNiceResponse));
			
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
	
	private class GoneEmotionState : EmotionState{
		public GoneEmotionState(NPC toControl, string currentDialogue) : base(toControl, ""){
		}

	}
	
	#endregion
	#endregion
}
