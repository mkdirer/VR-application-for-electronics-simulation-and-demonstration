using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Tilia.Interactions.Controllables.AngularDriver;
using System.Linq;

public class PowerSupplyDC : MonoBehaviour
{
    public static PowerSupplyDC Instance;

    [SerializeField] private Material materal1;
    [SerializeField] private Material materal2;
    Renderer rend;
    private static bool turnOnOffSupplyDC;
    private static float fullCircuitVoltage;
    private static float fullCurrent;
    [SerializeField] public static float totalResistance;
    public int numberOfLeds = 0;
    GameObject textToHide;
    public GameObject CurrentRotatinoWheel;
    [HideInInspector] public AngularDriveFacade getRotateCurrentValue;
    public GameObject currentText;
    [HideInInspector] public TextMeshPro numberCurrentText;
    public GameObject CoarseRotatinoWheel;
    [HideInInspector] public AngularDriveFacade getRotateCoarseValue;
    public GameObject FineRotatinoWheel;
    [HideInInspector] public AngularDriveFacade getRotateFineValue;
    public GameObject voltageText;
    [HideInInspector] public TextMeshPro numberVoltageText;
    public GameObject supplyPinPlus1;
    [HideInInspector] public OutputSignal sygnalOutput;
    public GameObject supplyPinMinus1;
    [HideInInspector] public InputSignal sygnalInput;
    public GameObject supplyPinPlus2;
    [HideInInspector] public OutputSignal sygnalOutput2;
    [SerializeField] public GameObject warning;
    [HideInInspector] GameObject[] BreadboardInputs;
    [HideInInspector] public List<InputSignal> SignalBreadboardInputs;


    [HideInInspector] public float current;
    [HideInInspector] public float voltage;
    [HideInInspector] public bool snapped;
    [HideInInspector] private static List<string> circiutEquipmentBlocks = new List<string>();

    [HideInInspector] GameObject[] BreadboardOutputs;
    [HideInInspector] public List<OutputSignal> SignalBreadboardOutputs;

    public static List<string> CirciutEquipmentBlocks { get => circiutEquipmentBlocks; set => circiutEquipmentBlocks = value; }
    public static bool TurnOnOffSupplyDC { get => turnOnOffSupplyDC; set => turnOnOffSupplyDC = value; }
    public static float FullCurrent { get => fullCurrent; set => fullCurrent = value; }

    void Start()
    {
        BreadboardOutputs = GameObject.FindGameObjectsWithTag("RightBreadboardPlus");
        BreadboardInputs = GameObject.FindGameObjectsWithTag("RightBreadBoardMinus");
        
        foreach (GameObject outputs in BreadboardOutputs)
        {
            OutputSignal outputSignl = outputs.GetComponent<OutputSignal>();
            if (outputSignl)
            {
                SignalBreadboardOutputs.Add(outputSignl);
            }

        }

    }

    void Update()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        numberCurrentText = currentText.GetComponent<TextMeshPro>();
        numberVoltageText = voltageText.GetComponent<TextMeshPro>();
        sygnalOutput = supplyPinPlus1.GetComponent<OutputSignal>();
        sygnalOutput2 = supplyPinPlus2.GetComponent<OutputSignal>();
        sygnalInput = supplyPinMinus1.GetComponent<InputSignal>();

        if (!TurnOnOffSupplyDC)
        {
            rend.sharedMaterial = materal1;
            numberCurrentText.SetText("0.00");
            numberVoltageText.SetText("00.00");

            foreach (OutputSignal outputs in SignalBreadboardOutputs)
            {
                outputs.Activate = false;
            }
            sygnalOutput.Activate = false;


            if (CirciutEquipmentBlocks != null)
            {
                foreach (string name in CirciutEquipmentBlocks)
                {
                    GameObject.Find(name).GetComponent<Elements>().TurnToPassive();
                }
            }
            

        }
        else
        {
            rend.sharedMaterial = materal2;
            getRotateCurrentValue = CurrentRotatinoWheel.GetComponent<AngularDriveFacade>();
            getRotateCoarseValue = CoarseRotatinoWheel.GetComponent<AngularDriveFacade>();
            getRotateFineValue = FineRotatinoWheel.GetComponent<AngularDriveFacade>();
            current = getRotateCurrentValue.TargetValue;
            voltage = getRotateCoarseValue.TargetValue * 25 + getRotateFineValue.TargetValue;

            if (getRotateCurrentValue != null)
            {
                numberCurrentText.SetText("{0:2}", getRotateCurrentValue.TargetValue);
                numberVoltageText.SetText(voltage < 10 ? "0{0:2}" : "{0:2}", voltage);
            }

            foreach (OutputSignal outputs in SignalBreadboardOutputs)
            {
                outputs.Activate = true;
            }
            sygnalOutput.Activate = true;
            if (voltage < 12 && voltage > 5 && current > 0 && BreadboardInputs.Any(signal => signal.GetComponent<InputSignal>().Activate == true))
            {
                if (CirciutEquipmentBlocks != null)
                {
                    if (CirciutEquipmentBlocks.Count >= InputSignal.numberOfCircuits)
                    {
                        SetParameterForSignal(CirciutEquipmentBlocks);
                        foreach (string name in CirciutEquipmentBlocks)
                        {
                            Elements connectedObject = GameObject.Find(name).GetComponent<Elements>();
                            if (connectedObject)
                            {
                                if (connectedObject.CircuitConditions)
                                {
                                    connectedObject.TurnToActive();
                                }
                            }
                        }

                    }
                }

            }
        }
    }
    
    public static void InstantiateAddList(string name)
    {
        if (!CirciutEquipmentBlocks.Contains(name))
        {
            CirciutEquipmentBlocks.Add(name);
        }
    }

    public static void InstantiateRemoveList(string name)
    {
        if(CirciutEquipmentBlocks != null)
        {
            if (CirciutEquipmentBlocks.Contains(name))
            {
                CirciutEquipmentBlocks.Remove(name);
            }
        }
    }
    
    public void SetParameterForSignal(List<string> circuitComponents)
    {
        List<string> myList = new List<string>();
        totalResistance = 0.00f;
        fullCircuitVoltage = voltage;
        FullCurrent = 0f;

        foreach (string name in circuitComponents)
        {
            BreadBoardElement connectedObject = GameObject.Find(name).GetComponent<BreadBoardElement>();
            if (connectedObject)
            {
                if (connectedObject.CircuitConditions)
                {

                    if (connectedObject != null)
                    {
                        Debug.Log("nazwa:" + name);
                        if (connectedObject.ComponentKind == BreadBoardElement.KindOfElement.LED)
                        {
                            fullCircuitVoltage -= 1.8f;
                        }
                        
                        if (connectedObject.ComponentKind == BreadBoardElement.KindOfElement.WITH_RESISTANCE)
                        {
                            if(myList.Count(signal => signal == connectedObject.ColumnSnapZoneOutput.name) == 0) 
                            {
                                if (circuitComponents.Count(signal => GameObject.Find(signal).GetComponent<BreadBoardElement>().ColumnSnapZoneOutput.name == connectedObject.ColumnSnapZoneOutput.name) > 1)
                                {
                                    myList.Add(connectedObject.ColumnSnapZoneOutput.name);
                                    totalResistance += connectedObject.ResistanceValue;
                                }
                                else
                                {
                                    totalResistance += connectedObject.ResistanceValue;
                                }
                            }
                        }

                        if(totalResistance != 0)
                        {
                            FullCurrent = fullCircuitVoltage / (totalResistance * 1.0f);
                            warning.SetActive(false);
                        }
                        else
                        {
                            warning.SetActive(true);
                        }
                    }
                }
            }
        }
    }
    

    public void ChangeMaterialWithButton()
    {
        TurnOnOffSupplyDC = !TurnOnOffSupplyDC;
    }

}
