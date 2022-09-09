using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sprouts.Physics.Player
{
    [System.Serializable]
    public abstract class PlayerMoveStateBase
    {
        protected Rigidbody2D Rigidbody;
        protected PlayerPhysicsValues Values;
        public bool IsActive { get; internal set; }

        public PlayerMoveStateBase(Rigidbody2D rigidbody, PlayerPhysicsValues values)
        {
            this.Rigidbody = rigidbody;
            this.Values = values;
        }
        public virtual void Exit()
        {
            IsActive = false;
        }

        public virtual void Enter()
        {
            IsActive = true;
        }
    }
}
