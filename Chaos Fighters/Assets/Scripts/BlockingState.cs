using UnityEngine;

public class BlockingState : State
{
    public override PlayerState[] ValidExitStates => new PlayerState[] { PlayerState.Idling, PlayerState.Moving, PlayerState.Jumping };

    public override void UpdateState()
    {
        base.UpdateState();


        if (player.IsPlayer1 && !Input.GetKey(KeyCode.S)) player.StateMachine.EnterState(PlayerState.Idling);
        else if (!player.IsPlayer1 && !Input.GetKey(KeyCode.DownArrow)) player.StateMachine.EnterState(PlayerState.Idling);
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();


        player.HandleMovement(0.5f);
    }
}