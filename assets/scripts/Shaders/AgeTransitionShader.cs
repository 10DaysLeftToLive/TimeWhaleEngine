using UnityEngine;
using System.Collections;
using SmoothMoves;

public class AgeTransitionShader : FadeEffect {
	
	//Reference to the LevelManager.
	public LevelManager levelManager;
	
	//The age state that the player should transition to after a fade in.
	protected string ageShiftAction = "";
	
	/// <summary>
	///  Initialize: <see cref="inhert doc"/>
	/// Checks to see if the LevelManager has been initializes so that the 
	/// player age transitions properly.
	/// </summary>
	protected override void Initialize() {
		if (levelManager == null) {
			Debug.LogError("LevelManager not set in AgeTransitionShader");
		}
		base.Initialize();
	}
	
	/// <summary>
	/// <seealso cref="Inherit Doc"/>
	/// Performs an age shift back in time if able.
	/// </summary>
	protected override void OnDragDown() {
		if (levelManager.CanAgeTransition(Strings.ButtonAgeShiftDown)) {
			DoFade();
			DoAgeShift(Strings.ButtonAgeShiftDown);
		}
	}
	
	/// <summary>
	/// <seealso cref="Inherit Doc"/>
	/// Performs an age shift back forward through time if able.
	/// </summary>
	protected override void OnDragUp() {
		if (levelManager.CanAgeTransition(Strings.ButtonAgeShiftUp)) {
			DoFade();
			DoAgeShift(Strings.ButtonAgeShiftUp);
		}
	}
	
	/// <summary>
	/// DoShiftAge:
	/// Gets the current AgeTransitionState(Young, Middle, Old)
	/// that the player will transition up to.
	/// </summary>
	/// <param name='ageShiftAction'>
	/// Age shift action.
	/// </param>
	private void DoAgeShift(string ageShiftAction) {
		this.ageShiftAction = ageShiftAction;
	}
	
	/// <summary>
	/// OnFadeInComplete:
	/// Transitions up and down ages when the fade color covers the screen.
	/// </summary>
	public override void OnFadeInComplete() {
		base.OnFadeInComplete();
		if (ageShiftAction.Equals(Strings.ButtonAgeShiftDown)) {
			Debug.Log ("AGE SHIFTED DOWN");
			levelManager.ShiftDownAge();
		}
		else {
			Debug.Log ("AGE SHIFTED UP");
			levelManager.ShiftUpAge();
		}
	}

}
	