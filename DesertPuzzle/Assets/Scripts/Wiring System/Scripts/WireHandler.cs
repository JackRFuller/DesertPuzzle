using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireHandler : MonoBehaviour
{
    private MeshRenderer mesh;
    private int colorIndex;
    private Material inActiveColor;
    private Material activeColor;

    public void GetMaterials(int Index)
    {
        mesh = GetComponent<MeshRenderer>();
        colorIndex = Index;
        inActiveColor = ColorManager.Instance.InActiveWireColors[colorIndex];
        activeColor = ColorManager.Instance.ActiveWireColours[colorIndex];

        mesh.material = inActiveColor;
    }
	
    public void SetWireActive()
    {
        mesh.material = activeColor;
    }

    public void SetWireInActive()
    {
        mesh.material = inActiveColor;
    }

}
