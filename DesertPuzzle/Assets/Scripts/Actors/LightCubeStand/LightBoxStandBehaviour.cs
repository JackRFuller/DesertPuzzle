using UnityEngine;
using System.Collections;

public class LightBoxStandBehaviour : Interactable
{
    [SerializeField]
    private Transform lightBoxHolder;
    private Transform lightBox;

    [Header("Lerping Attributes")]
    [SerializeField]
    private Transform lerpObject;
    [SerializeField]
    private float lerpSpeed;
    [SerializeField]
    private AnimationCurve lerpCurve;
    private Vector3 startPos;
    private Vector3 targetPos;
    private float timeStartedLerp;
    private bool isLerping;

    public void Interact()
    {
        if(lightBox == null)
        {
            if(PlayerInventory.LightBoxes.Count > 0)
            {
                lightBox = PlayerInventory.LightBoxes[PlayerInventory.LightBoxes.Count - 1];
                lightBox.gameObject.SetActive(true);                         

                lightBox.SendMessage("PlaceInStand", lerpObject, SendMessageOptions.DontRequireReceiver);

                PlayerInventory.RemoveItem(Item.ItemType.LightBox);
            }
        }
        else
        {
            lightBox.SendMessage("RemoveFromStand", SendMessageOptions.DontRequireReceiver);
            lightBox.transform.parent = null;
            lightBox = null;           
        }
    }

    public void InitiateLerp()
    {
        if(!isLerping)
        {
            startPos = lerpObject.eulerAngles;
            targetPos = new Vector3(lerpObject.eulerAngles.x,
                                    lerpObject.eulerAngles.y + 90,
                                    lerpObject.eulerAngles.z);

            timeStartedLerp = Time.time;
            isLerping = true;
        }
    }

    public override void Update()
    {
        base.Update();

        if (canInteract)
            if (ExtensionMethods.CheckForInteraction())
                Interact();

        if (isLerping)
            LerpObject();
    }

    void LerpObject()
    {
        float timeSinceStarted = Time.time - timeStartedLerp;
        float percentageComplete = timeSinceStarted / lerpSpeed;

        Vector3 lerpRotation = Vector3.Lerp(startPos, targetPos, lerpCurve.Evaluate(percentageComplete));

        lerpObject.rotation = Quaternion.Euler(lerpRotation);

        if(percentageComplete >= 1.0f)
        {
            isLerping = false;
        }
    }
	
}
