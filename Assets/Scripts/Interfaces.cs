using Assets.Scripts;
using UnityEngine;

public interface IPickupable
{
    PickupType Type { get; }
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
    PickupType pickupType { get; set; }
    bool TryToPickup();
}

