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
    public PlayerOverrideState OverrideState;

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
    public PlayerStates(Rigidbody2D rigidbody, PlayerPhysicsValues values)
    {
        WalkState = new PlayerWalkState(rigidbody, values);
        LadderState = new PlayerLadderState(rigidbody, values);
        FloatState = new PlayerFloatState(rigidbody, values);
        OverrideState = new PlayerOverrideState(rigidbody, values);

        CurrentState = WalkState;
    }

    internal void FixedUpdate(PlayerPhysicsValues values)
    {
        bool canWalk = values.DistanceToGround < 0.2f && values.Gravity != 0;
        CurrentState = canWalk ? WalkState : FloatState as PlayerMoveStateBase;
    }
}
