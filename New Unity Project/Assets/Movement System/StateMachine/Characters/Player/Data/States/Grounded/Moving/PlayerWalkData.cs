using System;
using UnityEngine;

namespace Movement_System.StateMachine.Characters.Player.Data.States.Grounded.Moving {
    [Serializable]
    public class PlayerWalkData {
        [field: SerializeField]
        [field: Range(0f, 1f)]
        public float SpeedModifier { get; private set; } = 0.225f;
    }
}
