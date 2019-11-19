using System;

namespace Assets.Core.Controller.Entities
{
    [Flags]
    public enum MoveState
    {
        Idle,
        Accelerating,
        Running
    }
}
