using UnityEngine;
using System.Collections;

public class PauseMenu : GUIControl{
	public GUIStyle buttonStyle;
	
	private Rect quitButtonRect;
	private Rect mainMenuButtonRect;
	//private Rect restartButtonRect;
	
	private static float FONTRATIO = 25; // kinda arbitrary
	
	public override void Init(){
		SetupRectangles();
        buttonStyle.fontSize = (Mathf.RoundToInt(Mathf.Min(ScreenSetup.screenWidth, ScreenSetup.screenHeight) / FONTRATIO));
	}
	
	public override void Render(){
		if (ButtonClick(quitButtonRect, "Quit", buttonStyle)){
			Game.Quit();
		}
		
		if (ButtonClick(mainMenuButtonRect, "Return To Menu", buttonStyle)){
			Game.GoToMainMenu();
		}
	}
	
	public override bool ClickOnGUI(Vector2 screenPos){
		return (quitButtonRect.Contains(screenPos));
	}
	
	private void SetupRectangles(){
		quitButtonRect = ScreenRectangle.NewRect(.4f,.5f,.2f,.15f);	
		//restartButtonRect = ScreenRectangle.NewRect(.4f,.3f,.2f,.2f);
		mainMenuButtonRect = ScreenRectangle.NewRect(.4f,.3f,.2f,.15f);
	}
}
