using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chat : MonoBehaviour {
	public Texture btn1, btn2;
	Vector3 pos, screenPos;
	Vector2 size;
	string msg;
	int charPerLine = 60;
	Rect rect;
	bool isActive;
	Vector2 offset;
	
	private string leftButtonText;
	private string rightButtonText;
	
	public delegate void ButtonClickDelegate();
	public delegate void ChoiceButtonClickDelegate(string choice);
	ChoiceButtonClickDelegate leftButtonClickDelegate;
	ButtonClickDelegate rightButtonClickDelegate;
	private bool showLeftButton = false;
	private bool showRightButton = false;
	private bool textFieldInit = false;
	private bool buttonInit = false;
	
	private GUIStyle textFieldStyle;
	private GUIStyle buttonStyle;
	private Font textFieldFont;
	private Font buttonFont;
	private Texture _charPortrait;
	
	private Vector2 bottomLeftChat;
	
	private Rect mainChatRect;
	private Rect leftButtonRect;
	private Rect rightButtonRect;
	
	private Rect portraitRect;
	private Rect textBoxRect;
	private Rect button1Rect;
	private Rect button2Rect;
	private Rect button3Rect;
	private Rect buttonGiveRect;
	
	private float[] topLeftPositionPercentages;
	
	private static float CHATHEIGHTPERCENTAGE = .7f;
	private static float BUTTONHEIGHTPERCENTAGE = 1-CHATHEIGHTPERCENTAGE;
	private static float CHATPADDING = .01f; // padding between chat and the screen in all directions
	private static float CHATINTERNALPADDING = .01f; // padding between chat elements in all directions
	private static float CHATBUTTONPADDING = .01f; // padding between chat elements in all directions
	private static float PORTRAITWIDTH = .2f;
	
	List<Choice> _choices;
	
	// Use this for initialization
	void Start () {
		SetChatRectangles();
	}
	
	void OnGUI () {
		//temporary loading statement for textFieldStyle
		if (!textFieldInit) {
			// Create style for a box
			textFieldStyle = new GUIStyle(GUI.skin.textField);
			textFieldStyle.fontSize = 24;
			
			// Load and set Font
			textFieldFont = (Font)Resources.Load("century", typeof(Font));
			textFieldStyle.font = textFieldFont;
			
			// Set color for selected and unselected buttons
			//textFieldStyle.normal.textColor = Color.green;

			textFieldInit = true;
		}
		
		//temporary loading statement for buttonStyle
		if (!buttonInit) {
			buttonStyle = new GUIStyle(GUI.skin.button);
			buttonStyle.fontSize = 24;
			 
			// Load and set Font
			buttonFont = (Font)Resources.Load("century", typeof(Font));
			buttonStyle.font = buttonFont;
			
			// Set color for selected and unselected buttons
			//buttonStyle.normal.textColor = Color.green;	
				
			buttonInit = true;
		}
		
		if (isActive) {
			
			GUI.Box (mainChatRect, "");
			GUI.TextField (textBoxRect, msg, textFieldStyle);
			GUI.Box (portraitRect, _charPortrait);
			/*if (GUI.Button(portraitRect, "Pic goes here")){
			}*/
			
			if (_choices.Count >= 1){
				if (GUI.Button(button1Rect, _choices[0]._choiceName, buttonStyle)){
					ClickChoice(_choices[0]);
				}
			}
			if (_choices.Count >= 2){
				if (GUI.Button(button2Rect, _choices[1]._choiceName, buttonStyle)){
					ClickChoice(_choices[1]);
				}
			}
			if (_choices.Count >= 3){
				if (GUI.Button(button3Rect, _choices[2]._choiceName, buttonStyle)){
					ClickChoice(_choices[2]);
				}
			}
			if (showRightButton && GUI.Button(buttonGiveRect, "Give", buttonStyle)){
				rightButtonClickDelegate();
			}
			/*
			if (showLeftButton){
				if (GUI.Button(leftButtonRect, leftButtonText)){
					leftButtonClickDelegate("Test");
				}
			}
			if (showRightButton){
				if (GUI.Button(rightButtonRect, rightButtonText)){
					rightButtonClickDelegate();
				}
			}*/
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
	
	public void CreateChatBox(List<Choice> choices, string text){
		_choices = choices;
		msg = ParseMessage(text);/*
		
		bool playerIsToLeft = (Utils.CalcDifference(npc.transform.position.x, player.transform.position.x) >= 0);
		
		topLeftPositionPercentages = CalculateRectangles(playerIsToLeft);
		
		Debug.Log("topLeftPosPer = " + topLeftPositionPercentages);*/
		
		isActive = true;
	}
	
	public void RemoveChatBox(){
		isActive	= false;
		showRightButton = false;
		showLeftButton = false;
		rightButtonClickDelegate = null;
		leftButtonClickDelegate = null;
	}
	
	public void CreateChatButtons(Texture bt1, Texture bt2){
		btn1 = bt1;
		btn2 = bt2;
	}
	
	private void SetLeftButton(ChoiceButtonClickDelegate leftButtonClick){
		leftButtonClickDelegate += leftButtonClick;
		showLeftButton = true;
	}
	
	private void SetRightButton(ButtonClickDelegate rightButtonClick){
		rightButtonClickDelegate += rightButtonClick;
		showRightButton = true;
	}
	
	// We will have the single constructor set the right button as we will only need the right for items some of the time
	public void SetButtonCallbacks(ChoiceButtonClickDelegate leftButtonClick){
		showRightButton = false;
		showLeftButton = false;
		SetLeftButton(leftButtonClick);
	}
	
	public void SetButtonCallbacks(ChoiceButtonClickDelegate leftButtonClick, ButtonClickDelegate rightButtonClick){
		showRightButton = false;
		showLeftButton = false;
		SetLeftButton(leftButtonClick);
		SetRightButton(rightButtonClick);
	}
	
	private void ClickChoice(Choice choice){
		leftButtonClickDelegate(choice._choiceName);
		UpdateMessage(choice._reactionDialog);
	}
	
	public void SetGrabText(string itemName){
		
	}
	
	public void setCharPortrait (Texture charPortrait) {
		Debug.Log ("INSIDE setCharPortrait: " + charPortrait.name);
		_charPortrait = charPortrait;
	}
	
	public void UpdateChoices(List<Choice> choices){
		_choices = choices;
	}
	
	private void SetChatRectangles(){
		GameObject player = GameObject.Find("PlayerCharacter");
		Vector3 maxBounds = Camera.main.WorldToScreenPoint (player.transform.position); // TODO
		
		// Overall Chat variables
		float chatWidth = 1 - (2f * CHATPADDING);
		float chatHeight = .5f - (2f * CHATPADDING);
		float chatTopLeftX = CHATPADDING;
		float chatTopLeftY = CHATPADDING;
		mainChatRect = ScreenRectangle.NewRect(chatTopLeftX, chatTopLeftY, chatWidth, chatHeight);
		
		// Portrait variables
		float portraitTopLeftX = chatTopLeftX + CHATINTERNALPADDING;
		float portraitTopLeftY = chatTopLeftY + CHATINTERNALPADDING;
		float portraitWidth = PORTRAITWIDTH;
		float portraitHeight = chatHeight - (2f * CHATINTERNALPADDING);
		portraitRect = ScreenRectangle.NewRect(portraitTopLeftX, portraitTopLeftY, portraitWidth, portraitHeight);
		
		// Text Box Variables
		float textBoxTopLeftX = chatTopLeftX + (2f * CHATBUTTONPADDING) + portraitWidth;
		float textBoxTopLeftY = chatTopLeftY + CHATINTERNALPADDING;
		float textBoxWidth = chatWidth - (3f * CHATINTERNALPADDING) - portraitWidth;
		float textBoxHeight = (chatHeight/2.0f) - CHATINTERNALPADDING;
		textBoxRect = ScreenRectangle.NewRect(textBoxTopLeftX, textBoxTopLeftY, textBoxWidth, textBoxHeight);
		
		// Button Box Variables
		float buttonBoxTopLeftX = textBoxTopLeftX;
		float buttonBoxTopLeftY = textBoxTopLeftY + textBoxHeight;
		float buttonBoxWidth = textBoxWidth;
		float buttonBoxHeight = textBoxHeight;
		
		// Button Variables
		float buttonWidth = (buttonBoxWidth - (3 * CHATBUTTONPADDING)) / 4;
		float buttonHeight = buttonBoxHeight;
		
		// Button one
		float button1TopLeftX = buttonBoxTopLeftX;
		float button2TopLeftX = button1TopLeftX + CHATBUTTONPADDING + buttonWidth;
		float button3TopLeftX = button2TopLeftX + CHATBUTTONPADDING + buttonWidth;
		float button4TopLeftX = button3TopLeftX + CHATBUTTONPADDING + buttonWidth;
		
		button1Rect = ScreenRectangle.NewRect(button1TopLeftX, buttonBoxTopLeftY, buttonWidth, buttonHeight);
		button2Rect = ScreenRectangle.NewRect(button2TopLeftX, buttonBoxTopLeftY, buttonWidth, buttonHeight);
		button3Rect = ScreenRectangle.NewRect(button3TopLeftX, buttonBoxTopLeftY, buttonWidth, buttonHeight);
		buttonGiveRect = ScreenRectangle.NewRect(button4TopLeftX, buttonBoxTopLeftY, buttonWidth, buttonHeight);
	}
}
