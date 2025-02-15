using UnityEngine;

public class StateMachineController
{
    public StateMachineController(PlayerController player)
    {
        this.player = player;

        attackState = new AttackState();
        movingState = new MovingState();
        idleState = new IdleState();
    }

    PlayerController player;

    State currentState;
    PlayerState currentStateEnum;

    AttackState attackState;
    MovingState movingState;
    IdleState idleState;


    public State CurrentState => currentState;
    public PlayerState CurrentStateEnum => currentStateEnum;


    public void EnterState(PlayerState state)
    {
        State nextState = null;

        nextState = state switch
        {
            PlayerState.Attacking => attackState,
            PlayerState.Moving => movingState,
            PlayerState.Idling => idleState,
            _ => attackState
        };


        if (nextState != currentState)
        {
            currentState?.ExitState();
            currentState = nextState;
            currentStateEnum = state;

            if (!currentState.Initialized) currentState.InitState(player);
            currentState.EnterState();

            DebugTextManager.Instance.SetVariable("State", currentStateEnum.ToString(), player);
        }
    }
}

public enum PlayerState
{
    Attacking,
    Moving,
    Idling,
    Blocking
}