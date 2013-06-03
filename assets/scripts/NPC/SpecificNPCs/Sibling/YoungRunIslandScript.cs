using UnityEngine;
using System.Collections;

public class YoungRunIslandScript : Schedule {
	
	public YoungRunIslandScript (NPC toManage) : base (toManage) {
		
		schedulePriority = (int)priorityEnum.Low;			
	}
	protected override void Init() {
		
		Add(new TimeTask(.2f, new IdleState(_toManage))); //or self-triggering
		//AddFlagGroup("startedRace");
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (12, .2f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(.2f, new IdleState(_toManage)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (11.8f, .2f, .3f), new MarkTaskDone(_toManage)))); // at bridge
		Add(new TimeTask(10f, new WaitTillPlayerCloseState(_toManage, ref _toManage.player)));
		Task reachCarpenterTask = new Task(new MoveThenDoState(_toManage, new Vector3 (28, .2f, .3f), new MarkTaskDone(_toManage))); // at carpenter
		reachCarpenterTask.AddFlagToSet(FlagStrings.RunToCarpenter);
		Add(reachCarpenterTask);
		Add(new TimeTask(10f, new IdleState(_toManage)));
		
		//NPCManager.instance.getNPC(StringsNPC.CarpenterYoung);
		//Add(new Task(new ChangeEmotionState(_toManage,(SiblingYoung.EMOTIONSTATENAME);
	
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (59, .2f, .3f), new MarkTaskDone(_toManage)))); // at base staircase
		Add(new TimeTask(2f, new WaitTillPlayerCloseState(_toManage, ref _toManage.player)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (66, -7.6f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(2f, new WaitTillPlayerCloseState(_toManage, ref _toManage.player)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (70, -8f, .3f), new MarkTaskDone(_toManage)))); // at Beach
		Add(new TimeTask(8f, new IdleState(_toManage)));
		//PIER (79,-5.1)

		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (66, -7.6f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(.25f, new IdleState(_toManage)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (59, .2f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(2f, new WaitTillPlayerCloseState(_toManage, ref _toManage.player)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (47, -1f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(2f, new WaitTillPlayerCloseState(_toManage, ref _toManage.player)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (53, 5f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(2f, new WaitTillPlayerCloseState(_toManage, ref _toManage.player)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (50, 10f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(2f, new WaitTillPlayerCloseState(_toManage, ref _toManage.player)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (55, 16f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(2f, new WaitTillPlayerCloseState(_toManage, ref _toManage.player)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (63, 16f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(15f, new IdleState(_toManage)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (75, 16f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(20f, new IdleState(_toManage)));
		//runToLighthouse.Add(new Task(new MoveThenDoState(_toManage, new Vector3 (28, .2f, .3f), new MarkTaskDone(_toManage))));

		Add(new TimeTask(.25f, new IdleState(_toManage)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (63, 16f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(1f, new IdleState(_toManage)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (55, 16f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(1.25f, new IdleState(_toManage)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (50, 10f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(1f, new IdleState(_toManage)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (37, 10f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(.5f, new IdleState(_toManage)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (40, 10f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(.5f, new IdleState(_toManage)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (27, 10f, .3f), new MarkTaskDone(_toManage))));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (27.05f, 10f, .3f), new MarkTaskDone(_toManage))));
		
		Add(new TimeTask(.25f, new IdleState(_toManage)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (-40, 16f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(2f, new IdleState(_toManage)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (-49, 18f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(2f, new IdleState(_toManage)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (-41, 25f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(.5f, new IdleState(_toManage)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (-48, 30f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(.1f, new IdleState(_toManage)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (-66, 29.5f, .3f), new MarkTaskDone(_toManage))));
		
		Add(new TimeTask(.25f, new IdleState(_toManage)));
		/*
		 * runToHome.Add(new Task(new MoveThenDoState(_toManage, new Vector3 (-48, 30f, .3f), new MarkTaskDone(_toManage))));
		runToHome.Add(new TimeTask(2f, new IdleState(_toManage)));
		runToHome.Add(new Task(new MoveThenDoState(_toManage, new Vector3 (-41, 25f, .3f), new MarkTaskDone(_toManage))));
		runToHome.Add(new TimeTask(1f, new IdleState(_toManage)));
		runToHome.Add(new Task(new MoveThenDoState(_toManage, new Vector3 (-49, 18f, .3f), new MarkTaskDone(_toManage))));
		runToHome.Add(new TimeTask(1f, new IdleState(_toManage)));
		runToHome.Add(new Task(new MoveThenDoState(_toManage, new Vector3 (-40, 16f, .3f), new MarkTaskDone(_toManage))));
		runToHome.Add(new TimeTask(1.5f, new IdleState(_toManage)));
		runToHome.Add(new Task(new MoveThenDoState(_toManage, new Vector3 (-18, 6f, .3f), new MarkTaskDone(_toManage))));
		*/
	
	
		/*
		runToCarpenter = new Schedule(_toManage, Schedule.priorityEnum.High);
		runToCarpenter.Add(new TimeTask(2, new IdleState(_toManage)));
		//runToCarpenter.Add(new Task(new MoveThenDoState(_toManage, NPCManager.instance.getNPC(StringsNPC.CarpenterYoung).transform.position, new MarkTaskDone(_toManage))));
		runToCarpenter.Add(new Task(new MoveThenDoState(_toManage, new Vector3 (5, .2f, .3f), new MarkTaskDone(_toManage))));
		runToCarpenter.Add (new TimeTask(1f, new IdleState(_toManage)));
		runToCarpenter.Add(new Task(new MoveThenDoState(_toManage, new Vector3 (4, .2f, .3f), new MarkTaskDone(_toManage))));
		runToCarpenter.Add (new TimeTask(2f, new IdleState(_toManage)));
		runToCarpenter.Add(new Task(new MoveThenDoState(_toManage, new Vector3 (10, .2f, .3f), new MarkTaskDone(_toManage))));

		//adding the ability to set flags would be nice
		//adding the ability to set emotion States on would be nice. (actions)
		runToCarpenter.SetCanChat(false); 
		*/
	
		//
		Add(new TimeTask(1f, new IdleState(_toManage)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (-5, -1.5f, .3f), new MarkTaskDone(_toManage))));
		Add(new TimeTask(.5f, new IdleState(_toManage)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (-2, -1.5f, .3f), new MarkTaskDone(_toManage))));
		
		//
		Add(new TimeTask(3f, new IdleState(_toManage)));
		Add(new Task(new MoveThenDoState(_toManage, new Vector3 (0, -1.5f, .3f), new MarkTaskDone(_toManage))));
	}
}
