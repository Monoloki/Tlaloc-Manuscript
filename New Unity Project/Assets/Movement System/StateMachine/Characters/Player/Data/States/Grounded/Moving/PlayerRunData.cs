using System;
using UnityEngine;

namespace Movement_System.StateMachine.Characters.Player.Data.States.Grounded.Moving {
    [Serializable]
    public class PlayerRunData {
        [field: SerializeField]
        [field: Range(1f, 2f)]
        public float SpeedModifier { get; private set; } = 1f;
    }
}
