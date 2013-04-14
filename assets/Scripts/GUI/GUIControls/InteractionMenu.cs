using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionMenu : GUIControl {
	private NPC npcChattingWith;
	private Player player;
	private List<string> buttonTexts;
	
	public Texture btn1, btn2;
	Vector3 pos, screenPos;
	Vector2 size;
	string msg;
	int charPerLine = 60;
	Vector2 offset;
	
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
	private List<Rect> buttonRects;
	
	#region Settings
	private static float CHATHEIGHTPERCENTAGE = .7f;
	private static float BUTTONHEIGHTPERCENTAGE = 1-CHATHEIGHTPERCENTAGE;
	private static float CHATPADDING = .01f; // padding between chat and the screen in all directions
	private static float CHATINTERNALPADDING = .01f; // padding between chat elements in all directions
	private static float CHATBUTTONPADDING = .01f; // padding between chat elements in all directions
	private static float PORTRAITWIDTH = .2f;
	private static int GIVEITEMBUTTON = 3; // index in rect list for wher give button should go
	#endregion
	
	public override void Init(){
		Debug.Log("Init of interaction");
		player = (Player) GameObject.Find(Strings.Player).GetComponent<Player>(); // the player will always stay the same so find it at the start
		buttonTexts = new List<string>();
		buttonRects = new List<Rect>();
		SetChatRectangles();
		npcChattingWith = null;
	}
	
	public override void Update(){
		
	}
	/*
	public void InitiateChat(ChatChoiceInfo chatChoiceInfo){
		_chatChoiceInfo = chatChoiceInfo;
	}*/
	
	public override void Render(){
		if (npcChattingWith == null){
			Debug.LogError("Trying to display a chat with no npc.");
			return;
		}
		
		DisplayButtonChoices();
		if (player.Inventory.HasItem()){
			DisplayGiveButton();
		}
	}
	
	private int currentButtonIndex;
	private void DisplayButtonChoices(){
		currentButtonIndex = 0;
		
		foreach (string text in buttonTexts){
			if (GUI.Button(buttonRects[currentButtonIndex], text)){
				DoClickOnChoice(text);
			}
			currentButtonIndex++;
		}
	}
	
	private void DisplayGiveButton(){
		if (GUI.Button(buttonRects[GIVEITEMBUTTON], "Give")){
			DoGiveClick();
		}
	}
	
	/*
	public void UpdateMessage(string newMessage){
		msg = ParseMessage(newMessage);
	}
	
	public void CreateChatBox(List<Choice> choices, string text){
		_choices = choices;
		msg = ParseMessage(text);
		
		isActive = true;
	}
	
	public void RemoveChatBox(){
		isActive	= false;
		showRightButton = false;
		showLeftButton = false;
		rightButtonClickDelegate = null;
		leftButtonClickDelegate = null;
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
	}*/
	
	private void DoClickOnChoice(string choice){
		Debug.Log("Doing click on " + choice);
	}
		
	private void DoGiveClick(){
		Debug.Log("Doing give click");
	}
	
	private void GetNPCReactionText(){
		
	}
	
	public void OpenChatForNPC(NPC _newNpcChatting){
		Debug.Log("Opening interaction with " + _newNpcChatting);
		npcChattingWith = _newNpcChatting;
		GetChoicesFromNPC();
	}
	
	private void GetChoicesFromNPC(){
		buttonTexts = npcChattingWith.GetButtonChats();
		foreach (string s in buttonTexts){
			Debug.Log("Choice: " + s);	
		}
	}
	
	private void GetPortraitTexture(){
		_charPortrait = npcChattingWith.GetPortrait();
	}
	
	private string ParseMessage(string message){
		if (message.Length > charPerLine){
			int index = 0;
			do {
				index = index + charPerLine;
				if (index >= message.Length)
					return (message);
				do {
					--index;
				}while(message[index] != ' ');

				message = message.Insert(index, "\n");
				message = message.Remove(index+1, 1);
			}while(true);
			
		}
		return (message);
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
		
		buttonRects.Add(ScreenRectangle.NewRect(button1TopLeftX, buttonBoxTopLeftY, buttonWidth, buttonHeight));
		buttonRects.Add(ScreenRectangle.NewRect(button2TopLeftX, buttonBoxTopLeftY, buttonWidth, buttonHeight));
		buttonRects.Add(ScreenRectangle.NewRect(button3TopLeftX, buttonBoxTopLeftY, buttonWidth, buttonHeight));
		buttonRects.Add(ScreenRectangle.NewRect(button4TopLeftX, buttonBoxTopLeftY, buttonWidth, buttonHeight));
	}
}