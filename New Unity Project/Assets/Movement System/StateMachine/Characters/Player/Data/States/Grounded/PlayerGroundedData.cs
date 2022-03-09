using System;
using Movement_System.StateMachine.Characters.Player.Data.States.Grounded.Moving;
using UnityEngine;

namespace Movement_System.StateMachine.Characters.Player.Data.States.Grounded {
    [Serializable]
    public class PlayerGroundedData {
        [field: SerializeField]
        [field: Range(0f, 25f)]
        public float BaseSpeed { get; } = 5f;

        [field: SerializeField] public PlayerRotationData BaseRotationData { get; private set; }
        [field: SerializeField] public PlayerWalkData     WalkData         { get; private set; }
        [field: SerializeField] public PlayerRunData      RunData          { get; private set; }
    }
}
