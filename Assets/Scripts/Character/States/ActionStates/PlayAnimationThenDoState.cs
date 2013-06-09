using UnityEngine;
using System.Collections;

public class PlayAnimationThenDoState : AbstractAnimationState {
	
	protected string _animationToPlayOnExit;
	private State _nextState;
	// Use this for initialization
	public PlayAnimationThenDoState(Character toControl, string animationToPlay) : base(toControl, animationToPlay) {
	}
	
	public PlayAnimationThenDoState(Character toControl, string animationToPlay, State nextState) : base(toControl, animationToPlay) {
		_nextState = nextState;
	}
	
	public PlayAnimationThenDoState(Character toControl, string animationToPlayOnEnter, string animationToPlayOnExit) : base(toControl, animationToPlayOnEnter) {
		_animationToPlayOnExit = animationToPlayOnExit;
	}
	
	public PlayAnimationThenDoState(Character toControl, string animationToPlayOnEnter, string animationToPlayOnExit, State nextState) : base(toControl, animationToPlayOnEnter) {
		_animationToPlayOnExit = animationToPlayOnExit;
		_nextState = nextState;
	}
	
	// Update is called once per frame
	public override void Update () {
		if (!character.animationData.IsPlaying(_animationToPlay)) {
			if (_nextState != null)
				character.EnterState(_nextState as State);
			else
				character.EnterState(new IdleState(character));
		}
	}
	
	public override void OnEnter() {
		character.PlayAnimation(_animationToPlay);
	}
	
	public override void OnExit() {
		if (_animationToPlayOnExit.Equals("")) {
			character.PlayAnimation(_animationToPlayOnExit);
		}
	}
}
