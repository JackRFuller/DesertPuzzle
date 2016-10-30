using UnityEngine;
using System.Collections;

public class PowerGeneratorBehaviour : Interactable, IActivatable
{
    [SerializeField]
    private PowerSwitch[] powerswitches;

    [SerializeField]
    private Transform energyShardPoint;
    private Transform energyShard;

    [Header("Base Attributes")]
    [SerializeField]
    private ObjectColor.ColorClass color;
    public ObjectColor.ColorClass Color { get { return color; } }
    [SerializeField]
    private MeshRenderer powerBase;
    private Color energyColor;
    private Material energyMaterial;
    public Material EnergyMaterial { get { return energyMaterial;} }

    void Start()
    {
        SetBaseColor();
    }

    void SetBaseColor()
    {
        Color energyColor = ColorManager.Instance.Colors[(int)color];       

        energyMaterial = powerBase.material;

        float emissionRate = 0.2f;
        Color finalColor = energyColor * Mathf.LinearToGammaSpace(emissionRate);

        energyMaterial.color = energyColor;
        
        energyMaterial.SetColor("_EmissionColor", finalColor);
        powerBase.material = energyMaterial;
    }

    public override void Update()
    {
        base.Update();

        if (canInteract)
            if (ExtensionMethods.CheckForInteraction())
                Interact();
    }


    public void Interact()
    {
        if(energyShard == null)
        {
            if (PlayerInventory.EnergyShards.Count > 0)
            {
                energyShard = PlayerInventory.EnergyShards[PlayerInventory.EnergyShards.Count - 1];
                energyShard.gameObject.SetActive(true);
                energyShardPoint.gameObject.SetActive(false);

                energyShard.SendMessage("PlacedInGenerator", energyShardPoint, SendMessageOptions.DontRequireReceiver); 
                PlayerInventory.RemoveItem(Item.ItemType.EnergyShard);
            }
        }
        else
        {            
            energyShard = null;
            energyShardPoint.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Triggered when there is an energy shard in place and it's being hit by a light beam
    /// </summary>
    public void Activate()
    {
        for (int i = 0; i < powerswitches.Length; i++)
        {
            powerswitches[i].Activate();
        }
    }
}
