using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public class ReadNPCDispostion : MonoBehaviour {
	private static string root = "NPC_DispostitionData/";
	
	public static Dictionary<string, float> ReadNPCItemsDispositonFromFile(string file){
		XmlDocument xmlFile = new XmlDocument();
		xmlFile.Load(file);
		
		int numberNPCs = FindNumberNPCs(xmlFile);
		string pathToNPC;
		string npcName;
		float npcDisposition;
		Dictionary<string, float> npcDispositionDic = new Dictionary<string, float>();
		
		Debug.Log("Number of NPCs = " + numberNPCs);
		
		for (int i = 0; i < numberNPCs; i++){
			pathToNPC = GetPathToNPC(i);
			npcName = GetNPCName(xmlFile, pathToNPC);
			npcDisposition = GetNPCDisposition(xmlFile, pathToNPC);
			Debug.Log("Disposition[" + i + "] = " + npcName + " : Disposition " + npcDisposition.ToString());
			npcDispositionDic.Add(npcName, npcDisposition);
		}
		
		return (npcDispositionDic);
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
	
	private static int GetNPCDisposition(XmlDocument xmlFile, string pathToNPC){
		return (int.Parse(xmlFile.SelectSingleNode(pathToNPC + "Disposition").InnerText));
	}
}
