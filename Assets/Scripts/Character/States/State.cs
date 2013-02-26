using UnityEngine;
using System.Collections;

public interface State {
	void Update();
	void OnEnter();
	void OnExit();
}