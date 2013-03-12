using UnityEngine;
using System.Collections;

public class Chat : MonoBehaviour {
	GameObject npc;
	public Texture btn1, btn2;
	Vector3 pos, screenPos;
	Vector2 size;
	string msg;
	int charPerLine = 20;
	Rect rect;
	bool isActive;
	Vector2 offset;
	
	public delegate void ButtonClickDelegate();
	ButtonClickDelegate leftButtonClickDelegate;
	ButtonClickDelegate rightButtonClickDelegate;
	private bool showLeftButton = false;
	private bool showRightButton = false;
	
	private Vector2 bottomLeftChat;
	
	private Rect mainChatRect;
	private Rect leftButtonRect;
	private Rect rightButtonRect;
	
	private float[] topLeftPositionPercentages;
	
	private static float CHATHEIGHTPERCENTAGE = .7f;
	private static float BUTTONHEIGHTPERCENTAGE = 1-CHATHEIGHTPERCENTAGE;
	private static float CHATPADDING = .01f; // padding between chat and the screen in all directions
	
	// Use this for initialization
	void Start () {
		size.x = 150; //TODO change based off of screen and zoom size, use screen settings not unity ones
		size.y = 100;
	}
	
	void OnGUI () {
		if (isActive){
			GUI.Box (mainChatRect, msg);
			if (showLeftButton){
				if (GUI.Button(leftButtonRect, "Talk To")){
					leftButtonClickDelegate();
				}
			}
			if (showRightButton){
				if (GUI.Button(rightButtonRect, "Give Item")){
					rightButtonClickDelegate();
				}
			}
		}
	}
	
	public void UpdateMessage(string newMessage){
		msg = ParseMessage(newMessage);
	}
	
	private string ParseMessage(string message){
		if (message.Length > charPerLine){
			for (int i = 1; i <= message.Length/charPerLine; i++){
				int index = charPerLine*i;
				do {
					--index;
				}while(message[index] != ' ');

				message = message.Insert(index, "\n");
			}
		}
		return (message);
	}
	
	public void CreateChatBox(GameObject npc, GameObject player, string text){
		this.npc = npc;
		msg = ParseMessage(text);
		
		bool playerIsToLeft = (Utils.CalcDifference(npc.transform.position.x, player.transform.position.x) >= 0);
		
		topLeftPositionPercentages = CalculateRectangles(playerIsToLeft);
		
		Debug.Log("topLeftPosPer = " + topLeftPositionPercentages);
		
		isActive = true;
	}
	
	public void RemoveChatBox(){
		isActive	= false;
		showRightButton = false;
		showLeftButton = false;
	}
	
	public void CreateChatButtons(Texture bt1, Texture bt2){
		btn1 = bt1;
		btn2 = bt2;
	}
	
	private void SetLeftButton(ButtonClickDelegate leftButtonClick){
		leftButtonClickDelegate += leftButtonClick;
		showLeftButton = true;
	}
	
	private void SetRightButton(ButtonClickDelegate rightButtonClick){
		rightButtonClickDelegate += rightButtonClick;
		showRightButton = true;
	}
	
	// We will have the single constructor set the right button as we will only need the right for items some of the time
	public void SetButtonCallbacks(ButtonClickDelegate leftButtonClick){
		showRightButton = false;
		showLeftButton = false;
		SetLeftButton(leftButtonClick);
	}
	
	public void SetButtonCallbacks(ButtonClickDelegate leftButtonClick, ButtonClickDelegate rightButtonClick){
		showRightButton = false;
		showLeftButton = false;
		SetLeftButton(leftButtonClick);
		SetRightButton(rightButtonClick);
	}
	
	// Sets up the buttons and chat to the right sizes and locations relative to the top left given as a percent of screen
	private void SetUpRectangles(float[] topLeftPercentages, float width){
		float chatWidth = width;
		float buttonWidth = chatWidth/2;
		float totalHeight = 1 - topLeftPercentages[1] - (CHATPADDING * 2);
		float chatHeight = totalHeight * CHATHEIGHTPERCENTAGE;
		float buttonHeight = totalHeight * BUTTONHEIGHTPERCENTAGE;
		
		mainChatRect = ScreenRectangle.NewRect(topLeftPercentages[0], topLeftPercentages[1], chatWidth, chatHeight);
		leftButtonRect = ScreenRectangle.NewRect(topLeftPercentages[0], topLeftPercentages[1] + chatHeight, buttonWidth, buttonHeight);
		rightButtonRect = ScreenRectangle.NewRect(topLeftPercentages[0] + buttonWidth, topLeftPercentages[1] + chatHeight, buttonWidth, buttonHeight);
	}
	
	// Calculate the percentage of the screen that the width of the chat takes
	private float CalculateRectangleXPercentage(float npcXPos, float npcXOffset){
		float xPointOnWholeScreen = npcXPos + npcXOffset;
		float xPointOnBarFilledScreen = xPointOnWholeScreen - ScreenSetup.verticalBarWidth;
		float widthPercentage = (xPointOnBarFilledScreen) / ScreenSetup.screenWidth;
		return (widthPercentage);
	}
	
	private float[] CalculateRectangles(bool playerIsLeft){
		float[] topLeftPositionPercentagesToReturn = new float[2];
		
		Vector3 minBounds = Camera.main.WorldToScreenPoint (npc.renderer.bounds.min);
		Vector3 maxBounds = Camera.main.WorldToScreenPoint (npc.renderer.bounds.max);
		
		offset.x = Mathf.Abs(minBounds.x - maxBounds.x);
		
		screenPos = Camera.main.WorldToScreenPoint (npc.transform.position);
		
		float chatXPer;
		float width;
		
		if (playerIsLeft){
			chatXPer = CalculateRectangleXPercentage(screenPos.x, offset.x);
			topLeftPositionPercentagesToReturn[0] = chatXPer;
			width = 1 - chatXPer - CHATPADDING;
		} else {
			chatXPer = CalculateRectangleXPercentage(screenPos.x, -offset.x);
			topLeftPositionPercentagesToReturn[0] = CHATPADDING;
			width = chatXPer - CHATPADDING;
		}
		Debug.Log("left width = " + CalculateRectangleXPercentage(screenPos.x, -offset.x));
		Debug.Log("right width = " + CalculateRectangleXPercentage(screenPos.x, offset.x));
		
		
		Debug.Log("width = " + width);
		
		topLeftPositionPercentagesToReturn[1] = CHATPADDING; // top y is same no matter what side
		
		Debug.Log("topLeftPositionPercentagesToReturn = " + topLeftPositionPercentagesToReturn);
		
		SetUpRectangles(topLeftPositionPercentagesToReturn, width);
		
		return(topLeftPositionPercentagesToReturn);
	}
}
