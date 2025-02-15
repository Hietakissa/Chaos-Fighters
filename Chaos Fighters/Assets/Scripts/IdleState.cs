using HietakissaUtils;
using UnityEngine;
using System;

public class IdleState : State
{
    public override PlayerState[] ValidExitStates => new PlayerState[] { PlayerState.Moving, PlayerState.Blocking, PlayerState.Attacking, PlayerState.Jumping };
    public override Predicate<PlayerController> EnterPredicate => (player =>
    {
        return player.InputVector.x == 0 && player.RB.linearVelocityX.Abs() < 0.5f;
    });
    override protected bool LoopAnimation => true;
    
    public override void UpdateState()
    {
        base.UpdateState();


        SetAnimationFrame();
    }

    public override void FixedUpdateState()
    {
        player.HandleMovement(0.15f);
    }
}