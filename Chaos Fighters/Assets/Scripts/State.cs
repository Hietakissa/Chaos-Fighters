using UnityEngine;
using System;

public abstract class State
{
    protected PlayerController player;
    protected FrameAnimationSO animation;

    protected float animationTime;
    protected float animationStep;
    protected int maxAnimationIndex;
    protected int animationIndex;

    protected virtual bool LoopAnimation => false;
    public virtual bool CanExit => true;
    public virtual PlayerState[] ValidExitStates => Array.Empty<PlayerState>();
    public virtual Predicate<PlayerController> EnterPredicate => (p => p != null);

    public bool Initialized { get; private set; }


    public virtual void InitState(PlayerController player)
    {
        this.player = player;
        animation = AnimationManager.Instance.GetAnimationForState(player);

        animationStep = 1f / animation.Framerate;
        maxAnimationIndex = animation.Frames.Length - 1;
    }
    public virtual void EnterState()
    {
        animationTime = 0f;
    }
    public virtual void UpdateState()
    {
        animationTime += Time.deltaTime;
        animationIndex = (int)Mathf.Floor(animationTime / animationStep);
        if (LoopAnimation) animationIndex %= (maxAnimationIndex + 1);
    }
    public virtual void FixedUpdateState()
    {

    }
    public virtual void ExitState()
    {

    }

    protected void SetAnimationFrame()
    {
        player.SpriteRenderer.sprite = animation.Frames[animationIndex];
    }
}