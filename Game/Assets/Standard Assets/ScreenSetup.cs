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

public class ScreenSetup : MonoBehaviour {
	// the altered screen width and height according to changes with the resolution
	public static float screenWidth = Screen.width;
	public static float screenHeight = Screen.height;
	
	// the size of each bar that may or may not have been added to accout for a different resolution
	public static float verticalBarWidth = 0;
	public static float horizontalBarHeight = 0;

	private static ScreenSetup ssm_instance = null;
	
	public static ScreenSetup instance{
		get {
            if (ssm_instance == null) {
                //  FindObjectOfType(...) returns the first ScreenSetup object in the scene.
                ssm_instance =  FindObjectOfType(typeof (ScreenSetup)) as ScreenSetup;
            }
 
            // If it is still null, create a new instance
            if (ssm_instance == null) {
                GameObject obj = new GameObject("ScreenSetup");
                ssm_instance = obj.AddComponent(typeof (ScreenSetup)) as ScreenSetup;
                Debug.Log("Could not locate an ScreenSetup object. ScreenSetup was Generated Automaticly.");
            }
 
            return ssm_instance;
        }
	}
	
	// will call all apropriate functions to dynamically calculate the screen settings
	public static void CalculateSettings(){
		ResolutionSetup.instance.InitializeResolutionSettings();
		
		CalulateScreenDimensions();
		CalculateBarSizes();
	}
	
	// will calulate the screen's width and height with respect to resolution changes
	private static void CalulateScreenDimensions(){
		screenWidth = Screen.width * ResolutionSetup.scaleWidth;
		screenHeight = Screen.height * ResolutionSetup.scaleHeight;
	}
	
	private static void CalculateBarSizes(){
		verticalBarWidth = Screen.width * ResolutionSetup.widthShift;
		horizontalBarHeight = Screen.height * ResolutionSetup.heightShift;
	}
}
