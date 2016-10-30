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
}
