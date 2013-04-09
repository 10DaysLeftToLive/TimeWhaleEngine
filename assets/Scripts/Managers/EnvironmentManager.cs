using UnityEngine;
using System.Collections.Generic;

// TODO - complete this environment manager to do needed environment changes
public class EnvironmentManager : ManagerSingleton<EnvironmentManager> {
	private static Dictionary<string, GameObject> dictEnviro = new Dictionary<string, GameObject>();
	
	public static void Add(GameObject environmentObject) {
		dictEnviro.Add(environmentObject.name, environmentObject);
	}
	
	public static void turnOnEnvironment(string environmentName) {
		// Turn on environment change
	}
	
	public static void turnOffEnvironment(string environmentName) {
		// Turn off environment change
	}
}
