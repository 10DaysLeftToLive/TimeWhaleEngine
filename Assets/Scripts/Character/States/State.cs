using UnityEngine;
using System.Collections;

public interface State {
	bool IsComplete {
		get;	
	}
	
	void Update();
	void OnEnter();
	void OnExit();
	void Pause();
	void Resume();
}