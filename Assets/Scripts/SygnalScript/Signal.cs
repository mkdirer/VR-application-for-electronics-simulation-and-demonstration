using UnityEngine;

public class Signal : MonoBehaviour
{
    [SerializeField] private bool snapped = false;
    [SerializeField] private bool activate = false;
    [HideInInspector] public GameObject wire;
    [HideInInspector] public GameObject parent;
    [HideInInspector] public WireSignal signalWire;

    public bool Snapped { get => snapped; set => snapped = value; }
    public bool Activate { get => activate; set => activate = value; }
    
    public void ClearSignal()
    {
        this.Snapped = false;
        this.Activate = false;
    }
}