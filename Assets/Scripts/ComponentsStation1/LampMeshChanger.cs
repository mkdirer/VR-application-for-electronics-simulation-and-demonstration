using UnityEngine;

public class LampMeshChanger : Elements
{
    [SerializeField] private Material materal1;
    [SerializeField] private Material materal2;
    [SerializeField] GameObject SnapZoneForPlus;
    [SerializeField] GameObject SnapZoneForMinus;
    [SerializeField] GameObject Bulb;
    [HideInInspector] private InputSignal SnappedZone1;
    [HideInInspector] private OutputSignal SnappedZone2;
    Renderer rend;

    void Update()
    {
        rend = Bulb.GetComponent<Renderer>();
        rend.enabled = true;
        SnappedZone1 = SnapZoneForPlus.GetComponent<InputSignal>();
        SnappedZone2 = SnapZoneForMinus.GetComponent<OutputSignal>();
        SnappedZone2.Activate = SnappedZone1.Activate;
        CircuitConditions = SnappedZone1.Snapped && SnappedZone1.Activate && SnappedZone2.Snapped && SnappedZone2.SnappedOutput; 
        if (CircuitConditions)
        {
            PowerSupplyDC.InstantiateAddList(this.name);
        }else
        {
            TurnToPassive();
            PowerSupplyDC.InstantiateRemoveList(this.name);
        }
    }

    public override void TurnToActive()
    {
        rend.sharedMaterial = materal2;
    }

    public override void TurnToPassive()
    {
        rend.sharedMaterial = materal1;
    }


}
