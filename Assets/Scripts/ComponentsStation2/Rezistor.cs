using UnityEngine;
using TMPro;

public class Rezistor : BreadBoardElement
{
    [HideInInspector] public float  maxResistanceValue = 360.00f;
    public GameObject currentText;
    [HideInInspector] public TextMeshPro numberCurrentText;
    public GameObject voltageText;
    [HideInInspector] public TextMeshPro numberVoltageText;
    public GameObject resistanceText;
    [HideInInspector] public TextMeshPro numberResistanceText;
    public GameObject powerText;
    [HideInInspector] public TextMeshPro numberPowerText;
    [SerializeField] public GameObject warning;
    public bool functionCalled = false;
    public bool functionCalled2 = true;

    void Start()
    {
        ComponentKind = KindOfElement.WITH_RESISTANCE;
        NumberInterval = 3;
        ResistanceValue = 360.00f;
    }

    void Update()
    {
        numberCurrentText = currentText.GetComponent<TextMeshPro>();
        numberVoltageText = voltageText.GetComponent<TextMeshPro>();
        numberResistanceText = resistanceText.GetComponent<TextMeshPro>();
        numberPowerText = powerText.GetComponent<TextMeshPro>();

        numberResistanceText.SetText(ResistanceValue < 10 ? "0{0:1} Ohm" : "{0:1} Ohm", ResistanceValue);
        numberVoltageText.SetText(VoltageDrop < 10 ? "0{0:3} V" : "{0:3} V", VoltageDrop);
        numberCurrentText.SetText(CurrentFlow < 10 ? "0{0:3} A" : "{0:3} A", CurrentFlow);
        numberPowerText.SetText(VoltageDrop * CurrentFlow < 10 ? "0{0:3} W" : "{0:3} W", VoltageDrop * CurrentFlow);

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
                ResistanceValue = SnappedZonePinSignal.transform.parent.gameObject.GetComponent<BreadBordSignal>().totalResistance;
                VoltageDrop = ResistanceValue * PowerSupplyDC.FullCurrent;
                CurrentFlow = VoltageDrop / maxResistanceValue;
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
            ResistanceValue = 0.00f;
            CurrentFlow = 0.00f;
            VoltageDrop = 0.00f;
            if(PowerSupplyDC.TurnOnOffSupplyDC)
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
        if(obj)
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
}
