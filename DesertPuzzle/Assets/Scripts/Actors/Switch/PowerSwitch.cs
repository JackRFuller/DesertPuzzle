using UnityEngine;
using System.Collections;

public class PowerSwitch : MonoBehaviour, IActivatable
{
    [SerializeField]
    private InvisibleObjects[] invisibleObjects;

    [SerializeField]
    private SwitchState switchState = SwitchState.Off;
    private enum SwitchState
    {
        On,
        Off,
    }

    [Header("Object Attributes")]
    [SerializeField]
    private MeshRenderer onLight;
    [SerializeField]
    private MeshRenderer offLight;
    [SerializeField]
    private Material onMaterial;
    [SerializeField]
    private Material offMaterial;
    [SerializeField]
    private Material inActiveMaterial;

    void Start()
    {
        SetInitialState();
    }

    void SetInitialState()
    {
        switch (switchState)
        {
            case SwitchState.On:
                TurnSwitchOn();
                break;
            case SwitchState.Off:
                TurnSwitchOff();
                break;
        }
    }

    public void Activate()
    {
        SetSwitchState();

        for (int i = 0; i < invisibleObjects.Length; i++)
        {
            invisibleObjects[i].Activate();
        }
    }

    void SetSwitchState()
    {

        switch(switchState)
        {
            case SwitchState.Off:
                TurnSwitchOn();
                break;
            case SwitchState.On:
                TurnSwitchOff();
                break;
        }
    }

    void TurnSwitchOn()
    {
        onLight.material = onMaterial;
        offLight.material = inActiveMaterial;

        switchState = SwitchState.On;
    }

    void TurnSwitchOff()
    {
        onLight.material = inActiveMaterial;
        offLight.material = offMaterial;

        switchState = SwitchState.Off;
    }
    
	




}
