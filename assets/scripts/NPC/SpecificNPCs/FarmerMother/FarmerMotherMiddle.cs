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
	protected override void Init() {
		id = NPCIDs.FARMER_MOTHER;
		base.Init();
	}
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
		if (disposition > 70 && husbandReady && !flagSet){
			FlagManager.instance.SetFlag(FlagStrings.FarmerOnBoard);
			flagSet = true;
		}
	}
	
	protected void ChangeHusbandStatus(){
		husbandReady = true;
		if (disposition > 70 && !flagSet){
			FlagManager.instance.SetFlag(FlagStrings.FarmerOnBoard);
			flagSet = true;
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
	}
	
	
	#region EmotionStates
	#region Initial Emotion State
	private class InitialEmotionState : EmotionState{
	
		bool WhyNotFlag = false;
		Choice MarriageChoice;
		Reaction MarriageReaction;
		
		Reaction NoteReaction;
		Reaction AppleReaction;
		Reaction ApplePieReaction;
		Reaction ShovelReaction;
		Reaction CaptainsLogReaction;
		
		Choice WhyNotChoice;
		Reaction WhyNotReaction;
		
		Choice HowsFarmingChoice;
		Reaction HowsFarmingReaction;
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, "What brings ya here?  I'm busy."){
			NoteReaction = new Reaction();
			NoteReaction.AddAction(new NPCTakeItemAction(toControl));
			NoteReaction.AddAction(new UpdateCurrentTextAction(toControl, "Hmm...thanks for givin this to me.  If my fool girl had seen this there's no tellin what she would have done."));
			_allItemReactions.Add(StringsItem.Note, new DispositionDependentReaction(NoteReaction));
			
			AppleReaction = new Reaction();
			AppleReaction.AddAction(new NPCTakeItemAction(toControl));
			AppleReaction.AddAction(new UpdateCurrentTextAction(toControl, "Mmm...that there was delicious!"));
			_allItemReactions.Add(StringsItem.Apple, new DispositionDependentReaction(AppleReaction));
			
			ApplePieReaction = new Reaction();
			ApplePieReaction.AddAction(new NPCTakeItemAction(toControl));
			ApplePieReaction.AddAction(new UpdateCurrentTextAction(toControl, "Mmm...that there was delicious!"));
			_allItemReactions.Add(StringsItem.ApplePie, new DispositionDependentReaction(ApplePieReaction));
			
			ShovelReaction = new Reaction();
			ShovelReaction.AddAction(new NPCTakeItemAction(toControl));
			ShovelReaction.AddAction(new UpdateCurrentTextAction(toControl, "I been needing a shovel.  Thanks!"));
			_allItemReactions.Add(StringsItem.Shovel, new DispositionDependentReaction(ShovelReaction));
					
			CaptainsLogReaction = new Reaction();
			CaptainsLogReaction.AddAction(new NPCCallbackAction(UpdateCaptainsLog));
			CaptainsLogReaction.AddAction(new UpdateCurrentTextAction(toControl, "No thanks!  I dun need  your silly stories!"));
			_allItemReactions.Add(StringsItem.CaptainsLog, new DispositionDependentReaction(CaptainsLogReaction));
			
			
			MarriageChoice = new Choice("What's this about marriage?", "That silly girl needs to settle herself down.");
			MarriageReaction = new Reaction();
			MarriageReaction.AddAction(new NPCCallbackAction(UpdateMarriage));
			MarriageReaction.AddAction(new UpdateCurrentTextAction(toControl, "That silly girl needs to settle herself down."));
			_allChoiceReactions.Add(MarriageChoice, new DispositionDependentReaction(MarriageReaction));
			
			WhyNotChoice = new Choice("Why are stories silly?", "Hmmph.  In my days you did what your parents toldja, worked however long they wanted and didn't stick your heads in the clouds.  Ya got to know and love your parents through their work!");
			WhyNotReaction = new Reaction();
			WhyNotReaction.AddAction(new NPCCallbackAction(UpdateWhysNot));
			WhyNotReaction.AddAction(new UpdateCurrentTextAction(toControl, "Hmmph.  In my days you did what your parents toldja, worked however long they wanted and didn't stick your heads in the clouds.  Ya got to know and love your parents through their work!"));
			
			HowsFarmingChoice = new Choice ("How's farming?", "Poor.  That fool husband of mine can't sell anythin right.  Always undercharges.  We're lucky we still have a house.  I dun know what my girl will do when she grows up.");
			HowsFarmingReaction =  new Reaction();
			HowsFarmingReaction.AddAction(new UpdateCurrentTextAction(toControl, "Poor.  That fool husband of mine can't sell anythin right.  Always undercharges.  We're lucky we still have a house.  I dun know what my girl will do when she grows up."));
			_allChoiceReactions.Add(HowsFarmingChoice, new DispositionDependentReaction(HowsFarmingReaction));
		
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
	
	}
	#endregion
	
	private class GoneEmotionState : EmotionState{
		public GoneEmotionState(NPC toControl, string currentDialogue) : base(toControl, ""){
		}
	}
	#endregion
}
