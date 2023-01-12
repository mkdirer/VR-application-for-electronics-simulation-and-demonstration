using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSignal : Signal
{
    [SerializeField] private bool snappedInput = false;
    [HideInInspector] public WireSignal newsignalWire;
    public static int numberOfCircuits = 0;
    public bool SnappedInput { get => snappedInput; set => snappedInput = value; }

    void Update()
    {
        if (signalWire)
        {
            signalWire.SnappedInput = this.SnappedInput;
            if (this.SnappedInput)
            {
                this.Snapped = signalWire.SnappedInput && signalWire.SnappedOutput;
                this.Activate = signalWire.Activate;
            }
            else
            {
                this.Snapped = false;
                this.Activate = false;
            }

        }
        else
        {
            this.Snapped = false;
            this.Activate = false;
        }
    }

    public void IncreaseNumberOfCircuit() => numberOfCircuits++;
    public void DecreaseNumberOfCircuit() => numberOfCircuits--;


    public void OnSnapped(Object obj)
    {
        wire = obj as GameObject;
        parent = wire.transform.parent.gameObject;

        if (signalWire)
        {
            newsignalWire = parent.GetComponent<WireSignal>();
            if (signalWire != newsignalWire)
            {
                signalWire = newsignalWire;
            }

            this.SnappedInput = true;
        }
        else
        {
            signalWire = parent.GetComponent<WireSignal>();
            if (signalWire)
            {
                this.SnappedInput = true;
            }
        }
    }

    public void UnSnapped(Object obj)
    {
        this.SnappedInput = false;
        signalWire.SnappedInput = this.SnappedInput;
        signalWire = null;
    }

}
