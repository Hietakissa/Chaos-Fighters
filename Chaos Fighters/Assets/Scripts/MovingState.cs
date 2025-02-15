using HietakissaUtils;
using UnityEngine;

public class MovingState : State
{
    public override PlayerState[] ValidExitStates => new PlayerState[] { PlayerState.Idling, PlayerState.Blocking, PlayerState.Attacking, PlayerState.Jumping };
    override protected bool LoopAnimation => true;

    public override void UpdateState()
    {
        base.UpdateState();


        if (player.InputVector.x == 0 && player.RB.linearVelocityX.Abs() < 0.5f) player.StateMachine.EnterState(PlayerState.Idling);
        else SetAnimationFrame();
    }

    public override void FixedUpdateState()
    {
        player.HandleMovement();
    }
}