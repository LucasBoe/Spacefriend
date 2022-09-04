using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sprouts.Physics.Player
{
    public class PlayerWalkState : PlayerMoveStateBase
    {
        public PlayerWalkState(Rigidbody2D rigidbody) : base(rigidbody) { }

        public override void FixedUpdate(PlayerPhysicsValues values)
        {
            float input = Input.GetAxis("Horizontal");

            float xBefore = Rigidbody.velocity.x;

            float x = input != 0 ? Mathf.Lerp(xBefore, input * values.WalkTargetVelocity, values.WalkAccelration) : Mathf.Lerp(xBefore, 0, values.WalkDecelleration);

            Rigidbody.velocity = new Vector2(x, Rigidbody.velocity.y);
        }
    }
}
