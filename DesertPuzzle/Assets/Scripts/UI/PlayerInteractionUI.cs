using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInteractionUI : MonoBehaviour, IEvent
{
    [SerializeField]
    private Text interationIcon;

    public void OnEnable()
    {
        EventManager.StartListening("InteractableObject", EnableInteractionIcon);
        EventManager.StartListening("NonInteractableObject", DisableInteractionIcon);
    }

    public void OnDisable()
    {
        EventManager.StopListening("InteractableObject", EnableInteractionIcon);
        EventManager.StopListening("NonInteractableObject", DisableInteractionIcon);
    }

    void Start()
    {
        DisableInteractionIcon();
    }    

    void EnableInteractionIcon()
    {
        interationIcon.enabled = true;
    }

    void DisableInteractionIcon()
    {
        interationIcon.enabled = false;
    }
	
}
