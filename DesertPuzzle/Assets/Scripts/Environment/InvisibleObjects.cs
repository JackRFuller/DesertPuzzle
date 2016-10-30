using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleObjects : MonoBehaviour
{
    private Material invisibleMaterial;

    //Lists
    public List<MeshRenderer> meshes = new List<MeshRenderer>();
    public List<Collider> colliders = new List<Collider>();
    public List<Component> components = new List<Component>();
    private List<Material> originalMaterials = new List<Material>();

    [SerializeField]
    private ObjectState objectState;
    private enum ObjectState
    {
        invisible,
        visible,
    }

    void Start()
    {
        GetKeyAttributes();
        GetAllComponents();        
        TurnObjectsInvisible();
    }

    void SetStartingState()
    {
        switch (objectState)
        {
            case ObjectState.invisible:
                TurnObjectsInvisible();
                break;
            case ObjectState.visible:
                TurnObjectsVisible();
                break;
        }
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
                TurnObjectsVisible();
                break;
            case ObjectState.visible:
                TurnObjectsInvisible();
                break;
        }
    }


    void GetKeyAttributes()
    {
        invisibleMaterial = ColorManager.Instance.InvisibleObjectMaterial;
    }

    void GetAllComponents()
    {
        foreach(Transform child in transform)
        {           
            foreach(var component in child.GetComponents<Component>())
            {
                if(component is MeshRenderer)
                {
                    meshes.Add(component as MeshRenderer);
                    MeshRenderer mesh = component as MeshRenderer;
                    originalMaterials.Add(mesh.material);                    
                }
                if(component is Collider)
                {
                    colliders.Add(component as Collider);                   
                }
                if(component is MovingObjects)
                {
                    components.Add(component as Component);                    
                }
            }
        }
    }

    void TurnObjectsInvisible()
    {
        for (int i = 0; i < meshes.Count; i++)
        {
            meshes[i].material = invisibleMaterial;
            meshes[i].receiveShadows = false;
            meshes[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }

        for (int i = 0; i < colliders.Count; i++)
        {
            colliders[i].enabled = false;
        }

        for (int i = 0; i < components.Count; i++)
        {
            (components[i] as MonoBehaviour).enabled = false;
        }

        objectState = ObjectState.invisible;
    }

    void TurnObjectsVisible()
    {
        for (int i = 0; i < meshes.Count; i++)
        {
            meshes[i].material = originalMaterials[i];
            meshes[i].receiveShadows = true;
            meshes[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }

        for (int i = 0; i < colliders.Count; i++)
        {
            colliders[i].enabled = enabled;
        }

        for (int i = 0; i < components.Count; i++)
        {
            (components[i] as MonoBehaviour).enabled = enabled;
        }

        objectState = ObjectState.visible;
    }
}
