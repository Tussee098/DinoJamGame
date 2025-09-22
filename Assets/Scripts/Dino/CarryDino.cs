using Assets.Scripts;
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

    public GameManager gameManager;
    private DinoAnimationBridge m_anim;

    public GameObject GameObjectPickup;
    private PickupType m_PickupType;
    public PickupType pickupType { get => m_PickupType; set => m_PickupType = pickupType; }

    public AudioSource PickupNoise;
    public AudioSource PickupOxygenTank;
    public AudioSource PickupPlank;
    public AudioSource PlankRepair;

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
        
        var dinoOxygen = gameObject.GetComponent<DinoOxygen>();
        var dinoMovement = gameObject.GetComponent<PlayerMovement>();
        
        switch (m_PickupType) {
            case PickupType.RepairBoard:
                PlankRepair.Play();
                gameManager.RepairBoat();
                break;
            case PickupType.OxygenTank:
                dinoOxygen.IncreaseOxygen();
                break;
            default:
                break;
        }
        m_PickupType = PickupType.None;
        dinoOxygen.ResetOxygen();
        dinoMovement.StartSwimPhase();
    }

    public bool TryToPickup()
    {
        PickupNoise.Play();
        if (m_PickupType != PickupType.None) return false;
        Debug.Log(m_PickupType);
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
            Debug.Log(col);
            if (!col) continue;

            var pickup = col.GetComponent<Pickup>();
            Debug.Log(pickup);
            m_PickupType = pickup.Type;

            switch (m_PickupType)
            {
                case PickupType.RepairBoard:
                    PickupPlank.Play();
                    break;
                case PickupType.OxygenTank:
                    PickupOxygenTank.Play();
                    break;
                default:
                    break;
            }

            Debug.Log("Picking up: " + m_PickupType);
            JumperMovement();

            GameObjectPickup = pickup.gameObject;


            pickup.TryPickup(this);

            _hits[i] = null;
            break;
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
