using UnityEngine;
using System.Collections;

public class InGameMenu : GUIControl {
	public GUIStyle buttonStyle;
	
	private Rect pauseButtonRect;
	private bool isPaused = false;
	
	public override void Init(){
		SetupRectangles();
	}
	
	public override void Render(){
		if (GUI.Button(pauseButtonRect, (isPaused ? "Resume" : "Pause"))){
			if (isPaused){
				GUIManager.Instance.HidePauseMenu();
			} else {
				GUIManager.Instance.ShowPauseMenu();
			}
			isPaused = !isPaused;
			EventManager.instance.RiseOnPauseToggleEvent(new PauseStateArgs(isPaused));	
		}
	}
	
	public void SetPausedState(bool newState){
		isPaused = newState;
	}
	
	public override bool ClickOnGUI(Vector2 screenPos){
		return (pauseButtonRect.Contains(screenPos));
	}
	
	private void SetupRectangles(){
		pauseButtonRect = ScreenRectangle.NewRect(.0f,.8f,.2f,.2f);	
	}
}
