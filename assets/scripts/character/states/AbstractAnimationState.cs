using UnityEngine;
using System.Collections;

public class AbstractAnimationState : AbstractState {
	protected string _animationToPlay;
	private bool lookRight;
	private bool reactInPlayerDirection;
	// Use this for initialization
	public AbstractAnimationState(Character toControl, string animation) : base(toControl){ 
		_animationToPlay = animation;
	}
	
	public AbstractAnimationState(Character toControl, string animation, bool reactInPlayerDirection) : base(toControl) {
		_animationToPlay = animation;
		this.reactInPlayerDirection = reactInPlayerDirection;
	}
	
	public AbstractAnimationState(Character toControl, string animation, bool reactInPlayerDirecton, bool lookRight) : base(toControl) {
		_animationToPlay = animation;
		this.reactInPlayerDirection = reactInPlayerDirecton;
		this.lookRight = lookRight;
	}
	
	public override void OnEnter(){
//		if (character is NPC) {
//			string textureAtlas = (character as NPC).textureAtlasName;
//			(character as NPC).ChangeFacialExpression(
//		}
		
		character.PlayAnimation(_animationToPlay);
		if (!reactInPlayerDirection) {
			if (lookRight) {
				character.LookRight();
			}
			else {
				character.LookLeft();
			}
		}
	}
	
	public override void Update(){}
	public override void OnExit(){}
}
