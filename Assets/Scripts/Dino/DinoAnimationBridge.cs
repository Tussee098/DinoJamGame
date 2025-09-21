using System;
using UnityEngine;

public class DinoAnimationBridge : MonoBehaviour
{
    Animator _anim;

    static readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
    static readonly int GroundedHash = Animator.StringToHash("Grounded");
    static readonly int WalkHash = Animator.StringToHash("Walk");
    static readonly int SwimHash = Animator.StringToHash("Swim");

    static readonly int LandHash = Animator.StringToHash("Land");
    static readonly int HitHash = Animator.StringToHash("Damaged");
    static readonly int DieHash = Animator.StringToHash("Die");
    static readonly int JumpHash = Animator.StringToHash("Jump");
    static readonly int FlutterHash = Animator.StringToHash("Flutter");

    [Header("State Names / Config")]
    [SerializeField] string hurtStateName = "Hurt";

    int _hurtStateFullPathHash;
    public int previousStateHash;
    int _previousLayerIndex = 0;

    public Action<string> OnAnimEvent;
    public void RaiseEvent(string evt) => OnAnimEvent?.Invoke(evt);

    void Start()
    {
        _anim = GetComponent<Animator>();
        _hurtStateFullPathHash = Animator.StringToHash(hurtStateName);
    }

    public void SetMoveSpeed(float speed) => _anim.SetFloat(MoveSpeedHash, speed);
    public void SetGrounded(bool grounded) => _anim.SetBool(GroundedHash, grounded);
    public void StartWalk() => _anim.SetTrigger(WalkHash);
    public void StartSwim() => _anim.SetTrigger(SwimHash);
    public void StartLand() => _anim.SetTrigger(LandHash);
    public void PlayJump() => _anim.SetTrigger(JumpHash);
    public void PlayFlutter() => _anim.SetTrigger(FlutterHash);
    public void PlayDie() => _anim.SetTrigger(DieHash);
    public void PlaySwimSide(bool side) => _anim.SetBool("SwimSide", side);
    

    public void PlayHit()
    {
        _previousLayerIndex = 0;
        var current = _anim.GetCurrentAnimatorStateInfo(_previousLayerIndex);
        previousStateHash = current.fullPathHash;
        _anim.SetTrigger(HitHash);
    }

    public void OnHurtFinished() => ReturnToPrevious();

    void ReturnToPrevious()
    {
        if (previousStateHash != 0)
            _anim.Play(previousStateHash, _previousLayerIndex, 0f);
    }

    void Update()
    {
        var st = _anim.GetCurrentAnimatorStateInfo(0);
        if (st.fullPathHash == _hurtStateFullPathHash && st.normalizedTime >= 1f)
            ReturnToPrevious();
    }
}
