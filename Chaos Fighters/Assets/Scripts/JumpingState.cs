using UnityEngine;
using System;

public class JumpingState : State
{
    public override PlayerState[] ValidExitStates => new PlayerState[] { PlayerState.Idling, PlayerState.Moving };
    public override Predicate<PlayerController> EnterPredicate => (player =>
    {
        return !player.IsGrounded;
    });
    protected override bool LoopAnimation => false;


    public override void EnterState()
    {
        base.EnterState();


        SetAnimationFrame();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();


        player.HandleMovement(0.15f, 0.05f);
    }
}