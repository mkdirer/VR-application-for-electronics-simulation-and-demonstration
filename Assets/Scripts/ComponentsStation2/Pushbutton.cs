using UnityEngine;

public class Pushbutton : BreadBoardElement
{
    [SerializeField] public GameObject warning;
    public bool functionCalled = false;
    public bool functionCalled2 = true;
    public bool buttonActivate = false;

    void Start()
    {
        ComponentKind = KindOfElement.PUSHBUTTON;
        NumberInterval = 1;
    }
    
    void Update()
    {
        if (SnappedZonePinSignal)
        {
            CircuitConditions = SnappedZonePinSignal.Activate;
            int outputNumber = int.Parse(SnappedZonePinSignal.tag) + NumberInterval;
            ColumnSnapZoneOutput = GameObject.Find(string.Format("Column{0}", outputNumber));

            if (ColumnSnapZoneOutput)
            {
                OutPutColumn = ColumnSnapZoneOutput.GetComponent<BreadBordSignal>();
                this.OutPutColumn = OutPutColumn;
                if (!functionCalled)
                {
                    ChangeNumberOfElements(true);
                    functionCalled2 = false;
                    functionCalled = true;
                }

                warning.SetActive(false);

                if (buttonActivate)
                {

                    if (SnappedZonePinSignal.Activate && !OutPutColumn.activateSignal && PowerSupplyDC.TurnOnOffSupplyDC)
                    {
                        CircuitConditions = true;
                        OutPutColumn.elementActivate = true;
                    }
                    else if (!SnappedZonePinSignal.Activate && OutPutColumn.activateSignal && PowerSupplyDC.TurnOnOffSupplyDC)
                    {
                        CircuitConditions = true;
                        SnappedZonePinSignal.Activate = true;
                        OutPutColumn.elementActivate = false;
                    }
                    else
                    {
                        OutPutColumn.elementActivate = false;
                        SnappedZonePinSignal.Activate = false;
                        OutPutColumn.activateSignal = false;
                    }
                }
                else
                {
                    OutPutColumn.elementActivate = false;
                    SnappedZonePinSignal.Activate = false;
                    OutPutColumn.activateSignal = false;
                }
            }

        }
        else
        {
            CurrentFlow = 0.00f;
            VoltageDrop = 0.00f;
            if (PowerSupplyDC.TurnOnOffSupplyDC)
            {
                warning.SetActive(true);
            }
            if (!functionCalled2)
            {
                functionCalled2 = true;
                ChangeNumberOfElements(false);
                functionCalled = false;
            }
            if (OutPutColumn)
            {
                OutPutColumn.elementActivate = false;
            }
            if (SnappedZonePinSignal)
            {
                SnappedZonePinSignal.Activate = false;
            }
            this.OutPutColumn = null;
            SnappedZonePinSignal = null;
        }
    }

    private void ChangeNumberOfElements(bool obj)
    {
        if (obj)
        {
            this.OutPutColumn.numberOfSnappedWire++;
        }
        else
        {
            if (this.OutPutColumn)
            {
                this.OutPutColumn.numberOfSnappedWire--;
            }
        }
    }

    public void ActivatePushbutton(bool obj)
    {
        if (obj)
        {
            buttonActivate = true;
        }
        else
        {
            buttonActivate = false;
        }
    }
}
