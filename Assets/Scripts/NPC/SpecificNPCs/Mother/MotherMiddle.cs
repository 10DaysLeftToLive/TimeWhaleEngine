using UnityEngine;
using System.Collections;

/// <summary>
/// Mother middle specific scripting values
/// </summary>
public class MotherMiddle : NPC {
	protected override void Init() {
		id = NPCIDs.MOTHER;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		Reaction gaveItems = new Reaction();
		gaveItems.AddAction(new UpdateCurrentTextAction(this, "Unlocked new secret love story!!"));
		flagReactions.Add(FlagStrings.GiveItems, gaveItems);
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Hello"));
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
		string[] stringList = {"random message 1", "random message 2", "random message 3", "random message 4", "random message 5", "random message 6", "random message 7"};
		Reaction gaveRose;
		Reaction gavePendant;
		Reaction gaveSeashell;
		Reaction randomMessage;
		bool rose = false, pendant = false, seashell = false;
	
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			gaveRose = new Reaction();
			gavePendant = new Reaction();
			gaveSeashell = new Reaction();
			randomMessage = new Reaction();
			
			gaveRose.AddAction(new NPCTakeItemAction(toControl));
			gaveRose.AddAction(new NPCCallbackAction(SetRose));
			//gaveRose.AddAction(new UpdateDefaultTextAction(toControl, "new default"));
			gaveRose.AddAction(new UpdateCurrentTextAction(toControl, "My bedroom window when I was little had this bed of peach colored roses, they made my room smell wonderful in the summers."));
			_allItemReactions.Add("apple",  new DispositionDependentReaction(gaveRose)); // change item to rose
			
			gavePendant.AddAction(new NPCTakeItemAction(toControl));
			gaveRose.AddAction(new NPCCallbackAction(SetPendant)); // CHANGE gaveRose
			gavePendant.AddAction(new UpdateCurrentTextAction(toControl, "I know this seems just like a silly necklace but your father worked for weeks when we were young to buy this for me. It still makes me smile."));
			_allItemReactions.Add("pendant",  new DispositionDependentReaction(gaveRose));
			
			gaveSeashell.AddAction(new NPCTakeItemAction(toControl));
			gaveRose.AddAction(new NPCCallbackAction(SetSeashell));  // CHANGE gaveRose
			gaveSeashell.AddAction(new UpdateCurrentTextAction(toControl, "Your father and I would spend afternoonds looking for shells. He loved to find the shiniest ones he could for me."));
			_allItemReactions.Add("seashell",  new DispositionDependentReaction(gaveRose));
			
			
			randomMessage.AddAction(new NPCCallbackAction(RandomMessage));
			
			SetOnOpenInteractionReaction(new DispositionDependentReaction(randomMessage));
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
			SetDefaultText(stringList[(int)Random.Range(0,6)]);
		}
		
		public override void UpdateEmotionState(){
			
		}
	
	}
	#endregion
	#endregion
}
