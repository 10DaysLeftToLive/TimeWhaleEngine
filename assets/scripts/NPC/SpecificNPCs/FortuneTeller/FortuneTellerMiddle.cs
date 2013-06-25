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
		Reaction giveWhittleReaction = new Reaction();
		#endregion
		
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			//items sorted alphabetically
			
			//Apple
			giveAppleReaction.AddAction(new UpdateCurrentTextAction(toControl, "I predict this apple is important to one of your family members."));
			_allItemReactions.Add(StringsItem.Apple,  new DispositionDependentReaction(giveAppleReaction));
			
			//Apple Pie
			giveApplePieReaction.AddAction(new UpdateCurrentTextAction(toControl, "Mmm... I sense a new beginning, a bittersweet celebration."));
			_allItemReactions.Add(StringsItem.ApplePie,  new DispositionDependentReaction(giveApplePieReaction));
			
			//Captain Log
			giveCaptainLogReaction.AddAction(new UpdateCurrentTextAction(toControl, "A tale of the sea, seemingly from a land afar. Perhaps treasure is involved too~"));
			_allItemReactions.Add(StringsItem.CaptainLog,  new DispositionDependentReaction(giveCaptainLogReaction));
			
			//Fishing Rod
			giveFishingRodReaction.AddAction(new UpdateCurrentTextAction(toControl, "The lake's home to the biggest fish. But only by he who has passion and gives up his legacy~"));
			_allItemReactions.Add(StringsItem.FishingRod,  new DispositionDependentReaction(giveFishingRodReaction));
			
			//Flute
			giveFluteReaction.AddAction(new UpdateCurrentTextAction(toControl, "I hear a tune near the new direction of the wind~"));
			_allItemReactions.Add(StringsItem.Flute,  new DispositionDependentReaction(giveFluteReaction));
			
			//Harp
			giveHarpReaction.AddAction(new UpdateCurrentTextAction(toControl, "The strum of a master plays a tune a little ways from the market~"));
			_allItemReactions.Add(StringsItem.Harp,  new DispositionDependentReaction(giveHarpReaction));

			
			//Lily Seeds
			giveLilySeedsReaction.AddAction(new UpdateCurrentTextAction(toControl, "Planted with others makes the garden even more beautiful~"));
			_allItemReactions.Add(StringsItem.LilySeeds,  new DispositionDependentReaction(giveLilySeedsReaction));
						
			//Note
			giveNoteReaction.AddAction(new UpdateCurrentTextAction(toControl, "I feel great emotions behind this and a great decision before you~"));
			_allItemReactions.Add(StringsItem.Note,  new DispositionDependentReaction(giveNoteReaction));
			
			//Pendant
			givePendantReaction.AddAction(new UpdateCurrentTextAction(toControl, "Return this to whom it belongs and they will be at peace~"));
			_allItemReactions.Add(StringsItem.Pendant,  new DispositionDependentReaction(givePendantReaction));
			
			//Rose
			giveRoseReaction.AddAction(new UpdateCurrentTextAction(toControl, "For me? No, someone else~"));
			_allItemReactions.Add(StringsItem.Rose,  new DispositionDependentReaction(giveRoseReaction));
			
			//Seashell
			giveSeashellReaction.AddAction(new UpdateCurrentTextAction(toControl, "An ordinary item to one is a priceless item to another~"));
			_allItemReactions.Add(StringsItem.Seashell,  new DispositionDependentReaction(giveSeashellReaction));
			
			//Shovel
			giveShovelReaction.AddAction(new UpdateCurrentTextAction(toControl, "An X, but perhaps what you're looking for has already been taken~"));
			_allItemReactions.Add(StringsItem.Shovel,  new DispositionDependentReaction(giveShovelReaction));
			
			//Sunflower Seeds
			giveSunflowerSeedsReaction.AddAction(new UpdateCurrentTextAction(toControl, "Follow the sun and plant the seeds there~"));
			_allItemReactions.Add(StringsItem.SunflowerSeeds,  new DispositionDependentReaction(giveSunflowerSeedsReaction));
			
			//Toolbox
			giveToolboxReaction.AddAction(new UpdateCurrentTextAction(toControl, "Trees, lots of trees. Who would need tools for tending to trees?~"));
			_allItemReactions.Add(StringsItem.Toolbox,  new DispositionDependentReaction(giveToolboxReaction));
			
			//Toy Boat
			giveToyBoatReaction.AddAction(new UpdateCurrentTextAction(toControl, "Perhaps the owner of a vessel knows its secrets~"));
			_allItemReactions.Add(StringsItem.ToyBoat,  new DispositionDependentReaction(giveToyBoatReaction));
						
			//Toy Sword
			giveToySwordReaction.AddAction(new UpdateCurrentTextAction(toControl, "What seems like a toy may actually be more~"));
			_allItemReactions.Add(StringsItem.ToySword,  new DispositionDependentReaction(giveToySwordReaction));
			
			//Tulip Seeds
			giveTulipSeedsReaction.AddAction(new UpdateCurrentTextAction(toControl, "Where two lips touch is where tulips should go~"));
			_allItemReactions.Add(StringsItem.TulipSeeds,  new DispositionDependentReaction(giveTulipSeedsReaction));
			
			//Vegetable
			giveVegetableReaction.AddAction(new UpdateCurrentTextAction(toControl, "Some sort of animal awaits its prize."));
			_allItemReactions.Add(StringsItem.Vegetable,  new DispositionDependentReaction(giveVegetableReaction));
		
			giveWhittleReaction.AddAction(new UpdateCurrentTextAction(toControl, "A skilled craftsman needs just a simple tool to create a masterpiece."));
			_allItemReactions.Add(StringsItem.Whittle,  new DispositionDependentReaction(giveWhittleReaction));
		}
		
		public override void UpdateEmotionState(){
			
		}
	
	}
	#endregion
	#endregion
}
