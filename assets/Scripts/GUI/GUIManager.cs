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
	private static bool alreadyMade = false; // denotes if this is the first gui manager that has been awakened
	
	private static GUIManager instance = null;
	public static GUIManager Instance {
		get {return instance;}	
	}
	
	private ChatMenu chatMenu;
	private InGameMenu inGameMenu;
	private InteractionMenu interactionMenu;
	private PauseMenu pauseMenu;
	
	void Awake(){
		if (alreadyMade){
			Destroy(this); // no need for 2 gui managers
			return;
		}
		instance = this;
		alreadyMade = true;
		DontDestroyOnLoad(this); // keep this manager and its loaded assests around
		activeControls = new List<GUIControl>();
		controlsToRemove = new List<GUIControl>();
		controlsToAdd = new List<GUIControl>();
		
		chatMenu = GetComponent<ChatMenu>();
		inGameMenu = GetComponent<InGameMenu>();
		interactionMenu = GetComponent<InteractionMenu>();
		pauseMenu = GetComponent<PauseMenu>();
	}
	
	public void AddNPCChat(NPCChat npcChatToAdd){
		if (!ControlActive(chatMenu)){
			LoadControl(chatMenu);
		}
		chatMenu.AddChat(npcChatToAdd);
	}
	
	public void AddInGameMenu(){
		if (ControlActive(chatMenu)){
			Debug.LogWarning("In game menu was already up");
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
		interactionMenu.OpenChatForNPC(npcToInteractWith);
		if (!ControlActive(interactionMenu)){
			MarkControlForAdding(interactionMenu);
		}
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
		interactionMenu.Refresh();	
	}
	
	public void CloseInteractionMenu(){
		if (!ControlActive(interactionMenu)){
			Debug.LogError("Can't close interaction display when it is not up.");
			return;
		}
		interactionMenu.Close();
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
		if (!guiControlToLoad.Initialized){	
			guiControlToLoad.Initialize();	
		}
		
		activeControls.Add(guiControlToLoad);
	}
	
	private void UnLoadControl(GUIControl guiControlToUnLoad){
		activeControls.Remove(guiControlToUnLoad);	
	}
	
	private void MarkControlForRemoval(GUIControl guiControlToUnLoad){
		controlsToRemove.Add(guiControlToUnLoad);
	}
	
	private void MarkControlForAdding(GUIControl guiControlToAdd){
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
