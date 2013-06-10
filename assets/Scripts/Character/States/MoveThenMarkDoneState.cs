using UnityEngine;
using System.Collections;

public class MoveThenMarkDoneState : MoveThenDoState {
	public MoveThenMarkDoneState(Character toControl, Vector3 goal) : base(toControl, goal, new MarkTaskDone(toControl)){}
	
	public MoveThenMarkDoneState(Character toControl, Vector3 goal, string animation) : base(toControl, goal, new MarkTaskDone(toControl), animation) {}
	
	public MoveThenMarkDoneState(Character toControl, Vector3 goal, string animation, float speed) : base(toControl, goal, new MarkTaskDone(toControl), animation, speed) {}
}
