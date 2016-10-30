using UnityEngine;
using System.Collections;

public class LightFilterHandler : Pickup
{
    [SerializeField]
    private EnergyBeamHandler energyBeam;
    [SerializeField]
    private Transform rayOrigin;
    [SerializeField]
    private Transform obj;
    private float zOffset;


    void Start()
    {
        zOffset = obj.localScale.z;
    }

	public void GetRayHitPoint(Vector3 hitPoint, Vector3 rayDirection, int numberOfReflections)
    {
        rayOrigin.position = new Vector3(hitPoint.x, hitPoint.y, hitPoint.z + zOffset);

        energyBeam.SetBeamAttributes(rayDirection, numberOfReflections);
        energyBeam.SendOutBeam();
    }

    public void DeactivateLightFilter()
    {
        energyBeam.DeActivateBeam();        
    }

    public void ChangeColor(ObjectColor.ColorClass objColor)
    {
        Color newColor = ColorManager.Instance.Colors[(int)objColor];
        newColor.a = 0.62f;
        Material newMaterial = mesh.material;
        newMaterial.color = newColor;
        mesh.material = newMaterial;

        Debug.Log(mesh.material.color.a);

        

        energyBeam.SetBeamColor(objColor);
    }
}
