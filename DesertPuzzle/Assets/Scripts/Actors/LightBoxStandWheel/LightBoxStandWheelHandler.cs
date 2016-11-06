using UnityEngine;
using System.Collections;

public class LightBoxStandWheelHandler : Interactable
{
    [Header("Components")]
    [SerializeField]
    private LightBoxStandBehaviour lightBoxStand;
    [SerializeField]
    private Animator wheelAnimation;
    private int direction = 0; //0 = Left; 1 = Right

    public override void Update()
    {
        base.Update();

        if (canInteract)
        {
            if(ExtensionMethods.CheckForTurningLeft())
            {
                direction = 0;
                RotateLightBoxStand();
            }
            if(ExtensionMethods.CheckForTurningRight())
            {
                direction = 1;
                RotateLightBoxStand();
            }
        }
            
    }

    public void RotateLightBoxStand()
    {
        lightBoxStand.InitiateLerp(direction);
        if(direction == 0)
            ExtensionMethods.PlayAnimation(wheelAnimation, "isTurningClockwise");
        if (direction == 1)
            ExtensionMethods.PlayAnimation(wheelAnimation, "isTurningAntiClockwise");

        StartCoroutine(ExtensionMethods.SetObjectToIdle(wheelAnimation, 1.0f));
    }


}
