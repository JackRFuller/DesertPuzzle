using UnityEngine;
using System.Collections;

public class InvisibleObject : MonoBehaviour, IActivatable {

    private MeshRenderer meshRenderer;
    private Material originalMaterial;
    private Material invisibleMaterial;
    private Collider objectCollider;

    [SerializeField]
    private ObjectState objectState;
    private enum ObjectState
    {
        invisible,
        visible,
    }

    void Start()
    {
        GetMainComponents();
        SetStartingState();
    }

    void SetStartingState()
    {
        switch (objectState)
        {
            case ObjectState.invisible:
                TurnObjectInvisible();
                break;
            case ObjectState.visible:
               TurnObjectVisible();
                break;
        }
    }


    void GetMainComponents()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material;
        objectCollider = GetComponent<Collider>();

        invisibleMaterial = ColorManager.Instance.InvisibleObjectMaterial;
    }

    public void Activate()
    {
        SetObjectState();
    }

    void SetObjectState()
    {
        switch (objectState)
        {
            case ObjectState.invisible:
                TurnObjectVisible();
                break;
            case ObjectState.visible:
                TurnObjectInvisible();
                break;
        }
    }


    void TurnObjectInvisible()
    {
        meshRenderer.material = invisibleMaterial;
        objectCollider.enabled = false;
        meshRenderer.receiveShadows = false;
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        objectState = ObjectState.invisible;
    }

    void TurnObjectVisible()
    {
        meshRenderer.material = originalMaterial;
        objectCollider.enabled = true;
        meshRenderer.receiveShadows = true;
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

        objectState = ObjectState.visible;
    }
}
