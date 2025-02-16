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
    protected override bool InvertAnimation => movingBackwards;

    bool movingBackwards;

    public override void UpdateState()
    {
        base.UpdateState();


        movingBackwards = player.FacingDirectionVector.x.Normalized() != player.InputVector.x.Normalized();
        DebugTextManager.Instance.SetVariable("Moving Backwards", movingBackwards.ToString(), player);
        SetAnimationFrame();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();


        player.HandleMovement();
    }
}