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
	private static bool alreadyMade = false; // denotes if this is the first gui manager that has been awakened
	
	public ChatMenu chatMenu;
	public InGameMenu inGameMenu;
	public InteractionMenu interactionMenu;
	
	void Awake(){
		if (alreadyMade){
			Destroy(this); // no need for 2 gui managers
			return;
		}
		alreadyMade = true;
		DontDestroyOnLoad(this); // keep this manager and its loaded assests around
		activeControls = new List<GUIControl>();
		
		chatMenu = new ChatMenu();
		inGameMenu = new InGameMenu();
		interactionMenu = new InteractionMenu();
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
	
	public void Update(){
		UpdateControls();	
	}
	
	public void OnGUI()	{
		RenderControls();
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
		activeControls.Remove(guiControlToUnLoad);	
	}
	
	private void RenderControls(){
		foreach (GUIControl control in activeControls){
			control.Render();
		}
	}
	
	private void UpdateControls(){
		foreach (GUIControl control in activeControls){
			control.UpdateControl();
		}
	}
}
