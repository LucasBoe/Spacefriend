using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sprouts.Physics.Player
{
    public class PlayerWalkState : PlayerInputBasedState
    {
        public PlayerWalkState(Rigidbody2D rigidbody, PlayerPhysicsValues values) : base(rigidbody, values) { }

        public override void On¡xisInput(Vector2 input)
        {
            float xBefore = Rigidbody.velocity.x;
            float x = input.x != 0 ? Mathf.Lerp(xBefore, input.x * Values.WalkTargetVelocity, Values.WalkAccelration) : Mathf.Lerp(xBefore, 0, Values.WalkDecelleration);
            Rigidbody.velocity = new Vector2(x, Rigidbody.velocity.y);
        }
    }
}
