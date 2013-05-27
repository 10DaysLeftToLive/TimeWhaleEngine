using UnityEngine;
using System.Collections;

/*
 * ScreenSetup.cs
 * 	Responsible for calulating and storing all dynamically calculated screen settings
 * 
 *  Exposes the following:
 * 		-screenWidth
 * 		-screenHeight
 * 		-verticalBarWidth
 * 		-horizontalBarHeight
 */

public class ScreenSetup {
	// the altered screen width and height according to changes with the resolution
	public static float screenWidth = Screen.width;
	public static float screenHeight = Screen.height;
	
	// the size of each bar that may or may not have been added to accout for a different resolution
	public static float verticalBarWidth = 0;
	public static float horizontalBarHeight = 0;
	
	// will call all apropriate functions to dynamically calculate the screen settings
	public static void CalculateSettings(){
		ResolutionManager.InitializeResolutionSettings();
		
		CalulateScreenDimensions();
		CalculateBarSizes();
	}
	
	// will calulate the screen's width and height with respect to resolution changes
	private static void CalulateScreenDimensions(){
		screenWidth = Screen.width * ResolutionManager.scaleWidth;
		screenHeight = Screen.height * ResolutionManager.scaleHeight;
	}
	
	private static void CalculateBarSizes(){
		//verticalBarWidth = Screen.width * ResolutionManager.widthShift;
		//horizontalBarHeight = Screen.height * ResolutionManager.heightShift;
	}
}
