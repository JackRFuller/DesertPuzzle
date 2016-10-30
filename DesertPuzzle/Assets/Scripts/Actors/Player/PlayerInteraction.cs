using UnityEngine;
using System.Collections;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private PlayerInventory playerInventory;

    [Header("Item Pickup")]
    [SerializeField]
    private float pickupRange;

    void Update()
    {
        DetectItems();
    }

    void DetectItems()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, pickupRange))
        { 
        //{
        //    if(hit.collider.tag.Equals("Interactable"))
        //    {
        //        EventManager.TriggerEvent("InteractableObject");

        //        if(ExtensionMethods.CheckForInteraction())
        //            hit.transform.SendMessage("Interact",this, SendMessageOptions.DontRequireReceiver);
        //    }

            //if(!hit.collider.tag.Equals("Pickup") && !hit.collider.tag.Equals("Interactable"))
            //{
            //    EventManager.TriggerEvent("NonInteractableObject");
            //}
        }
        else
        {
            EventManager.TriggerEvent("NonInteractableObject");
        }
    } 
}
