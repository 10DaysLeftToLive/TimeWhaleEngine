using UnityEngine;
using System;
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
	public Texture talkingIndicator;
	
	public GUIStyle textFieldStyle;
	public GUIStyle buttonStyle;
	public GUIStyle portraitStyle;
	public GUIStyle interactionBoxStyle;
	public GUIStyle talkingIndicatorStyle;
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
	private Rect talkingIndicatorRect;
	private List<Rect> buttonRects;
	private List<string> buttonTexts;
	float textBoxHeight;
	#endregion
	
	#region Button Data
	private float buttonBoxTopLeftX;
	private float buttonBoxTopLeftY;
	private float buttonBoxWidth;
	private float buttonBoxHeight;
	private float buttonWidth;
	private float buttonHeight;
	#endregion
	
	#region Settings
	private static float CHATPADDING = .01f; // padding between chat and the screen in all directions
	private static float CHATINTERNALPADDING = .01f; // padding between chat elements in all directions
	private static float CHATBUTTONPADDING = .02f; // padding between chat elements in all directions
	private static float CHATHEIGHT = .3f;
	private static float TEXTBOXPERCENTAGE = .6f;
	private static float PORTRAITHEIGHT = .32f;
	private static float INTERATIONMENUARTRATIO = 5.0f/1.0f; // width / height
	private static float FONTRATIOTEXTBOX = 20; // kinda arbitrary
	private static float FONTRATIOBUTTONS = 30; // kinda arbitrary
	private static float INDICATORSIZE = .1f;
	#endregion
	
	public override void Init(){
		player = (Player) GameObject.Find(Strings.Player).GetComponent<Player>(); // the player will always stay the same so find it at the start
		buttonTexts = new List<string>();
		buttonRects = new List<Rect>();
		SetChatRectangles();
		mainDisplayText = null;
        textFieldStyle.fontSize = (Mathf.RoundToInt(Mathf.Min(ScreenSetup.screenWidth, ScreenSetup.screenHeight) / FONTRATIOTEXTBOX));
        buttonStyle.fontSize = (Mathf.RoundToInt(Mathf.Min(ScreenSetup.screenWidth, ScreenSetup.screenHeight) / FONTRATIOBUTTONS));
	}
	
	public override void Render(){		
		if (npcChattingWith == null){
			Debug.LogError("Trying to display a chat with no npc.");
			return;
		}
		DrawBackgroundBox();
		DrawTextBox();
		DisplayButtonChoices();
		DisplayPortrait();	
		DisplayTalkingIndicator();
	}
	
	#region Display Functions
	private int currentButtonIndex;
	private void DisplayButtonChoices(){
		try {
			currentButtonIndex = 0;
			
			foreach (string text in buttonTexts){
				if (currentButtonIndex == 3){
					Debug.LogError("Trying to display more than 3 choices");
					return;
				}
				try {
					if (ButtonClick(buttonRects[currentButtonIndex], text, buttonStyle)){
						DoClickOnChoice(text);
					}
				} catch (Exception e){
					Debug.LogWarning("Button choices were not altered correctly for " + npcChattingWith.name + " updating them");
					GetChoicesFromNPC();
					return;
				}
				currentButtonIndex++;
			}
			if (player.Inventory.HasItem() && npcChattingWith.CanTakeItem(player.Inventory.GetItem().name)){
				if (ButtonClick(buttonRects[currentButtonIndex], "Give " + player.Inventory.GetItem().name, buttonStyle)){
					DoGiveClick();
				}
			}
		} catch {
			Debug.LogWarning("Choices were changed mid-interaction. Refreshing them");
			GetChoicesFromNPC();
		}
	}
	
	public override bool ClickOnGUI(Vector2 screenPos){
		return (mainChatRect.Contains(screenPos));
	}
	
	public void Close(){
		npcChattingWith.LeaveInteraction();	
		player.LeaveInteraction();
	}
	
	private void DisplayPortrait(){
		GUI.DrawTexture (portraitRect, charPortrait);	
	}
	
	private void DrawBackgroundBox(){
		GUI.Box (mainChatRect, "", interactionBoxStyle);
	}
	
	private void DrawTextBox(){
		if (mainDisplayText == null) Refresh();
		GUI.Box (textBoxRect, mainDisplayText, textFieldStyle);
	}
	
	private void DisplayTalkingIndicator(){
		GUI.DrawTexture(talkingIndicatorRect, talkingIndicator);
	}
	#endregion
	
	public void DoClickOnChoice(string choice){
		npcChattingWith.ReactToChoice(choice);
	}
	
	private void DoGiveClick(){
		npcChattingWith.ReactToBeingGivenItem(player.Inventory.GetItem());
	}

	public void OpenChatForNPC(NPC _newNpcChatting){
		npcChattingWith = _newNpcChatting;
		Refresh();
	}
	
	public void Refresh(){
		UpdateDisplayText(GetDisplayText());
		GetChoicesFromNPC();
		GetPortraitTexture();
	}
	
	public void UpdateDisplayText(string newText){
		mainDisplayText = newText;
	}
	
	private void GetChoicesFromNPC(){
		buttonTexts = npcChattingWith.GetButtonChats();
		SetUpChoiceButtonRectangles(buttonTexts.Count + (player.Inventory.HasItem() && npcChattingWith.CanTakeItem(player.Inventory.GetItem().name) ? 1 : 0));
	}
	
	private void GetPortraitTexture(){
		charPortrait = npcChattingWith.GetPortrait();
	}
	
	private string GetDisplayText(){
		return (npcChattingWith.GetDisplayText());	
	}
	
	private void SetUpChoiceButtonRectangles(int numChoices){
		buttonRects.Clear();
		float totalButtonWidth = (numChoices * buttonWidth) + ((numChoices - 1) * CHATBUTTONPADDING);
		
		float button1TopLeftX = (buttonBoxTopLeftX + buttonBoxWidth/2) - totalButtonWidth/2; // move the first button off set from the center by the number of buttons
		float spaceBetweenTopPointXs = CHATBUTTONPADDING + buttonWidth;
		
		for (int i = 0; i < numChoices; i++){
			buttonRects.Add(ScreenRectangle.NewRect(button1TopLeftX + (spaceBetweenTopPointXs * i), buttonBoxTopLeftY, buttonWidth, buttonHeight));
		}
	}
	
	private void SetChatRectangles(){	
		float sideSpace; // the space on the left and right of the interactions menu to properly scale the assets
		
		float portraitHeight = PORTRAITHEIGHT;
		float screenRatio = ScreenSetup.screenHeight/ScreenSetup.screenWidth;
		float portraitWidth = PORTRAITHEIGHT * screenRatio;
		
		float chatHeight = CHATHEIGHT;
		float chatWidth = (chatHeight * INTERATIONMENUARTRATIO) * screenRatio;
		
		if (portraitWidth + chatWidth > 1){
			chatWidth = 1 - portraitWidth - CHATINTERNALPADDING;
			sideSpace = 0;
		} else {
			sideSpace = (1.0f - (portraitWidth + chatWidth))/2.0f;
		}
		
		// Portrait variables
		float portraitTopLeftX = sideSpace;
		float portraitTopLeftY = CHATPADDING;
		portraitRect = ScreenRectangle.NewRect(portraitTopLeftX, portraitTopLeftY, portraitWidth, portraitHeight);
		
		// Overall Chat variables
		float chatTopLeftX = sideSpace + portraitWidth + CHATINTERNALPADDING;
		float chatTopLeftY = (2 * CHATPADDING);
		mainChatRect = ScreenRectangle.NewRect(chatTopLeftX, chatTopLeftY, chatWidth, chatHeight);
		
		// Text Box Variables
		float textBoxTopLeftX = chatTopLeftX + CHATINTERNALPADDING;
		float textBoxTopLeftY = chatTopLeftY + CHATINTERNALPADDING;
		float textBoxWidth = chatWidth - (2f * CHATINTERNALPADDING);
		textBoxHeight = (chatHeight*TEXTBOXPERCENTAGE) - CHATINTERNALPADDING;
		
		textBoxRect = ScreenRectangle.NewRect(textBoxTopLeftX, textBoxTopLeftY, textBoxWidth, textBoxHeight);
		
		float indicatorSizeX = INDICATORSIZE * screenRatio;
		
		float talkingIndicatorTopLeftX = textBoxTopLeftX - indicatorSizeX - CHATINTERNALPADDING/2;
		float talkingIndicatorTopLeftY = .18f;
		
		talkingIndicatorRect = ScreenRectangle.NewRect(talkingIndicatorTopLeftX, talkingIndicatorTopLeftY, indicatorSizeX,INDICATORSIZE);
		
		// Button Box Variables
		buttonBoxTopLeftX = textBoxTopLeftX;
		buttonBoxTopLeftY = textBoxTopLeftY + textBoxHeight;
		buttonBoxWidth = textBoxWidth - (2 * CHATINTERNALPADDING);
		buttonBoxHeight = chatHeight - textBoxHeight - (3 * CHATINTERNALPADDING);
		
		// Button Variables
		buttonWidth = (buttonBoxWidth - (3 * CHATBUTTONPADDING)) / 4;
		buttonHeight = buttonBoxHeight;
	}
}