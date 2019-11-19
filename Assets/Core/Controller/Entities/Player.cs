using Assets.Core.Utils;
using UnityEngine;

namespace Assets.Core.Controller.Entities
{
    public sealed class Player : Entity
    {
        private static readonly KeyCode[] movementKeys = new[] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };

        protected override void OnUpdate()
        {
            if (IsMoving())
            {
                moveState = currentSpeed == speed ? MoveState.Running : MoveState.Accelerating;

                if (moveState == MoveState.Accelerating)
                    currentSpeed++;
            }
            else
            {
                if (moveState != MoveState.Idle)
                {
                    moveState = MoveState.Idle;
                    currentSpeed = 0;
                }
            }

            base.OnUpdate();
        }

        private bool IsMoving()
            => GU.AnyKeyPressed(movementKeys);
    }
}
