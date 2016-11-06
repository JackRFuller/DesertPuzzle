using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    protected InteractionType interactionType;

    [SerializeField]
    protected MeshRenderer mesh;

    protected bool isInSphereOfInfluence;
    protected bool canInteract;

    protected enum InteractionType
    {
        oneFunction,
        multipleFunctions,
    }

    public virtual void Update()
    {
        if(isInSphereOfInfluence)
        {
            if(mesh.isVisible)
            {
                if(interactionType == InteractionType.multipleFunctions)
                {
                    EventManager.TriggerEvent("ObjectInteraction");
                }
                else
                {
                    EventManager.TriggerEvent("PickupInteraction");
                }
                
                canInteract = true;
            }
            else
            {
                if(interactionType == InteractionType.multipleFunctions)
                {
                    EventManager.TriggerEvent("DisableInteraction");
                }
                else
                {
                    EventManager.TriggerEvent("DisablePickupInteraction");
                }
                
            }
        }
    }
    
	void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            isInSphereOfInfluence = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            isInSphereOfInfluence = false;
            canInteract = false;
            if (interactionType == InteractionType.multipleFunctions)
            {
                EventManager.TriggerEvent("DisableInteraction");
            }
            else
            {
                EventManager.TriggerEvent("DisablePickupInteraction");
            }
        }
    }
}
