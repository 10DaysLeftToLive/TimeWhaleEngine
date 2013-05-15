using UnityEngine;
using System.Collections;
/****************************************************************************************************************
	ScreenRectangle.cs
		Will be responible for creating rectangles for gui purposes.
		It will convert any given points into actual screen points to account for any bars that are added
		then it will give them a height and width acording to the actual scaled screen dimensions.
		
		Exposes the following functions:
			Rect NewRect(float topLeftXPercentage, float topLeftYPercentage)
			Rect NewRect(float topLeftXPercentage, float topLeftYPercentage, 
						 float screenWidthPercent, float screenHeightPercent)
			
		
		Email issues to Jared Mavis - jmavis@ucsc.edu
****************************************************************************************************************/

public static class ScreenRectangle {
	// The standard height and width percentage of all rectangles when a new rectangle's dimensions are not specified 
	public static float standardWidthPercent = .2f;
	public static float standardHeightPercent = .15f;

	/*
	Rect NewRect(float topLeftXPercentage, float topLeftYPercentage)
		Inputs:
			float topLeftXPercentage 
				The top left x point of the rectangle as a percentage of screen size from left(0) to right(1)
			float topLeftYPercentage
				The top left y point of the rectangle as a percentage of screen size from top(0) to bottom(1)
		Output:		
			Will return a new rectange with the given top left point percentage of the screen and with standard rectangle 
			width and height as a percentage of the remaining screen dimensions
		
		Examples:
			NewRect(.5,.5) will create a rectangle with the top left point in the center of the screen with standard rectangle
			size
			NewRect(0,0) will create a rectangle with the top left point in the top left corner of the screen with standard
			rectangle size
			
		Notes:
			Overloaded function called if there are no sizes given to the NewRect function
	*/
	public static Rect NewRect(float _topLeftXPercentage, float _topLeftYPercentage){
		return (NewRect(_topLeftXPercentage, _topLeftYPercentage, standardWidthPercent, standardHeightPercent));
	}
	
	private static float screenWidth;
	private static float screenHeight;
	private static float horizontalBarHeight;
	private static float verticalBarWidth;						   
	
	// Set the top left point based off of the given percantage
	private static float topLeftXPoint;
	private static float topLeftYPoint;
	
	// Shift the top left point based on any bar sizes that have been added to the screen
	private static float shiftedTopLeftX;
	private static float shiftedTopLeftY;
	
	private static float rectWidth;
	private static float rectHeight;
	
	/*
			Rect NewRect(float topLeftXPercentage, float topLeftYPercentage, 
						 float screenWidthPercent, float screenHeightPercent)
			Inputs:
				float topLeftXPercentage 
					The top left x point of the rectangle as a percentage of screen size from left(0) to right(1)
				float topLeftYPercentage
					The top left y point of the rectangle as a percentage of screen size from top(0) to bottom(1)
				float screenWidthPercent
					The percentage of the screen's width that the rectangle will take extending to the right of the top
					left point ranges from 0-1 where 0 is no width and 1 is 100% width
				float screenHeightPercent
					The percentage of the screen's height that the rectangle will take extending below the top
					left point ranges from 0-1 where 0 is no height and 1 is 100% height
					
			Output:		
				Will return a new rectange with the given top left point percentage of the screen and with the given rectangle 
				width and height as a percentage of the remaining screen dimensions
			
			Examples:
				NewRect(.5,.5,.5,.5) will create a rectangle with the top left point in the center and takes up the bottom left quarter of the 
					screen
				NewRect(0,0,1,1) will create a rectangle with the top left point in the top left corner of the screen and fill the screen with
					the rectangle
				
			Notes:
				Overloaded function called if there are sizes given to the NewRect function
	*/
	public static Rect NewRect(float _topLeftXPercentage, float _topLeftYPercentage, 
								   float _screenWidthPercent, float _screenHeightPercent){
		// Retrieve Values from the screen settings manager
		screenWidth = ScreenSetup.screenWidth;
		screenHeight = ScreenSetup.screenHeight;
		horizontalBarHeight = ScreenSetup.horizontalBarHeight;
		verticalBarWidth = ScreenSetup.verticalBarWidth;						   
		
		// Set the top left point based off of the given percantage
		topLeftXPoint = screenWidth * _topLeftXPercentage;
		topLeftYPoint = screenHeight * _topLeftYPercentage;
		
		// Shift the top left point based on any bar sizes that have been added to the screen
		shiftedTopLeftX = topLeftXPoint + verticalBarWidth;
		shiftedTopLeftY = topLeftYPoint + horizontalBarHeight;
		
		rectWidth = screenWidth * _screenWidthPercent;
		rectHeight = screenHeight * _screenHeightPercent;
								   
		return (new Rect(shiftedTopLeftX, shiftedTopLeftY, rectWidth, rectHeight));
	}
}
