using UnityEngine;
using System.Collections;

/// <summary>
/// LighthouseGirl young specific scripting values
/// </summary>
public class LighthouseGirlYoung : NPC {
	protected override void Init() {
		id = NPCIDs.LIGHTHOUSE_GIRL;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "So my mom wants me to learn how to cook...but I'm gonna grow up to be a great warrrio, not a cook! Get some kind of cooked food and I'll rewards you!"));
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
		Choice OwnChores, WhatDoYouNeed, Sure;
		Reaction OwnChoresReaction, WhatDoYouNeedReaction, SureReaction;
		Choice GetApple = new Choice("I'll get you an apple", "Well...okay...that will help...");
		Reaction GaveApplePie = new Reaction();
		Reaction GaveApple = new Reaction();
	
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			OwnChores = new Choice("Do your own chores", "Hmmmpphhh...Great warriors don't need help from other people!");
			WhatDoYouNeed = new Choice("What do you need?", "Apple pie! My mom wants me to learn how to cook or something stupid like that...");
			Sure = new Choice("Sure", "My mom wants to bake apple pie! So give me some and we can pretend I baked it!");
			OwnChoresReaction = new Reaction(); WhatDoYouNeedReaction = new Reaction(); SureReaction = new Reaction();
			
			_allChoiceReactions.Add(OwnChores,new DispositionDependentReaction(OwnChoresReaction));
			
			WhatDoYouNeedReaction.AddAction(new NPCCallbackAction(WhatDoNeed));
			_allChoiceReactions.Add(WhatDoYouNeed,new DispositionDependentReaction(WhatDoYouNeedReaction));
			
			SureReaction.AddAction(new NPCCallbackAction(SureResponse));
			_allChoiceReactions.Add(Sure,new DispositionDependentReaction(SureReaction));
		
		}
		
		public override void UpdateEmotionState(){
			
		}
		
		public void WhatDoNeed(){
			Reaction GetAppleReaction = new Reaction();
			GetAppleReaction.AddAction(new NPCCallbackAction(GetAnApple));
			_allChoiceReactions.Add(GetApple,new DispositionDependentReaction(GetAppleReaction));
		}
		
		public void SureResponse(){
			//_allItemReactions.Add(StringsItem.Apple,  new DispositionDependentReaction(GetAnApple));
		}
		
		public void GetAnApple(){
			
		}
	
	}
	#endregion
	#endregion
}
