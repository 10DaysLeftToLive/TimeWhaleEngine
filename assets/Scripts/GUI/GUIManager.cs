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
	private static bool alreadyMade = false; // denotes if this is the first gui manager that has been awakened
	
	private static GUIManager instance = null;
	public static GUIManager Instance {
		get {return instance;}	
	}
	
	public ChatMenu chatMenu;
	public InGameMenu inGameMenu;
	public InteractionMenu interactionMenu;
	
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
		
		chatMenu = GetComponent<ChatMenu>();
		inGameMenu = GetComponent<InGameMenu>();
		interactionMenu = GetComponent<InteractionMenu>();
	}
	
	public void AddNPCChat(NPCChat npcChatToAdd){
		if (!ControlActive(chatMenu)){
			LoadControl (chatMenu);
		}
		chatMenu.AddChat(npcChatToAdd);
	}
	
	public void InitiateInteraction(NPC npcToInteractWith){
		LoadControl(interactionMenu);
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
		interactionMenu.Refresh();	
	}
	
	public void CloseInteractionMenu(){
		if (!ControlActive(interactionMenu)){
			Debug.LogError("Can't update interaction display when it is not up.");
			return;
		}
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
		foreach (GUIControl control in activeControls){
			control.ClearResponse();	
		}
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
		Debug.Log("Unloading " + guiControlToUnLoad.ToString());
		activeControls.Remove(guiControlToUnLoad);	
	}
	
	private void MarkControlForRemoval(GUIControl guiControlToUnLoad){
		controlsToRemove.Add(guiControlToUnLoad);
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
		controlsToRemove.Clear();
	}
	
	private void UpdateControls(){
		foreach (GUIControl control in activeControls){
			control.UpdateControl();
		}
	}
}
