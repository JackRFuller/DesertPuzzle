using UnityEngine;
using System.Collections;

public interface IEvent
{
    void OnEnable();
    void OnDisable();
}

public interface IInteractable
{
    void Interact(PlayerInteraction playerInteraction);
}

public interface IActivatable
{
    void Activate(); 
}

