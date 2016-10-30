using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInventoryUI : MonoBehaviour, IEvent
{
    [SerializeField]
    private Text lightBoxCountText;
    [SerializeField]
    private Text energyShardCountText;
    [SerializeField]
    private Text lightFilterCountText;

    public void OnEnable()
    {
        EventManager.StartListening("UpdatePlayerInventory", UpdateInPlayerInventory);
    }

    public void OnDisable()
    {
        EventManager.StopListening("UpdatePlayerInventory", UpdateInPlayerInventory);
    }

    void UpdateInPlayerInventory()
    {
        lightBoxCountText.text = PlayerInventory.LightBoxes.Count.ToString();
        energyShardCountText.text = PlayerInventory.EnergyShards.Count.ToString();
        lightFilterCountText.text = PlayerInventory.LightFilters.Count.ToString();
    }
}
