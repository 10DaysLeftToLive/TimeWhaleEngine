using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * InteractionMenu.cs
 * 	Responsible for displaying an interaction with an npc
 * 	 it will ask the npc for the list of buttons texts, portrait and defult text.
 * 		the emotion state of the npc will provide these
 * 	When a click on a button occurs it will call the corrisponding function of the npc
 * 	 (which the emotion state will fullfill) and will expect a text back of what to display now
 */
public class InteractionMenu : GUIControl {
	private NPC npcChattingWith;
	private Player player;
	
	#region Publicly edited visual data
	public GUIStyle textFieldStyle;
	public GUIStyle buttonStyle;
	public Font textFieldFont;
	public Font buttonFont;
	#endregion
	
	#region Saved Data for this interaction
	private string mainDisplayText;
	private Texture charPortrait;
	#endregion
	
	#region Rectangle Data
	private Rect mainChatRect;
	private Rect portraitRect;
	private Rect textBoxRect;
	private Rect leaveButtonRect;
	private List<Rect> buttonRects;
	private List<string> buttonTexts;
	#endregion
	
	#region Settings
	private static float CHATPADDING = .01f; // padding between chat and the screen in all directions
	private static float CHATINTERNALPADDING = .01f; // padding between chat elements in all directions
	private static float CHATBUTTONPADDING = .01f; // padding between chat elements in all directions
	private static float PORTRAITWIDTH = .2f;
	private static int GIVEITEMBUTTON = 3; // index in rect list for wher give button should go
	private static float LEAVEBUTTONHEIGHT = .1f;
	private static int charPerLine = 45;
	#endregion
	
	public override void Init(){
		player = (Player) GameObject.Find(Strings.Player).GetComponent<Player>(); // the player will always stay the same so find it at the start
		buttonTexts = new List<string>();
		buttonRects = new List<Rect>();
		SetChatRectangles();
		mainDisplayText = null;
	}
	
	private bool hasInitializedStyles = false;
	public override void Render(){		
		if (npcChattingWith == null){
			Debug.LogError("Trying to display a chat with no npc.");
			return;
		}
		DrawBackgroundBox();
		DrawTextBox();
		DisplayButtonChoices();
		DisplayPortrait();
		DisplayLeaveButton();
		if (player.Inventory.HasItem()){
			DisplayGiveButton();
		}
	}
	
	private int currentButtonIndex;
	private void DisplayButtonChoices(){
		currentButtonIndex = 0;
		
		foreach (string text in buttonTexts){
			if (currentButtonIndex == 3){
				Debug.LogError("Trying to display more than 3 choices");
				return;
			}
			if (ButtonClick(buttonRects[currentButtonIndex], text, buttonStyle)){
				DoClickOnChoice(text);
			}
			currentButtonIndex++;
		}
	}
	
	public override bool ClickOnGUI(Vector2 screenPos){
		return (mainChatRect.Contains(screenPos));
	}
	
	public void Close(){
		npcChattingWith.LeaveInteraction();	
		player.LeaveInteraction();
	}
	
	private void DisplayGiveButton(){
		if (ButtonClick(buttonRects[GIVEITEMBUTTON], "Give " + player.Inventory.GetItem().name, buttonStyle)){
			DoGiveClick();
		}
	}
	
	private void DisplayPortrait(){
		GUI.Box (portraitRect, charPortrait);	
	}
	
	private void DrawBackgroundBox(){
		GUI.Box (mainChatRect, "", textFieldStyle);
	}
	
	private void DisplayLeaveButton(){
		if (ButtonClick(leaveButtonRect, "Leave", buttonStyle)){
			GUIManager.Instance.CloseInteractionMenu();
		}
	}
	
	private void DrawTextBox(){
		if (mainDisplayText == null) Refresh();
		GUI.TextField (textBoxRect, mainDisplayText, textFieldStyle);
	}
	
	public void DoClickOnChoice(string choice){
		Debug.Log("Doing click on " + choice);
		npcChattingWith.ReactToChoice(choice);
	}
	
	private void DoGiveClick(){
		Debug.Log("Doing give click");
		npcChattingWith.ReactToBeingGivenItem(player.Inventory.GetItem());
	}

	public void OpenChatForNPC(NPC _newNpcChatting){
		Debug.Log("Opening interaction with " + _newNpcChatting);
		npcChattingWith = _newNpcChatting;
		Refresh();
	}
	
	public void Refresh(){
		UpdateDisplayText(GetDisplayText());
		GetChoicesFromNPC();
		GetPortraitTexture();
	}
	
	public void UpdateDisplayText(string newText){
		mainDisplayText = newText;//ParseMessage(newText);
	}
	
	private void GetChoicesFromNPC(){
		buttonTexts = npcChattingWith.GetButtonChats();
	}
	
	private void GetPortraitTexture(){
		charPortrait = npcChattingWith.GetPortrait();
	}
	
	private string GetDisplayText(){
		return (npcChattingWith.GetDisplayText());	
	}
	
	// Convert the given message's formatting so it will fit into the screen nicely
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
		// Overall Chat variables
		float chatWidth = 1 - (2f * CHATPADDING);
		float chatHeight = .45f - (2f * CHATPADDING);
		float chatTopLeftX = CHATPADDING;
		float chatTopLeftY = CHATPADDING;
		mainChatRect = ScreenRectangle.NewRect(chatTopLeftX, chatTopLeftY, chatWidth, chatHeight);
		
		// Portrait variables
		float portraitTopLeftX = chatTopLeftX + CHATINTERNALPADDING;
		float portraitTopLeftY = chatTopLeftY + CHATINTERNALPADDING;
		float portraitWidth = PORTRAITWIDTH;
		float portraitHeight = chatHeight - (2f * CHATINTERNALPADDING) - LEAVEBUTTONHEIGHT;
		portraitRect = ScreenRectangle.NewRect(portraitTopLeftX, portraitTopLeftY, portraitWidth, portraitHeight);
		leaveButtonRect = ScreenRectangle.NewRect(portraitTopLeftX, portraitTopLeftY + portraitHeight, portraitWidth, LEAVEBUTTONHEIGHT); 
		
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