using UnityEngine;
using System.Collections;

/// <summary>
/// Farmer Mother young specific scripting values
/// </summary>
public class FarmerMotherYoung : NPC {	
	protected override void Init() {
		id = NPCIDs.FARMER_MOTHER;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Talk to me later. I'm busy!"));
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}
	
	Schedule openningWaitingSchedule;
	Schedule postOpenningSchedule;

	protected override void SetUpSchedules(){
		//Wait for player to come before initiating Mother talking to Father
		openningWaitingSchedule = new Schedule(this, Schedule.priorityEnum.DoNow);
		openningWaitingSchedule.Add(new TimeTask(30, new WaitTillPlayerCloseState(this, player)));
		openningWaitingSchedule.Add(new TimeTask(10, new MoveThenDoState(this, new Vector3(transform.position.x + 1,transform.position.y, transform.position.z), new IdleState(this))));
		scheduleStack.Add(openningWaitingSchedule);
		
		//After talking to father about daughter
		postOpenningSchedule = new Schedule(this,Schedule.priorityEnum.High);
		postOpenningSchedule.Add(new TimeTask(10, new IdleState(this)));
		postOpenningSchedule.Add(new TimeTask(10, new MoveThenDoState(this, new Vector3(transform.position.x - 10,transform.position.y, transform.position.z), new IdleState(this))));
		postOpenningSchedule.Add(new TimeTask(30, new ChangeEmotionState(this, new PostOpenningConvoEmotionState(this, "What do you want? I got lots of work to do!"))));
		scheduleStack.Add(postOpenningSchedule);
		
		//Talks to father about daughter
		scheduleStack.Add(new NPCConvoSchedule(this, NPCManager.instance.getNPC(StringsNPC.FarmerFatherYoung), 
			new YoungFarmerMotherToFarmerFatherOpenningScriptedDialogue(),Schedule.priorityEnum.High));
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
	
	#region Post-Openning Conversation Emotion State
	private class PostOpenningConvoEmotionState : EmotionState{
		NPC farmerMotherYoung;
		
		Reaction reactionToWhatAbout;
		Choice whatAboutChoice = new Choice("What was that about?", "You saw our little spat? Don't worry about it! " +
			"It's just some talk on how ta raise our daughter. Don't need ta fill her head with silly stories...");
		
		Choice growGardenChoice = new Choice("Yes", "Well then, ya should be well versed in how ta plant seeds." +
										"Here in exchange for tha pendant! I'll give ya these sunflower seeds. ");
		Choice dontGrowGardenChoice = new Choice("No", "Well, I can teach ya how if ya want ta! " +
			"Just find a mound of earth and put tha seeds you're carrying in it. Then just let time do its work."  +
				"Here in exchange for tha pendant! I'll give ya these sunflower seeds.");
		
		Choice tellOnDaughterChoice = new Choice ("Tell on daughter", "*Sigh* Thanks fer talking ta me about this.");
		
	
		public PostOpenningConvoEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			farmerMotherYoung = toControl;
			
			reactionToWhatAbout = new Reaction(new NPCCallbackAction(askedWhatAbout));
			_allChoiceReactions.Add(whatAboutChoice, new DispositionDependentReaction(reactionToWhatAbout));
			
			Reaction reactionToGivePendant = new Reaction();
			reactionToGivePendant.AddAction(new NPCTakeItemAction(toControl));
			reactionToGivePendant.AddAction(new NPCCallbackAction(givePendant));
			
			_allItemReactions.Add(StringsItem.Apple, new DispositionDependentReaction(reactionToGivePendant));
			
		}
		
		public override void UpdateEmotionState(){
			
		}
			
		private void askedWhatAbout(){
			_allChoiceReactions.Remove(whatAboutChoice);
			GUIManager.Instance.RefreshInteraction();
		}
		
		private void tellOnDaughter(){
			farmerMotherYoung.AddSchedule(new NPCConvoSchedule(farmerMotherYoung, NPCManager.instance.getNPC(StringsNPC.LighthouseGirlYoung), 
				new YoungFarmerMotherToLighthouseGirlToldOn(),Schedule.priorityEnum.DoNow));
				
		}

		private void givePendant(){
			_allChoiceReactions.Remove(whatAboutChoice);
			Reaction reactionToGiveSunflowerSeeds = new Reaction(new NPCCallbackAction(dropSunflowerSeeds));
			
			_allItemReactions.Remove(StringsItem.Apple);
			SetDefaultText("You want to grow a flower garden, eh? Ya ever done it before?");
			
			_allChoiceReactions.Add(growGardenChoice, new DispositionDependentReaction(reactionToGiveSunflowerSeeds));
			_allChoiceReactions.Add(dontGrowGardenChoice, new DispositionDependentReaction(reactionToGiveSunflowerSeeds));
			
			GUIManager.Instance.RefreshInteraction();
		}
		
		private void dropSunflowerSeeds(){
			_allChoiceReactions.Remove(growGardenChoice);
			_allChoiceReactions.Remove(dontGrowGardenChoice);
			GUIManager.Instance.RefreshInteraction();
			SetDefaultText("What do you want? I got lots of work to do!");
			_allChoiceReactions.Add(whatAboutChoice, new DispositionDependentReaction(reactionToWhatAbout));
			
		}
					
		
	
	}
	#endregion
	#endregion
}
