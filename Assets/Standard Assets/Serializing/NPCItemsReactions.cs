using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

public class NPCItemsReactions {
	[XmlAttribute("name")]
    public string name;
    public List<Item> items;
	
	public NPCItemsReactions(){
		items = new List<Item>();
	}
	
	public void Add(Item itemToAdd){
		items.Add(itemToAdd);
	}
}