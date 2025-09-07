using Player;
using System;
using UnityEngine;

public class CarryDino : MonoBehaviour, ICarrier
{
    [Header("Pickup")]
    public Transform pickupOrigin;
    public float pickupRadius = 0.7f;
    public LayerMask collectibleMask;

    private readonly Collider2D[] _hits = new Collider2D[8];

    public int CurrentWeight { get; private set; }
    public IPickupable Current { get; private set; }


    public bool TryToPickup()
    {
        bool value = false;
        if (!pickupOrigin) pickupOrigin = transform;
        Collider[] _hits = Physics.OverlapSphere(
            pickupOrigin.position,
            pickupRadius,
            collectibleMask
        );
        if(_hits.Length > 0) value = true;
        for (int i = 0; i < _hits.Length; i++)
        {
            var col = _hits[i];
            if (!col) continue;

            var pickup = col.GetComponent<IPickupable>();

            JumperMovement();
            
            pickup.TryPickup(this);
            CurrentWeight = pickup.Weight;

            _hits[i] = null;
        }

        return value;
    }

    private void JumperMovement()
    {
        gameObject.GetComponent<PlayerMovement>().StartJumpPhase();
    }

    private void SwimmingMovement()
    {
        gameObject.GetComponent<PlayerMovement>().StartSwimPhase();
    }
}
