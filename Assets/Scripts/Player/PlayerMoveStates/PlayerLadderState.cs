using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sprouts.Physics.Player
{
    public class PlayerLadderState : PlayerMoveStateBase
    {
        public PlayerLadderState(Rigidbody2D rigidbody, PlayerPhysicsValues values) : base(rigidbody, values) { }
    }
}
