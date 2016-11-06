using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    protected Item.ItemType pickupType;

    [SerializeField]
    protected MeshRenderer mesh;  

    protected bool canBePickedUp;   

    public virtual void Update()
    {
        if(canBePickedUp)
        {
            if(mesh.isVisible)
            {
                EventManager.TriggerEvent("PickupInteraction");

                if(ExtensionMethods.CheckForInteraction())
                {
                    switch (pickupType)
                    {
                        case Item.ItemType.EnergyShard:
                            PlayerInventory.AddEnergyShard(this.transform);
                            break;
                        case Item.ItemType.LightBox:
                            PlayerInventory.AddLightBoxes(this.transform);
                            break;
                        case Item.ItemType.LightFilter:
                            PlayerInventory.AddLightFilter(this.transform);
                            break;
                    }

                    EventManager.TriggerEvent("UpdatePlayerInventory");


                    canBePickedUp = false;
                    gameObject.SetActive(false);

                    EventManager.TriggerEvent("DisablePickupInteraction");
                }
            } 
            else
            {
                EventManager.TriggerEvent("DisablePickupInteraction");
            }           
        }
    } 
    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            canBePickedUp = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            canBePickedUp = false;
            EventManager.TriggerEvent("DisablePickupInteraction");
        }
    }

}
