using UnityEngine;

public class AttackState : State
{
    protected override PlayerState[] ValidExitStates => new PlayerState[]{ PlayerState.Idling, PlayerState.Moving };

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