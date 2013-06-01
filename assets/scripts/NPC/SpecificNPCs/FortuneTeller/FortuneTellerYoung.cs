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
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Hey there stranger~" + "\n" + "Care for your fortune?"));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}

	protected override void SetUpSchedules(){
		
	}
	//particles alpha-blended
	
	// Whatever you do, don't leave until we're entirely finished.
	
	//Hmmmmmm .. *closes eyes* .. your path lay with several important decisions.
	//Let me ask a few questions to better see what you will do.
	
	// If a close friend
	// Do you take the left path or the right path? - Left, Right, Neither
	// Would you rather work alone or work together? - Alone, Together, Work?
	// If you could only save one person, who would it be? - A lover, A sibling, a stranger
	//
	
	//Do you have someone you'd consider a best friend?
	// yes - 
	// no  - What about family? 
			// Yes -
			// No -
			// I don't have family... - I'm very sorry.. (Gets slightly emotional for bringing up your family) 
										// - "My condolences, I didn't mean to bring up something that may be burdensome
										// - It's ok.
										// - ...
	
	// Have you told them you appreciate them recently?
	// yes - Mm ... That is the way to maintain the ones you love 
	// no  - I see .. Perhaps you should let them know?
	
	
	// Have any regrets that you wish you could go back and change?
	// Yes, No
	
	// Now as payment, I'll need something delicious to snack on.
	
#region EmotionStates
	#region Initial Emotion State
	private class InitialEmotionState : EmotionState{
		Choice acceptFortuneChoice = new Choice("Sure.", "Let's begin~" + "\n\n" + "Given two paths, should you take the left one or the right one?");
		Choice declineFortuneChoice = new Choice("No thanks.", "C'mon, it'll be fun!");
		Choice acceptReluctantChoice = new Choice ("Ok..", "That's the spirit! Let's begin~" + "\n\n" + "Given two paths, should you take the left path or the right path");
		 
		Reaction acceptFortuneState = new Reaction();
		Reaction acceptReluctantState = new Reaction();
		Reaction declineFortuneState = new Reaction();
		Reaction fullDeclineState = new Reaction();
		
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue) {
			acceptFortuneState.AddAction(new NPCEmotionUpdateAction(toControl, new BeginFortuneState(toControl, "Your path lay with several important decisions. Take a moment to reflect~")));
			acceptFortuneState.AddAction(new NPCCallbackAction(UpdateAcceptedChoices));
			
			acceptReluctantState.AddAction(new NPCEmotionUpdateAction(toControl, new BeginFortuneState(toControl, "Your path lay with several important decisions. Take a moment to reflect~")));	
			declineFortuneState.AddAction(new NPCCallbackAction(UpdateDeclinedChoices));
			
			fullDeclineState.AddAction(new NPCEmotionUpdateAction(toControl, new StillConvincingState(toControl,"Ready for your fortune now?")));
			fullDeclineState.AddAction(new NPCCallbackOnNPCAction(setCurrentText, toControl));
			
			_allChoiceReactions.Add(acceptFortuneChoice, new DispositionDependentReaction(acceptFortuneState));
			_allChoiceReactions.Add(declineFortuneChoice, new DispositionDependentReaction(declineFortuneState));
		}
		
		public void UpdateAcceptedChoices() {
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
			_allChoiceReactions.Remove(declineFortuneChoice);
			_allChoiceReactions.Remove(acceptFortuneChoice);
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
//Leaving chat goes to the default text: Your path lay with several important decisions. Take a moment to reflect~
//Even before you're done with the conversation
		Choice leftChoice = new Choice("Left", "Interesting.." + "\n\n" + "Do you prefer being left alone or being with others?");//
			Choice aloneChoice = new Choice ("Alone", "Mm.. alone, but wise.." + "\n\n" + "A stranger looks distressed. What do you do?");//
				Choice aidChoice = new Choice ("Lend them aid", "Mmm.. a noble helper." + "\n" + "They lash out at you, how do you respond?");//
					Choice helpChoice = new Choice ("Continue Helping", "..Seeing the good in everyone." + "\n\n" + "If such a person asked you to keep a secret, would you?");//
						Choice keepSecretChoice = new Choice ("Always","Truly a genuine person ... Your fate is in good hands~");//
						Choice tellSecretChoice = new Choice ("Depends","Hesistent, but with good intentions? .." + "\n\n" + "They seem troubled. Do you stay or leave?");//
							Choice leaveSecretChoice = new Choice ("I leave", "To begin alone, to stay alone. A rational path~");//
							Choice dontLeaveSecretChoice = new Choice ("I'd stay", "Beginning alone and ending together." + "\n\n" + "A supportive path~");//
					Choice leaveChoice = new Choice ("Leave.", "Mm.. Safety is important.." + "\n\n" + "What if you had the opportunity to save a life. Would you take it?");//
						Choice saveLifeChoice = new Choice ("I would", "Interesting.. What if it put your life at risk, would you still try?");//
							Choice stillSaveLifeChoice = new Choice ("I would", "You are a selfless and humble soul.." +"\n\n" + "Don't forget your own worth as well!~");//
							Choice changeSaveLifeChoice = new Choice ("No", "A valid choice.. Why sacrifice for a stranger when you already know yourself?" +"\n\n" + "Stay well and healthy~");//
						Choice maybeSaveLifeChoice = new Choice ("Depends", "Questioning.. What if your own life was at risk?");
							//stillSaveLifeChoice //
							//changeSaveLifeChoice //
						Choice dontSaveLifeChoice = new Choice ("Never", "I see.. You are your utmost priority." +"\n\n" + "Stay well and healthy~");//
				Choice questionChoice = new Choice ("Question their motive", "Mm. Seeking clarity." + "\n\n" + "They want a friend, do you allow them to follow?");//
						//keepSecretChoice //
						//tellSecretChoice //
							//leaveSecretChoice //
							//dontLeaveSecretChoice //
				Choice nothingChoice = new Choice ("Do nothing", "Mm. More important endeavors to attend?" + "\n\n" + "What if they were in tears?"); //
					Choice yesToTearsChoice = new Choice ("I'd help", "You carry sympathy.. " + "\n\n" + "They ask you to share their burden, do you share it?"); //
						Choice shareBurdenChoice = new Choice ("I'd help", "You carry sympathy.. " + "\n\n" + "They ask you to share their burden, do you share it?"); //
						Choice dontShareBurdenChoice = new Choice ("I'd help", "Burden for a stranger" + "\n\n" + "They ask you to share their burden, do you share it?"); //
					Choice noToTearsChoice = new Choice ("No", "True to your intentions.." + "\n\n" + "You are strong willed~"); //
		
			Choice togetherChoice = new Choice ("With Others", "Mm, friendships are important." + "\n\n" + "If you could only save one person, who would it be?"); //
				Choice saveLoverChoice = new Choice ("A Lover", "The one true love... worth more than anything in world." + "\n\n" + "Would you risk your life for them?"); //
					Choice loverRiskLifeChoice = new Choice ("Always","Mmm..." + "\n\n" + "But what if it would cause them grief knowing you gave your own life to save them?"); //
						Choice yesRiskLifeChoice = new Choice ("I Still Would", "True love prevails over the odds." + "\n\n" + "May they be a lucky one~");
						Choice noRiskLifeChoice = new Choice ("I don't know.", "It's ok not to know now ..." + "\n\n" +"but what would happen if such an event were to occur?~");
					Choice loverUnsureLifeChoice = new Choice ("Depends","It's ok not to know... Perhaps they wouldn't want that either~");
					Choice loverNeverLifeChoice = new Choice ("No","You trust in yourself first.. Perhaps that is the right answer~");
				Choice saveFamilyChoice = new Choice ("A Sibling", "Family first... an unwritten trust.");
					Choice FamilyQuestion = new Choice ("","");
				Choice saveMentorChoice = new Choice ("A Mentor","Noble...a stranger with potential.");
					Choice MentorQuestion = new Choice ("","");
		
			//Choice itemOne = new Choice ("itemOne","");
			//Choice itemTwo = new Choice ("itemTwo","");
			//Choice itemThree = new Choice ("itemThree","");
			//Choice itemFour = new Choice ("itemFour","");
			//Choice itemFive = new Choice ("itemFive","");
			//Choice itemSix =  new Choice ("itemSix","");
				
		Choice rightChoice = new Choice("Right", "I see.." + "\n\n" + "Do you prefer being right or hearing how you might be wrong?");
			
		
//Add more choices 
		
		Reaction leftReaction = new Reaction();
			Reaction aloneReaction = new Reaction(); 
				Reaction aidReaction = new Reaction();
					Reaction helpReaction = new Reaction();
						Reaction keepSecretReaction = new Reaction();
						Reaction tellSecretReaction = new Reaction();
							Reaction leaveSecretReaction = new Reaction();
							Reaction dontLeaveSecretReaction = new Reaction();
					Reaction leaveReaction = new Reaction();
						Reaction saveLifeReaction = new Reaction();
							Reaction stillSaveLifeReaction = new Reaction();
							Reaction changeSaveLifeReaction = new Reaction();
						Reaction maybeSaveLifeReaction = new Reaction();
						Reaction dontSaveLifeReaction = new Reaction();
				Reaction questionReaction = new Reaction();
				Reaction nothingReaction = new Reaction();
					Reaction shareBurdenReaction = new Reaction();
						Reaction yesToTearsReaction = new Reaction();
						Reaction noToTearsReaction = new Reaction();
					Reaction dontShareBurdenReaction = new Reaction();
			Reaction togetherReaction = new Reaction();
				Reaction saveLoverReaction = new Reaction();
					Reaction loverRiskLifeReaction = new Reaction();
						Reaction yesRiskLifeReaction = new Reaction();
						Reaction noRiskLifeReaction  = new Reaction();
					Reaction loverUnsureLifeReaction = new Reaction();
					Reaction loverNeverLifeReaction = new Reaction();
				Reaction saveFamilyReaction = new Reaction();
					Reaction familyQuestion = new Reaction();
				Reaction saveMentorReaction = new Reaction();
					Reaction mentorQuestion = new Reaction();
		
		
		Reaction rightReaction = new Reaction();
		
		public BeginFortuneState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue) {
			
			leftReaction.AddAction(new NPCCallbackAction(UpdateLeftChoice));
				aloneReaction.AddAction(new NPCCallbackAction(UpdateAloneChoice));
					aidReaction.AddAction(new NPCCallbackAction(UpdateAidChoice));
						helpReaction.AddAction(new NPCCallbackAction(UpdateHelpChoice)); 
							tellSecretReaction.AddAction(new NPCCallbackAction(UpdateTellSecretChoice));
							keepSecretReaction.AddAction(new NPCCallbackAction(UpdateKeepSecretChoice));
								leaveSecretReaction.AddAction(new NPCCallbackAction(UpdateLeaveSecretChoice));
								dontLeaveSecretReaction.AddAction(new NPCCallbackAction(UpdateLeaveSecretChoice));
						leaveReaction.AddAction(new NPCCallbackAction(UpdateLeaveChoice));
							saveLifeReaction.AddAction(new NPCCallbackAction(UpdateSaveLifeChoice));
								stillSaveLifeReaction.AddAction(new NPCCallbackAction(UpdateStillSaveLifeChoice));
								changeSaveLifeReaction.AddAction(new NPCCallbackAction(UpdateChangeSaveLifeChoice));
							maybeSaveLifeReaction.AddAction(new NPCCallbackAction(UpdateMaybeSaveLifeChoice));
							dontSaveLifeReaction.AddAction(new NPCCallbackAction(UpdateDontSaveLifeChoice));
					questionReaction.AddAction(new NPCCallbackAction(UpdateQuestionChoice));
					nothingReaction.AddAction(new NPCCallbackAction(UpdateNothingChoice));
						shareBurdenReaction.AddAction(new NPCCallbackAction(UpdateShareBurdenChoice));
							yesToTearsReaction.AddAction(new NPCCallbackAction(UpdateYesToTearsChoice));
							noToTearsReaction.AddAction(new NPCCallbackAction(UpdateNoToTearsChoice));
						dontShareBurdenReaction.AddAction(new NPCCallbackAction(UpdateDontShareBurdenChoice));
				togetherReaction.AddAction(new NPCCallbackAction(UpdateTogetherChoice));
					saveLoverReaction.AddAction(new NPCCallbackAction(UpdateSaveLoverChoice));
						loverRiskLifeReaction.AddAction(new NPCCallbackAction(UpdateLoverRiskLifeChoice));
							yesRiskLifeReaction.AddAction(new NPCCallbackAction(UpdateYesRiskLifeChoice));
							noRiskLifeReaction.AddAction(new NPCCallbackAction(UpdateNoRiskLifeChoice));
						loverUnsureLifeReaction.AddAction(new NPCCallbackAction(UpdateLoverUnsureLifeChoice));
						loverNeverLifeReaction.AddAction(new NPCCallbackAction(UpdateLoverNeverLifeChoice));
					saveFamilyReaction.AddAction(new NPCCallbackAction(UpdateSaveFamilyChoice));
						
					saveMentorReaction.AddAction(new NPCCallbackAction(UpdateSaveMentorChoice));			
						
			
			rightReaction.AddAction(new NPCCallbackAction(UpdateRightChoice));

			
			_allChoiceReactions.Add(leftChoice, new DispositionDependentReaction(leftReaction));
			_allChoiceReactions.Add(rightChoice, new DispositionDependentReaction(rightReaction));
		}
		#region Left Choice		
		//
		public void UpdateLeftChoice() {
			_allChoiceReactions.Remove(leftChoice);
			_allChoiceReactions.Remove(rightChoice);
			_allChoiceReactions.Add(togetherChoice, new DispositionDependentReaction(togetherReaction));
			_allChoiceReactions.Add(aloneChoice, new DispositionDependentReaction(aloneReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		//
		public void UpdateAloneChoice() {
			_allChoiceReactions.Remove(togetherChoice);
			_allChoiceReactions.Remove(aloneChoice);
			_allChoiceReactions.Add(aidChoice, new DispositionDependentReaction(aidReaction));
			_allChoiceReactions.Add(questionChoice, new DispositionDependentReaction(questionReaction));
			_allChoiceReactions.Add(nothingChoice, new DispositionDependentReaction(nothingReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		//
		public void UpdateAidChoice() {
			_allChoiceReactions.Remove(aidChoice);
			_allChoiceReactions.Remove(questionChoice);
			_allChoiceReactions.Remove(nothingChoice);
			_allChoiceReactions.Add(leaveChoice, new DispositionDependentReaction(leaveReaction));
			_allChoiceReactions.Add(helpChoice, new DispositionDependentReaction(helpReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		//
			public void UpdateHelpChoice() {
				_allChoiceReactions.Remove(helpChoice);
				_allChoiceReactions.Remove(leaveChoice);
				_allChoiceReactions.Add(tellSecretChoice, new DispositionDependentReaction(tellSecretReaction));
				_allChoiceReactions.Add(keepSecretChoice, new DispositionDependentReaction(keepSecretReaction));
				GUIManager.Instance.RefreshInteraction();
			}
		#endregion
		#region here
				public void UpdateKeepSecretChoice() {
					_allChoiceReactions.Remove(keepSecretChoice);
					_allChoiceReactions.Remove(tellSecretChoice);
					//_allChoiceReactions.Add(leaveSecretChoice, new DispositionDependentReaction(dontLeaveSecretReaction));
					//_allChoiceReactions.Add(dontLeaveSecretChoice, new DispositionDependentReaction(leaveSecretReaction));
					GUIManager.Instance.RefreshInteraction();
					//GUIManager.Instance.CloseInteractionMenu();
					//Activate passive chat
				}
		
				public void UpdateTellSecretChoice() {
					_allChoiceReactions.Remove(keepSecretChoice);
					_allChoiceReactions.Remove(tellSecretChoice);
					_allChoiceReactions.Add(leaveSecretChoice, new DispositionDependentReaction(leaveSecretReaction));
					_allChoiceReactions.Add(dontLeaveSecretChoice, new DispositionDependentReaction(dontLeaveSecretReaction));
					GUIManager.Instance.RefreshInteraction();
				}
		
			public void UpdateLeaveSecretChoice() {
				_allChoiceReactions.Remove(leaveSecretChoice);
				_allChoiceReactions.Remove(dontLeaveSecretChoice);
				//_allChoiceReactions.Add(saveFamilyChoice, new DispositionDependentReaction(saveFamilyReaction));
				//_allChoiceReactions.Add(saveLeaderChoice, new DispositionDependentReaction(saveLeaderReaction));
				GUIManager.Instance.RefreshInteraction();
			}
		#endregion
		public void UpdateLeaveChoice() {
			_allChoiceReactions.Remove(helpChoice);
			_allChoiceReactions.Remove(leaveChoice);
			_allChoiceReactions.Add(dontSaveLifeChoice, new DispositionDependentReaction(dontSaveLifeReaction));
			_allChoiceReactions.Add(saveLifeChoice, new DispositionDependentReaction(saveLifeReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void UpdateSaveLifeChoice() {
			_allChoiceReactions.Remove(saveLifeChoice);
			_allChoiceReactions.Remove(dontSaveLifeChoice);
			_allChoiceReactions.Add(changeSaveLifeChoice, new DispositionDependentReaction(changeSaveLifeReaction));
			_allChoiceReactions.Add(stillSaveLifeChoice, new DispositionDependentReaction(stillSaveLifeReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void UpdateMaybeSaveLifeChoice() {
			_allChoiceReactions.Remove(saveLifeChoice);
			_allChoiceReactions.Remove(dontSaveLifeChoice);
			_allChoiceReactions.Add(changeSaveLifeChoice, new DispositionDependentReaction(changeSaveLifeReaction));
			_allChoiceReactions.Add(stillSaveLifeChoice, new DispositionDependentReaction(stillSaveLifeReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void UpdateStillSaveLifeChoice() {
			_allChoiceReactions.Remove(stillSaveLifeChoice);
			_allChoiceReactions.Remove(changeSaveLifeChoice);
			//_allChoiceReactions.Add(saveLifeChoice, new DispositionDependentReaction(stillSaveLifeReaction));
			//_allChoiceReactions.Add(maybeSaveLifeChoice, new DispositionDependentReaction(changeSaveLifeReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void UpdateChangeSaveLifeChoice() {
			_allChoiceReactions.Remove(stillSaveLifeChoice);
			_allChoiceReactions.Remove(changeSaveLifeChoice);
			//_allChoiceReactions.Add(saveLifeChoice, new DispositionDependentReaction(stillSaveLifeReaction));
			//_allChoiceReactions.Add(maybeSaveLifeChoice, new DispositionDependentReaction(changeSaveLifeReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void UpdateDontSaveLifeChoice() {
			_allChoiceReactions.Remove(saveLifeChoice);
			_allChoiceReactions.Remove(maybeSaveLifeChoice);
			_allChoiceReactions.Remove(dontSaveLifeChoice);
			//_allChoiceReactions.Add(dontSaveLifeChoice, new DispositionDependentReaction(dontSaveLifeReaction));
			GUIManager.Instance.RefreshInteraction();
		}
			
		public void UpdateQuestionChoice() {
			_allChoiceReactions.Remove(aidChoice);
			_allChoiceReactions.Remove(questionChoice);
			_allChoiceReactions.Remove(nothingChoice);
			
			_allChoiceReactions.Add(tellSecretChoice, new DispositionDependentReaction(tellSecretReaction));
			_allChoiceReactions.Add(keepSecretChoice, new DispositionDependentReaction(keepSecretReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void UpdateNothingChoice() {
				_allChoiceReactions.Remove(aidChoice);
			_allChoiceReactions.Remove(questionChoice);
			_allChoiceReactions.Remove(nothingChoice);
		
			_allChoiceReactions.Add(noToTearsChoice, new DispositionDependentReaction(noToTearsReaction));
			_allChoiceReactions.Add(yesToTearsChoice, new DispositionDependentReaction(yesToTearsReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void UpdateYesToTearsChoice() {
			_allChoiceReactions.Remove(yesToTearsChoice);
			_allChoiceReactions.Remove(noToTearsChoice);
		
			_allChoiceReactions.Add(dontShareBurdenChoice, new DispositionDependentReaction(dontShareBurdenReaction));
			_allChoiceReactions.Add(shareBurdenChoice, new DispositionDependentReaction(shareBurdenReaction));
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void UpdateShareBurdenChoice() {
			_allChoiceReactions.Remove(shareBurdenChoice);
			_allChoiceReactions.Remove(dontShareBurdenChoice);
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void UpdateDontShareBurdenChoice() {
			_allChoiceReactions.Remove(shareBurdenChoice);
			_allChoiceReactions.Remove(dontShareBurdenChoice);
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void UpdateNoToTearsChoice() {
			_allChoiceReactions.Remove(yesToTearsChoice);
			_allChoiceReactions.Remove(noToTearsChoice);
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void UpdateTogetherChoice() {
			_allChoiceReactions.Remove(togetherChoice);
			_allChoiceReactions.Remove(aloneChoice);
			
			//NotYetChoice = new Choice ("Not yet.", "Ok, come back soon! Predictions come and go you know!");
			_allChoiceReactions.Add(saveLoverChoice, new DispositionDependentReaction(saveLoverReaction));
			_allChoiceReactions.Add(saveFamilyChoice, new DispositionDependentReaction(saveFamilyReaction));
			_allChoiceReactions.Add(saveMentorChoice, new DispositionDependentReaction(saveMentorReaction));
			//toControl.UpdateDefaultText("");
			GUIManager.Instance.RefreshInteraction();	
		}
		#region SavePeople
		public void UpdateSaveLoverChoice() {
			_allChoiceReactions.Remove(saveLoverChoice);
			_allChoiceReactions.Remove(saveFamilyChoice);
			_allChoiceReactions.Remove(saveMentorChoice);
			_allChoiceReactions.Add(loverNeverLifeChoice, new DispositionDependentReaction(loverNeverLifeReaction));
			_allChoiceReactions.Add(loverUnsureLifeChoice, new DispositionDependentReaction(loverUnsureLifeReaction));
			_allChoiceReactions.Add(loverRiskLifeChoice, new DispositionDependentReaction(loverRiskLifeReaction));
			GUIManager.Instance.RefreshInteraction();
			
		}
		
		public void UpdateLoverRiskLifeChoice() {
			_allChoiceReactions.Remove(loverRiskLifeChoice);
			_allChoiceReactions.Remove(loverUnsureLifeChoice);
			_allChoiceReactions.Remove(loverNeverLifeChoice);
			
			_allChoiceReactions.Add(noRiskLifeChoice, new DispositionDependentReaction(noRiskLifeReaction));
			_allChoiceReactions.Add(yesRiskLifeChoice, new DispositionDependentReaction(yesRiskLifeReaction));

			GUIManager.Instance.RefreshInteraction();
		}
		
		
		public void UpdateYesRiskLifeChoice() {
			_allChoiceReactions.Remove(yesRiskLifeChoice);
			_allChoiceReactions.Remove(noRiskLifeChoice);
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void UpdateNoRiskLifeChoice() {
			_allChoiceReactions.Remove(yesRiskLifeChoice);
			_allChoiceReactions.Remove(noRiskLifeChoice);
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void UpdateLoverUnsureLifeChoice() {
			_allChoiceReactions.Remove(loverRiskLifeChoice);
			_allChoiceReactions.Remove(loverUnsureLifeChoice);
			_allChoiceReactions.Remove(loverNeverLifeChoice);
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void UpdateLoverNeverLifeChoice() {
			_allChoiceReactions.Remove(loverRiskLifeChoice);
			_allChoiceReactions.Remove(loverUnsureLifeChoice);
			_allChoiceReactions.Remove(loverNeverLifeChoice);
			GUIManager.Instance.RefreshInteraction();
		}
		
		public void UpdateSaveFamilyChoice() {
			_allChoiceReactions.Remove(saveLoverChoice);
			_allChoiceReactions.Remove(saveFamilyChoice);
			_allChoiceReactions.Remove(saveMentorChoice);
			
			//_allChoiceReactions.Add(aloneChoice, new DispositionDependentReaction(saveFamilyReaction));
			//_allChoiceReactions.Add(togetherChoice, new DispositionDependentReaction(togetherReaction));
			
			GUIManager.Instance.RefreshInteraction();
			
		}
		
		public void UpdateSaveMentorChoice() {
			_allChoiceReactions.Remove(saveLoverChoice);
			_allChoiceReactions.Remove(saveFamilyChoice);
			_allChoiceReactions.Remove(saveMentorChoice);
			
			//_allChoiceReactions.Add(aloneChoice, new DispositionDependentReaction(declineFortuneState));
			//_allChoiceReactions.Add(togetherChoice, new DispositionDependentReaction(acceptFortuneState));
			
			GUIManager.Instance.RefreshInteraction();
			
		}
		#endregion
		
		public void UpdateRightChoice() {
			_allChoiceReactions.Remove(leftChoice);
			_allChoiceReactions.Remove(rightChoice);
			_allChoiceReactions.Add(togetherChoice, new DispositionDependentReaction(togetherReaction));
			_allChoiceReactions.Add(aloneChoice, new DispositionDependentReaction(aloneReaction));
			GUIManager.Instance.RefreshInteraction();
		}	
	}
	#endregion				
	#region Initial Emotion State
	private class StillConvincingState : EmotionState {
		bool choicesActivated = false;
		Choice readyChoice = new Choice("I'm ready!", "Excellent choice! Given two path, do you take the left path or the right path?");
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
		acceptFortuneState.AddAction(new NPCCallbackOnNPCAction(updateCurrentText, toControl));
		acceptFortuneState.AddAction(new NPCEmotionUpdateAction(toControl, new BeginFortuneState(toControl, "Let's begin~" + "\n\n" + "Given two paths, should you take the left one or the right one?")));
//
		declineFortuneState.AddAction(new NPCCallbackOnNPCAction(updateDecline, toControl));		
//
		changeToGiveUpState.AddAction(new NPCEmotionUpdateAction(toControl, new PostFortuneState(toControl, ". . .")));
//
		}
		
		public void updateCurrentText(NPC toControl) {
			toControl.UpdateDefaultText("Do you take the left path or the right path?");	
			GUIManager.Instance.RefreshInteraction();
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
