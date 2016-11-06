using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PowerGeneratorBehaviour : MonoBehaviour
{
    [Header("Coloured Switches")]
    [SerializeField]
    private List<PowerSwitch> whiteSwitches = new List<PowerSwitch>();
    [SerializeField]
    private List<PowerSwitch> greenSwitches = new List<PowerSwitch>();
    [SerializeField]
    private List<PowerSwitch> blueSwitches = new List<PowerSwitch>();
    [SerializeField]
    private List<PowerSwitch> pinkSwitches = new List<PowerSwitch>();
    [SerializeField]
    private List<PowerSwitch> orangeSwitches = new List<PowerSwitch>();

    private List<PowerSwitch> activatedSwitches = new List<PowerSwitch>();

    [Header("Coloured Wires")]
    [SerializeField]
    private List<WireHandler> whiteWires = new List<WireHandler>();
    [SerializeField]
    private List<WireHandler> greenWires = new List<WireHandler>();
    [SerializeField]
    private List<WireHandler> blueWires = new List<WireHandler>();
    [SerializeField]
    private List<WireHandler> pinkWires = new List<WireHandler>();
    [SerializeField]
    private List<WireHandler> orangeWires = new List<WireHandler>();

    private List<WireHandler> activatedWires = new List<WireHandler>();

    private List<PowerSwitch> activeSwitches = new List<PowerSwitch>();
    private List<WireHandler> activeWires = new List<WireHandler>();
    
    void Start()
    {
        //SetBaseColor();
        InitiateWires();
    }

    void InitiateWires()
    {
        for(int i = 0; i < whiteWires.Count; i++)
        {
            whiteWires[i].GetMaterials(0);
        }
        for (int i = 0; i < greenWires.Count; i++)
        {
            greenWires[i].GetMaterials(1);
        }
        for (int i = 0; i < blueWires.Count; i++)
        {
            blueWires[i].GetMaterials(2);
        }
        for (int i = 0; i < pinkWires.Count; i++)
        {
            pinkWires[i].GetMaterials(3);
        }
        for (int i = 0; i < orangeWires.Count; i++)
        {
            orangeWires[i].GetMaterials(4);
        }
    }

    public void ActivateSwitches(int switchIndex)
    {
        if (activeSwitches.Count > 0)
            DisableSwitches();

        switch(switchIndex)
        {
            case 0:
                activatedSwitches = whiteSwitches;
                activatedWires = whiteWires;
                break;
            case 1:
                activatedSwitches = greenSwitches;
                activatedWires = greenWires;
                break;
            case 2:
                activatedSwitches = blueSwitches;
                activatedWires = blueWires;
                break;
            case 3:
                activatedSwitches = pinkSwitches;
                activatedWires = pinkWires;
                break;
            case 4:
                activatedSwitches = orangeSwitches;
                activatedWires = orangeWires;
                break;
        }

        for(int i = 0; i < activatedSwitches.Count; i++)
        {
            activeSwitches.Add(activatedSwitches[i]);
            activeWires.Add(activatedWires[i]);
        }

        for(int i = 0; i < activatedSwitches.Count; i++)
        {
            activeSwitches[i].Activate();
            activeWires[i].SetWireActive();
        }
    }

    public void DisableSwitches()
    {
        if (activeSwitches.Count == 0)
            return;

        for (int i = 0; i < activatedSwitches.Count; i++)
        {
            activeSwitches[i].Activate();
            activeWires[i].SetWireInActive();
        }

        activeSwitches.Clear();
        activeWires.Clear();
    }   
}
