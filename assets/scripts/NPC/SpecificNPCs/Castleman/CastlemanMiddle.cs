using UnityEngine;
using System.Collections;

/// <summary>
/// CastlemanMiddle specific scripting values
/// </summary>
public class CastlemanMiddle : NPC {
	protected override void Init() {
		id = NPCIDs.CASTLE_MAN;
		base.Init();
	}
	
	protected override void SetFlagReactions(){
		
	}
	
	protected override EmotionState GetInitEmotionState(){
		return (new InitialEmotionState(this, "Are you looking for a Castle too?"));
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
		Reaction gaveSeashell;
		Reaction gaveRose;
		Reaction lookLike;
		Reaction lastSeen;
		Reaction randomMessage;
		bool rose = false, seaShell = false;
		//List of random things the insane castleman says.
		string[] stringList = {"Are you looking for a Castle too?",
				"Castle castle castle castle castle...Oh hello there!",
				"In the sky my castle!  No wait that's a bird...",
				"Have you found it! Have you found it!  Have you found it!",
				"I think I saw my castle walking around yesterday!",
				"Look under that leave!  My castle!  Wait...that's an earthworm...",
				"Is my castle in the sky?  Is my castle in a pie?  Is my castle in the rye?",
				"Oh castle, Oh castle, how great thy beauty is!",
				"I wish I was a castle..."};
		int stringCounter = 9; //Counts the number of strings in string list.
		
		
		public InitialEmotionState(NPC toControl, string currentDialogue) : base(toControl, currentDialogue){
			Choice seen = new Choice("Last Seen", "Twas at a beach!  When my castle was swallowed by the cavernous waves!");
			Choice look = new Choice("What does it look like?", "It was glowing with great light, and lined with many flags and alabaster walls.  It was the most beautiful thing in the world....Please find it for me.");
			
			//Code for the choices for interacting with the castleman
			lastSeen = new Reaction();
			lookLike = new Reaction();
			lastSeen.AddAction(new UpdateCurrentTextAction(toControl, "Twas at a beach!  When my castle was swallowed by the cavernous waves!"));
			lookLike.AddAction(new UpdateCurrentTextAction(toControl, "blah"));
			
			//Code for giving the seashell
			Reaction gaveShell = new Reaction();
			gaveShell.AddAction(new NPCTakeItemAction(toControl));
			gaveShell.AddAction(new NPCCallbackAction(SetSeaShell));
			gaveShell.AddAction(new UpdateCurrentTextAction(toControl, "A piece of the wall of my castle!  Perhaps there is hope for my sufferings!"));
			_allItemReactions.Add(StringsItem.SeaShell,  new DispositionDependentReaction(gaveShell)); // change item to shell
			
			//Code for the giving rose interaction.
			gaveRose = new Reaction();
			gaveRose.AddAction(new NPCTakeItemAction(toControl));
			gaveRose.AddAction(new NPCCallbackAction(SetRose));
			gaveRose.AddAction(new UpdateCurrentTextAction(toControl, "A piece of the jungle of thorns!  If you can brave that perhaps...perhaps you are the one who can truly find my castle."));
			_allItemReactions.Add(StringsItem.Rose,  new DispositionDependentReaction(gaveRose)); // change item to rose
			
			_allChoiceReactions.Add(seen, new DispositionDependentReaction(lastSeen));
			_allChoiceReactions.Add(look, new DispositionDependentReaction(lookLike));
			
			//Sets up the random dialogue for the castleman.
			randomMessage = new Reaction();
			randomMessage.AddAction(new NPCCallbackAction(RandomMessage));
			SetOnOpenInteractionReaction(new DispositionDependentReaction(randomMessage));
		}
		public void RandomMessage(){
			SetDefaultText(stringList[(int)Random.Range(0,stringCounter)]);
		}
		public void SetRose(){
			rose = true;
			if (seaShell == true && rose == true){
				//Set a flag here for the castleman
			}
		}
		public void SetSeaShell(){
			seaShell = true;
			if (seaShell == true && rose == true){
				//Set a flag here for the castleman
			}
		}
		public override void UpdateEmotionState(){
			
		}
	
	}
	private class SaneState: EmotionState{
		Choice say;
		Reaction SayWhat;
		Choice writing;
		Reaction writingWhat;
		Choice LetHer;
		Reaction LetHerJudge;
		
		public SaneState (NPC toControl, string currentDialogue): base(toControl, currentDialogue){
			say = new Choice("What does it say?", "It's never right.  I mean look at this: 'Roses are red'?  How pedestrian can you get?  The farmer's daughter will never like me with garbage like that...");
			writing = new Choice("What are you writing?", "It's a love letter for the farmer's daughter...it's just all wrong.  I mean 'Roses are red'?  So simplistic.");
			SayWhat = new Reaction();
			writingWhat = new Reaction();
			
			SayWhat.AddAction(new UpdateCurrentTextAction(toControl, "It's never right.  I mean look at this: 'Roses are red'?  How pedestrian can you get?  The farmer's daughter will never like me with garbage like that..."));
			SayWhat.AddAction(new NPCCallbackAction(changeChoices));
			writingWhat.AddAction(new UpdateCurrentTextAction(toControl, "What are you writing? It's a love letter for the farmer's daughter...it's just all wrong.  I mean 'Roses are red'?  So simplistic."));
			writingWhat.AddAction(new NPCCallbackAction(changeChoices));
			
			LetHer = new Choice("Have you tried letting her judge it?", "But what if its not perfect? Hold on.  Maybe you have a point.  Here, you try and deliver it to her.  The farmer never lets me anywhere near her daughter.");
			LetHerJudge = new Reaction();
			LetHerJudge.AddAction(new UpdateCurrentTextAction(toControl, "But what if its not perfect? Hold on.  Maybe you have a point.  Here, you try and deliver it to her.  The farmer never lets me anywhere near her daughter."));
			
			
			_allChoiceReactions.Add(say, new DispositionDependentReaction(SayWhat));
			_allChoiceReactions.Add(writing, new DispositionDependentReaction(writingWhat));
			
		}
		public void changeChoices(){
			_allChoiceReactions.Clear();
			_allChoiceReactions.Add(LetHer, new DispositionDependentReaction(LetHerJudge));
			
		}
	}
	#endregion
	#endregion
}
