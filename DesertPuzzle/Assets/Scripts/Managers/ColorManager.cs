using UnityEngine;
using System.Collections;

public class ColorManager : MonoSingleton<ColorManager>
{
    [SerializeField]
    private Color[] colors;
    public Color[] Colors { get { return colors; } }

    [SerializeField]
    private Material invisibleObjectMaterial;
    public Material InvisibleObjectMaterial { get { return invisibleObjectMaterial; } }

    [Header("Wire Colours - Inactive")]
    [SerializeField]
    private Material[] inActiveWireColours;
    public Material[] InActiveWireColors { get { return inActiveWireColours; } }
    [SerializeField]
    private Material[] activeWireColours;
    public Material[] ActiveWireColours { get { return activeWireColours; } }
}
