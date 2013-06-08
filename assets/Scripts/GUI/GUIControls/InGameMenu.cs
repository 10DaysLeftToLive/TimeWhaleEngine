using UnityEngine;
using System.Collections;

public class InGameMenu : GUIControl {
	public GUIStyle pausedButtonStyle;
	public GUIStyle resumeButtonStyle;
	public float pauseButtonSize = .15f;
	
	private float MAXPAUSEBUTTONSIZE = 128; // in pixels
	private Rect pauseButtonRect;
	private bool isPaused = false;
	
	public override void Init(){
		SetupRectangles();
	}
	
	public override void Render(){
		if (GUI.Button(pauseButtonRect, "", (isPaused ? resumeButtonStyle : pausedButtonStyle))){
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
		float largestScreenSize = (Mathf.Max(ScreenSetup.screenWidth, ScreenSetup.screenHeight));
		
		if (pauseButtonSize * largestScreenSize  > MAXPAUSEBUTTONSIZE){
			pauseButtonSize = MAXPAUSEBUTTONSIZE / largestScreenSize;
		}
		
		float screenRatio = ScreenSetup.screenHeight/ScreenSetup.screenWidth;
		float width = pauseButtonSize * screenRatio;
		pauseButtonRect = ScreenRectangle.NewRect(.0f,1f-pauseButtonSize,width,pauseButtonSize);	
	}
}
