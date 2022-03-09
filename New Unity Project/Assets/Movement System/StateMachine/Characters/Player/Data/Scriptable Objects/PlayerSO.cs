using Movement_System.StateMachine.Characters.Player.Data.States.Grounded;
using UnityEngine;

namespace Movement_System.StateMachine.Characters.Player.Data.Scriptable_Objects {
    [CreateAssetMenu(fileName = "Player", menuName = "Custom Tools/Movement System/Characters/Player")]
    public class PlayerSO : ScriptableObject {
        [field: SerializeField] public PlayerGroundedData GroundedData { get; private set; }
    }
}
