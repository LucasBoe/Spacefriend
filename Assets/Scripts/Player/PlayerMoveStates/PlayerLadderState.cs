using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sprouts.Physics.Player
{
    public class PlayerLadderState : PlayerMoveStateBase
    {
        public PlayerLadderState(Rigidbody2D rigidbody) : base(rigidbody) { }

        public override void FixedUpdate(PlayerPhysicsValues values)
        {
            throw new System.NotImplementedException();
        }
    }
}
