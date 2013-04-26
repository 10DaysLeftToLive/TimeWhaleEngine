using UnityEngine;
using System.Collections;

public class MoveThenMarkDoneState : MoveThenDoState {
	public MoveThenMarkDoneState(Character toControl, Vector3 goal) : base(toControl, goal, new MarkTaskDone(toControl)){}
}
