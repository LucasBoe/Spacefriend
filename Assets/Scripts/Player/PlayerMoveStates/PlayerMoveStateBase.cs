using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sprouts.Physics.Player
{
    [System.Serializable]
    public abstract class PlayerMoveStateBase
    {
        public Rigidbody2D Rigidbody;
        public bool IsActive { get; internal set; }

        public PlayerMoveStateBase(Rigidbody2D rigidbody)
        {
            this.Rigidbody = rigidbody;
        }
        public abstract void FixedUpdate(PlayerPhysicsValues values);
        public virtual void Exit() => IsActive = false;
        public virtual void Enter() => IsActive = true;
    }
}
