using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicButtonBoolean : MonoBehaviour
{
    [SerializeField] private Material materal1;
    [SerializeField] private Material materal2;
    [SerializeField] private GameObject button;
    [HideInInspector] private bool activate;
    Renderer rend;
    public bool toggleTurnOnOff = false;
    [SerializeField] public GameObject supplyPinIn;
    [HideInInspector] public InputSignal firstSignal;
    [SerializeField] public GameObject supplyPinOut1;
    [HideInInspector] public OutputSignal secondSignal1;
    [SerializeField] public GameObject supplyPinOut2;
    [HideInInspector] public OutputSignal secondSignal2;
    [SerializeField]  public GameObject supplyPinOut3;
    [HideInInspector] public OutputSignal secondSignal3;

    public bool Activate { get => activate; set => activate = value; }

    void Update()
    {
        rend = button.GetComponent<Renderer>();
        rend.enabled = true;
        firstSignal = supplyPinIn.GetComponent<InputSignal>();
        secondSignal1 = supplyPinOut1.GetComponent<OutputSignal>();
        secondSignal2 = supplyPinOut2.GetComponent<OutputSignal>();
        secondSignal3 = supplyPinOut3.GetComponent<OutputSignal>();

        Activate = firstSignal.Activate;

        if (!toggleTurnOnOff)
        {
            secondSignal3.Activate = false;
            secondSignal1.Activate = false;
            secondSignal2.Activate = false;

            rend.sharedMaterial = materal1;
        }
        else
        {
            secondSignal1.Activate = firstSignal.Activate;
            secondSignal2.Activate = firstSignal.Activate;
            secondSignal3.Activate = firstSignal.Activate;

            rend.sharedMaterial = materal2;
        }
    }

    public void ChangeMaterialWithButton()
    {
        toggleTurnOnOff = !toggleTurnOnOff;
    }

}