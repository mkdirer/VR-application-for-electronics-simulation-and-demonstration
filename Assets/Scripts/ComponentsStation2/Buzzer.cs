using UnityEngine;
using TMPro;

public class Buzzer : Rezistor
{
    public AudioSource audioSource;
    public float pitchOff = 0f;
    public float pitchValue = 0.5f;
    public GameObject frequencyText;
    [HideInInspector] public TextMeshPro numberFrequencyText;

    void Start()
    {
        maxResistanceValue = 6000f;
        ComponentKind = KindOfElement.WITH_RESISTANCE;
        NumberInterval = 1;
    }

    void Update()
    {
        numberCurrentText = currentText.GetComponent<TextMeshPro>();
        numberVoltageText = voltageText.GetComponent<TextMeshPro>();
        numberFrequencyText = frequencyText.GetComponent<TextMeshPro>();

        numberFrequencyText.SetText("{0:0} Hz", 2000);
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
                ResistanceValue = SnappedZonePinSignal.transform.parent.gameObject.GetComponent<BreadBordSignal>().totalResistance;
                VoltageDrop = ResistanceValue * PowerSupplyDC.FullCurrent;
                CurrentFlow = VoltageDrop/maxResistanceValue;

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
        audioSource.Play();
        if (CurrentFlow != 0 && CurrentFlow < 0.0030f)
        {
            audioSource.volume = CurrentFlow / 0.0001f;
        }
    }

    public override void TurnToPassive()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
