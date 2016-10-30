using UnityEngine;
using System.Collections;

public class LightBoxStandWheelHandler : Interactable
{
    [SerializeField]
    private LightBoxStandBehaviour lightBoxStand;

    public override void Update()
    {
        base.Update();

        if (canInteract)
            if (ExtensionMethods.CheckForInteraction())
                Interact();
    }

    public void Interact()
    {
        lightBoxStand.InitiateLerp();
    }
}
