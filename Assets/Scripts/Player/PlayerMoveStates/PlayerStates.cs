using Sprouts.Physics.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStates
{
    public PlayerWalkState WalkState;
    public PlayerLadderState LadderState;
    public PlayerFloatState FloatState;

    [SerializeField] private PlayerMoveStateBase _currentState;
    public PlayerMoveStateBase CurrentState
    {
        get { return _currentState; }
        set
        {
            if (_currentState != value)
            {
                _currentState?.Exit();
                _currentState = value;
                _currentState?.Enter();
            }
        }
    }
    public PlayerStates(Rigidbody2D rigidbody)
    {
        WalkState = new PlayerWalkState(rigidbody);
        LadderState = new PlayerLadderState(rigidbody);
        FloatState = new PlayerFloatState(rigidbody);

        CurrentState = WalkState;
    }

    internal void FixedUpdate(PlayerPhysicsValues values)
    {
        bool canWalk = values.DistanceToGround < 0.2f && values.Gravity != 0;
        CurrentState = canWalk ? WalkState : FloatState as PlayerMoveStateBase;
        CurrentState.FixedUpdate(values);
    }
}
