using System;
using UnityEngine;

public class DinoAnimationBridge : MonoBehaviour
{
    Animator _anim;

    // Hash once; no magic strings in Update loops
    static readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
    static readonly int GroundedHash = Animator.StringToHash("Grounded");
    static readonly int WalkHash = Animator.StringToHash("Walk");
    static readonly int SwimHash = Animator.StringToHash("Swim");
    static readonly int LandHash = Animator.StringToHash("Land");
    static readonly int HitHash = Animator.StringToHash("Hit");
    static readonly int DieHash = Animator.StringToHash("Die");
    static readonly int JumpHash = Animator.StringToHash("Jump");
    static readonly int FlutterHash = Animator.StringToHash("Flutter");

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void SetMoveSpeed(float speed) => _anim.SetFloat(MoveSpeedHash, speed);
    public void SetGrounded(bool grounded) => _anim.SetBool(GroundedHash, grounded);
    public void StartWalk() => _anim.SetTrigger(WalkHash);
    public void StartSwim() => _anim.SetTrigger(SwimHash);
    public void StartLand() => _anim.SetTrigger(LandHash);
    public void PlayJump() => _anim.SetTrigger(JumpHash);
    public void PlayFlutter() => _anim.SetTrigger(FlutterHash);
    public void PlayHit() => _anim.SetTrigger(HitHash);
    public void PlayDie() => _anim.SetTrigger(DieHash);

    // Optional: expose events for animation callbacks (see section 4)
    public System.Action<string> OnAnimEvent;
    public void RaiseEvent(string evt) => OnAnimEvent?.Invoke(evt);


}
