using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager Instance;

    [SerializeField] FrameAnimationSO player1AttackAnimation;
    [SerializeField] FrameAnimationSO player1MovingAnimation;
    [SerializeField] FrameAnimationSO player1IdleAnimation;
    [SerializeField] FrameAnimationSO player1BlockAnimation;
    [SerializeField] FrameAnimationSO player1JumpAnimation;

    public void Init()
    {
        Instance = this;
    }

    public FrameAnimationSO GetAnimationForState(PlayerController player)
    {
        if (player.IsPlayer1)
        {
            return player.StateMachine.CurrentStateEnum switch
            {
                PlayerState.Attacking => player1AttackAnimation,
                PlayerState.Moving => player1MovingAnimation,
                PlayerState.Idling => player1IdleAnimation,
                PlayerState.Blocking => player1BlockAnimation,
                PlayerState.Jumping => player1JumpAnimation
            };
        }
        else return player1IdleAnimation;
    }
}