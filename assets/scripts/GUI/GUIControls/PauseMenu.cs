using UnityEngine;
using System.Collections;

public class PauseMenu : GUIControl{
	public GUIStyle buttonStyle;
	
	private Rect quitButtonRect;
	//private Rect restartButtonRect;
	
	public override void Init(){
		SetupRectangles();
	}
	
	public override void Render(){
		if (ButtonClick(quitButtonRect, "Quit")){
			Game.Quit();
		}
	}
	
	public override bool ClickOnGUI(Vector2 screenPos){
		return (quitButtonRect.Contains(screenPos));
	}
	
	private void SetupRectangles(){
		quitButtonRect = ScreenRectangle.NewRect(.4f,.5f,.2f,.2f);	
		//restartButtonRect = ScreenRectangle.NewRect(.4f,.3f,.2f,.2f);
	}
}
