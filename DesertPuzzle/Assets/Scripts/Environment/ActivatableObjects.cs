using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableObjects : MonoBehaviour
{
    [SerializeField]
    private List<RisingPlatforms> risingPlatforms = new List<RisingPlatforms>();

    public void PowerOn()
    {
        for(int i = 0; i < risingPlatforms.Count; i++)
        {
            risingPlatforms[i].InitiateMovement(true);
        }
    }

    public void PowerOff()
    {
        for (int i = 0; i < risingPlatforms.Count; i++)
        {
            risingPlatforms[i].InitiateMovement(false);
        }
    }
}
