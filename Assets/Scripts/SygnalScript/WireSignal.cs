using UnityEngine;

public class WireSignal : Signal
{

    [SerializeField] private bool snappedInput = false;
    [SerializeField] private bool snappedOutput = false;
    [SerializeField] private GameObject warning;
    [SerializeField] private GameObject lineElectric;
    [SerializeField] private int numbreOfSnaps;

    public bool SnappedInput { get => snappedInput; set => snappedInput = value; }
    public bool SnappedOutput { get => snappedOutput; set => snappedOutput = value; }
    public int NumbreOfSnaps { get => numbreOfSnaps; set => numbreOfSnaps = value; }

    void Update()
    {
        if((!SnappedOutput && !SnappedInput) || (!SnappedOutput && SnappedInput))
        {
            this.Snapped = false;
            this.Activate = false;
            warning.SetActive(PowerSupplyDC.TurnOnOffSupplyDC);
            lineElectric.SetActive(false);
        }
        else if(SnappedOutput && SnappedInput && Activate)
        {
            warning.SetActive(false);
            lineElectric.SetActive(PowerSupplyDC.TurnOnOffSupplyDC);
        }
        else
        {
            lineElectric.SetActive(false);
            warning.SetActive(false);
        }
    }
}
