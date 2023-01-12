using UnityEngine;

public class OutputSignal : Signal
{
    [SerializeField] private bool snappedOutput = false;
    [HideInInspector] public WireSignal newsignalWire;

    public bool SnappedOutput { get => snappedOutput; set => snappedOutput = value; }

    void Update()
    {
        if (signalWire)
        {
            signalWire.SnappedOutput = this.SnappedOutput;
            this.Snapped = signalWire.SnappedOutput && signalWire.SnappedInput;
            if(this.SnappedOutput)
            {
                signalWire.Snapped = this.Snapped;
                signalWire.Activate = this.Activate;
            }
        }
    }

    public void OnSnapped(Object obj)
    {
        wire = obj as GameObject;
        parent = wire.transform.parent.gameObject;
        if (signalWire)
        {
            newsignalWire = parent.GetComponent<WireSignal>();
            if(signalWire != newsignalWire)
            {
                signalWire = newsignalWire;
            }

            this.SnappedOutput = true;
        }
        else
        {
            signalWire = parent.GetComponent<WireSignal>();
            if (signalWire)
            {
                this.SnappedOutput = true;
            }
        }
    }

    
    public void UnSnapped(Object obj)
    {
        this.SnappedOutput = false;
        signalWire.SnappedOutput = this.SnappedOutput;
        signalWire = null;
    }
}
