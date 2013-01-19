using UnityEngine;
using System.Collections;

public class npcManager : MonoBehaviour {
	
	public Component[] npcs;
	
	// Use this for initialization
	void Start () {
		npcs = GetComponentsInChildren<npcClass>();
		foreach(npcClass npc in npcs){
			if (npc.npcName == "Susan"){
				npc.SetDisposition(4);
			}else if (npc.npcName == "Charlie"){
				npc.SetDisposition(1);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		foreach(npcClass npc in npcs){
			if(Input.GetKey("b")){
				npc.UpdateText("" + npc.GetDisposition());
			}else{
				npc.UpdateText(npc.npcName);
			}
			
			if (npc.npcName == "Charlie" && npc.GetState() != 1  && Input.GetKeyDown("v")){
				npc.ChangeState(1);	
				print("here");
			}
				
		}	
	}
}
