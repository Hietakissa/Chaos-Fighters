using System.Collections.Generic;
using UnityEngine;

public class StateMachineController
{
    public StateMachineController(PlayerController player)
    {
        this.player = player;

        attackState = new AttackState();
        movingState = new MovingState();
        idleState = new IdleState();
        jumpingState = new JumpingState();
        blockingState = new BlockingState();

        states.Add(attackState);
        states.Add(movingState);
        states.Add(idleState);
        states.Add(jumpingState);
        states.Add(blockingState);
    }

    List<State> states = new();
    PlayerController player;

    State currentState;
    PlayerState currentStateEnum;

    AttackState attackState;
    MovingState movingState;
    IdleState idleState;
    JumpingState jumpingState;
    BlockingState blockingState;


    public State CurrentState => currentState;
    public PlayerState CurrentStateEnum => currentStateEnum;
    public List<State> States => states;


    public void EnterState(PlayerState state)
    {
        State nextState = null;

        nextState = state switch
        {
            PlayerState.Attacking => attackState,
            PlayerState.Moving => movingState,
            PlayerState.Idling => idleState,
            PlayerState.Jumping => jumpingState,
            PlayerState.Blocking => blockingState,
            _ => idleState
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
    Blocking,
    Jumping
}