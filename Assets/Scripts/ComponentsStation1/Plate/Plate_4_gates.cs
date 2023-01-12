using System;
using UnityEngine;

public class Plate_4_gates : Elements
{
    public enum BooleanOperation
    {
        AND,
        OR,
        XOR,
        NAND,
        NOR,
        XNOR
    };

    [SerializeField] private BooleanOperation booleanTypeOperation = BooleanOperation.AND;
    public GameObject Pin1;
    [HideInInspector] public InputSignal pin1Parameter;
    public GameObject Pin2;
    [HideInInspector] public InputSignal pin2Parameter;
    public GameObject Pin3;
    [HideInInspector] public OutputSignal pin3Parameter;
    public GameObject Pin4;
    [HideInInspector] public InputSignal pin4Parameter;
    public GameObject Pin5;
    [HideInInspector] public InputSignal pin5Parameter;
    public GameObject Pin6;
    [HideInInspector] public OutputSignal pin6Parameter;
    public GameObject Pin7;
    [HideInInspector] public OutputSignal pin7Parameter;
    public GameObject Pin8;
    [HideInInspector] public OutputSignal pin8Parameter;
    public GameObject Pin9;
    [HideInInspector] public InputSignal pin9Parameter;
    public GameObject Pin10;
    [HideInInspector] public InputSignal pin10Parameter;
    public GameObject Pin11;
    [HideInInspector] public OutputSignal pin11Parameter;
    public GameObject Pin12;
    [HideInInspector] public InputSignal pin12Parameter;
    public GameObject Pin13;
    [HideInInspector] public InputSignal pin13Parameter;
    public GameObject Pin14;
    [HideInInspector] public InputSignal pin14Parameter;
    [SerializeField] private GameObject warning;

    Func<bool, bool, bool> operation;

    void Start()
    {
        switch (booleanTypeOperation)
        {
            case BooleanOperation.AND:
                operation = (x, y) => x && y;
                break;
            case BooleanOperation.OR:
                operation = (x, y) => x || y;
                break;
            case BooleanOperation.NAND:
                operation = (x, y) => !x || !y;
                break;
            case BooleanOperation.NOR:
                operation = (x, y) => !x && !y;
                break;
            case BooleanOperation.XOR:
                operation = (x, y) => x ^ y;
                break;
            case BooleanOperation.XNOR:
                operation = (x, y) => !(x ^ y);
                break;
        }
    }

    void Update()
    {
        pin1Parameter = Pin1.GetComponent<InputSignal>();
        pin2Parameter = Pin2.GetComponent<InputSignal>();
        pin3Parameter = Pin3.GetComponent<OutputSignal>();
        pin4Parameter = Pin4.GetComponent<InputSignal>();
        pin5Parameter = Pin5.GetComponent<InputSignal>();
        pin6Parameter = Pin6.GetComponent<OutputSignal>();
        pin7Parameter = Pin7.GetComponent<OutputSignal>();
        pin8Parameter = Pin8.GetComponent<OutputSignal>();
        pin9Parameter = Pin9.GetComponent<InputSignal>();
        pin10Parameter = Pin10.GetComponent<InputSignal>();
        pin11Parameter = Pin11.GetComponent<OutputSignal>();
        pin12Parameter = Pin12.GetComponent<InputSignal>();
        pin13Parameter = Pin13.GetComponent<InputSignal>();
        pin14Parameter = Pin14.GetComponent<InputSignal>();
        
        CircuitConditions = pin14Parameter.Snapped && pin14Parameter.Activate && pin7Parameter.Snapped && pin7Parameter.SnappedOutput;
        if (CircuitConditions)
        {
            PowerSupplyDC.InstantiateAddList(this.name);
            warning.SetActive(false);
            pin7Parameter.Activate = true;

            if (operation(pin1Parameter.Activate, pin2Parameter.Activate))
            {
                pin3Parameter.Activate = true;
            }
            else
            {
                pin3Parameter.Activate = false;
            }


            if (operation(pin4Parameter.Activate, pin5Parameter.Activate))
            {
                pin6Parameter.Activate = true;
            }
            else
            {
                pin6Parameter.Activate = false;
            }

            if (operation(pin9Parameter.Activate, pin10Parameter.Activate))
            {
                pin8Parameter.Activate = true;
            }
            else
            {
                pin8Parameter.Activate = false;
            }

            if (operation(pin12Parameter.Activate, pin13Parameter.Activate))
            {
                pin11Parameter.Activate = true;
            }
            else
            {
                pin11Parameter.Activate = false;
            }



        }
        else
        {
            TurnToPassive();
            PowerSupplyDC.InstantiateRemoveList(this.name);
            warning.SetActive(PowerSupplyDC.TurnOnOffSupplyDC);

            pin7Parameter.Activate = false;
            pin3Parameter.Activate = false;
            pin6Parameter.Activate = false;
            pin8Parameter.Activate = false;
            pin11Parameter.Activate = false;
        }
    }
}
