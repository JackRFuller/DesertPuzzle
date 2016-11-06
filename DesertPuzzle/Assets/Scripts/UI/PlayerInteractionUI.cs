using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInteractionUI : MonoBehaviour, IEvent
{
    [SerializeField]
    private GameObject pickUpIcon;
    [SerializeField]
    private GameObject interactionIcon;

    public void OnEnable()
    {
        EventManager.StartListening("PickupInteraction", EnablePickUp);
        EventManager.StartListening("ObjectInteraction", EnableInteraction);
        EventManager.StartListening("DisablePickupInteraction", DisablePickup);
        EventManager.StartListening("DisableInteraction", DisableInteraction);
    }

    public void OnDisable()
    {
        EventManager.StopListening("PickupInteraction", EnablePickUp);
        EventManager.StopListening("ObjectInteraction", EnableInteraction);
        EventManager.StopListening("DisablePickupInteraction", DisablePickup);
        EventManager.StopListening("DisableInteraction", DisableInteraction);
    }

    void Start()
    {
        DisableInteraction();
        DisablePickup();
    }   
    
    void EnablePickUp()
    {
        pickUpIcon.SetActive(true);
    }
    
    void DisablePickup()
    {
        pickUpIcon.SetActive(false);
    } 

    void EnableInteraction()
    {
        interactionIcon.SetActive(true);
    }

    void DisableInteraction()
    {
        interactionIcon.SetActive(false);
    }
}
