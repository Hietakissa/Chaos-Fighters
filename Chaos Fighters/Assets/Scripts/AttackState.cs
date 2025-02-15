using UnityEngine;

public class AttackState : State
{
    public override PlayerState[] ValidExitStates => new PlayerState[]{ PlayerState.Idling, PlayerState.Moving, PlayerState.Jumping };

    public override void EnterState()
    {
        base.EnterState();


        player.RB.linearVelocityX = 0f;
    }

    public override void UpdateState()
    {
        base.UpdateState();


        if (animationIndex > maxAnimationIndex) player.StateMachine.EnterState(PlayerState.Moving);
        else SetAnimationFrame();
    }

    public override void FixedUpdateState()
    {
        player.HandleMovement(0);
    }
}