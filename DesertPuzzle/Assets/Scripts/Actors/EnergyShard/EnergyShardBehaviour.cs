using UnityEngine;
using System.Collections;

public class EnergyShardBehaviour : MonoBehaviour
{
    private bool hasBeenActivated;
    [SerializeField]
    private PowerGeneratorBehaviour powerGeneratorScript;
    
    private int colorIndex = -1;
    public int ColorIndex { get { return colorIndex; } }

    [Header("Energy Shard Colors")]
    [SerializeField]
    private Material inactiveMaterial;
    [SerializeField]
    private Material[] shardColors;

    private MeshRenderer mesh;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.material = inactiveMaterial;
    }

    //void PlacedInGenerator(Transform placementPoint)
    //{
    //    rb.isKinematic = true;
    //    transform.position = placementPoint.position;
    //    powerGeneratorScript = placementPoint.parent.GetComponent<PowerGeneratorBehaviour>();

    //    //Change Energy Shard Color
    //    mesh.material = powerGeneratorScript.EnergyMaterial;

    //    colorIndex = (int)powerGeneratorScript.Color;       
    //}

    public void ActivateEnergyShard(int color)
    {
        if(colorIndex != color)
        {
            colorIndex = color;

            //Change Color
            mesh.material = shardColors[colorIndex];

            powerGeneratorScript.ActivateSwitches(colorIndex);  
        }
    }

    public void DeActivateEnergyShard()
    {
        //powerGeneratorScript.Activate();
        powerGeneratorScript.DisableSwitches();
        mesh.material = inactiveMaterial;
        colorIndex = -1;
    }
	
}
