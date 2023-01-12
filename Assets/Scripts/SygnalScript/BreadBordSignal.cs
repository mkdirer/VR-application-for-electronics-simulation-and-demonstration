using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BreadBordSignal : MonoBehaviour
{
    [HideInInspector] GameObject[] breadboardPinInOnColumn;
    [HideInInspector] public List<PlugsSygnal> signalBreadboardPinInOnColumn;
    [HideInInspector] List<PlugsSygnal> sygnalComponents = new List<PlugsSygnal>();
    public int numberOfSnappedWire = 0;
    [HideInInspector] public GameObject activatePins;
    [HideInInspector] public bool elementActivate = false;
    [HideInInspector] public bool activateSignal = false;
    [HideInInspector] public float voltages;
    [HideInInspector] public bool functionCalled = false;

    [SerializeField] public float totalResistance;
    void Start()
    {

        sygnalComponents.AddRange(GetComponentsInChildren<PlugsSygnal>());
        activatePins = transform.Find("ActivatePins").gameObject;
    }
    
    void Update()
    {
        if (sygnalComponents != null)
        {
            if (numberOfSnappedWire == 0 || PowerSupplyDC.TurnOnOffSupplyDC == false)
            {
                functionCalled = false;
                foreach (PlugsSygnal pin in sygnalComponents)
                {
                    pin.Activate = false;
                    activatePins.SetActive(false);
                }
            }
            else
            {
                if (sygnalComponents.Any(signal => signal.Activate == true) || elementActivate)
                {
                    totalResistance = ParallelResistance(sygnalComponents);
                    functionCalled = true;
                    foreach (PlugsSygnal sygnal in sygnalComponents)
                    {
                        sygnal.Activate = true;
                        activatePins.SetActive(true);
                    }

                    activateSignal = true;
                }
                else
                {
                    foreach (PlugsSygnal sygnal in sygnalComponents)
                    {
                        sygnal.Activate = false;
                        activatePins.SetActive(false);
                    }
                    activateSignal = false;
                }
            } 
        }
    }

    public float ParallelResistance(List<PlugsSygnal> sygnalComponents)
    {
        totalResistance = 0;
        foreach (PlugsSygnal sygnal in sygnalComponents)
        {
            if(sygnal.signalElement != null)
            {
                if (sygnal.signalElement.ComponentKind == BreadBoardElement.KindOfElement.WITH_RESISTANCE)
                {
                    if((sygnal.signalElement as Rezistor).maxResistanceValue != 0)
                    {
                        totalResistance += 1f / ((sygnal.signalElement as Rezistor).maxResistanceValue);
                    }
                }
            }

        }
        return totalResistance == 0? 0: (1f / totalResistance);
    }


}
