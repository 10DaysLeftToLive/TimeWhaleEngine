using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;


public class ReadNPCItemData : MonoBehaviour {
	public static Dictionary<string, float> ReadNPCItemDataFromFile(XmlDocument xmlFile, string rootOfNPC){
		Dictionary<string, float> itemsToDispostions = new Dictionary<string, float>();
		
		string pathToItems = GetPathToItems(rootOfNPC);
		
		int numberOfItems = GetNumberItems(xmlFile, pathToItems);
		
		Debug.Log("NumberItems = " + numberOfItems);
		
		string pathToItem;
		string itemName;
		int itemDispositionChange;
		
		for (int i = 0; i < numberOfItems; i++){
			pathToItem = GetPathToItem(pathToItems, i);
			itemName = GetItemName(xmlFile, pathToItem);
			itemDispositionChange = GetItemDispositionChange(xmlFile, pathToItem);
			Debug.Log("item[" + i + "] = " + itemName + " : " + itemDispositionChange);
			itemsToDispostions.Add(itemName, itemDispositionChange);
		}
		
		return (itemsToDispostions);
	}
	
	private static string GetPathToItems(string rootOfNPC){
		return (rootOfNPC + "Items/");
	}
	
	private static int GetNumberItems(XmlDocument xmlFile, string pathToItems){
		string numberItems = xmlFile.SelectSingleNode(pathToItems +  "NumberItems").InnerText;
		return (int.Parse(numberItems));
	}
	
	private static string GetPathToItem(string pathToItems, int itemNumber){
		return (pathToItems + "Item[@id='" + itemNumber + "']/");
	}
	
	private static string GetItemName(XmlDocument xmlFile, string pathToItem){
		return (xmlFile.SelectSingleNode(pathToItem + "Name").InnerText);
	}
	
	private static int GetItemDispositionChange(XmlDocument xmlFile, string pathToItem){
		string dispositionChange = xmlFile.SelectSingleNode(pathToItem + "DispositionChange").InnerText;
		return (int.Parse(dispositionChange));
	}
}
