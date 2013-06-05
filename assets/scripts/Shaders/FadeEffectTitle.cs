using UnityEngine;
using System.Collections;

public class FadeEffectTitle : FadeEffect {
	public TitleMenu titleMenu;
	private bool canFade = true;
	
	/// <summary>
	/// Performs actions when the finger has swiped down.
	/// </summary>
	protected override void OnDragDown() {
		
	}
	
	/// <summary>
	/// Peforms actions when the finger has swiped up.
	/// </summary>
	protected override void OnDragUp() {
		Debug.Log("FADADADADAEEEE");
		if (!isFading && !isGamePaused() && canFade) {
			DoFade();
			titleMenu.TransitionToMainMenu();
			canFade = false;
		}
	}
}
