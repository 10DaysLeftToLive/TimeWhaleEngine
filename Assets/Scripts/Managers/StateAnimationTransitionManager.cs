using UnityEngine;
using System.Collections;
using SmoothMoves;
using System.Collections.Generic; 

public class StateAnimationTransitionManager : ManagerSingleton<StateAnimationTransitionManager> {
	Dictionary<State, Dictionary<State, string> > transitions; // 2d array of transitions may be up for change when this is implemented
	
	public void Init(){
		transitions = new Dictionary<State, Dictionary<State, string>>();
	}
	
	public string GetTransitionAnimation(State currentState, State nextState){
		return ("Stand"); // Placeholder
	}
}
