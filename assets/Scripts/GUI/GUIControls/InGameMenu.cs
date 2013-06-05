using UnityEngine;
using System.Collections;

public class InGameMenu : GUIControl {
	public GUIStyle buttonStyle;
	public Texture2D pauseTexture;
	public Texture2D unpauseTexture;
	public float pauseButtonSize = .15f;
	
	
	private Rect pauseButtonRect;
	private bool isPaused = false;
	
	public override void Init(){
		SetupRectangles();
	}
	
	public override void Render(){
		if (GUI.Button(pauseButtonRect, (isPaused ? pauseTexture : unpauseTexture), buttonStyle)){
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
		pauseButtonRect = ScreenRectangle.NewRect(.0f,.98f-pauseButtonSize,pauseButtonSize,pauseButtonSize);	
	}
}
