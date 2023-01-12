using System;
using UnityEngine;

public class Not_pate : Elements
{
    public GameObject Pin1;
    [HideInInspector] public InputSignal pin1Parameter;
    public GameObject Pin2;
    [HideInInspector] public OutputSignal pin2Parameter;
    public GameObject Pin3;
    [HideInInspector] public InputSignal pin3Parameter;
    public GameObject Pin4;
    [HideInInspector] public OutputSignal pin4Parameter;
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
    [HideInInspector] public OutputSignal pin10Parameter;
    public GameObject Pin11;
    [HideInInspector] public InputSignal pin11Parameter;
    public GameObject Pin12;
    [HideInInspector] public OutputSignal pin12Parameter;
    public GameObject Pin13;
    [HideInInspector] public InputSignal pin13Parameter;
    public GameObject Pin14;
    [HideInInspector] public InputSignal pin14Parameter;
    [SerializeField] private GameObject warning;

    Func<bool, bool> operation = (x) => !(x);

    void Update()
    {
        pin1Parameter = Pin1.GetComponent<InputSignal>();
        pin2Parameter = Pin2.GetComponent<OutputSignal>();
        pin3Parameter = Pin3.GetComponent<InputSignal>();
        pin4Parameter = Pin4.GetComponent<OutputSignal>();
        pin5Parameter = Pin5.GetComponent<InputSignal>();
        pin6Parameter = Pin6.GetComponent<OutputSignal>();
        pin7Parameter = Pin7.GetComponent<OutputSignal>();
        pin8Parameter = Pin8.GetComponent<OutputSignal>();
        pin9Parameter = Pin9.GetComponent<InputSignal>();
        pin10Parameter = Pin10.GetComponent<OutputSignal>();
        pin11Parameter = Pin11.GetComponent<InputSignal>();
        pin12Parameter = Pin12.GetComponent<OutputSignal>();
        pin13Parameter = Pin13.GetComponent<InputSignal>();
        pin14Parameter = Pin14.GetComponent<InputSignal>();

        CircuitConditions = pin14Parameter.Snapped && pin14Parameter.Activate && pin7Parameter.Snapped && pin7Parameter.SnappedOutput;
        if (CircuitConditions)
        {
            PowerSupplyDC.InstantiateAddList(this.name);
            warning.SetActive(false);
            pin7Parameter.Activate = true;

            if (operation(pin1Parameter.Activate))
            {
                pin2Parameter.Activate = true;
            }
            else
            {
                pin2Parameter.Activate = false;
            }


            if (operation(pin3Parameter.Activate))
            {
                pin4Parameter.Activate = true;
            }
            else
            {
                pin4Parameter.Activate = false;
            }

            if (operation(pin5Parameter.Activate))
            {
                pin6Parameter.Activate = true;
            }
            else
            {
                pin6Parameter.Activate = false;
            }

            if (operation(pin9Parameter.Activate))
            {
                pin8Parameter.Activate = true;
            }
            else
            {
                pin8Parameter.Activate = false;
            }

            if (operation(pin11Parameter.Activate))
            {
                pin10Parameter.Activate = true;
            }
            else
            {
                pin10Parameter.Activate = false;
            }

            if (operation(pin13Parameter.Activate))
            {
                pin12Parameter.Activate = true;
            }
            else
            {
                pin12Parameter.Activate = false;
            }



        }
        else
        {
            TurnToPassive();
            PowerSupplyDC.InstantiateRemoveList(this.name);
            warning.SetActive(PowerSupplyDC.TurnOnOffSupplyDC);
            pin7Parameter.Activate = false;

            pin2Parameter.Activate = false;
            pin4Parameter.Activate = false;
            pin6Parameter.Activate = false;
            pin8Parameter.Activate = false;
            pin10Parameter.Activate = false;
            pin12Parameter.Activate = false;
        }
    }
}
