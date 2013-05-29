using UnityEngine;
using System.Collections;

public class SeaCaptainTreasureHuntSchedule : Schedule {
	public SeaCaptainTreasureHuntSchedule(NPC npc) : base(npc, Schedule.priorityEnum.High) {
	}
	
	protected override void Init ()
	{
		Vector3 startingPosition = new Vector3(74f, -3f, .05f);
		Vector3 farmDigPos = new Vector3(52.5f, 16f, .05f);
		Vector3 reflectDigPos = new Vector3(-28f, 18f, .05f);
		Vector3 carpenterDigPos = new Vector3(16f, -1f, .05f);
		Vector3 beachDigPos = new Vector3(43f, -7.5f, .05f);
		
		SetCanInteract(false);
		Add(new Task(new MoveThenMarkDoneState(_toManage, farmDigPos), _toManage, 0, "Let's go find me some treasure!"));
		Add(new TimeTask(2f, new IdleState(_toManage), _toManage, 0, "*Digging*"));
		Add(new TimeTask(3f, new IdleState(_toManage), _toManage, 0, "Ah hah! My treas-"));
		Add(new TimeTask(3f, new IdleState(_toManage), _toManage, 0, "Is this some exotic plant?"));
		Add(new TimeTask(3.5f, new IdleState(_toManage), _toManage, 0, "Well judging by your face. This is a common plant."));
		Add(new TimeTask(3f, new IdleState(_toManage), _toManage, 0f, "Where is the treasure then?"));
		Add(new TimeTask(3f, new IdleState(_toManage), _toManage, 0, "Oh! I had the map upside down."));
		Add(new Task(new MoveThenMarkDoneState(_toManage, reflectDigPos), _toManage, 0, "Come now. The map says something about a reflection tree."));
		Add(new TimeTask(3f, new IdleState(_toManage), _toManage, 0, "It must be here!"));
		Add(new TimeTask(2f, new IdleState(_toManage), _toManage, 0, "*Digging*"));
		Add(new TimeTask(2f, new IdleState(_toManage), _toManage, 0, "Nothing"));
		Add(new TimeTask(3f, new IdleState(_toManage), _toManage, 0, "Interesting... that was just an inkstain."));
		Add(new TimeTask(3f, new IdleState(_toManage), _toManage, 0, "Hmm, looks like we are supposed to go to a giant tree."));
		Add(new TimeTask(3f, new IdleState(_toManage), _toManage, 0, "I know that grumpy guy lives in the stump of one."));
		Add(new Task(new MoveThenMarkDoneState(_toManage, carpenterDigPos), _toManage, 0, "Let's try there and hope the grumpy guy isn't there."));
		Add(new TimeTask(3f, new IdleState(_toManage), _toManage, 0, "This must be it. I can feel it in me bones."));
		Add(new TimeTask(2f, new IdleState(_toManage), _toManage, 0, "*Digging*"));
		Add(new TimeTask(2f, new IdleState(_toManage), _toManage, 0, "A flute?"));
		Add(new TimeTask(2f, new IdleState(_toManage), _toManage, 0, "This can't be the treasure."));
		Add(new TimeTask(2f, new IdleState(_toManage), _toManage, 0, "Wait a second..."));
		Add(new TimeTask(3f, new IdleState(_toManage), _toManage, 0, "The X is where I am supposed to go? It isn't the map maker's signature?"));
		Add(new TimeTask(3f, new IdleState(_toManage), _toManage, 0, "Haha, I knew that all along. I was just testing you."));
		Add(new Task(new MoveThenMarkDoneState(_toManage, beachDigPos), _toManage, 0, "Now let's go dig up some treasure!"));
		Add(new TimeTask(3f, new IdleState(_toManage), _toManage, 0, "Alright, the moment of truth."));
		Add(new TimeTask(2f, new IdleState(_toManage), _toManage, 0, "*Digging*"));
		Add(new TimeTask(2f, new IdleState(_toManage), _toManage, 0, "NOOO! It isn't here!"));
		Add(new TimeTask(3f, new IdleState(_toManage), _toManage, 0, "All there is is this portrait. It must of already been plundered."));
		Add(new TimeTask(3f, new IdleState(_toManage), _toManage, 0, "I must be the worst pirate ever."));
		Add(new TimeTask(3f, new IdleState(_toManage), _toManage, 0, "I lost my ship over non-existent treasure..."));
		Add(new Task(new MoveThenMarkDoneState(_toManage, startingPosition), _toManage, 0, "Well... thanks for your help. I have to find a way of this island now."));
	}
}
