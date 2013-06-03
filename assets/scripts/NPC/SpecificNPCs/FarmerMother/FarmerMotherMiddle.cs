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
	protected override void Init() {
		id = NPCIDs.FARMER_MOTHER;
		base.Init();
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
	#endregion
	
	protected override void SetFlagReactions(){
		SetupReactions();
		flagReactions.Add(FlagStrings.FarmAlive, moveAway);
		flagReactions.Add(FlagStrings.NotSilly, notSilly);
		flagReactions.Add(FlagStrings.TellOnDaughter, tellOn);
		flagReactions.Add(FlagStrings.StoriesAreSilly, sillyStories);
		flagReactions.Add(FlagStrings.YourRight, yourRight);
		flagReactions.Add(FlagStrings.WorkAndStories, workandRead);
		flagReactions.Add(FlagStrings.GiveAppleToFarmer, apple);
		flagReactions.Add(FlagStrings.GiveApplePieToFarmer, applePie);
		flagReactions.Add(FlagStrings.GiveShovelToFarmer, shovel);
		flagReactions.Add(FlagStrings.GiveRopeToFarmer, rope);
		
		flagReactions.Add(FlagStrings.HusbandOnBoard, husbandOnBoard);
	}
	
	protected override EmotionState GetInitEmotionState(){
		initialState = new InitialEmotionState(this,"What do you want? I'm busy." );
		startingPosition = transform.position;
		startingPosition.y += LevelManager.levelYOffSetFromCenter;
		this.transform.position = new Vector3(200,0,0);
		return (new GoneEmotionState(this, ""));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}

	protected override void SetUpSchedules(){
		
	}
	
	protected void ResetPosition(){
		this.transform.position = startingPosition;	
	}
	
	protected void ChangeDisposition(NPC npc, string disp){
		disposition += int.Parse(disp);
		if (disposition > 70){
			initialState.PassStringToEmotionState("happy");
		}
	}
	
	protected void ChangeHusbandStatus(){
		husbandReady = true;
		if (disposition > 70 && !flagSet){
			FlagManager.instance.SetFlag(FlagStrings.FarmerOnBoard);
			flagSet = true;
		}
	}
	
	protected void FlagToNPC(NPC npc, string text){
		if (text == "castle"){
			carpenterDate = false;	
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
		castleDate.AddAction(new NPCCallbackSetStringAction(FlagToNPC, this, "castle"));
	}
	
	
	#region EmotionStates
	#region Initial Emotion State
	private class InitialEmotionState : EmotionState{
		bool farmerOnBoard = false;
		bool WhyNotFlag = false;
		Choice MarriageChoice = new Choice("What's this about marriage?", "That silly girl needs to settle herself down.");
		Choice WhyNotChoice = new Choice("Why are stories silly?", "Hmmph.  In my days you did what your parents toldja, worked however long they wanted and didn't stick your heads in the clouds.  Ya got to know and love your parents through their work!");
		Choice HowsFarmingChoice = new Choice ("How's farming?", "Poor.  That fool husband of mine can't sell anythin right.  Always undercharges.  We're lucky we still have a house.  I dun know what my girl will do when she grows up.");
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
			NoteReaction.AddAction(new UpdateCurrentTextAction(control, "Hmm...thanks for givin this to me.  If my fool girl had seen this there's no tellin what she would have done."));
			AppleReaction.AddAction(new NPCTakeItemAction(control));
			AppleReaction.AddAction(new UpdateCurrentTextAction(control, "Mmm...that there was delicious!"));
			ApplePieReaction.AddAction(new NPCTakeItemAction(control));
			ApplePieReaction.AddAction(new UpdateCurrentTextAction(control, "Mmm...that there was delicious!"));
			ShovelReaction.AddAction(new NPCTakeItemAction(control));
			ShovelReaction.AddAction(new UpdateCurrentTextAction(control, "I been needing a shovel.  Thanks!"));
			CaptainsLogReaction.AddAction(new NPCCallbackAction(UpdateCaptainsLog));
			CaptainsLogReaction.AddAction(new UpdateCurrentTextAction(control, "No thanks!  I dun need  your silly stories!"));
			
			MarriageReaction.AddAction(new NPCCallbackAction(UpdateMarriage));
			WhyNotReaction.AddAction(new NPCCallbackAction(UpdateWhysNot));
			MarriageIdeaReaction.AddAction(new NPCCallbackAction(MarriageIdeaResponse));
			ConvinceDaughterReaction.AddAction(new NPCCallbackAction(ConvinceDaughterResponse));
		}
		
		public void ConvinceDaughterResponse(){
			if (farmerOnBoard){
				FlagManager.instance.SetFlag(FlagStrings.FarmerOnBoard);
				_allChoiceReactions.Remove(ConvinceDaughterChoice);
			}else {
				_allChoiceReactions.Remove(ConvinceDaughterChoice);
				_allChoiceReactions.Add(MarriageIdeaChoice, new DispositionDependentReaction(MarriageIdeaReaction));
			}
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void MarriageIdeaResponse(){
			_allChoiceReactions.Remove(MarriageIdeaChoice);
			_allChoiceReactions.Add(ConvinceDaughterChoice, new DispositionDependentReaction(ConvinceDaughterReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void UpdateMarriage(){
			
		}
		public void UpdateWhysNot(){
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
		
		public override void UpdateEmotionState(){
			
		}
		
		public override void PassStringToEmotionState(string text){
			if (text == "happy"){
				_allChoiceReactions.Add(MarriageIdeaChoice, new DispositionDependentReaction(MarriageIdeaReaction));
			}
			if (text == "husband"){
				farmerOnBoard = true;
				MarriageIdeaChoice = new Choice("Convince daughter to marry", "Well...My husband has convinced me in to allowing you to give it a shot.");
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
