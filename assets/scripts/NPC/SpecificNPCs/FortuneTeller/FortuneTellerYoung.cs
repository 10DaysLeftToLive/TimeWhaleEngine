using UnityEngine;
using System.Collections;

/// <summary>
/// FortuneTellerYoung specific scripting values
/// </summary>
public class FortuneTellerYoung : NPC {
	protected override void Init() {
		id = NPCIDs.FORTUNE_TELLER;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		#region Race To Carpenter House
		/*
		Reaction raceToCarpenterHouse = new Reaction();
		raceToCarpenterHouse.AddAction(new NPCEmotionUpdateAction(this, new InitialEmotionState(this, "Careful, the Carpenter is really mean!")));
		raceToCarpenterHouse.AddAction(new ShowOneOffChatAction(this, "These are our neighbors!", 2f));
		raceToCarpenterHouse.AddAction(new NPCAddScheduleAction(this, carpenterRaceSchedule));
		flagReactions.Add(FlagStrings.RunToCarpenter, raceToCarpenterHouse);
		*/
		#endregion
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Hey there stranger~ Care for your fortune?"));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}

	protected override void SetUpSchedules(){
		
	}
	
	// Hey there stranger~ Care for your fortune?
	// sure! ==> Enter BeginFortuneState
	// no thanks ==> Ok.., maybe later.
	
	
	// Whatever you do, don't leave until we're entirely finished.
	
	//Hmmmmmm .. *closes eyes* .. your path lay with several important decisions.
	//Let me ask a few questions to better see what you will do.
	
	// If a close friend
	// Do you take the left path or the right path? - Left, Right, Neither
	// Would you rather work alone or work together? - Alone, Together, Work?
	// If you could only save one person, who would it be? - A lover, A sibling, a stranger
	// 
	
	// Now as payment, I'll need something delicious to snack on.
	
#region EmotionStates
	#region Initial Emotion State
	private class InitialEmotionState : EmotionState{
		Choice acceptFortuneChoice = new Choice("Sure.", "Let's begin~");
		Choice declineFortuneChoice = new Choice("No thanks.", "C'mon, it'll be fun!");
		Choice acceptReluctantChoice = new Choice ("Ok..", "That's the spirit!");
		
		Reaction acceptFortuneState = new Reaction();
		Reaction acceptReluctantState = new Reaction();
		Reaction declineFortuneState = new Reaction();
		Reaction fullDeclineState = new Reaction();
		
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue) {
			acceptFortuneState.AddAction(new NPCEmotionUpdateAction(toControl, new BeginFortuneState(toControl, "Hmmm... your path lay with several important decisions. Take a moment, then tell me when you're ready~")));
			acceptFortuneState.AddAction(new NPCCallbackAction(UpdateAcceptedChoices));
			
			acceptReluctantState.AddAction(new NPCEmotionUpdateAction(toControl, new BeginFortuneState(toControl, "Hmmm... your path lay with several important decisions. Take a moment, then tell me when you're ready~")));
			
			declineFortuneState.AddAction(new NPCCallbackAction(UpdateDeclinedChoices));
			
			fullDeclineState.AddAction(new NPCEmotionUpdateAction(toControl, new StillConvincingState(toControl,"Ready for your fortune now?")));
			fullDeclineState.AddAction(new NPCCallbackOnNPCAction(setCurrentText, toControl));
			
			_allChoiceReactions.Add(acceptFortuneChoice, new DispositionDependentReaction(acceptFortuneState));
			_allChoiceReactions.Add(declineFortuneChoice, new DispositionDependentReaction(declineFortuneState));
		}
		
		public void UpdateAcceptedChoices() {
		//Choice startFortune = new Choice("Let's get started", "Hmmmmm .. your path lay with several important decisions.");
		//Choice resistFortune = new Choice("Questions?", " ");
			_allChoiceReactions.Remove(declineFortuneChoice);
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void setCurrentText(NPC toControl) {
			SetDefaultText("Comeback Later, k?");
			_allChoiceReactions.Remove(acceptReluctantChoice);
			_allChoiceReactions.Remove(declineFortuneChoice);
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void UpdateDeclinedChoices() {
		//Choice startFortune = new Choice("Let's get started", "Hmmmmm .. your path lay with several important decisions.");
		//Choice resistFortune = new Choice("Questions?", " ");
			_allChoiceReactions.Remove(declineFortuneChoice);
			_allChoiceReactions.Remove(acceptFortuneChoice);
			//GUIManager.Instance.RefreshInteraction();
			declineFortuneChoice = new Choice ("Maybe later..", "Come back later then, k?");
			_allChoiceReactions.Add(acceptReluctantChoice, new DispositionDependentReaction(acceptReluctantState)); 
			_allChoiceReactions.Add(declineFortuneChoice, new DispositionDependentReaction(fullDeclineState));
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("You know you want to!");
		}
	}
	#endregion
	#region Initial Emotion State

	// Do you take the left path or the right path? - Left, Right, Neither
	// Would you rather work alone or work together? - Alone, Together, Work?
	// If you could only save one person, who would it be? - A lover, A sibling, a stranger
	// 
	private class BeginFortuneState : EmotionState {
		Choice leftChoice = new Choice("Left", "Mm, interesting.");
		Choice rightChoice = new Choice("Right", "I see.");
		Choice aloneChoice = new Choice ("Alone", "Alone... Sometimes alone makes life easier");
		Choice togetherChoice = new Choice ("Together", "Always with friends...Support.");
		Choice saveLoverChoice = new Choice ("Lover", "The one true love... worth more than the world.");
		Choice saveFamilyChoice = new Choice ("Sibling", "Family first... an unwritten trust.");
		Choice saveLeaderChoice = new Choice ("Leader","Noble...a stranger with potential.");
//Add more choices 
		
		Reaction leftReaction = new Reaction();
		Reaction rightReaction = new Reaction();
		Reaction aloneReaction = new Reaction();
		Reaction togetherReaction = new Reaction();
		Reaction saveLoverReaction = new Reaction();
		Reaction saveFamilyReaction = new Reaction();
		Reaction saveLeaderReaction = new Reaction();
		
		public BeginFortuneState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue) {
			leftReaction.AddAction(new NPCCallbackAction(updateChoice));
			rightReaction.AddAction(new NPCCallbackAction(updateChoice));
		
			_allChoiceReactions.Add(leftChoice, new DispositionDependentReaction(leftReaction));
			_allChoiceReactions.Add(rightChoice, new DispositionDependentReaction(rightReaction));
		}
		
		public void updateChoice() {
			_allChoiceReactions.Remove(leftChoice);
			_allChoiceReactions.Remove(rightChoice);
			
			//NotYetChoice = new Choice ("Not yet.", "Ok, come back soon! Predictions come and go you know!");
			_allChoiceReactions.Add(aloneChoice, new DispositionDependentReaction(aloneReaction));
			_allChoiceReactions.Add(togetherChoice, new DispositionDependentReaction(togetherReaction));
			
			//toControl.UpdateDefaultText("");
			GUIManager.Instance.RefreshInteraction();
		}
		/*
		public void updateChoice() {
			_allChoiceReactions.Remove(leftChoice);
			_allChoiceReactions.Remove(rightChoice);
			
			//NotYetChoice = new Choice ("Not yet.", "Ok, come back soon! Predictions come and go you know!");
			_allChoiceReactions.Add(aloneChoice, new DispositionDependentReaction(declineFortuneState));
			_allChoiceReactions.Add(togetherChoice, new DispositionDependentReaction(acceptFortuneState));
			
			//toControl.UpdateDefaultText("");
			GUIManager.Instance.RefreshInteraction();
			
		}
		
		public void updateChoice() {
			_allChoiceReactions.Remove(leftChoice);
			_allChoiceReactions.Remove(rightChoice);
			
			//NotYetChoice = new Choice ("Not yet.", "Ok, come back soon! Predictions come and go you know!");
			_allChoiceReactions.Add(aloneChoice, new DispositionDependentReaction(declineFortuneState));
			_allChoiceReactions.Add(togetherChoice, new DispositionDependentReaction(acceptFortuneState));
			
			//toControl.UpdateDefaultText("");
			GUIManager.Instance.RefreshInteraction();
			
		}
		*/
	}
	#endregion
				
	#region Initial Emotion State
	private class StillConvincingState : EmotionState {
		bool choicesActivated = false;
		Choice readyChoice = new Choice("I'm ready!", "Excellent choice!");
		Choice NotYetChoice = new Choice("Not yet.", "We aren't getting any younger!");
		Reaction activateChoices = new Reaction();
		Reaction acceptFortuneState = new Reaction();
		Reaction declineFortuneState = new Reaction();
		Reaction changeToGiveUpState = new Reaction();
		int numDeclines = 0;
		
		public StillConvincingState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue) {
//
		activateChoices.AddAction(new NPCCallbackAction(choicesOn));
		SetOnOpenInteractionReaction(new DispositionDependentReaction(activateChoices));
//
		acceptFortuneState.AddAction(new NPCEmotionUpdateAction(toControl, new BeginFortuneState(toControl, "Yay!")));
//
		declineFortuneState.AddAction(new NPCCallbackOnNPCAction(updateDecline, toControl));		
//
		changeToGiveUpState.AddAction(new NPCEmotionUpdateAction(toControl, new PostFortuneState(toControl, "The future is what we make of it...")));
//
		}
		
		public void choicesOn() {
			if (!choicesActivated) {
				_allChoiceReactions.Add(readyChoice, new DispositionDependentReaction(acceptFortuneState));
				_allChoiceReactions.Add(NotYetChoice, new DispositionDependentReaction(declineFortuneState));		
				choicesActivated = true;
			}
		}
		
		public void updateDecline(NPC toControl) {
			numDeclines++;
			if (numDeclines == 1) {
				_allChoiceReactions.Remove(readyChoice);
				_allChoiceReactions.Remove(NotYetChoice);
				GUIManager.Instance.RefreshInteraction();
				NotYetChoice = new Choice ("Not yet.", "Please!! I need practice!");
				
				_allChoiceReactions.Add(NotYetChoice, new DispositionDependentReaction(declineFortuneState));
				_allChoiceReactions.Add(readyChoice, new DispositionDependentReaction(acceptFortuneState));
			}
			
			if (numDeclines == 2) {
				_allChoiceReactions.Remove(readyChoice);
				_allChoiceReactions.Remove(NotYetChoice);
				toControl.UpdateDefaultText("You're back! Ready for your fortune?");
				GUIManager.Instance.RefreshInteraction();
				NotYetChoice = new Choice ("Not yet.", "Ok, come back soon! Predictions come and go you know!");
				
				_allChoiceReactions.Add(NotYetChoice, new DispositionDependentReaction(declineFortuneState));
				_allChoiceReactions.Add(readyChoice, new DispositionDependentReaction(acceptFortuneState));
			}
			
			if (numDeclines == 3) {
				_allChoiceReactions.Remove(readyChoice);
				_allChoiceReactions.Remove(NotYetChoice);
				toControl.UpdateDefaultText("I promise it won't be scary!");
				GUIManager.Instance.RefreshInteraction();
				NotYetChoice = new Choice ("Not yet.", "Ok... You win.");
				
				_allChoiceReactions.Add(NotYetChoice, new DispositionDependentReaction(declineFortuneState));
				_allChoiceReactions.Add(readyChoice, new DispositionDependentReaction(acceptFortuneState));
			}
			
			if (numDeclines == 4) {
				_allChoiceReactions.Remove(readyChoice);
				_allChoiceReactions.Remove(NotYetChoice);
				toControl.UpdateDefaultText("Please...?");
				GUIManager.Instance.RefreshInteraction();
				readyChoice = new Choice ("I'm ready!", "I'm glad you finally decided to join me~");
				NotYetChoice = new Choice ("Not yet.", "Good catch. I thought that trick would work.");
				
				_allChoiceReactions.Add(readyChoice, new DispositionDependentReaction(acceptFortuneState));
				_allChoiceReactions.Add(NotYetChoice, new DispositionDependentReaction(declineFortuneState));
			}
			
			if (numDeclines >= 5) {
				//FlagManager.instance.SetFlag(FlagStrings.MeanToFortuneteller);
				_allChoiceReactions.Remove(readyChoice);
				_allChoiceReactions.Remove(NotYetChoice);
				toControl.UpdateDefaultText("Ok... I'll find someone else do their fortune then..");
				GUIManager.Instance.RefreshInteraction();
				SetOnCloseInteractionReaction(new DispositionDependentReaction(changeToGiveUpState));
			}
		}
	}
	#endregion
	#region Initial Emotion State
	private class PostFortuneState : EmotionState {
		//Choice acceptFortuneChoice = new Choice("Sure.", "Excellent choice!");
		//Choice declineFortuneChoice = new Choice("No thanks.", "C'mon, it'll be fun!");
		//Reaction acceptFortuneState = new Reaction();
		//Reaction declineFortuneState = new Reaction();
		
		public PostFortuneState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue) {
			//acceptFortuneState.AddAction(new NPCEmotionUpdateAction(toControl, new State(toControl));
		//	acceptFortuneState.AddAction(new NPCCallbackAction());
		//	acceptFortuneState.AddAction();
			
			//_allChoiceReactions.Add(acceptFortuneChoice, new DispositionDependentReaction(acceptFortuneState));
			//_allChoiceReactions.Add(acceptFortuneChoice, new DispositionDependentReaction(declineFortuneState));
		}
	}
	#endregion
#endregion
}
