using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sprouts.Physics.Player
{
    public abstract class PlayerInputBasedState : PlayerMoveStateBase
    {
        protected PlayerInputBasedState(Rigidbody2D rigidbody, PlayerPhysicsValues values) : base(rigidbody, values) { }

        public override void Exit()
        {
            base.Exit();
            InputHandler.AxisInputEvent -= On¡xisInput;
        }

        public override void Enter()
        {
            base.Enter();
            InputHandler.AxisInputEvent += On¡xisInput;
        }

        public abstract void On¡xisInput(Vector2 input);
    }
}
