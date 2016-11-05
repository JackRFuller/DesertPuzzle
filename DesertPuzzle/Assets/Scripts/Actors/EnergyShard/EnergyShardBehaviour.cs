using UnityEngine;
using System.Collections;

public class EnergyShardBehaviour : MonoBehaviour
{
    private bool hasBeenActivated;
    [SerializeField]
    private PowerGeneratorBehaviour powerGeneratorScript;
    
    private int colorIndex = 0;
    public int ColorIndex { get { return colorIndex; } }
    

    //void PlacedInGenerator(Transform placementPoint)
    //{
    //    rb.isKinematic = true;
    //    transform.position = placementPoint.position;
    //    powerGeneratorScript = placementPoint.parent.GetComponent<PowerGeneratorBehaviour>();

    //    //Change Energy Shard Color
    //    mesh.material = powerGeneratorScript.EnergyMaterial;

    //    colorIndex = (int)powerGeneratorScript.Color;       
    //}

    public void ActivateEnergyShard()
    {
        if(!hasBeenActivated)
        {
            powerGeneratorScript.Activate();
            hasBeenActivated = true;
        }
    }

    public void DeActivateEnergyShard()
    {
        powerGeneratorScript.Activate();
        hasBeenActivated = false;
    }
	
}
