using UnityEngine;
using System.Collections;

public class ExtensionMethods : MonoBehaviour
{
    public static bool CheckIfRayAndShardAreSameColor(EnergyShardBehaviour energyShard, int colorIndex)
    {
        if (energyShard.ColorIndex == colorIndex)
            return true;
        else
            return false;
    }

    public static bool CheckForInteraction()
    {
        if (Input.GetKeyUp(KeyCode.E))
            return true;
        else return false;
    }
    
	   
}
