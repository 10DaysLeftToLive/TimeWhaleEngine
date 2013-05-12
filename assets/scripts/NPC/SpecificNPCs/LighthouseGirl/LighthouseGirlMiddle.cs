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
		return (new InitialEmotionState(this, "Eric is working here!"));
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
		Choice YesChoice = new Choice("Yes", "Alright! So see if you can get him to cut down my mother's favorite tree?, or steal his tools and give them to me...");
		Choice AnotherTimeChoice = new Choice("Maybe another time", "Another time...");
		
		Reaction GoOnReaction = new Reaction();
		Reaction OkReaction = new Reaction();
		Reaction ContinueReaction = new Reaction();
		Reaction PlanReaction = new Reaction();
		Reaction MarriageReaction = new Reaction();
		Reaction YesReaction = new Reaction();
		Reaction AnotherTimeReaction = new Reaction();
		
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
			PlanReaction.AddAction(new NPCCallbackAction(PlanResponse));
			_allChoiceReactions.Add(PlanChoice,new DispositionDependentReaction(PlanReaction));
			
			MarriageReaction.AddAction(new NPCCallbackAction(MarriageResponse));
			_allChoiceReactions.Add(MarriageChoice,new DispositionDependentReaction(MarriageReaction));
		
		}
		
		public void GoOnResponse(){		}
		public void OkResponse(){
			_allChoiceReactions.Remove(OkChoice);

			ContinueReaction.AddAction(new NPCCallbackAction(ContinueResponse));
			_allChoiceReactions.Add(ContinueChoice,new DispositionDependentReaction(ContinueReaction));
			
			GUIManager.Instance.RefreshInteraction();
		}
		public void ContinueResponse(){
			_allChoiceReactions.Remove(ContinueChoice);
			
			AnotherTimeReaction.AddAction(new NPCCallbackAction(AnotherTimeResponse));
			_allChoiceReactions.Add(AnotherTimeChoice,new DispositionDependentReaction(AnotherTimeReaction));
			
			YesReaction.AddAction(new NPCCallbackAction(YesResponse));
			_allChoiceReactions.Add(YesChoice,new DispositionDependentReaction(YesReaction));
			
			GUIManager.Instance.RefreshInteraction();
		}
		public void PlanResponse(){
			_allChoiceReactions.Remove(PlanChoice);
			_allChoiceReactions.Remove(MarriageChoice);
			
			OkReaction.AddAction(new NPCCallbackAction(OkResponse));
			_allChoiceReactions.Add(OkChoice,new DispositionDependentReaction(OkReaction));
			
			GUIManager.Instance.RefreshInteraction();
		}
		public void MarriageResponse(){	
		}
		public void YesResponse(){
			_allChoiceReactions.Remove(YesChoice);
			_allChoiceReactions.Remove(AnotherTimeChoice);
			GUIManager.Instance.RefreshInteraction();
		}
		public void AnotherTimeResponse(){
			_allChoiceReactions.Remove(YesChoice);
			_allChoiceReactions.Remove(AnotherTimeChoice);
			GUIManager.Instance.RefreshInteraction();
		}
		
		public override void UpdateEmotionState(){
			
		}
	
	}
	#endregion
	#endregion
}
