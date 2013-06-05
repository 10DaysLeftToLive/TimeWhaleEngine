using UnityEngine;
using System.Collections;

public class PlayAnimationThenDoState : AbstractState {
	protected string _animationToPlay;
	protected string _animationToPlayOnExit;
	private State _nextState;
	// Use this for initialization
	public PlayAnimationThenDoState(Character toControl, string animationToPlay) : base(toControl) {
		_animationToPlay = animationToPlay;
	}
	
	public PlayAnimationThenDoState(Character toControl, string animationToPlay, State nextState) : base(toControl) {
		_animationToPlay = animationToPlay;
		_nextState = nextState;
	}
	
	public PlayAnimationThenDoState(Character toControl, string animationToPlayOnEnter, string animationToPlayOnExit) : base(toControl) {
		_animationToPlay = animationToPlayOnEnter;
		_animationToPlayOnExit = animationToPlayOnExit;
	}
	
	public PlayAnimationThenDoState(Character toControl, string animationToPlayOnEnter, string animationToPlayOnExit, State nextState) : base(toControl) {
		_animationToPlay = animationToPlayOnEnter;
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
