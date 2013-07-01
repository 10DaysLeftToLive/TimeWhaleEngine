using UnityEngine;
using System.Collections;

/// <summary>
/// Mother middle specific scripting values
/// </summary>
public class MotherMiddle : NPC {
	InitialEmotionState state;
	
	protected override void SetFlagReactions(){
		Reaction gaveItems = new Reaction();
		//gaveItems.AddAction(new UpdateCurrentTextAction(this, "Unlocked new secret love story!!"));
		//flagReactions.Add(FlagStrings.GiveItems, gaveItems);
		
		Reaction motherYoungSeeds = new Reaction();
		motherYoungSeeds.AddAction(new UpdateCurrentTextAction(this, "Unlocked new secret love story!!"));
		motherYoungSeeds.AddAction(new NPCCallbackAction(SendText)); 
		flagReactions.Add(FlagStrings.GiveItems, motherYoungSeeds);
	}
	
	protected override EmotionState GetInitEmotionState(){
		state  = new InitialEmotionState(this, "Hello");
		return (state);
	}
	
	protected override Schedule GetSchedule(){
		Schedule schedule = new DefaultSchedule(this);
		return (schedule);
	}

	protected override void SetUpSchedules(){
		
	}
	
	public void SendText(){
		//state.PassStringToEmotionState("new string");
	}
	
	
	#region EmotionStates
	#region Initial Emotion State
	private class InitialEmotionState : EmotionState{
		string[] stringList = {"Hello dear... how are you?", "The Garden looks ok... but I wish it was more lively.", "*cough* *cough* *cough*", "Want to hear a story?"};
		int stringCounter = 4;
		Reaction gaveRose;
		Reaction gavePendant;
		Reaction gaveSeashell;
		Reaction randomMessage;
		bool rose = false, pendant = false, seashell = false;
		
		Choice TempFarmerReturnChoice = new Choice("Please bring back the farmers!", "Fine... with a twirl of my wrist... poof! the farmers have returned!");
		Reaction TempFarmerReturnReaction = new Reaction();
	
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			gaveRose = new Reaction();
			gavePendant = new Reaction();
			gaveSeashell = new Reaction();
			randomMessage = new Reaction();
			
			gaveRose.AddAction(new NPCCallbackAction(SetSeashell));  // DELETE
			gaveRose.AddAction(new NPCCallbackAction(SetPendant)); // DELETE
			
			gaveRose.AddAction(new NPCTakeItemAction(toControl));
			gaveRose.AddAction(new NPCCallbackAction(SetRose));
			//gaveRose.AddAction(new UpdateDefaultTextAction(toControl, "new default"));
			gaveRose.AddAction(new UpdateCurrentTextAction(toControl, "My bedroom window when I was little had this bed of peach colored roses, they made my room smell wonderful in the summers."));
			_allItemReactions.Add(StringsItem.Apple,  new DispositionDependentReaction(gaveRose)); // change item to rose
			
			gavePendant.AddAction(new NPCTakeItemAction(toControl));
			gavePendant.AddAction(new NPCCallbackAction(SetPendant)); 
			gavePendant.AddAction(new UpdateCurrentTextAction(toControl, "I know this seems just like a silly necklace but your father worked for weeks when we were young to buy this for me. It still makes me smile."));
			_allItemReactions.Add("pendant",  new DispositionDependentReaction(gaveRose));
			
			gaveSeashell.AddAction(new NPCTakeItemAction(toControl));
			gaveSeashell.AddAction(new NPCCallbackAction(SetSeashell));  
			gaveSeashell.AddAction(new UpdateCurrentTextAction(toControl, "Your father and I would spend afternoons looking for shells. He loved to find the shiniest ones he could for me."));
			_allItemReactions.Add("seashell",  new DispositionDependentReaction(gaveRose));
			
			
			randomMessage.AddAction(new NPCCallbackAction(RandomMessage));
			
			SetOnOpenInteractionReaction(new DispositionDependentReaction(randomMessage));
			
			TempFarmerReturnReaction.AddAction(new UpdateCurrentTextAction(toControl, "Blah"));
			TempFarmerReturnReaction.AddAction(new NPCCallbackAction(TempResponse));
			_allChoiceReactions.Add(TempFarmerReturnChoice,new DispositionDependentReaction(TempFarmerReturnReaction));
		}
		
		public void TempResponse(){
			FlagManager.instance.SetFlag(FlagStrings.FarmAlive);
		}
		
		public void UpdateText() {
			//FlagManager.instance.SetFlag(FlagStrings.GaveApple);
		}
		
		public void SetRose(){
			rose = true;
			if (rose && pendant && seashell){
				FlagManager.instance.SetFlag(FlagStrings.GiveItems);
			}
		}
		
		public void SetPendant(){
			pendant = true;
			if (rose && pendant && seashell){
				FlagManager.instance.SetFlag(FlagStrings.GiveItems);
			}
		}
		
		public void SetSeashell(){
			seashell = true;
			if (rose && pendant && seashell){
				FlagManager.instance.SetFlag(FlagStrings.GiveItems);
			}
		}
		
		public void RandomMessage(){
			SetDefaultText(stringList[(int)Random.Range(0,stringCounter)]);
		}
		
		public override void PassStringToEmotionState(string text){
			stringCounter++;
		}
	
	}
	#endregion
	#endregion
}
