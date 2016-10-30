using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    private static List<Transform> energyShards = new List<Transform>();
    public static List<Transform> EnergyShards { get { return energyShards; } }

    private static List<Transform> lightBoxes = new List<Transform>();
    public static List<Transform> LightBoxes { get { return lightBoxes; } }

    private static List<Transform> lightFilters = new List<Transform>();
    public static List<Transform> LightFilters { get { return lightFilters; } }
    
    public static void AddEnergyShard(Transform energyShard)
    {
        energyShards.Add(energyShard);
    }

    public static void AddLightBoxes(Transform lightBox)
    {
        lightBoxes.Add(lightBox);
    }

    public static void AddLightFilter(Transform lightFilter)
    {
        lightFilters.Add(lightFilter);
    }

    public static void RemoveItem(Item.ItemType itemType)
    {
        switch(itemType)
        {
            case Item.ItemType.EnergyShard:
                energyShards.RemoveAt(energyShards.Count - 1);
                break;
            case Item.ItemType.LightBox:
                lightBoxes.RemoveAt(lightBoxes.Count - 1);
                break;
            case Item.ItemType.LightFilter:
                lightFilters.RemoveAt(lightFilters.Count - 1);
                break;
        }

        EventManager.TriggerEvent("UpdatePlayerInventory");
    }
        
}
