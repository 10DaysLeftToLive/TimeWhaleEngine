using UnityEngine;
using System.Collections;

/// <summary>
/// CastlemanOld specific scripting values
/// </summary>
public class CastlemanOld : NPC {
	protected override void Init() {
		id = NPCIDs.CASTLE_MAN;
		base.Init();
		//this.SetCharacterPortrait(StringsNPC.Crazy);
	}
	/*
	 * THE PLAN
	 * CASTLEMAN EMOTION STATES
	 * 1. CRAZY STATE - also initial emotion state
	 * 	says crazy things
	 * 
	 * 2. ANGRY STATE - time for the rage machine
	 *  flagReactions.Add(FlagStrings.PostDatingCarpenter, datingThyEnemy);
	 * 
	 * 3. MARRIED STATE - hoo yeeeeaaaahahhahahahah
	 *  flagReactions.Add(FlagStrings.CastleMarriage, castleMarriage);
	 * 
	 * 4. SAD STATE - not married full of regret
	 * 	(also totally optional depending on the flags)
	 *  flagReactions.Add(FlagStrings.StartTalkingToLighthouse, TalkWithLighthouseFirstTime);   OR FlagStrings.FinishedInitialConversationWithCSONFriend
	 * 
	 * 5. RAN OFF STATE - ran off to get married (optional)
	 * 	is no where to be found, as is Lighthouse Girl
	 * 	flagReactions.Add(FlagStrings.PostCastleDate, gotTheGirl); but NOT flagReactions.Add(FlagStrings.CastleMarriage, castleMarriage);
	 * 	
	*/
	protected override void SetFlagReactions(){
		Reaction castleMarriage = new Reaction();
		castleMarriage.AddAction(new NPCCallbackSetStringAction(MoveForMarriage, this, "castle"));
		castleMarriage.AddAction(new NPCEmotionUpdateAction(this, new MarriedEmotionState(this, "Today is a good day.")));
		castleMarriage.AddAction(new NPCChangePortraitAction(this, StringsNPC.Happy));
		flagReactions.Add(FlagStrings.CastleMarriage, castleMarriage);
		
		Reaction castleSad = new Reaction();
		castleSad.AddAction(new NPCEmotionUpdateAction(this, new SadEmotionState(this, "I missed my chance...")));
		castleSad.AddAction(new NPCChangePortraitAction(this, StringsNPC.Sad));
		flagReactions.Add(FlagStrings.StartTalkingToLighthouse, castleSad);
		flagReactions.Add(FlagStrings.FinishedInitialConversationWithCSONFriend, castleSad);
		
		Reaction castleAngry = new Reaction();
		castleAngry.AddAction(new NPCChangePortraitAction(this, StringsNPC.Angry));
		castleAngry.AddAction(new NPCEmotionUpdateAction(this, new AngryEmotionState(this, "What are YOU doing here?")));
		flagReactions.Add(FlagStrings.PostDatingCarpenter, castleAngry);
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Hey... Want to be friends...?"));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}

	protected override void SetUpSchedules(){
		
	}
	
	protected void MoveForMarriage(NPC npc, string text){
		if (text == "castle"){
			this.transform.position = new Vector3(1,0+LevelManager.levelYOffSetFromCenter*2, this.transform.position.z);
		}
	}
	
	#region EmotionStates
	#region Initial Emotion State
	private class InitialEmotionState : EmotionState{
	
		Choice noThanksChoice = new Choice("No Thanks", "Well... I...");
		Reaction noThanksReaction = new Reaction();
		
		Choice sureChoice = new Choice("Sure", "You will? Oh thank you thank you thank you.");
		Reaction sureReaction = new Reaction();
		
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
_npcInState.ChangeFacialExpression(StringsNPC.Default);
			noThanksReaction.AddAction(new NPCCallbackAction(noThanksResult));
			noThanksReaction.AddAction(new NPCEmotionUpdateAction(toControl, new CrazyTalkEmotionState(toControl, "...")));
			sureReaction.AddAction(new NPCCallbackAction(sureResult));
			
			_allChoiceReactions.Add(noThanksChoice, new DispositionDependentReaction(noThanksReaction));
			_allChoiceReactions.Add(sureChoice, new DispositionDependentReaction(sureReaction));
		}
		
		public override void UpdateEmotionState(){
			
		}
		
		private void noThanksResult()
		{
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
_npcInState.ChangeFacialExpression(StringsNPC.Sad);
			_allChoiceReactions.Clear();
		}
		
		private void sureResult()
		{
			_allChoiceReactions.Clear();
			SetDefaultText("Thank you thank you thank you.");
			_npcInState.SetCharacterPortrait(StringsNPC.Happy);
_npcInState.ChangeFacialExpression(StringsNPC.Happy);
			GUIManager.Instance.RefreshInteraction();
			//Set Portrait to CastleManCrazyHappy
		}
	
	}
	#endregion
	#region CrazyTalkEmotionState
	private class CrazyTalkEmotionState : EmotionState
	{
		string[] stringList = new string[30];
		Reaction randomMessage;
		int stringCounter = 0;

		public CrazyTalkEmotionState(NPC toControl, string currentDialogue)
			: base(toControl, currentDialogue)
		{
			randomMessage = new Reaction();

			randomMessage.AddAction(new NPCCallbackAction(RandomMessage));
			SetOnOpenInteractionReaction(new DispositionDependentReaction(randomMessage)); 

			stringList[0] = "...";
			stringList[1] = "...I want you not";
			stringList[2] = "Hey... Want to be friends...?";
			stringList[3] = "I need no one...no...one...";
			stringCounter = 4;
		}

		public void RandomMessage()
		{
			_npcInState.SetCharacterPortrait(StringsNPC.Angry);
_npcInState.ChangeFacialExpression(StringsNPC.Angry);

			SetDefaultText(stringList[(int)Random.Range(0, stringCounter)]);
		}
	}
	#endregion
	#region AngryState
	private class AngryEmotionState : EmotionState
	{
		Choice whySoBitterChoice = new Choice("What's with you?", "You ruined everything, and you dare to ask me if I'm in the wrong!?");
		Reaction whySoBitterReaction = new Reaction();
		
		public AngryEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue)
		{
			_npcInState.SetCharacterPortrait(StringsNPC.Angry);
_npcInState.ChangeFacialExpression(StringsNPC.Angry);
			_allChoiceReactions.Add(whySoBitterChoice, new DispositionDependentReaction(whySoBitterReaction));
			whySoBitterReaction.AddAction(new NPCCallbackAction(AngryReply));
			whySoBitterReaction.AddAction(new ShowOneOffChatAction(toControl, "You ruined everything, and you dare to ask me if I'm in the wrong!?"));
		}
		
		private void AngryReply()
		{
			_npcInState.SetCharacterPortrait(StringsNPC.Angry);
_npcInState.ChangeFacialExpression(StringsNPC.Angry);
			GUIManager.Instance.CloseInteractionMenu();
			_allChoiceReactions.Clear();
			SetDefaultText("Go Away");
			GUIManager.Instance.RefreshInteraction();
		}
	}
	#endregion
	#region MarriedState
	private class MarriedEmotionState : EmotionState
	{
		Choice howIsLifeChoice = new Choice("How's life for you?", "I still can't believe where I am right now. Thanks.");
		Reaction howisLifeReaction = new Reaction();
		
		Choice areThingsWellChoice = new Choice("Is eveyrthing well?", "Not everything, wife's being bitter about her mother again.");
		Reaction areThingsWellReaction = new Reaction();
		
		Choice whyBitterChoice = new Choice("Why is she bitter?", "Don't you remember? Her mother never approved of our marriage.");
		Reaction whyBitterReaction = new Reaction();
		
		Choice herMotherChoice = new Choice("What about her Mother?", "Her mother never approved of our marriage. It caused problems for them, I wish I could help but you can't change the past.");
		Reaction herMotherReaction = new Reaction();
		
		public MarriedEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue)
		{
			howisLifeReaction.AddAction(new NPCCallbackAction(HowIsLifeResult));
			_npcInState.SetCharacterPortrait(StringsNPC.Happy);
_npcInState.ChangeFacialExpression(StringsNPC.Happy);
			
			_allChoiceReactions.Add(howIsLifeChoice, new DispositionDependentReaction(howisLifeReaction));
		}
		
		private void HowIsLifeResult()
		{
			_allChoiceReactions.Remove(howIsLifeChoice);
			_npcInState.SetCharacterPortrait(StringsNPC.Happy);
_npcInState.ChangeFacialExpression(StringsNPC.Happy);
			SetDefaultText("Thanks for everything you've done!");
		}
		
	}
	#endregion
	#region SadState
	private class SadEmotionState : EmotionState
	{
		Choice whatChanceDidYouMissChoice = new Choice("What chance did you miss?", "I lost her, I lost my Castle...");
		Reaction whatChanceDidYouMissReaction = new Reaction();
		
		Choice areYouAlrightChoice = new Choice("Are you alright?", "Huh? Oh yes, I'm alright, just getting lost in thought again.");
		Reaction areYouAlrightReaction = new Reaction();
		
		Choice whatCastleChoice = new Choice("What Castle?", "I lost my Castle by the sea... \nShe was bright and lightened me...");
		Reaction whatCastleReaction = new Reaction();
		
		Choice brightChoice = new Choice("Bright?", "She shone brighter than the grandest sword... \nIt burned a fire that I ignored...");
		Reaction brightReaction = new Reaction();
		
		Choice ignoredChoice = new Choice("Ignored?", "I waited and waited on every dawn... I waited, and now she's gone...");
		Reaction ignoredReaction = new Reaction();
		
		public SadEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue)
		{
			whatChanceDidYouMissReaction.AddAction(new NPCCallbackAction(WhatChanceRseult));
			areYouAlrightReaction.AddAction(new NPCCallbackAction(AreYouAlrightResult));
			whatCastleReaction.AddAction(new NPCCallbackAction(WhatCastleResult));
			brightReaction.AddAction(new NPCCallbackAction(BrightResult));
			ignoredReaction.AddAction(new NPCCallbackAction(IgnoredResult));
			
			_allChoiceReactions.Add(whatChanceDidYouMissChoice,new DispositionDependentReaction(whatChanceDidYouMissReaction));
			_allChoiceReactions.Add(areYouAlrightChoice, new DispositionDependentReaction(areYouAlrightReaction));
		}
		
		private void AreYouAlrightResult()
		{
			_allChoiceReactions.Clear();
			SetDefaultText("Sorry about that, the mind's been wandering a bit lately.");
			_npcInState.SetCharacterPortrait(StringsNPC.Default);
_npcInState.ChangeFacialExpression(StringsNPC.Default);
			GUIManager.Instance.RefreshInteraction();
		}
		
		private void WhatChanceRseult()
		{
			_allChoiceReactions.Remove(whatChanceDidYouMissChoice);
			_allChoiceReactions.Add(whatCastleChoice, new DispositionDependentReaction(whatCastleReaction));
			SetDefaultText("My castle... my castle...");
			_npcInState.SetCharacterPortrait(StringsNPC.Sad);
_npcInState.ChangeFacialExpression(StringsNPC.Sad);
			GUIManager.Instance.RefreshInteraction();
		}
		
		private void WhatCastleResult()
		{
			_allChoiceReactions.Remove(whatCastleChoice);
			_allChoiceReactions.Add(brightChoice, new DispositionDependentReaction(brightReaction));
			SetDefaultText("bright...");
			GUIManager.Instance.RefreshInteraction();
		}
		
		private void BrightResult()
		{
			_allChoiceReactions.Remove(brightChoice);
			_allChoiceReactions.Add(ignoredChoice, new DispositionDependentReaction(ignoredReaction));
			SetDefaultText("ignored...");
			GUIManager.Instance.RefreshInteraction();
		}
		
		private void IgnoredResult()
		{
			_allChoiceReactions.Remove(ignoredChoice);
			SetDefaultText("gone...");
			GUIManager.Instance.RefreshInteraction();
		}
	}
	#endregion
	#region RanOffState
	private class RanOffEmotionState : EmotionState
	{
		public RanOffEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue)
		{
			//toControl.transform.position = new Vector3(
		}
	}
	#endregion
	#endregion
}
