using UnityEngine;

public interface IPickupable
{
    int Weight { get; }
    bool TryPickup(ICarrier instigator);
}

public interface IHazard
{
    int Damage {  get; }
}
public interface IHurtable
{
    LayerMask HazardMask { get; }
    Collider HurtBox { get; }
    void Hurt(IHazard hazard);
}
public interface ICarrier
{
    bool TryToPickup();
    int CurrentWeight { get; }
}

