using UnityEngine;
using System.Collections;

public class LoadingScreen : GUIControl{
	public GUIStyle loadinglabelStyle;
	public Texture2D backgroundTexture;
	public float fadeOutTime = .5f; // in seconds
	
	private Rect loadingLabelRect;
	private Rect backgroundRect;
	private bool isFadingOut = false;
	private float currentFadeTime = 0;
	private float currentAlpha = 1;
	private static float FONTRATIO = 10; // kinda arbitrary
	
	public override void Init(){
		SetupRectangles();
        loadinglabelStyle.fontSize = (Mathf.RoundToInt(Mathf.Min(ScreenSetup.screenWidth, ScreenSetup.screenHeight) / FONTRATIO));
	}
	
	public override void UpdateControl(){
		if (isFadingOut){
			if (currentFadeTime < fadeOutTime){
				FadeOut(Time.deltaTime);
			} else {
				StopFadeOut();
			}
		}
	}
	
	public override void Render(){
		GUI.color = new Color(1,1,1, currentAlpha);
		GUI.DrawTexture(backgroundRect, backgroundTexture);
		GUI.Label(loadingLabelRect, "Loading...", loadinglabelStyle);
		GUI.color = new Color(1,1,1,1);
	}
	
	public override bool ClickOnGUI(Vector2 screenPos){
		return (true);
	}
	
	public void StartFadeOut(){
		currentFadeTime = 0;
		currentAlpha = 1;
		isFadingOut = true;
	}
	
	private void FadeOut(float deltaTime){
		currentFadeTime += deltaTime;
		currentAlpha = 1-currentFadeTime/fadeOutTime;
	}
	
	private void StopFadeOut(){
		isFadingOut = false;
		GUIManager.Instance.HideLoadingScreen();
	}
	
	private void SetupRectangles(){
		backgroundRect = ScreenRectangle.NewRect(0,0,1,1); // fill screen
			
		GUIContent loadingText = new GUIContent("Loading...");
		Vector2 loadingTextSize = loadinglabelStyle.CalcSize(loadingText);
		Vector2 loadingTextSizePercentage = new Vector2();
		loadingTextSizePercentage.x = loadingTextSize.x / ScreenSetup.screenWidth;
		loadingTextSizePercentage.y = loadingTextSize.y / ScreenSetup.screenHeight;
		
		loadingLabelRect = ScreenRectangle.NewRect(.5f - loadingTextSizePercentage.x,.5f-loadingTextSizePercentage.y,loadingTextSizePercentage.x,loadingTextSizePercentage.y);
	}
}
