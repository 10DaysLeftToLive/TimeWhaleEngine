using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * GUIManager.cs
 * 	Responsible for managing all menus/buttons/chats
 *  Will maintain a list of current controls it is displaying, and can be switched into mutliple modes
 */
public class GUIManager : MonoBehaviour {
	private List<GUIControl> activeControls;
	private List<GUIControl> controlsToRemove;
	private List<GUIControl> controlsToAdd;
	
	private static GUIManager instance = null;
	public static GUIManager Instance {
		get {return instance;}	
	}
	
	private ChatMenu chatMenu;
	private InGameMenu inGameMenu;
	private InteractionMenu interactionMenu;
	private PauseMenu pauseMenu;
	private LoadingScreen loadingScreen;
	
	void Awake(){
		if (instance != null){
			Debug.LogWarning("There are 2 gui managers");
		}
		instance = this;
		activeControls = new List<GUIControl>();
		controlsToRemove = new List<GUIControl>();
		controlsToAdd = new List<GUIControl>();
		
		chatMenu = GetComponent<ChatMenu>();
		inGameMenu = GetComponent<InGameMenu>();
		interactionMenu = GetComponent<InteractionMenu>();
		pauseMenu = GetComponent<PauseMenu>();
		loadingScreen = GetComponent<LoadingScreen>();
	}
	
	public void ShowLoadingScreen(){
		MarkControlForAdding(loadingScreen);
	}
	
	public void StartFadingLoadingScreen(){
		loadingScreen.StartFadeOut();
	}
	
	/// <summary>
	/// Hides the loading screen. Do not call directly instead see StartFadingLoadingScreen.
	/// </summary>
	public void HideLoadingScreen(){
		MarkControlForRemoval(loadingScreen);
	}
	
	public void AddNPCChat(NPCChat npcChatToAdd){
		if (!ControlActive(chatMenu)){
			MarkControlForAdding(chatMenu);
		}
		chatMenu.AddChat(npcChatToAdd);
	}
	
	public void AddInGameMenu(){
		if (ControlActive(chatMenu)){
			return;
		}
		MarkControlForAdding(inGameMenu);
	}
	
	public void ShowPauseMenu(){
		if (ControlActive(pauseMenu)){
			Debug.LogWarning("Trying to show pause menu when it is already up");
			return;
		}
		ToggleControlsClickableState(false);
		MarkControlForAdding(pauseMenu);
		inGameMenu.SetPausedState(false);
	}
	
	public void HidePauseMenu(){
		if (!ControlActive(pauseMenu)){
			Debug.LogWarning("Trying to close pause menu when it is not up");
			return;
		}
		ToggleControlsClickableState(true);
		MarkControlForRemoval(pauseMenu);	
	}
	
	private void ToggleControlsClickableState(bool state){
		foreach (GUIControl control in activeControls){	
			if (control == inGameMenu) continue; // don't diable the pause button
			control.ToggleClickable(state);
		}
	}
	
	public void InitiateInteraction(NPC npcToInteractWith){
		if (!ControlActive(interactionMenu)){
			MarkControlForAdding(interactionMenu);
		}
		interactionMenu.OpenChatForNPC(npcToInteractWith);
	}
	
	public void UpdateInteractionDisplay(string newText){
		if (!ControlActive(interactionMenu)){
			Debug.LogError("Can't update interaction display when it is not up.");
			return;
		}
		interactionMenu.UpdateDisplayText(newText);
	}
	
	public void Update(){
		UpdateControls();	
	}
	
	public void OnGUI()	{
		RenderControls();
	}
	
	public void RefreshInteraction(){
		if (!ControlActive(interactionMenu)){
			MarkControlForAdding(interactionMenu);
		}
		interactionMenu.Refresh();	
	}
	
	public void CloseInteractionMenu(){
		if (!ControlActive(interactionMenu)){
			Debug.LogWarning("Trying to close interaction menu when it was not up.");
			return;
		}
		interactionMenu.Close();
	}
	
	/// <summary>
	/// Removes the interaction menu. Do not call this directly see CloseInteractionMenu()
	/// </summary>
	public void RemoveInteractionMenu(){
		MarkControlForRemoval(interactionMenu);	
	}
	
	public bool ClickOnGUI(Vector2 clickOnScreen){
		// flip the screen to start form the top right
		Vector2 convertedScreenClick = new Vector2(clickOnScreen.x, ScreenSetup.screenHeight - clickOnScreen.y); 		
		foreach (GUIControl control in activeControls){
			if (control.ClickOnGUI(convertedScreenClick)){
				return (true);	
			}
		}
		return (false);
	}
	
	private void ClearControls(){
		activeControls.Clear();
	}
	
	private bool ControlActive(GUIControl guiControl){
		return (activeControls.Contains(guiControl));		
	}
	
	private void LoadControl(GUIControl guiControlToLoad){
		
		
		activeControls.Add(guiControlToLoad);
	}
	
	private void UnLoadControl(GUIControl guiControlToUnLoad){
		activeControls.Remove(guiControlToUnLoad);	
	}
	
	private void MarkControlForRemoval(GUIControl guiControlToUnLoad){
		controlsToRemove.Add(guiControlToUnLoad);
	}
	
	private void MarkControlForAdding(GUIControl guiControlToAdd){
		if (!guiControlToAdd.Initialized){	
			guiControlToAdd.Initialize();	
		}
		controlsToAdd.Add(guiControlToAdd);
	}
	
	private void RenderControls(){
		CleanupControls();
		foreach (GUIControl control in activeControls){
			control.Render();
		}
	}
	
	private void CleanupControls(){
		foreach (GUIControl control in controlsToRemove){
			UnLoadControl(control);
		}
		foreach (GUIControl control in controlsToAdd){
			LoadControl(control);	
		}
		controlsToAdd.Clear();
		controlsToRemove.Clear();
	}
	
	private void UpdateControls(){
		foreach (GUIControl control in activeControls){
			control.UpdateControl();
		}
	}
}
