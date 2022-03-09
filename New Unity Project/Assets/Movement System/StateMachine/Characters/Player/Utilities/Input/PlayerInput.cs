using UnityEngine;

namespace Movement_System.StateMachine.Characters.Player.Utilities.Input {
    public class PlayerInput : MonoBehaviour {
        public PlayerInputActions               InputActions  { get; private set; }
        public PlayerInputActions.PlayerActions PlayerActions { get; private set; }

        private void Awake(){
            InputActions = new PlayerInputActions();

            PlayerActions = InputActions.Player;
        }

        private void OnEnable(){
            InputActions.Enable();
        }

        private void OnDisable(){
            InputActions.Disable();
        }
    }
}
