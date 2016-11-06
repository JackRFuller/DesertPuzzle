using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightFilterStand : Interactable
{
    private Color[] colors;

    [SerializeField]
    private Transform placementPoint;
    private LightFilterHandler lightfilterScript;

    [SerializeField]
    private ObjectColor.ColorClass lightFilterColor;

    [Header("UI Elements")]
    [SerializeField]
    private Image currentColor;
    [SerializeField]
    private Image nextColor;
    private int colorIndex = 0;

    [Header("Lerping Attributes")]
    [SerializeField]
    private float speed;
    [SerializeField]
    private AnimationCurve speedCurve;
    private bool isChangingColor;
    private float timeStarted;
   
     
    void Start()
    {
        colorIndex = (int)lightFilterColor;
        colors = ColorManager.Instance.Colors;

        currentColor.color = colors[colorIndex];
        
    }

    public override void Update()
    {
        base.Update();

        if (canInteract)
            if (ExtensionMethods.CheckForInteraction())
                Interact();

        if (isChangingColor)
            ChangeColors();
    }

    void Interact()
    {
        if(lightfilterScript == null)
        {
            //Check Player Inventory
            if(PlayerInventory.LightFilters.Count > 0)
            {
                Transform lightFilter = PlayerInventory.LightFilters[PlayerInventory.LightFilters.Count - 1];

                lightFilter.GetComponent<Rigidbody>().isKinematic = true;
                


                lightFilter.position = placementPoint.position;
                
                lightFilter.parent = this.transform;
                lightFilter.rotation = placementPoint.rotation;

                lightfilterScript = lightFilter.GetComponent<LightFilterHandler>();

                lightfilterScript.ChangeColor(lightFilterColor);

                lightFilter.gameObject.SetActive(true);

                PlayerInventory.RemoveItem(Item.ItemType.LightFilter);
            }
        }
        else
        {
            lightfilterScript = null;
        }
    }

    public void CycleColours(int direction)
    {
        if(direction == 0)
        {
            colorIndex--;
            if (colorIndex < 0)
                colorIndex = colors.Length - 1;
        }
        if(direction == 1)
        {
            colorIndex++;
            if (colorIndex == colors.Length)
                colorIndex = 0;
        }

        nextColor.color = colors[colorIndex];

        timeStarted = Time.time;
        isChangingColor = true;
    }

    void ChangeColors()
    {
        float timeSinceStarted = Time.time - timeStarted;
        float percentageComplete = timeSinceStarted / speed;

        nextColor.fillAmount = Mathf.Lerp(0, 1, speedCurve.Evaluate(percentageComplete));

        if(percentageComplete >= 1.0f)
        {
            currentColor.color = nextColor.color;
            nextColor.fillAmount = 0;

            ChangeFilterColor();            

            if(lightfilterScript)
            {
                lightfilterScript.ChangeColor(lightFilterColor);
            }


            isChangingColor = false;
        }
    }

    void ChangeFilterColor()
    {
        switch(colorIndex)
        {
            case 0:
                lightFilterColor = ObjectColor.ColorClass.white;
                break;
            case 1:
                lightFilterColor = ObjectColor.ColorClass.green;
                break;
            case 2:
                lightFilterColor = ObjectColor.ColorClass.blue;
                break;
            case 3:
                lightFilterColor = ObjectColor.ColorClass.pink;
                break;
            case 4:
                lightFilterColor = ObjectColor.ColorClass.orange;
                break;
        }
    }

   

   
}
