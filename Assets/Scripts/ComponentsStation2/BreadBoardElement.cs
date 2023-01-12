using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadBoardElement : Elements
{
    public enum KindOfElement
    {
        WITH_RESISTANCE,
        LED,
        PUSHBUTTON
    };

    private KindOfElement componentKind;
    [SerializeField] private int numberInterval = 1;
    [SerializeField] private float voltageDrop;
    [SerializeField] private float currentFlow;
    [HideInInspector] private float resistanceValue = 360.00f;
    [HideInInspector] GameObject columnSnapZoneOutput;
    [HideInInspector] private BreadBordSignal outPutColumn;
    

    public int NumberInterval { get => numberInterval; set => numberInterval = value; }
    public KindOfElement ComponentKind { get => componentKind; set => componentKind = value; }
    public float VoltageDrop { get => voltageDrop; set => voltageDrop = value; }
    public float CurrentFlow { get => currentFlow; set => currentFlow = value; }
    public float ResistanceValue { get => resistanceValue; set => resistanceValue = value; }
    public BreadBordSignal OutPutColumn { get => outPutColumn; set => outPutColumn = value; }
    public GameObject ColumnSnapZoneOutput { get => columnSnapZoneOutput; set => columnSnapZoneOutput = value; }
}
