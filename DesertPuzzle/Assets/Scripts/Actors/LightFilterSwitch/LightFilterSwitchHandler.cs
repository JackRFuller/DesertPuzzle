using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFilterSwitchHandler : Interactable
{
    [Header("Components")]
    [SerializeField]
    private LightFilterStand[] lightFilters;
    [SerializeField]
    private Animator handleAnims;

    private int turningHandle = 0; //0 = Left; 1 = Right
    private bool isSwitchingColors;

    public override void Update()
    {
        base.Update();

        if(canInteract)
        {
            if(!isSwitchingColors)
            {
                if (ExtensionMethods.CheckForTurningLeft())
                {
                    turningHandle = 0;
                    Interact();
                }
                if (ExtensionMethods.CheckForTurningRight())
                {
                    turningHandle = 1;
                    Interact();
                }
            }
        }
    }

    void Interact()
    {
        isSwitchingColors = true;

        if(turningHandle == 0)
        {
            ExtensionMethods.PlayAnimation(handleAnims, "turnLeftHandle");
        }
        else if(turningHandle == 1)
        {
            ExtensionMethods.PlayAnimation(handleAnims, "turnRightHandle");
        }

        for(int i = 0; i < lightFilters.Length; i++)
        {
            lightFilters[i].CycleColours(turningHandle);
        }

        StartCoroutine(ExtensionMethods.SetObjectToIdle(handleAnims, 1));
        isSwitchingColors = false;     
    }
}
