using UnityEngine;
using System.Collections;

/// <summary>
/// CastlemanOld specific scripting values
/// </summary>
public class CastlemanOld : NPC {
	protected override void Init() {
		id = NPCIDs.CASTLE_MAN;
		base.Init();
		this.SetCharacterPortrait(StringsNPC.Crazy);
	}
	/*
	 * THE PLAN
	 * CASTLEMAN EMOTION STATES
	 * 1. CRAZY STATE - also initial emotion state
	 * 	says crazy things
	 * 
	 * 2. ANGRY STATE - time for the rage machine
	 * 	is angry at the player and refuses to speak with them (wow this shit it going to be simple)
	 * 
	 * 3. MARRIED STATE - hoo yeeeeaaaahahhahahahah
	 * 	he's happy as fuck, just chillen like a badass motherfucker, maybe have a quest in there? If not it's no biggie
	 * 	probably need to teleport this badass to the lighthouse, poor windmill, nobody loves you
	 * 
	 * 4. SAD STATE - not married full of regret
	 * 	writes poetry every day and daaaaaayyyyyyyymmmmmmmmmmm, sound sweet as fuck
	 * 	mostly just has dialogue, muses about the world and shit
	 * 	(also totally optional depending on the flags)
	 * 
	 * 5. RAN OFF STATE - ran off to get married (optional)
	 * 	is no where to be found, as is Lighthouse Girl
	 * 	
	*/
	protected override void SetFlagReactions(){
		Reaction castleMarriage = new Reaction();
		castleMarriage.AddAction(new NPCCallbackSetStringAction(MoveForMarriage, this, "castle"));
		flagReactions.Add(FlagStrings.CastleMarriage, castleMarriage);
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
	
	
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			
		
		}
		
		public override void UpdateEmotionState(){
			
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
			_allChoiceReactions.Add(whySoBitterChoice, new DispositionDependentReaction(whySoBitterReaction));
			whySoBitterReaction.AddAction(new NPCCallbackAction(AngryReply));
		}
		
		private void AngryReply()
		{
			_npcInState.SetCharacterPortrait(StringsNPC.Angry);
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
		
		public MarriedEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue)
		{
			
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
		
		Choice ignoredChoice = new Choice("Ignored?", "I waited and waited on every dawn... \nI waited, and now she's gone...");
		Reaction ignoredReaction = new Reaction();
		
		public SadEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue)
		{
			
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
