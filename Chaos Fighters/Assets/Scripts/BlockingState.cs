using System;
using UnityEngine;

public class BlockingState : State
{
    public override PlayerState[] ValidExitStates => new PlayerState[] { PlayerState.Idling, PlayerState.Moving, PlayerState.Jumping };
    public override Predicate<PlayerController> EnterPredicate => (player =>
    {
        return player.IsGrounded && Input.GetKey(player.GetKeyCodeForKey(Key.Block)) && player.StaminaManager.Stamina > 0f;
    });
    protected override bool LoopAnimation => true;

    public override void UpdateState()
    {
        base.UpdateState();


        //if (player.IsPlayer1 && !Input.GetKey(KeyCode.S)) player.StateMachine.EnterState(PlayerState.Idling);
        //else if (!player.IsPlayer1 && !Input.GetKey(KeyCode.DownArrow)) player.StateMachine.EnterState(PlayerState.Idling);
        player.StaminaManager.DrainStamina(GameManager.Instance.StaminaSettings.PassiveBlockStaminaConsumption * Time.deltaTime);
        SetAnimationFrame();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();


        player.HandleMovement(0.25f);
    }
}