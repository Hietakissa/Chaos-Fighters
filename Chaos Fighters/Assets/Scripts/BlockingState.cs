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
    protected override FrameAnimationSO OverriddenAnimation => overrideAnim;

    FrameAnimationSO overrideAnim;
    FrameAnimationSO blockHitAnimation;
    float hitTime;

    public override void InitState(PlayerController player)
    {
        base.InitState(player);


        if (player.IsPlayer1) blockHitAnimation = AnimationManager.Instance.Player1BlockHitAnimation;
        else blockHitAnimation = AnimationManager.Instance.Player2BlockHitAnimation;
    }

    public override void EnterState()
    {
        base.EnterState();


        player.OnHit += Player_OnHit;
        player.RB.linearVelocityX = 0f;
        player.IsBlocking = true;
        DebugTextManager.Instance.SetVariable("Is Blocking", player.IsBlocking.ToString(), player);
    }

    public override void ExitState()
    {
        base.ExitState();


        player.OnHit -= Player_OnHit;
        player.IsBlocking = false;
        DebugTextManager.Instance.SetVariable("Is Blocking", player.IsBlocking.ToString(), player);
    }

    public override void UpdateState()
    {
        base.UpdateState();


        //if (player.IsPlayer1 && !Input.GetKey(KeyCode.S)) player.StateMachine.EnterState(PlayerState.Idling);
        //else if (!player.IsPlayer1 && !Input.GetKey(KeyCode.DownArrow)) player.StateMachine.EnterState(PlayerState.Idling);
        if (hitTime > 0f)
        {
            hitTime -= Time.deltaTime;
            overrideAnim = blockHitAnimation;
        }
        else overrideAnim = null;

        player.StaminaManager.DrainStamina(GameManager.Instance.StaminaSettings.PassiveBlockStaminaConsumption * Time.deltaTime);
        SetAnimationFrame();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();


        player.HandleMovement(0f);
    }


    void Player_OnHit()
    {
        hitTime = 0.3f;
    }
}