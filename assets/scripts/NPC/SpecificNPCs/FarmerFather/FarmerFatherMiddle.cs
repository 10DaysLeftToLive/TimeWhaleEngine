using UnityEngine;
using System.Collections;

/// <summary>
/// FarmerFatherMiddle specific scripting values
/// </summary>
public class FarmerFatherMiddle : NPC {	
	protected override void Init() {
		id = NPCIDs.FARMER_FATHER;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "||||AW SNAPS I'M OLD"));
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
		bool MarriageFlag = false;
		//Toy Puzzle
		//SeaShell
		//Apple or Apple Pie
		//Portrait
		//Toy Puzzle
		//Captain's Log
		
		//Choices
		//Business
		//Marriage
		Choice BusinessChoice;
		Choice MarriageChoice;
		Reaction BusinessReaction;
		Reaction MarriageReaction;
		
		Choice DontLikeItChoice;
		Reaction DontLikeItReaction;
		Choice YouSureChoice;
		Reaction YouSureReaction;
		Choice StandUpChoice;
		Reaction StandUpReaction;
		Choice HelpHerChoice;
		Reaction HelpHerReaction;
		Choice SoundSureChoice;
		Reaction SoundSureReaction;
		Choice YouCanChoice;
		Reaction YouCanReaction;
		Choice YouHaveItChoice;
		Reaction YouHaveItReaction;
		
		
		Reaction ToyPuzzleReaction;
		Reaction SeaShellReaction;
		Reaction AppleReaction;
		Reaction ApplePieReaction;
		Reaction CaptainsLogReaction;
		Reaction PortraitReaction;
	
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, "Hello there!  How's it going?"){
			
			
			//Code for giving the Portrait
			Reaction PortraitReaction = new Reaction();
			PortraitReaction.AddAction(new NPCTakeItemAction(toControl));
			//PortraitReaction.AddAction(new NPCCallbackAction(SetSeaShell));
			PortraitReaction.AddAction(new UpdateCurrentTextAction(toControl, "Thanks a ton this will make a great addition to my study.  Can't let my wife see it though...she'll have me throw it out."));
			_allItemReactions.Add(StringsItem.Portrait,  new DispositionDependentReaction(PortraitReaction)); // change item to shell
			
			//Code for giving the seashell
			Reaction SeaShellReaction = new Reaction();
			SeaShellReaction.AddAction(new NPCTakeItemAction(toControl));
			//SeaShellReaction.AddAction(new NPCCallbackAction(SetSeaShell));
			SeaShellReaction.AddAction(new UpdateCurrentTextAction(toControl, "This looks pretty nice.."));
			_allItemReactions.Add(StringsItem.SeaShell,  new DispositionDependentReaction(SeaShellReaction)); // change item to shell
		
			//Code for giving the Toy Puzzle
			Reaction ToyPuzzleReaction = new Reaction();
			ToyPuzzleReaction.AddAction(new NPCTakeItemAction(toControl));
			//ToyPuzzleReaction.AddAction(new NPCCallbackAction(SetSeaShell));
			ToyPuzzleReaction.AddAction(new UpdateCurrentTextAction(toControl, "Heh...this looks like an interesting problem...I'm gonna try and solve it."));
			_allItemReactions.Add(StringsItem.ToyPuzzle,  new DispositionDependentReaction(ToyPuzzleReaction)); // change item to shell
			
			//Code for giving the Apple
			Reaction AppleReaction = new Reaction();
			AppleReaction.AddAction(new NPCTakeItemAction(toControl));
			//AppleReaction.AddAction(new NPCCallbackAction(UpdateCaptainsLog));
			AppleReaction.AddAction(new UpdateCurrentTextAction(toControl, "That tasted great.  You'll have to tell me where you got it some time..."));
			_allItemReactions.Add(StringsItem.Apple,  new DispositionDependentReaction(AppleReaction)); // change item to shell
			
			//Code for giving the Apple Pie
			Reaction ApplePieReaction = new Reaction();
			ApplePieReaction.AddAction(new NPCTakeItemAction(toControl));
			//ApplePieReaction.AddAction(new NPCCallbackAction(SetSeaShell));
			ApplePieReaction.AddAction(new UpdateCurrentTextAction(toControl, "That tasted great.  You'll have to tell me where you got it some time..."));
			_allItemReactions.Add(StringsItem.ApplePie,  new DispositionDependentReaction(ApplePieReaction)); // change item to shell
			
			//Code for giving the Captains Log
			Reaction CaptainsLogReaction = new Reaction();
			CaptainsLogReaction.AddAction(new NPCCallbackAction(UpdateCaptainsLog));
			CaptainsLogReaction.AddAction(new UpdateCurrentTextAction(toControl, "I wish I had stories like this to give to my daughter earlier...but I guess its okay, she turned out to be very brave anyways..."));
			_allItemReactions.Add(StringsItem.CaptainsLog,  new DispositionDependentReaction(CaptainsLogReaction)); // change item to shell
			
			MarriageChoice = new Choice("So about this marriage?", "I...I don't like it...but I'm sure my wife knows what she's doing.");
			MarriageReaction = new Reaction();
			MarriageReaction.AddAction(new NPCCallbackAction(UpdateMarriage));
			_allChoiceReactions.Add(MarriageChoice, new DispositionDependentReaction(MarriageReaction));
			//MarriageReaction.AddAction(new UpdateCurrentTextAction(toControl, "I...I don't like it...but I'm sure my wife knows what she's doing."));
			
			BusinessChoice = new Choice("How's your business?", "It's going poorly...I just can never find the strength to be a hawk when it comes to business...");
			BusinessReaction = new Reaction();
			BusinessReaction.AddAction(new UpdateCurrentTextAction(toControl, "It's going poorly...I just can never find the strength to be a hawk when it comes to business..."));
			_allChoiceReactions.Add(BusinessChoice, new DispositionDependentReaction(BusinessReaction));
			
			//The Convincing him to marry path.
			DontLikeItChoice = new Choice("Why don't you like it?", "I dunno...I...think my daughter should be allowed to think for herself..");
			DontLikeItReaction = new Reaction();
			DontLikeItReaction.AddAction(new NPCCallbackAction(UpdateDontLikeIt));
			DontLikeItReaction.AddAction(new UpdateCurrentTextAction(toControl, "I dunno...I...think my daughter should be allowed to think for herself.."));
			
			YouSureChoice = new Choice("Are you sure?", "She's always dealt with things.  I'm sure she knows what she's doing...");
			YouSureReaction = new Reaction();
			YouSureReaction.AddAction(new NPCCallbackAction(UpdateYouSure));
			YouSureReaction.AddAction(new UpdateCurrentTextAction(toControl, "She's always dealt with things.  I'm sure she knows what she's doing..."));
			
			StandUpChoice = new Choice("Then stand up for her!", "But...I can't!  Its too hard...it's too hard..");
			StandUpReaction = new Reaction();
			StandUpReaction.AddAction(new NPCCallbackAction(UpdateStandUp));
			StandUpReaction.AddAction(new UpdateCurrentTextAction(toControl, "But...I can't!  Its too hard...it's too hard.."));
			
			HelpHerChoice = new Choice("Help her think then!", "But...I can't!  Its too hard...it's too hard..");
			HelpHerReaction = new Reaction();
			HelpHerReaction.AddAction(new NPCCallbackAction(UpdateHelpHer));
			HelpHerReaction.AddAction(new UpdateCurrentTextAction(toControl, "But...I can't!  Its too hard...it's too hard.."));
			
			SoundSureChoice = new Choice("You don't sound sure.", "I dunno...I...think my daughter should be allowed to think for herself..");
			SoundSureReaction = new Reaction();
			SoundSureReaction.AddAction(new NPCCallbackAction(UpdateSoundSure));
			SoundSureReaction.AddAction(new UpdateCurrentTextAction(toControl, "I dunno...I...think my daughter should be allowed to think for herself.."));
			
			YouCanChoice = new Choice("If she can do it you can!", "It's going poorly...I just can never find the strength to be a hawk when it comes to business...");
			YouCanReaction = new Reaction();
			YouCanReaction.AddAction(new NPCCallbackAction(UpdateYouCan));
			YouCanReaction.AddAction(new UpdateCurrentTextAction(toControl, "It's going poorly...I just can never find the strength to be a hawk when it comes to business..."));
			
			YouHaveItChoice = new Choice("I know you have it in you!", "It's going poorly...I just can never find the strength to be a hawk when it comes to business...");
			YouHaveItReaction = new Reaction();
			YouHaveItReaction.AddAction(new NPCCallbackAction(UpdateYouHaveIt));
			YouHaveItReaction.AddAction(new UpdateCurrentTextAction(toControl, "It's going poorly...I just can never find the strength to be a hawk when it comes to business..."));
			
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
	
	}
	#endregion
	#endregion
}
