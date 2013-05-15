using UnityEngine;
using System.Collections;

/// <summary>
/// LighthouseGirl Middle specific scripting values
/// </summary>
public class LighthouseGirlMiddle : NPC {
	InitialEmotionState initialState;
	protected override void Init() {
		id = NPCIDs.LIGHTHOUSE_GIRL;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		
	}
	
	protected override EmotionState GetInitEmotionState(){
		initialState = new InitialEmotionState(this, "Hi! Would you mind helping me out? I need to get out of my arranged marriage, but need help with distracting my mom to make it work!");
		return (initialState);
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
			
			_allItemReactions.Add(StringsItem.ToolBox, new DispositionDependentReaction(ToolsReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		public void YourRightResponse(){
			_allChoiceReactions.Clear();
			if (planStarted){
				SetDefaultText("Exactly! There's no time worth wasting. Now to get back to our plan and ruin this marriage!");
				
				_allItemReactions.Add(StringsItem.ToolBox, new DispositionDependentReaction(ToolsReaction));
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
			_allItemReactions.Add(StringsItem.ToolBox, new DispositionDependentReaction(ToolsReaction));
			
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
	#endregion
	#endregion
}
