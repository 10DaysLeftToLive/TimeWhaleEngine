using UnityEngine;
using System.Collections;

/*
 * ResolutionSetup.cs
 * 	Responisble for performing relevant calculations for screen resizing to a set aspect ratio
 * 
 */

public class ResolutionSetup : MonoBehaviour {
	public static float widthShift = 0; // records the width of any side bars added ot the screen.
	public static float scaleWidth = 1; // record the percentage of the width available after adding side bars
	public static float heightShift = 0; // records the height of any top bars added ot the screen.
	public static float scaleHeight = 1; // record the percentage of the height available after adding top bars
	
	private static ResolutionSetup rm_instance = null;
	
	public static ResolutionSetup instance{
		get {
            if (rm_instance == null) {
                //  FindObjectOfType(...) returns the first ResolutionSetup in the scene.
                rm_instance =  FindObjectOfType(typeof (ResolutionSetup)) as ResolutionSetup;
            }
 
            // If it is still null, create a new instance
            if (rm_instance == null) {
                GameObject obj = new GameObject("ResolutionSetup");
                rm_instance = obj.AddComponent(typeof (ResolutionSetup)) as ResolutionSetup;
            }
 
            return rm_instance;
        }
	}
	
	// Use this for initialization
	public void InitializeResolutionSettings () {
	    // set the desired aspect ratio (the values in this example are
	    // hard-coded for 16:9, but you could make them into public
	    // variables instead so you can set them at design time)
	    float targetaspect = 3.0f / 2.0f;
	
	    // determine the game window's current aspect ratio
	    float windowaspect = (float)Screen.width / (float)Screen.height;
		
	    // current viewport height should be scaled by this amount
	    scaleHeight = windowaspect / targetaspect;		
	
	    // obtain camera component so we can modify its viewport
	    Camera camera = Camera.main;
	
	    // if scaled height is less than current height, add letterbox
	    if (scaleHeight < 1.0f)
	    {
	        Rect rect = camera.rect;
	
	        rect.width = 1.0f;
	        rect.height = scaleHeight;
	        rect.x = 0;
	        rect.y = (1.0f - scaleHeight) / 2.0f;
			
			heightShift = rect.y;
	        
	        camera.rect = rect;
	    } 
		else // add pillarbox
	    {
	        scaleWidth = 1.0f / scaleHeight;
			scaleHeight = 1;
	
	       /* Rect rect = camera.rect;
	
	        rect.width = scaleWidth;
	        rect.height = 1.0f;
	        rect.x = (1.0f - scaleWidth) / 2.0f;
	        rect.y = 0;
			
			widthShift = rect.x;
	
	        camera.rect = rect;*/
	    }
	}
}