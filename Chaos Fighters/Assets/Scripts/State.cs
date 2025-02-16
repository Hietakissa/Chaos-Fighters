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
    protected virtual bool InvertAnimation => false;
    protected virtual bool ClampAnimationIndex => false;
    protected virtual FrameAnimationSO OverriddenAnimation => null;
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
        Initialized = true;
    }
    public virtual void EnterState()
    {
        animationTime = 0f;
    }
    public virtual void UpdateState()
    {
        if (InvertAnimation)
        {
            animationTime -= Time.deltaTime * animation.Frames.Length;
            if (animationTime < 0f) animationTime = animationStep * animation.Frames.Length;
        }
        else
        {
            animationTime += Time.deltaTime * animation.Frames.Length;
        }

        animationIndex = (int)Mathf.Floor(animationTime / animationStep);
        if (LoopAnimation) animationIndex %= (maxAnimationIndex + 1);
        else if (ClampAnimationIndex && animationIndex > maxAnimationIndex) animationIndex = maxAnimationIndex;
    }
    public virtual void FixedUpdateState()
    {

    }
    public virtual void ExitState()
    {

    }

    protected void SetAnimationFrame()
    {
        if (OverriddenAnimation != null) player.SpriteRenderer.sprite = OverriddenAnimation.Frames[animationIndex];
        else player.SpriteRenderer.sprite = animation.Frames[animationIndex];
    }
}