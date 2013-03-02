using UnityEngine;
using System.Collections;


/*
 * LevelMasks.cs
 * Contains all levels and their masks
 */
public static class LevelMasks {
	#region Layers
	public static int ClimbableLayer = 8;
	public static int GroundLayer = 9;
	public static int ImpassableLayer = 10;
	public static int LadderTopLayer = 13;
	public static int MechanicsLayer = 14;
	#endregion
	
	#region Mashs
	public static int ClimbableMask = (1 << ClimbableLayer);
	public static int GroundMask = (1 << GroundLayer);
	public static int ImpassableMask = (1 << ImpassableLayer);
	public static int LadderTopMask = (1 << LadderTopLayer);
	public static int MechanicsMask = (1 << MechanicsLayer);
	#endregion
}
