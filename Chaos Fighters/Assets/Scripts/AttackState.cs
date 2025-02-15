using System;
using UnityEngine;

public class AttackState : State
{
    public override PlayerState[] ValidExitStates => new PlayerState[]{ PlayerState.Idling, PlayerState.Moving, PlayerState.Jumping };
    public override Predicate<PlayerController> EnterPredicate => (player =>
    {
        return Input.GetKeyDown(player.GetKeyCodeForKey(Key.Attack)) && player.IsGrounded;
    });
    public override bool CanExit => !attacking;

    bool attacking;

    public override void EnterState()
    {
        base.EnterState();


        attacking = true;
        player.RB.linearVelocityX = 0f;
    }

    public override void UpdateState()
    {
        base.UpdateState();


        if (animationIndex > maxAnimationIndex) attacking = false;
        else SetAnimationFrame();
    }

    public override void FixedUpdateState()
    {
        player.HandleMovement(0);
    }
}