using HietakissaUtils;
using System;

public class MovingState : State
{
    public override PlayerState[] ValidExitStates => new PlayerState[] { PlayerState.Idling, PlayerState.Blocking, PlayerState.Attacking, PlayerState.Jumping };
    public override Predicate<PlayerController> EnterPredicate => (player =>
    {
        return player.InputVector.x != 0 && player.IsGrounded;
    });
    override protected bool LoopAnimation => true;

    public override void UpdateState()
    {
        base.UpdateState();


        SetAnimationFrame();
    }

    public override void FixedUpdateState()
    {
        player.HandleMovement();
    }
}