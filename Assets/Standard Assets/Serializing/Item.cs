using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

public class Item {
	[XmlAttribute("name")]
    public string name;
    public float dispositionChange;
	
	public Item(){
		
	}
	
	public Item(string _name, float _dispositionChange){
		name = _name;
		dispositionChange = _dispositionChange;
	}
}