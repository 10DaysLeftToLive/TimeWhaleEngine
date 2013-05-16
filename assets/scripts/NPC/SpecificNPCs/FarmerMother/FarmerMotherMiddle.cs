using UnityEngine;
using System.Collections;

/// <summary>
/// FarmerMotherMiddle specific scripting values
/// </summary>
public class FarmerMotherMiddle : NPC {	
	protected override void Init() {
		id = NPCIDs.FARMER_MOTHER;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "|||| I better bush this dirt off my shoulders."));
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
			FlagManager.instance.SetFlag(FlagStrings.ConversationInMiddleFarmerMother);
		}
		public void UpdateCaptainsLog(){
			if(WhyNotFlag == true){
				_allChoiceReactions.Add(WhyNotChoice, new DispositionDependentReaction(WhyNotReaction));
			}
			WhyNotFlag = false;
		}
		
		public override void UpdateEmotionState(){
			
		}
	
	}
	#endregion
	#endregion
}
