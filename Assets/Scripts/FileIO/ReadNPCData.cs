using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public class ReadNPCData : MonoBehaviour {
	private static string root = "NPC_LevelData/";
	
	public static Dictionary<string, Dictionary<string, float>> ReadNPCItemsDispositonFromFile(string levelData){
		Dictionary<string, Dictionary<string, float>> npcToNPCItems = new Dictionary<string, Dictionary<string, float>>();
		
		XmlDocument xmlFile = new XmlDocument();
		xmlFile.Load(levelData);
		
		int numberNPCs = FindNumberNPCs(xmlFile);
		string pathToNPC;
		string npcName;
		Dictionary<string, float> npcItemToDisposition;
		
		Debug.Log("Number of NPCs = " + numberNPCs);
		
		for (int i = 0; i < numberNPCs; i++){
			pathToNPC = GetPathToNPC(i);
			npcName = GetNPCName(xmlFile, pathToNPC);
			npcItemToDisposition = ReadNPCItemData.ReadNPCItemDataFromFile(xmlFile, pathToNPC);
			Debug.Log("Name[" + i + "] = " + npcName);
			Debug.Log(npcItemToDisposition.ToString());
			npcToNPCItems.Add(npcName, npcItemToDisposition);
		}
		return (npcToNPCItems);
	}
			
	private static int FindNumberNPCs(XmlDocument xmlFile){
		string numberNPCs = xmlFile.SelectSingleNode(root + "NumberNPCs").InnerText;
		return (int.Parse(numberNPCs));
	}
	
	private static string GetPathToNPC(int NPC_Number){
		return (root + "NPC[@id='" + NPC_Number + "']/");
	}
	
	private static string GetNPCName(XmlDocument xmlFile, string pathToNPC){
		return (xmlFile.SelectSingleNode(pathToNPC + "Name").InnerText);
	}
}
