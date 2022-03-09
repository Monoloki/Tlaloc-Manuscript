using System;
using UnityEngine;

namespace Movement_System.StateMachine.Characters.Player.Data.States {
    [Serializable]
    public class PlayerRotationData {
        [field: SerializeField] public Vector3 TargetRotationReachTime { get; private set; }
    }
}
