using UnityEngine;


public class PlugsSygnal : Signal
{
    [HideInInspector] public WireSignal newsignalWire;
    [HideInInspector] public BreadBoardElement newSignalElement;
    [HideInInspector] public BreadBoardElement signalElement;
    public static int numberOfSnaps = 0;
    public bool snappedOutputChange = false;
    public bool snappedInputChange = false;
    [HideInInspector] public GameObject pinParent;
    [HideInInspector] public BreadBordSignal column;
    void Start()
    {

        pinParent = transform.parent.gameObject;
        column = pinParent.GetComponent<BreadBordSignal>();
    }
    
    void Update()
    {
        if (signalWire)
        {
            //from cable
            if (this.Snapped && signalWire.SnappedOutput && !snappedInputChange)
            {
                this.Activate = true;
                signalWire.Snapped = true;
                signalWire.SnappedInput = true;
                snappedOutputChange = true;
            }
            //to cable
            else if (this.Snapped && signalWire.SnappedInput && !snappedOutputChange)
            {
                signalWire.Snapped = this.Snapped;
                signalWire.Snapped = true;
                signalWire.Activate = this.Activate;
                signalWire.SnappedOutput = true;
                snappedInputChange = true;
            }
            // nie ustawiony kierunek
            else if (this.Snapped && !signalWire.SnappedInput && !signalWire.SnappedOutput && signalWire.NumbreOfSnaps == 2 && this.Activate)
            {
                signalWire.Snapped = true;
                signalWire.Activate = this.Activate;
                signalWire.SnappedOutput = true;
            }
        }
    }
    
    public void OnSnapped(Object obj)
    {
        if (PowerSupplyDC.TurnOnOffSupplyDC == true)
        {
            PowerSupplyDC.TurnOnOffSupplyDC = false;
        }
        wire = obj as GameObject;
        if (wire.tag == "WireTag")
        {
            parent = wire.transform.parent.gameObject;
            if (signalWire)
            {
                newsignalWire = parent.GetComponent<WireSignal>();
                if (signalWire != newsignalWire)
                {
                    signalWire = newsignalWire;
                }
                column.numberOfSnappedWire++;
                signalWire.NumbreOfSnaps++;
                this.Snapped = true;
            }
            else
            {
                signalWire = parent.GetComponent<WireSignal>();
                if (signalWire)
                {
                    column.numberOfSnappedWire++;
                    signalWire.NumbreOfSnaps++;
                    this.Snapped = true;
                }
            }
        }
        else
        {
            if (signalElement)
            {
                newSignalElement = wire.GetComponent<BreadBoardElement>();
                if (signalElement != newSignalElement)
                {
                    signalElement = newSignalElement;
                }
                signalElement.SnappedZonePinSignal = this;
                column.numberOfSnappedWire++;
                this.Snapped = true;
            }
            else
            {
                signalElement = wire.GetComponent<BreadBoardElement>();
                signalElement.SnappedZonePinSignal = this;
                if (signalElement)
                {
                    column.numberOfSnappedWire++;
                    this.Snapped = true;
                }
            }
        }
    }


    public void UnSnapped(Object obj)
    {
        if (PowerSupplyDC.TurnOnOffSupplyDC == true)
        {
            PowerSupplyDC.TurnOnOffSupplyDC = false;
        }

        if(signalWire)
        {
            if (snappedOutputChange)
                signalWire.SnappedInput = false;
            if (snappedInputChange)
                signalWire.SnappedOutput = false;
            signalWire.Snapped = false;
            signalWire.Activate = false;
            signalWire.NumbreOfSnaps--;
            signalWire.SnappedOutput = false;
        }

        if (signalElement)
        {
            signalElement.SnappedZonePinSignal = null;
        }
        
        column.numberOfSnappedWire--;
        this.Snapped = false;
        signalWire = null;
        signalElement = null;
        snappedOutputChange = false;
        snappedInputChange = false;
    }


}
