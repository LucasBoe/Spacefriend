using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sprouts.Physics.Player
{
    public class PlayerFloatState : PlayerInputBasedState
    {
        public PlayerFloatState(Rigidbody2D rigidbody, PlayerPhysicsValues values) : base(rigidbody, values) { }

        public override void Enter()
        {
            base.Enter();
            Rigidbody.freezeRotation = false;
        }
        public override void Exit()
        {
            base.Exit();
            Rigidbody.freezeRotation = true;
            float rot = Rigidbody.rotation > 180 ? Rigidbody.rotation - 360f : Rigidbody.rotation;
            Rigidbody.rotation = Mathf.Abs(rot) < 90 ? 0 : 180;
        }

        public override void OnÁxisInput(Vector2 input)
        {
            RotateTowardsGround(Values);
            Rigidbody.AddForceIgnoreMass(input * Values.FloatForce);
        }

        private void RotateTowardsGround(PlayerPhysicsValues values)
        {
            float gravityDir = Rigidbody.gravityScale.Sign();
            if (values.DistanceToGround < 8f)
            {
                float currentAngle = Rigidbody.rotation;
                float targetAngle = gravityDir > 0 ? 0 : 180;

                float deltaAngle = Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetAngle));

                float lerpValue = values.DistanceToRotationLerpValue.Evaluate(values.DistanceToGround) * Time.fixedDeltaTime;
                float lerped = Mathf.LerpAngle(currentAngle, targetAngle, lerpValue);

                float additionalForce = values.DistanceToForceSupportStrength.Evaluate(values.DistanceToGround) * gravityDir * (deltaAngle / 90f);

                Rigidbody.AddForceIgnoreMass(new Vector2(0, additionalForce));
                Rigidbody.SetRotation(lerped);
            }
        }
    }
}
