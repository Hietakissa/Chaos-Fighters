using UnityEngine;

public class IdleState : State
{
    public override PlayerState[] ValidExitStates => new PlayerState[] { PlayerState.Moving, PlayerState.Blocking, PlayerState.Attacking, PlayerState.Jumping };
    override protected bool LoopAnimation => true;

    public override void UpdateState()
    {
        base.UpdateState();


        if (player.InputVector.x != 0) player.StateMachine.EnterState(PlayerState.Moving);
        else SetAnimationFrame();
    }

    public override void FixedUpdateState()
    {
        player.HandleMovement(0);
    }
}