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

    private DinoAnimationBridge m_anim;
    public int CurrentWeight { get; private set; }
    public IPickupable Current { get; private set; }

    private bool IsBoatLayer(int layer) => layer == LayerMask.NameToLayer("Boat");

    public void Start()
    {
        m_anim = GetComponentInChildren<DinoAnimationBridge>(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsBoatLayer(collision.gameObject.layer))
            OnBoatContact();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsBoatLayer(other.gameObject.layer))
            OnBoatContact();
    }
    private void OnBoatContact()
    {
        Debug.Log("OnBoatContact");
        var dinoOxygen = gameObject.GetComponent<DinoOxygen>();
        var dinoMovement = gameObject.GetComponent<PlayerMovement>();
        dinoOxygen.ResetOxygen();
        dinoMovement.StartSwimPhase();
    }

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
        m_anim.StartWalk();
    }

    private void SwimmingMovement()
    {
        gameObject.GetComponent<PlayerMovement>().StartSwimPhase();
    }
}
