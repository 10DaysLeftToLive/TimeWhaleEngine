using UnityEngine;
using System.Collections;

public class SeaCaptainTreasureHuntSchedule : Schedule {
	public SeaCaptainTreasureHuntSchedule(NPC npc) : base(npc, Schedule.priorityEnum.High) {
	}
	
	protected override void Init ()
	{
		Vector3 startingPosition = new Vector3(74f, -3.09f + LevelManager.levelYOffSetFromCenter, 0f);
		Vector3 farmDigPos = new Vector3(52.5f, 16f + LevelManager.levelYOffSetFromCenter, 0f);
		Vector3 reflectDigPos = new Vector3(-28f, 18f + LevelManager.levelYOffSetFromCenter, 0f);
		Vector3 carpenterDigPos = new Vector3(16f, -1f + LevelManager.levelYOffSetFromCenter, 0f);
		Vector3 beachDigPos = new Vector3(43f, -7.5f + LevelManager.levelYOffSetFromCenter, 0f);
		
		NPCChat farmDigChat = new NPCChat();
		farmDigChat.AddChatInfo(new ChatInfo(_toManage, "Ah hah! My treas-"));
		farmDigChat.AddChatInfo(new ChatInfo(_toManage, "Is this some exotic plant?"));
		farmDigChat.AddChatInfo(new ChatInfo(_toManage, "Well judging by your face. This is a common plant."));
		farmDigChat.AddChatInfo(new ChatInfo(_toManage, "Where is the treasure then?"));
		farmDigChat.AddChatInfo(new ChatInfo(_toManage, "Oh! I had the map upside down."));
		
		NPCChat reflectDigChat = new NPCChat();
		reflectDigChat.AddChatInfo(new ChatInfo(_toManage, "Nothing?"));
		reflectDigChat.AddChatInfo(new ChatInfo(_toManage, "Interesting... that was just an ink stain."));
		reflectDigChat.AddChatInfo(new ChatInfo(_toManage, "Hmm, looks like we are supposed to go to a giant tree."));
		reflectDigChat.AddChatInfo(new ChatInfo(_toManage, "I know that that grumpy guy lives in the stump of a giant tree."));
		
		NPCChat carpenterDigChat = new NPCChat();
		carpenterDigChat.AddChatInfo(new ChatInfo(_toManage, "A flute?"));
		carpenterDigChat.AddChatInfo(new ChatInfo(_toManage, "This can't be the treasure."));
		carpenterDigChat.AddChatInfo(new ChatInfo(_toManage, "Wait a second..."));
		carpenterDigChat.AddChatInfo(new ChatInfo(_toManage, "The X is where I am supposed to go?"));
		carpenterDigChat.AddChatInfo(new ChatInfo(_toManage, "I thought it was the map maker's signature?"));
		carpenterDigChat.AddChatInfo(new ChatInfo(_toManage, "Haha, I knew that all along. I was just testing you."));
		
		NPCChat beachDigChat = new NPCChat();
		beachDigChat.AddChatInfo(new ChatInfo(_toManage, "NOOO! It isn't here!"));
		beachDigChat.AddChatInfo(new ChatInfo(_toManage, "There is only this portrait. It must of already been plundered."));
		beachDigChat.AddChatInfo(new ChatInfo(_toManage, "I must be the worst pirate ever."));
		beachDigChat.AddChatInfo(new ChatInfo(_toManage, "I lost my ship over non-existent treasure..."));
		
		SetCanInteract(false);
		Add(new Task(new MoveThenMarkDoneState(_toManage, farmDigPos), _toManage, 0, "Let's go find me some treasure!"));
		Add(new TimeTask(2f, new IdleState(_toManage), _toManage, 0, "*Digging*"));
		Add(new Task(new NPCChatState(_toManage, _toManage.player, farmDigChat)));
		Add(new Task(new MoveThenMarkDoneState(_toManage, reflectDigPos), _toManage, 0, "Come now. The map says something about a reflection tree."));
		Add(new TimeTask(3f, new IdleState(_toManage), _toManage, 0, "It must be here! *Digging*"));
		Add(new Task(new NPCChatState(_toManage, _toManage.player, reflectDigChat)));
		Add(new Task(new MoveThenMarkDoneState(_toManage, carpenterDigPos), _toManage, 0, "Let's try there and hope the grumpy guy isn't there."));
		Add(new TimeTask(3f, new IdleState(_toManage), _toManage, 0, "This must be it. I can feel it in me bones. *Digging*"));
		Add(new Task(new NPCChatState(_toManage, _toManage.player, carpenterDigChat)));
		Add(new Task(new MoveThenMarkDoneState(_toManage, beachDigPos), _toManage, 0, "Now let's go dig up some treasure!"));
		Add(new TimeTask(3f, new IdleState(_toManage), _toManage, 0, "Alright, the moment of truth. *Digging*"));
		Add(new Task(new NPCChatState(_toManage, _toManage.player, beachDigChat)));
		Add(new Task(new MoveThenMarkDoneState(_toManage, startingPosition), _toManage, 0, "Well... thanks for your help. I have to find a way off this island now."));
	}
}
