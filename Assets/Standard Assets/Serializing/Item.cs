using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

public class Item {
	[XmlAttribute("name")]
    public string name;
    public int dispositionChange;
	[XmlAttribute("emotionImage")]
	public string emotionImage;
	
	public Item(){
		
	}
	
	public Item(string _name, int _dispositionChange, string _emotionImage){
		name = _name;
		dispositionChange = _dispositionChange;
		emotionImage = _emotionImage;
	}
}