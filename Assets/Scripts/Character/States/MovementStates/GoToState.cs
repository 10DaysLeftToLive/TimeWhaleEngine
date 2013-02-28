using UnityEngine;
using System.Collections;

public interface GoToState {
	void Move(Vector3 moveDelta);
	void OnGoalReached();
	void OnStuck();
}