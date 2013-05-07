using UnityEngine;
using System.Collections;

public class YoungRunIslandScript : Schedule {
	
	public YoungRunIslandScript (NPC toManage) : base (toManage) {
		
		schedulePriority = (int)priorityEnum.DoNow;			
	}
	
		protected override void Init() {
			Add(new TimeTask(.75f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (6, .2f, .3f), new MarkTaskDone(_toManage))));
			Add (new TimeTask(5, new WaitTillPlayerCloseState());
			Add(new TimeTask(.5f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (4, .2f, .3f), new MarkTaskDone(_toManage))));
			Add(new TimeTask(.5f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (11, .2f, .3f), new MarkTaskDone(_toManage))));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (10.90f, .2f, .3f), new MarkTaskDone(_toManage))));

			//
			Add(new TimeTask(.25f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (28, .2f, .3f), new MarkTaskDone(_toManage))));
			
			//
			Add(new TimeTask(.25f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (59, .2f, .3f), new MarkTaskDone(_toManage))));
			//wait for player to reach, or wait 10 seconds
			Add(new TimeTask(2f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (66, -7.6f, .3f), new MarkTaskDone(_toManage))));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (70, -8f, .3f), new MarkTaskDone(_toManage))));
			//PIER (79,-5.1)

			Add(new TimeTask(.25f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (66, -7.6f, .3f), new MarkTaskDone(_toManage))));
			Add(new TimeTask(.25f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (59, .2f, .3f), new MarkTaskDone(_toManage))));
			Add(new TimeTask(2f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (47, -1f, .3f), new MarkTaskDone(_toManage))));
			Add(new TimeTask(2f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (53, 5f, .3f), new MarkTaskDone(_toManage))));
			Add(new TimeTask(1f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (50, 10f, .3f), new MarkTaskDone(_toManage))));
			Add(new TimeTask(1f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (55, 16f, .3f), new MarkTaskDone(_toManage))));
			Add(new TimeTask(1f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (63, 16f, .3f), new MarkTaskDone(_toManage))));
			
			Add(new TimeTask(.25f, new IdleState(_toManage)));
			Add(new Task(new MoveThenDoState(_toManage, new Vector3 (70, 16f, .3f), new MarkTaskDone(_toManage))));
				
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
			runToCarpenter = new Schedule(this, Schedule.priorityEnum.High);
			runToCarpenter.Add(new TimeTask(2, new IdleState(this)));
			//runToCarpenter.Add(new Task(new MoveThenDoState(this, NPCManager.instance.getNPC(StringsNPC.CarpenterYoung).transform.position, new MarkTaskDone(this))));
			runToCarpenter.Add(new Task(new MoveThenDoState(this, new Vector3 (5, .2f, .3f), new MarkTaskDone(this))));
			runToCarpenter.Add (new TimeTask(1f, new IdleState(this)));
			runToCarpenter.Add(new Task(new MoveThenDoState(this, new Vector3 (4, .2f, .3f), new MarkTaskDone(this))));
			runToCarpenter.Add (new TimeTask(2f, new IdleState(this)));
			runToCarpenter.Add(new Task(new MoveThenDoState(this, new Vector3 (10, .2f, .3f), new MarkTaskDone(this))));
	
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
	
	// Use _toManage for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
