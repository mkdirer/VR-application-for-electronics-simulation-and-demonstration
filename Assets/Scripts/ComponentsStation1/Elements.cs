using UnityEngine;

public class Elements : MonoBehaviour
{
    [HideInInspector] private bool circuitConditions;
    [HideInInspector] private PlugsSygnal snappedZoneColumnSignal;
    public bool CircuitConditions { get => circuitConditions; set => circuitConditions = value; }
    public PlugsSygnal SnappedZonePinSignal { get => snappedZoneColumnSignal; set => snappedZoneColumnSignal = value; }

    public virtual void TurnToActive() { }
    public virtual void TurnToPassive() { }
}
