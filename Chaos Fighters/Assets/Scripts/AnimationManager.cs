using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager Instance;

    [SerializeField] FrameAnimationSO player1AttackAnimation;
    [SerializeField] FrameAnimationSO player1MovingAnimation;
    [SerializeField] FrameAnimationSO player1IdleAnimation;

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
            };
        }
        else return player1IdleAnimation;
    }
}