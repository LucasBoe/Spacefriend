using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sprouts.Physics.Player
{
    public class PlayerOverrideState : PlayerMoveStateBase
    {
        public PlayerOverrideState(Rigidbody2D rigidbody, PlayerPhysicsValues values) : base(rigidbody, values) { }
    }
}
