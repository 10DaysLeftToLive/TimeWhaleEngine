using UnityEngine;
using System.Collections;

public interface GoToState {
	void Update();
	void OnGoalReached();
	void OnStuck();
}