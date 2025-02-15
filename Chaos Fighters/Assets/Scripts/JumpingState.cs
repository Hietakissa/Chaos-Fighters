using UnityEngine;
using System;

public class JumpingState : State
{
    public override PlayerState[] ValidExitStates => new PlayerState[] { PlayerState.Idling, PlayerState.Moving };
    public override Predicate<PlayerController> EnterPredicate => (player =>
    {
        return player.IsGrounded && Input.GetKeyDown(player.GetKeyCodeForKey(Key.Jump));
    });
    protected override bool LoopAnimation => true;


    public override void EnterState()
    {
        base.EnterState();


        player.RB.AddForce(Vector2.up * GameManager.Instance.MovementSettings.JumpForce, ForceMode2D.Impulse);
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();


        player.HandleMovement(0.3f);
    }
}