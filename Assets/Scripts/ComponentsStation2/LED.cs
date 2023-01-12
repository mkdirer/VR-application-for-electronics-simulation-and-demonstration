using UnityEngine;
using TMPro;

public class LED : BreadBoardElement
{
    [SerializeField] private Material materal1;
    [SerializeField] private Material materal2;
    [SerializeField] private Material materal3;
    [SerializeField] private Material materal4;
    Renderer rend;
    [HideInInspector] public float maxResistanceValue = 0;
    [SerializeField] private float elementVoltage;
    public GameObject currentText;
    [HideInInspector] public TextMeshPro numberCurrentText;
    public GameObject voltageText;
    [HideInInspector] public TextMeshPro numberVoltageText;
    public GameObject maxForwardCurrentText;
    [HideInInspector] public TextMeshPro numberMaxForwardCurrent;
    [SerializeField] GameObject bulb;
    [SerializeField] public GameObject warning;
    public bool functionCalled = false;
    public bool functionCalled2 = true;
    public float ElementVoltage { get => elementVoltage; set => elementVoltage = value; }

    void Start()
    {
        ComponentKind = KindOfElement.LED;
        NumberInterval = 1;
    }
    
    void Update()
    {
        rend = bulb.GetComponent<Renderer>();
        rend.enabled = true;
        numberCurrentText = currentText.GetComponent<TextMeshPro>();
        numberVoltageText = voltageText.GetComponent<TextMeshPro>();
        numberMaxForwardCurrent = maxForwardCurrentText.GetComponent<TextMeshPro>();

        numberMaxForwardCurrent.SetText("{0:0} mA", 20);
        numberVoltageText.SetText(VoltageDrop < 10 ? "0{0:3} V" : "{0:3} V", VoltageDrop);
        numberCurrentText.SetText(CurrentFlow < 10 ? "0{0:3} A" : "{0:3} A", CurrentFlow);
        

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
                VoltageDrop = 1.8f;
                CurrentFlow = PowerSupplyDC.FullCurrent;

                if (SnappedZonePinSignal.Activate || OutPutColumn.activateSignal)
                {
                    Debug.Log("Dodaje nazwe: " + this.name);
                    PowerSupplyDC.InstantiateAddList(this.name);
                }
                else
                {
                    PowerSupplyDC.InstantiateRemoveList(this.name);
                }

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

        }
        else
        {
            CurrentFlow = 0.00f;
            VoltageDrop = 0.00f;
            PowerSupplyDC.InstantiateRemoveList(this.name);
            TurnToPassive();
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

    public override void TurnToActive()
    {
        if (CurrentFlow != 0 && CurrentFlow < 0.021f)
        {
            if (CurrentFlow > 0.017f)
            {
                rend.sharedMaterial = materal4;
            }else if(CurrentFlow > 0.012f)
            {
                rend.sharedMaterial = materal3;
            }
            else
            {
                rend.sharedMaterial = materal2;
            }
            
        }
        else
        {
            rend.sharedMaterial = materal1;
            warning.SetActive(true);
        }
    }

    public override void TurnToPassive()
    {
        rend.sharedMaterial = materal1;
    }
}
