using UnityEngine;
using System.Collections;

public class LightBoxBehaviour : Pickup
{
    [Header("Components")]
    [SerializeField]
    private EnergyBeamHandler energyBeam;
    [SerializeField]
    private Rigidbody rb;    

    //Reflection Attributes
    private int numberOfReflections = 3;    

   

    private LightBoxState lightBoxState;
    private enum LightBoxState
    {
       OnStand,
       Inactive,
    }

    private GameObject activeEnergyShard;

    private const int colorIndex = 0;

    void Start()
    {
       Init();
    }

    void Init()
    {
        lightBoxState = LightBoxState.Inactive;
        energyBeam.SetBeamAttributes(new Vector3(0,0,-1), numberOfReflections);
        energyBeam.ToggleLineRenderer();
    }

    public override void Update()
    {
        base.Update();

        if (lightBoxState == LightBoxState.OnStand)
        {
            energyBeam.SetBeamDirect(-transform.forward);
            energyBeam.SendOutBeam();
        }
            
    }
   
    void PlaceInStand(Transform placementPoint)
    {
        rb.isKinematic = true;        

        transform.parent = placementPoint.parent;
        transform.position = placementPoint.position;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        energyBeam.ToggleLineRenderer();

        lightBoxState = LightBoxState.OnStand;
    }

    void RemoveFromStand()
    {
        transform.parent = null;  
        lightBoxState = LightBoxState.Inactive;
        energyBeam.ToggleLineRenderer();
    }
	
}
