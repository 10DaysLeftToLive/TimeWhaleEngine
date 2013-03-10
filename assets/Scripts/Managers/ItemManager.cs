using UnityEngine;
using System.Collections;

public class ItemManager {

	public enum Age {Child, Adult, Old};
	
	Age plushie = Age.Child;
	Age key = Age.Adult;
	Age gear = Age.Adult;
	Age flower = Age.Child;
	
	public int FirstAppearance(GameObject obj){
		switch(obj.name){
			case "Plushie": 
				return (int)plushie;
			case "GoldenGear":
				return (int)gear;
			case "Flower": 
				return (int)flower;
			case "Key": 
				return (int)key;
		}
		return -1;
	}
}
