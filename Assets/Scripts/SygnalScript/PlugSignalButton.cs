using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugSignalButton : Signal
{
    [SerializeField] private bool snappedInput = false;
    [SerializeField] private bool snappedOutput = false;

    public bool SnappedInput { get => snappedInput; set => snappedInput = value; }
    public bool SnappedOutput { get => snappedOutput; set => snappedOutput = value; }
    
    void Update()
    {
        if (signalWire)
        {
            if (signalWire.SnappedOutput)
            {
                this.Snapped = signalWire.SnappedOutput && signalWire.SnappedInput;
                signalWire.Snapped = this.Snapped;
                signalWire.Activate = this.Activate;
            }
            else if (signalWire.SnappedInput)
            {
                this.Snapped = signalWire.SnappedInput;
                this.Activate = signalWire.Activate;
            }
            else
            {
                this.Snapped = false;
                this.Activate = false;
            }
        }
    }

    public void OnSnapped(Object obj)
    {
        if (signalWire)
        {
            if(signalWire.SnappedInput)
            {
                this.Snapped = true;
                this.SnappedOutput = true;
            }else if(signalWire.SnappedOutput)
            {
                this.Snapped = true;
                this.SnappedInput = true;
            }
        }
        else
        {
            wire = obj as GameObject;
            parent = wire.transform.parent.gameObject;
            signalWire = parent.GetComponent<WireSignal>();
            if (signalWire)
            {
                if (signalWire.SnappedInput)
                {
                    signalWire.SnappedOutput = true;
                }
                else if (signalWire.SnappedOutput)
                {
                    signalWire.SnappedInput = true;
                }
            }
        }
    }

    public void UnSnapped(Object obj)
    {
        signalWire.SnappedInput = false;
        signalWire.SnappedOutput = false;
    }


}

