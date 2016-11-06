using UnityEngine;
using System.Collections;

public class ExtensionMethods : MonoBehaviour
{
    public static bool CheckIfRayAndShardAreSameColor(EnergyShardBehaviour energyShard, int colorIndex)
    {
        //Debug.Log(energyShard.ColorIndex +  " " + colorIndex);

        if (energyShard.ColorIndex == colorIndex)
            return true;
        else
            return false;
    }

    #region Input

    public static bool CheckForInteraction()
    {
        if (Input.GetMouseButtonUp(0))
            return true;
        else return false;
    }

    public static bool CheckForTurningLeft()
    {
        if (Input.GetMouseButtonUp(0))
            return true;
        else
            return false;
    }

    public static bool CheckForTurningRight()
    {
        if (Input.GetMouseButtonUp(1))
            return true;
        else
            return false;
    }


    #endregion

    /// <summary>
    /// Used to control boolean triggered animations
    /// </summary>
    /// <param name="anim"></param>
    /// <param name="targetAnimation"></param>
    public static void PlayAnimation(Animator anim, string targetAnimation)
    {
        for(int i = 0; i < anim.parameterCount; i++)
        {
            if(anim.parameters[i].name != targetAnimation)
            {
                anim.SetBool(anim.parameters[i].name, false);
            }
            else
            {
                anim.SetBool(anim.parameters[i].name, true);
            }
        }
    }

    public static IEnumerator SetObjectToIdle(Animator anim, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        for (int i = 0; i < anim.parameterCount; i++)
        {
            anim.SetBool(anim.parameters[i].name, false);            
        }
    }
    
	   
}
