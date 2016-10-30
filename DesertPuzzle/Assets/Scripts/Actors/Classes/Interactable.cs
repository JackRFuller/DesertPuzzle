using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    protected MeshRenderer mesh;

    protected bool isInSphereOfInfluence;
    protected bool canInteract;

    public virtual void Update()
    {
        if(isInSphereOfInfluence)
        {
            if(mesh.isVisible)
            {
                EventManager.TriggerEvent("InteractableObject");
                canInteract = true;
            }
            else
            {
                EventManager.TriggerEvent("NonInteractableObject");
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
            EventManager.TriggerEvent("NonInteractableObject");
        }
    }
}
