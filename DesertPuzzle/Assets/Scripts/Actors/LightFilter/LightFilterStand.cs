using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFilterStand : Interactable
{
    [SerializeField]
    private Transform placementPoint;
    private LightFilterHandler lightfilterScript;

    [SerializeField]
    private ObjectColor.ColorClass lightFilterColor;

    public override void Update()
    {
        base.Update();

        if (canInteract)
            if (ExtensionMethods.CheckForInteraction())
                Interact();
    }

    void Interact()
    {
        if(lightfilterScript == null)
        {
            //Check Player Inventory
            if(PlayerInventory.LightFilters.Count > 0)
            {
                Transform lightFilter = PlayerInventory.LightFilters[PlayerInventory.LightFilters.Count - 1];
                lightFilter.position = placementPoint.position;
                lightFilter.parent = this.transform;

                lightfilterScript = lightFilter.GetComponent<LightFilterHandler>();

                lightfilterScript.ChangeColor(lightFilterColor);

                lightFilter.gameObject.SetActive(true);

                PlayerInventory.RemoveItem(Item.ItemType.LightFilter);
            }
        }
        else
        {
            lightfilterScript = null;
        }
    }
}
