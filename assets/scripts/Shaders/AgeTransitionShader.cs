using UnityEngine;
using System.Collections;
using SmoothMoves;

public static class DebugAgeTransition {
	public static bool ageTransition = true;
}

public class AgeTransitionShader : FadeEffect {
	
	//Reference to the LevelManager.
	public LevelManager levelManager;
	
	//The age state that the player should transition to after a fade in.
	protected string ageShiftAction = "";
	
	private bool ageShiftComplete = false;
	
	
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
		if (shaderNotSupported) {
			levelManager.ShiftDownAge();
		}
		else if (levelManager.CanAgeTransition(Strings.ButtonAgeShiftDown) && !isFading) {
			DoAgeShift(Strings.ButtonAgeShiftDown);
			DoFade();
		}
		base.OnDragDown();
	}
	
	/// <summary>
	/// <seealso cref="Inherit Doc"/>
	/// Performs an age shift back forward through time if able.
	/// </summary>
	protected override void OnDragUp() {
		if (shaderNotSupported) {
			levelManager.ShiftUpAge();
		}
		else if (levelManager.CanAgeTransition(Strings.ButtonAgeShiftUp) && !isFading) {
			DoAgeShift(Strings.ButtonAgeShiftUp);
			DoFade();
		}
		base.OnDragUp();
	}
	
	protected override void OnFadeStart(RenderTexture source) {
		base.OnFadeStart(source);
	}
	
	protected override void UpdateInterpolationFactor() {
		if (interpolationFactor > 0.5) {
			if (!ageShiftComplete) {
				if (ageShiftAction.Equals(Strings.ButtonAgeShiftDown)) {
					levelManager.ShiftDownAge();
				}
				else {
					levelManager.ShiftUpAge();
				}
				ageShiftComplete = true;
			}
		}
		base.UpdateInterpolationFactor();
	}
	
	protected void DebugUpdateAgeTransition() {
		
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
		ageShiftComplete = false;
	}

}
	