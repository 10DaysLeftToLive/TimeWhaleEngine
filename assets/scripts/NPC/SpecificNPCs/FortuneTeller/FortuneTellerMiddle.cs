using UnityEngine;
using System.Collections;

/// <summary>
/// FortuneTellerMiddle specific scripting values
/// </summary>
public class FortuneTellerMiddle : NPC {
	protected override void Init() {
		id = NPCIDs.FORTUNE_TELLER;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		//appleReaction.AddAction()
			
		/*Reaction enterHappy = new Reaction();
		enterHappy.AddAction(new ShowOneOffChatAction(this, "I think there's a good spot over here!"));
		enterHappy.AddAction(new NPCAddScheduleAction(this, moveMotherHappyState));
		flagReactions.Add (FlagStrings.EnterHappyState, enterHappy);*/
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Have an item for me to appraise?"));
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
		
		#region Reaction Declarations
		Reaction giveAppleReaction = new Reaction();
		Reaction giveApplePieReaction = new Reaction();
		Reaction giveCaptainLogReaction = new Reaction();
		Reaction giveFishingRodReaction = new Reaction();
		Reaction giveFluteReaction = new Reaction();
		Reaction giveHarpReaction = new Reaction();
		Reaction giveLilySeedsReaction = new Reaction();
		Reaction giveNoteReaction = new Reaction();
		Reaction givePendantReaction = new Reaction();
		Reaction giveRoseReaction = new Reaction();
		Reaction giveSeashellReaction = new Reaction();
		Reaction giveShovelReaction = new Reaction();
		Reaction giveSunflowerSeedsReaction = new Reaction();
		Reaction giveToolboxReaction = new Reaction();
		Reaction giveToyBoatReaction = new Reaction();
		Reaction giveToySwordReaction = new Reaction();
		Reaction giveTulipSeedsReaction = new Reaction();
		Reaction giveVegetableReaction = new Reaction();
		#endregion
		
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			//items sorted alphabetically
			
			//Apple
			giveAppleReaction.AddAction(new UpdateCurrentTextAction(toControl, "I predict this apple is important to one of your family members~"));
			_allItemReactions.Add("apple",  new DispositionDependentReaction(giveAppleReaction));
			
			//Apple Pie
			giveApplePieReaction.AddAction(new UpdateCurrentTextAction(toControl, "Mmm. Who you give this to will surely appreciate your visit in their time of new arrival~"));
			_allItemReactions.Add("Apple Pie",  new DispositionDependentReaction(giveApplePieReaction));
			
			//Captain Log
			giveCaptainLogReaction.AddAction(new UpdateCurrentTextAction(toControl, "A tale of the sea, seemingly from a land afar. Perhaps treasure is involved too~"));
			_allItemReactions.Add("Captain's Log",  new DispositionDependentReaction(giveCaptainLogReaction));
			
			//Fishing Rod
			giveFishingRodReaction.AddAction(new UpdateCurrentTextAction(toControl, "The lake catches the biggest fish. But only by he who has passion and gives up his legacy~"));
			_allItemReactions.Add("Fishing Rod",  new DispositionDependentReaction(giveFishingRodReaction));
			
			//Flute
			giveFluteReaction.AddAction(new UpdateCurrentTextAction(toControl, "I hear a tune near the new direction of the wind~"));
			_allItemReactions.Add("Flute",  new DispositionDependentReaction(giveFluteReaction));
			
			//Harp
			giveHarpReaction.AddAction(new UpdateCurrentTextAction(toControl, "The strum of a master plays a tune a little ways from the market~"));
			_allItemReactions.Add("Harp",  new DispositionDependentReaction(giveHarpReaction));

			
			//Lily Seeds
			giveLilySeedsReaction.AddAction(new UpdateCurrentTextAction(toControl, "Planted with others makes the garden even more beautiful~"));
			_allItemReactions.Add("Lily Seeds",  new DispositionDependentReaction(giveLilySeedsReaction));
						
			//Note
			giveNoteReaction.AddAction(new UpdateCurrentTextAction(toControl, "Love can be determined with something as simple as this~"));
			_allItemReactions.Add("Note",  new DispositionDependentReaction(giveNoteReaction));
			
			//Pendant
			givePendantReaction.AddAction(new UpdateCurrentTextAction(toControl, "Return this to whom it belongs and they will be at peace~"));
			_allItemReactions.Add("Pendant",  new DispositionDependentReaction(givePendantReaction));
			
			//Rose
			giveRoseReaction.AddAction(new UpdateCurrentTextAction(toControl, "For me? No, someone else~"));
			_allItemReactions.Add("Rose",  new DispositionDependentReaction(giveRoseReaction));
			
			//Seashell
			giveSeashellReaction.AddAction(new UpdateCurrentTextAction(toControl, "An ordinary item to one is a priceless item to another~"));
			_allItemReactions.Add("Seashell",  new DispositionDependentReaction(giveSeashellReaction));
			
			//Shovel
			giveShovelReaction.AddAction(new UpdateCurrentTextAction(toControl, "An X, but perhaps what you're looking for has already been taken~"));
			_allItemReactions.Add("shovel",  new DispositionDependentReaction(giveShovelReaction));
			
			//Sunflower Seeds
			giveSunflowerSeedsReaction.AddAction(new UpdateCurrentTextAction(toControl, "Follow the sun and plant the seeds there~"));
			_allItemReactions.Add("Sunflower",  new DispositionDependentReaction(giveSunflowerSeedsReaction));
			
			//Toolbox
			giveToolboxReaction.AddAction(new UpdateCurrentTextAction(toControl, "Trees, lots of trees. Who would need tools for tending to trees?~"));
			_allItemReactions.Add("Toolbox",  new DispositionDependentReaction(giveToolboxReaction));
			
			//Toy Boat
			giveToyBoatReaction.AddAction(new UpdateCurrentTextAction(toControl, "Perhaps the owner of a vessel knows its secrets~"));
			_allItemReactions.Add("Toy Boat",  new DispositionDependentReaction(giveToyBoatReaction));
						
			//Toy Sword
			giveToySwordReaction.AddAction(new UpdateCurrentTextAction(toControl, "What seems like a toy may actually be more~"));
			_allItemReactions.Add("Toy Sword",  new DispositionDependentReaction(giveToySwordReaction));
			
			//Tulip Seeds
			giveTulipSeedsReaction.AddAction(new UpdateCurrentTextAction(toControl, "Where two lips touch is where tulips should go~"));
			_allItemReactions.Add("Tulip Seeds",  new DispositionDependentReaction(giveTulipSeedsReaction));
			
			//Vegetable
			giveVegetableReaction.AddAction(new UpdateCurrentTextAction(toControl, "Some sort of animal awaits its prize."));
			_allItemReactions.Add("Vegetable",  new DispositionDependentReaction(giveVegetableReaction));
		}
		
		public override void UpdateEmotionState(){
			
		}
	
	}
	#endregion
	#endregion
}
