using UnityEngine;
using System.Collections;

public class SeaCaptainTreasureHuntSchedule : Schedule {
	public SeaCaptainTreasureHuntSchedule(NPC npc) : base(npc, Schedule.priorityEnum.High) {
	}
	
	protected override void Init ()
	{
		Vector3 startingPosition = new Vector3(74f, -3.09f + LevelManager.levelYOffSetFromCenter, 1f);
		Vector3 farmDigPos = new Vector3(52.5f, 16f + LevelManager.levelYOffSetFromCenter, 1f);
		Vector3 reflectDigPos = new Vector3(-28f, 18f + LevelManager.levelYOffSetFromCenter, 1f);
		Vector3 carpenterDigPos = new Vector3(16f, -1f + LevelManager.levelYOffSetFromCenter, 1f);
		Vector3 beachDigPos = new Vector3(52f, -7.5f + LevelManager.levelYOffSetFromCenter, 1f);
		
		NPCChat startHuntChat = new NPCChat();
		startHuntChat.AddChatInfo(new ChatInfo(_toManage, "Thanks matey! Let's go find me some treasure!"));
		
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
		Add(new Task(new NPCChatState(_toManage, _toManage.player, startHuntChat)));
		Add(new Task(new MoveThenMarkDoneState(_toManage, farmDigPos), _toManage, 3f, "It feels good to be exploring again."));
		Add(new Task(new NPCChatState(_toManage, _toManage.player, new NPCChat(_toManage, "This looks like the spot"))));
		Add(new Task(new DigState(_toManage, StringsItem.Vegetable)));
		Add(new Task(new NPCChatState(_toManage, _toManage.player, farmDigChat)));
		Add(new Task(new MoveThenMarkDoneState(_toManage, reflectDigPos), _toManage, 0, "Come now. The map says something about a reflection tree."));
		Add(new Task(new NPCChatState(_toManage, _toManage.player, new NPCChat(_toManage, "Hopefully this is the spot"))));
		Add(new Task(new DigState(_toManage)));
		Add(new Task(new NPCChatState(_toManage, _toManage.player, reflectDigChat)));
		Add(new Task(new MoveThenMarkDoneState(_toManage, carpenterDigPos), _toManage, 0, "Let's try over there and hope the grumpy guy isn't there."));
		Add(new Task(new NPCChatState(_toManage, _toManage.player, new NPCChat(_toManage, "This had better be the spot!"))));
		Add(new Task(new DigState(_toManage, StringsItem.Flute)));
		Add(new Task(new NPCChatState(_toManage, _toManage.player, carpenterDigChat)));
		Add(new Task(new MoveThenMarkDoneState(_toManage, beachDigPos), _toManage, 0, "Now let's go dig up some treasure!"));
		Add(new Task(new NPCChatState(_toManage, _toManage.player, new NPCChat(_toManage, "If this isn't where me treasure be buried, then I must not like gold"))));
		Add(new Task(new DigState(_toManage, StringsItem.SeashellTwo)));
		Add(new Task(new NPCChatState(_toManage, _toManage.player, beachDigChat)));
		Add(new Task(new MoveThenMarkDoneState(_toManage, startingPosition), _toManage, 0, "Well... thanks for your help. I have to find a way off this island now."));
	}
}
