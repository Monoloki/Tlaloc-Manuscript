using Movement_System.StateMachine.Characters.Player.Data.Scriptable_Objects;
using Movement_System.StateMachine.Characters.Player.StateMachines.Movement;
using Movement_System.StateMachine.Characters.Player.Utilities.Input;
using UnityEngine;

namespace Movement_System.StateMachine.Characters.Player {
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour {
        private PlayerMovementStateMachine _movementStateMachine;

        [field: Header("References")]
        [field: SerializeField]
        public PlayerSO Data { get; private set; }

        public PlayerInput Input               { get; private set; }
        public Rigidbody   Rigidbody           { get; private set; }
        public Transform   MainCameraTransform { get; private set; }

        private void Awake(){
            Rigidbody = GetComponent<Rigidbody>();
            Input     = GetComponent<PlayerInput>();

            MainCameraTransform = UnityEngine.Camera.main.transform;

            _movementStateMachine = new PlayerMovementStateMachine(this);
        }

        private void Start(){
            _movementStateMachine.ChangeState(_movementStateMachine.IdlingState);
        }

        private void Update(){
            _movementStateMachine.HandleInput();
            _movementStateMachine.Update();
        }

        private void FixedUpdate(){
            _movementStateMachine.PhysicsUpdate();
        }
    }
}
