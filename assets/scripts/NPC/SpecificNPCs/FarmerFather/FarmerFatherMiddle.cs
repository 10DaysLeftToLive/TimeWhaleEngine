using UnityEngine;
using System.Collections;

/// <summary>
/// FarmerFatherMiddle specific scripting values
/// </summary>
public class FarmerFatherMiddle : NPC {
	InitialEmotionState initialState;
	Vector3 startingPosition;
	int disposition = 50;
	bool farmerHelped = false;
	bool farmersOnBoard = false;
	bool flagSet = false;
	bool carpenterDate = true;
	protected override void Init() {
		id = NPCIDs.FARMER_FATHER;
		base.Init();
	}
	#region ReactionInstantiate
	Reaction moveAway = new Reaction();
	
	Reaction sillyStories = new Reaction();
	Reaction yourCoward = new Reaction();
	Reaction alreadyBrave = new Reaction();
	Reaction illDoIt = new Reaction();
	Reaction illSellForYou = new Reaction();
	Reaction postDateCastle = new Reaction();
	Reaction postDateCarpenter = new Reaction();
	Reaction castleDate = new Reaction();
	Reaction stoodUp = new Reaction();
	Reaction farmer = new Reaction();
	
	Reaction apple = new Reaction();
	Reaction applePie = new Reaction();
	Reaction shovel = new Reaction();
	Reaction rope = new Reaction();
	#endregion
	
	protected override void SetFlagReactions(){
		SetupReactions();
		flagReactions.Add(FlagStrings.FarmAlive, moveAway);
		flagReactions.Add(FlagStrings.HusbandSillyStories, sillyStories);
		flagReactions.Add(FlagStrings.YourCoward, yourCoward);
		flagReactions.Add(FlagStrings.AlreadyBrave, alreadyBrave);
		flagReactions.Add(FlagStrings.IllDoIt, illDoIt);
		flagReactions.Add(FlagStrings.IllSellForYou, illSellForYou);
		flagReactions.Add(FlagStrings.PostCastleDate, postDateCastle);
		flagReactions.Add(FlagStrings.PostDatingCarpenter, postDateCarpenter);
		flagReactions.Add(FlagStrings.CastleDate, castleDate);
		flagReactions.Add(FlagStrings.FarmerOnBoard, farmer);
		flagReactions.Add(FlagStrings.StoodUp, stoodUp);
		
		flagReactions.Add(FlagStrings.GiveAppleToFarmer, apple);
		flagReactions.Add(FlagStrings.GiveApplePieToFarmer, applePie);
		flagReactions.Add(FlagStrings.GiveShovelToFarmer, shovel);
		flagReactions.Add(FlagStrings.GiveRopeToFarmer, rope);
	}
	
	protected override EmotionState GetInitEmotionState(){
		initialState = new InitialEmotionState(this, "H-hey there. D-do you have some time to listen? If you do, come back later when my wife isn't around..");
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
		if (disposition > 70 && farmerHelped && !flagSet){
			initialState.PassStringToEmotionState("happy");
			flagSet = true;
		}
	}
	
	protected void FarmerHelped(){
		farmerHelped = true;
		if (disposition > 70 && !flagSet){
			initialState.PassStringToEmotionState("happy");
			flagSet = true;
		}
	}
	
	protected void FlagToNPC(NPC npc, string text){
		if (text == "castle"){
			carpenterDate = false;	
		}
		if (text == "carpenterSuccess" && farmersOnBoard){
			initialState.PassStringToEmotionState("carpenterSuccess");
		}
		if (text == "stoodUp" && farmersOnBoard){
			initialState.PassStringToEmotionState("stoodUp");
			disposition -= 20;
		}
		if (text == "farmerMotherOnBoard"){
			farmersOnBoard = true;	
		}
	}
	
	protected void SetupReactions(){
		moveAway.AddAction(new NPCCallbackAction(ResetPosition));
		moveAway.AddAction(new NPCEmotionUpdateAction(this, initialState));
		
		sillyStories.AddAction(new NPCCallbackSetStringAction(ChangeDisposition, this, "-10"));
		yourCoward.AddAction(new NPCCallbackSetStringAction(ChangeDisposition, this, "-15"));
		
		alreadyBrave.AddAction(new NPCCallbackSetStringAction(ChangeDisposition, this, "10"));
		
		illDoIt.AddAction(new NPCCallbackSetStringAction(ChangeDisposition, this, "5"));
		
		illSellForYou.AddAction(new NPCCallbackAction(FarmerHelped));
		
		apple.AddAction(new NPCCallbackSetStringAction(ChangeDisposition, this, "5"));
		applePie.AddAction(new NPCCallbackSetStringAction(ChangeDisposition, this, "15"));
		shovel.AddAction(new NPCCallbackSetStringAction(ChangeDisposition, this, "5"));
		rope.AddAction(new NPCCallbackSetStringAction(ChangeDisposition, this, "5"));
		
		farmer.AddAction(new NPCCallbackSetStringAction(FlagToNPC, this, "farmerMotherOnBoard"));
		postDateCastle.AddAction(new NPCEmotionUpdateAction(this, new HateEmotionState(this, "")));
		postDateCastle.AddAction(new NPCCallbackSetStringAction(ChangeDisposition, this, "-100"));
		
		postDateCarpenter.AddAction(new NPCCallbackSetStringAction(FlagToNPC, this, "carpenterSuccess"));
		
		castleDate.AddAction(new NPCCallbackSetStringAction(FlagToNPC, this, "castle"));
		
		stoodUp.AddAction(new NPCCallbackSetStringAction(FlagToNPC, this, "stoodUp"));
	}
	
	
	#region EmotionStates
	#region Initial Emotion State
	private class InitialEmotionState : EmotionState{
		bool MarriageFlag = false;
		bool convinceFlag = false;
		Choice BusinessChoice = new Choice("How's your business?", "It's going poorly...I just can never find the strength to be a hawk when it comes to business...");
		Choice MarriageChoice = new Choice("So about this marriage?", "I...I don't like it...but I'm sure my wife knows what she's doing.");
		Reaction BusinessReaction = new Reaction();
		Reaction MarriageReaction = new Reaction();
		
		Choice DontLikeItChoice = new Choice("Why don't you like it?", "I dunno...I...think my daughter should be allowed to think for herself..");
		Choice YouSureChoice = new Choice("Are you sure?", "She's always dealt with things.  I'm sure she knows what she's doing...");
		Choice StandUpChoice = new Choice("Then stand up for her!", "But...I can't!  Its too hard...it's too hard..");
		Choice HelpHerChoice = new Choice("Help her think then!", "But...I can't!  Its too hard...it's too hard..");
		Choice SoundSureChoice = new Choice("You don't sound sure.", "I dunno...I...think my daughter should be allowed to think for herself..");
		Choice YouCanChoice = new Choice("If she can do it you can!", "It's going poorly...I just can never find the strength to be a hawk when it comes to business...");
		Choice YouHaveItChoice = new Choice("I know you have it in you!", "It's going poorly...I just can never find the strength to be a hawk when it comes to business...");
		Choice MarriageIdeaChoice = new Choice("I have an idea about the marriage.", "I'm listening");
		Choice ConvinceDaughterChoice = new Choice("Convince daughter to marry", "I have tried that before with no luck but if you want to give it a try then go ahead.");
		Choice WorkingOnItChoice = new Choice("I'm still working on it", "Give up now..");
		Choice NoChoice = new Choice("No", "It's no hope.");
		
		Reaction DontLikeItReaction = new Reaction();
		Reaction YouSureReaction = new Reaction();
		Reaction StandUpReaction = new Reaction();
		Reaction HelpHerReaction = new Reaction();
		Reaction SoundSureReaction = new Reaction();
		Reaction YouCanReaction = new Reaction();
		Reaction YouHaveItReaction = new Reaction();
		Reaction MarriageIdeaReaction = new Reaction();
		Reaction ConvinceDaughterReaction = new Reaction();
		Reaction WorkingOnItReaction = new Reaction();
		Reaction NoReaction = new Reaction();
		
		Reaction ToyPuzzleReaction = new Reaction();
		Reaction SeaShellReaction = new Reaction();
		Reaction AppleReaction = new Reaction();
		Reaction ApplePieReaction = new Reaction();
		Reaction CaptainsLogReaction = new Reaction();
		Reaction PortraitReaction = new Reaction();
	
		NPC control;
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, "Hello there!  How's it going?"){
			control = toControl;
//			_allItemReactions.Add(StringsItem.Portrait,  new DispositionDependentReaction(PortraitReaction)); // change item to shell
			_allItemReactions.Add(StringsItem.Seashell,  new DispositionDependentReaction(SeaShellReaction)); // change item to shell
			_allItemReactions.Add(StringsItem.ToyPuzzle,  new DispositionDependentReaction(ToyPuzzleReaction)); // change item to shell
			_allItemReactions.Add(StringsItem.Apple,  new DispositionDependentReaction(AppleReaction)); // change item to shell
			_allItemReactions.Add(StringsItem.ApplePie,  new DispositionDependentReaction(ApplePieReaction)); // change item to shell
//			_allItemReactions.Add(StringsItem.CaptainsLog,  new DispositionDependentReaction(CaptainsLogReaction)); // change item to shell
			
			_allChoiceReactions.Add(MarriageChoice, new DispositionDependentReaction(MarriageReaction));
			_allChoiceReactions.Add(BusinessChoice, new DispositionDependentReaction(BusinessReaction));
			

		}
		
		public void SetupReactions(){
			PortraitReaction.AddAction(new NPCTakeItemAction(control));
			PortraitReaction.AddAction(new UpdateCurrentTextAction(control, "Thanks a ton this will make a great addition to my study.  Can't let my wife see it though...she'll have me throw it out."));
			SeaShellReaction.AddAction(new NPCTakeItemAction(control));
			SeaShellReaction.AddAction(new UpdateCurrentTextAction(control, "This looks pretty nice.."));
			ToyPuzzleReaction.AddAction(new NPCTakeItemAction(control));
			ToyPuzzleReaction.AddAction(new UpdateCurrentTextAction(control, "Heh...this looks like an interesting problem...I'm gonna try and solve it."));
			AppleReaction.AddAction(new NPCTakeItemAction(control));
			AppleReaction.AddAction(new UpdateCurrentTextAction(control, "That tasted great.  You'll have to tell me where you got it some time..."));
			ApplePieReaction.AddAction(new NPCTakeItemAction(control));
			ApplePieReaction.AddAction(new UpdateCurrentTextAction(control, "That tasted great.  You'll have to tell me where you got it some time..."));
			CaptainsLogReaction.AddAction(new NPCCallbackAction(UpdateCaptainsLog));
			CaptainsLogReaction.AddAction(new UpdateCurrentTextAction(control, "I wish I had stories like this to give to my daughter earlier...but I guess its okay, she turned out to be very brave anyways..."));
			
			
			MarriageIdeaReaction.AddAction(new NPCCallbackAction(MarriageIdeaResponse));
			ConvinceDaughterReaction.AddAction(new NPCCallbackAction(ConvinceDaughterResponse));
			
			MarriageReaction.AddAction(new NPCCallbackAction(UpdateMarriage));
			//The Convincing him to marry path.
			DontLikeItReaction.AddAction(new NPCCallbackAction(UpdateDontLikeIt));
			YouSureReaction.AddAction(new NPCCallbackAction(UpdateYouSure));
			StandUpReaction.AddAction(new NPCCallbackAction(UpdateStandUp));
			HelpHerReaction.AddAction(new NPCCallbackAction(UpdateHelpHer));
			SoundSureReaction.AddAction(new NPCCallbackAction(UpdateSoundSure));
			YouCanReaction.AddAction(new NPCCallbackAction(UpdateYouCan));
			YouHaveItReaction.AddAction(new NPCCallbackAction(UpdateYouHaveIt));
		}
		
		public void NoResponse(){
			_allChoiceReactions.Clear();
			_allChoiceReactions.Add(MarriageChoice, new DispositionDependentReaction(MarriageReaction));
			_allChoiceReactions.Add(BusinessChoice, new DispositionDependentReaction(BusinessReaction));
			SetDefaultText("Hello there!  How's it going?");
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void ConvinceDaughterResponse(){
			_allChoiceReactions.Clear();
			SetDefaultText("Any luck convincing my daughter?");
			if (!convinceFlag){
				FlagManager.instance.SetFlag(FlagStrings.HusbandOnBoard);	
			}
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void MarriageIdeaResponse(){
			_allChoiceReactions.Clear();
			_allChoiceReactions.Add(ConvinceDaughterChoice, new DispositionDependentReaction(ConvinceDaughterReaction));
			SetDefaultText("So what was your marriage idea?");
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void UpdateCaptainsLog(){
			FlagManager.instance.SetFlag(FlagStrings.ConversationInMiddleFather);	
		}
		public void UpdateMarriage(){
			/*if (MarriageFlag == false){
				SetDefaultText("My wife has everything planned out...I just hope our daughter enjoys the wedding...");
				GUIManager.Instance.RefreshInteraction();
				SetDefaultText("Hello there!  How's it going?");
			}
			else{
				SetDefaultText("My wife has everything planned out...I just hope our daughter enjoys the wedding...");
				GUIManager.Instance.RefreshInteraction();
			}*/
			_allChoiceReactions.Add(DontLikeItChoice, new DispositionDependentReaction(DontLikeItReaction));
			_allChoiceReactions.Add(YouSureChoice, new DispositionDependentReaction(YouSureReaction));
			_allChoiceReactions.Remove(MarriageChoice);
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void UpdateYouSure(){
			_allChoiceReactions.Remove(YouSureChoice);
			_allChoiceReactions.Remove(DontLikeItChoice);
			_allChoiceReactions.Add(SoundSureChoice, new DispositionDependentReaction(SoundSureReaction));
			GUIManager.Instance.RefreshInteraction();
			//SetDefaultText("");
		}
		public void UpdateDontLikeIt(){
			_allChoiceReactions.Remove(YouSureChoice);
			_allChoiceReactions.Remove(DontLikeItChoice);
			_allChoiceReactions.Add(StandUpChoice, new DispositionDependentReaction (StandUpReaction));
			_allChoiceReactions.Add (HelpHerChoice, new DispositionDependentReaction(HelpHerReaction));
			GUIManager.Instance.RefreshInteraction();
			
		}
		public void UpdateSoundSure(){
			_allChoiceReactions.Remove(SoundSureChoice);
			_allChoiceReactions.Add(StandUpChoice, new DispositionDependentReaction (StandUpReaction));
			_allChoiceReactions.Add (HelpHerChoice, new DispositionDependentReaction(HelpHerReaction));
			GUIManager.Instance.RefreshInteraction();
			
		}
		public void UpdateHelpHer(){
			_allChoiceReactions.Remove(StandUpChoice);
			_allChoiceReactions.Remove(HelpHerChoice);
			_allChoiceReactions.Add(YouCanChoice, new DispositionDependentReaction (YouCanReaction));
			_allChoiceReactions.Add (YouHaveItChoice, new DispositionDependentReaction(YouHaveItReaction));
			GUIManager.Instance.RefreshInteraction();
			
		}
		public void UpdateStandUp(){
			_allChoiceReactions.Remove(StandUpChoice);
			_allChoiceReactions.Remove(HelpHerChoice);
			_allChoiceReactions.Add(YouCanChoice, new DispositionDependentReaction (YouCanReaction));
			_allChoiceReactions.Add (YouHaveItChoice, new DispositionDependentReaction(YouHaveItReaction));
			GUIManager.Instance.RefreshInteraction();
			
		}
		public void UpdateYouCan(){
			_allChoiceReactions.Remove(YouCanChoice);
			_allChoiceReactions.Remove(YouHaveItChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Finished Convo");
			
		}
		public void UpdateYouHaveIt(){
			_allChoiceReactions.Remove(YouCanChoice);
			_allChoiceReactions.Remove(YouHaveItChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("Finished Convo");
		}
		
		public override void UpdateEmotionState(){
			
		}
		
		public override void PassStringToEmotionState(string text){
			if (text == "happy"){
				_allChoiceReactions.Add(MarriageIdeaChoice, new DispositionDependentReaction(MarriageIdeaReaction));
			}
			if (text == "carpenterSuccess"){
				SetDefaultText("I've heard what you did for my daughter. Your idea worked!");
			}
			if (text == "stoodUp"){
				SetDefaultText("Went through all that trouble...for nothing.");
			}
		}
	
	}
	
	private class GoneEmotionState : EmotionState{
		public GoneEmotionState(NPC toControl, string currentDialogue) : base(toControl, ""){
		}
		
	}
	
	private class HateEmotionState : EmotionState{
		public HateEmotionState(NPC toControl, string currentDialogue) : base(toControl, "Do not talk to me."){
		}
		
	}
	#endregion
	#endregion
}
